#tool "nuget:?package=GitVersion.CommandLine&version=4.0.0-beta0012"

public class BuildVersion
{
    public string CakeVersion { get; private set; }
    public string Milestone { get; private set; }
    public string SemVersion { get; private set; }
    public string Version { get; private set; }

    public static BuildVersion GetInstance(
        ICakeContext context
        )
    {
        var buildSystem = context.BuildSystem();

        string cakeVersion = typeof(ICakeContext).Assembly.GetName().Version.ToString();
        string milestone = null;
        string semVersion = null;
        string version = null;

        if (context.IsRunningOnWindows()) // && !parameters.SkipGitVersion
        {
            context.Information("Calculating Semantic Version");

            if (!buildSystem.IsLocalBuild) // || parameters.IsPublishBuild || parameters.IsReleaseBuild
            {
                context.GitVersion(new GitVersionSettings
                {
                    //UpdateAssemblyInfoFilePath = "./src/SolutionInfo.cs",
                    UpdateAssemblyInfo = true,
                    OutputType = GitVersionOutput.BuildServer
                });
            }

            GitVersion gitVersion = context.GitVersion(new GitVersionSettings
            {
                OutputType = GitVersionOutput.Json
            });

            version = gitVersion.MajorMinorPatch;
            semVersion = gitVersion.LegacySemVerPadded;
            milestone = string.Concat("v", version);

            context.Information($"Calculated Semantic Version: {semVersion}");

            if (string.IsNullOrEmpty(version) || string.IsNullOrEmpty(semVersion))
            {
                context.Information("Fetching version from first SolutionInfo");

                version = ReadSolutionInfoVersion(context);
                semVersion = version;
                milestone = string.Concat("v", version);
            }
        }

        return new BuildVersion
        {
            CakeVersion = cakeVersion,
            Milestone = milestone,
            SemVersion = semVersion,
            Version = version,
        };
    }

    public static string ReadSolutionInfoVersion(ICakeContext context)
    {
        // var solutionInfo = context.ParseAssemblyInfo("./src/SolutionInfo.cs");

        // if (!string.IsNullOrEmpty(solutionInfo.AssemblyVersion))
        // {
        //     return solutionInfo.AssemblyVersion;
        // }

        // throw new CakeException("Could not parse version.");

        return "";
    }
}