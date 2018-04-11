/* ---------------------------------------------------------------------------------------------------- */
/* Global Variables */

var Build = new Builder(Context, BuildSystem, target => RunTarget(target));
var publishingError = false;

/* ---------------------------------------------------------------------------------------------------- */
/* Global Methods */

public void CopyBuildOutput()
{
    Information("");
    Information("Copying build output...");

    foreach(var project in ParseSolution(Build.Solution.SolutionFilePath).GetProjects())
    {
        var parsedProject = ParseProject(project.Path, Build.Parameters.Configuration, "AnyCPU");
        var assemblyName = parsedProject.AssemblyName;
        var isLibrary = parsedProject.IsLibrary();
        var references = parsedProject.References;

        if (parsedProject.AssemblyName == null || parsedProject.OutputType == null || parsedProject.OutputPaths?.Length == 0)
        {
            Information("AssemblyName: {0}", parsedProject.AssemblyName);
            Information("OutputType:   {0}", parsedProject.OutputType);
            Information("OutputPaths:  {0}", parsedProject.OutputPaths);

            throw new Exception(string.Format("Unable to parse project file correctly: {0}", project.Path));
        }

        var isNUnitProject = false;

        foreach (var reference in references)
        {
            Verbose("Reference Include: {0}", reference.Include);

            var include = reference.Include.ToLower();

            if (include.Contains("nunit.framework"))
            {
                isNUnitProject = true;
                break;
            }
        }

        DirectoryPath directoryPath = null;

        if (isLibrary && isNUnitProject)
        {
            Information("Project has an output type of library and is a NUnit Test Project: {0}", assemblyName);
            directoryPath = Build.Paths.Directories.PublishedNUnitTests;
        }
        else
        {
            Information("Project has an output type of library: {0}", assemblyName);

            if (parsedProject.IsVS2017ProjectFormat)
            {
                foreach(var outputPath in parsedProject.OutputPaths)
                {
                }
                continue;
            }
            else
            {
                directoryPath = Build.Paths.Directories.PublishedLibraries;
            }
        }

        var files = GetFiles(parsedProject.OutputPaths[0].FullPath + "/**/*");
        var outputFolder = directoryPath.Combine(assemblyName);

        CleanDirectory(outputFolder);
        CopyFiles(files, outputFolder, true);
    }
}

public void Print(string name, List<string> values)
{
    int pos = 0;

    foreach (var value in values)
    {
        pos = Math.Max(pos, value.IndexOf(":"));
    }

    if (!String.IsNullOrWhiteSpace(name))
    {
        Context.Information("");
        Context.Information(name);
        Context.Information("----------------------------------------");
    }

    foreach (var value in values)
    {
        var str = "";

        if (!string.IsNullOrWhiteSpace(value))
        {
            var indexSeperator = value.IndexOf(":");
            var padCount = pos - indexSeperator;

            str = value.Insert(indexSeperator + 1, "".PadRight(padCount));
        }

        Context.Information(str);
    }
}

Action<string[], Action> Require = (value, action) =>
{
    var script = MakeAbsolute(File(string.Format("./{0}.cake", Guid.NewGuid())));

    try
    {
        System.IO.File.WriteAllLines(script.FullPath, value);
        var arguments = new Dictionary<string, string>();
        var cake = Build.Parameters.CakeConfiguration;

        if (cake.GetValue("NuGet_UseInProcessClient") != null)
        {
            arguments.Add("nuget_useinprocessclient", cake.GetValue("NuGet_UseInProcessClient"));
        }

        if (cake.GetValue("Settings_SkipVerification") != null)
        {
            arguments.Add("settings_skipverification", cake.GetValue("Settings_SkipVerification"));
        }

        CakeExecuteScript(script, new CakeSettings
        {
            Arguments = arguments
        });
    }
    finally
    {
        if (FileExists(script))
        {
            DeleteFile(script);
        }
    }

    action();
};

/* ---------------------------------------------------------------------------------------------------- */
/* Setup/Teardown */

Setup(context =>
{
    Information(Figlet(Build.Solution.Name));
    Information("Starting Setup...");

    Build.Version.CalculateVersion();

    Information("");
    Information("Building version {0} of {1} using version {2} of Cake",
        Build.Version.SemVersion,
        Build.Solution.Name,
        Build.Version.CakeVersion);
});

// Teardown(context =>
// {
//     Information("Starting Teardown...");
// });

/* ---------------------------------------------------------------------------------------------------- */
/* Task Definitions */

Task("ShowInfo")
    .Does(() =>
    {
        Print("Version", Build.Version.GetInfo());
        Print("Environment", Build.Environment.GetInfo());
        Print("Parameters", Build.Parameters.GetInfo());
        Print("Solution", Build.Solution.GetInfo());
        Print("Directories", Build.Paths.Directories.GetInfo());
        Print("Files", Build.Paths.Files.GetInfo());
    });

Task("Clean")
    .Does(() =>
    {
        Information("Cleaning...");
        CleanDirectory(Build.Paths.Directories.Artifacts);
    });

Task("Restore")
    .Does(() =>
    {
        Information("Restoring...");
        NuGetRestore(Build.Solution.SolutionFilePath);
    });

Task("Build")
    .Does(() =>
    {
        Information("Building...");

        if (Build.Parameters.IsRunningOnWindows)
        {
            var msBuildSettings = new MSBuildSettings()
                .SetVerbosity(Verbosity.Minimal)
                .SetConfiguration(Build.Parameters.Configuration)
                .WithTarget("Build")
                .WithProperty("TreatWarningsAsErrors", "true")
                ;

            MSBuild(Build.Solution.SolutionFilePath, msBuildSettings);
        }
        else
        {
            var xBuildSettings = new XBuildSettings()
                .SetVerbosity(Verbosity.Minimal)
                .SetConfiguration(Build.Parameters.Configuration)
                .WithTarget("Build")
                .WithProperty("TreatWarningsAsErrors", "true");

            XBuild(Build.Solution.SolutionFilePath, xBuildSettings);
        }

        CopyBuildOutput();
    });

/* ---------------------------------------------------------------------------------------------------- */
/* Execution */

Task("Default")
    .IsDependentOn("PrintAppVeyorEnvironmentVariables")
    .IsDependentOn("ShowInfo")
    .IsDependentOn("Clean")
    .IsDependentOn("Restore")
    .IsDependentOn("Build")
    .IsDependentOn("CreateNuGetPackages")
    .IsDependentOn("UploadAppVeyorArtifacts")

    // .IsDependentOn("PublishNuGetPackages")
    // .IsDependentOn("PublishMyGetPackages")
    // .IsDependentOn("PublishGitHubRelease")
    ;