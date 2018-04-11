public class BuildPaths
{
    public BuildDirectories Directories { get; }
    public BuildFiles Files { get; }

    public BuildPaths(ICakeContext context)
    {
        // Directories
        var root = context.MakeAbsolute(context.Directory("./"));
        var nuspec = root.Combine("nuspec");
        var source = root.Combine("src");
        var artifacts = root.Combine("artifacts");
        var docs = artifacts.Combine("docs");
        var log = artifacts.Combine("log");

        var packages = artifacts.Combine("packages");
        var packagesNuGet = packages.Combine("nuget");
        var packagesZip = packages.Combine("zip");

        var tmp = artifacts.Combine("tmp");
        var publishedApplications = tmp.Combine("PublishedApplications");
        var publishedLibraries = tmp.Combine("PublishedLibraries");
        var publishedWebsites = tmp.Combine("PublishedWebsites");
        var publishedNUnitTests = tmp.Combine("PublishedNUnitTests");

        var tests = artifacts.Combine("tests");
        var testCoverage = tests.Combine("coverage");
        var testResults = tests.Combine("results");
        var testResultsNUnit = testResults.Combine("NUnit");

        Directories = new BuildDirectories(
            root,
            nuspec,
            source,
            artifacts,
            docs,
            log,
            packages,
            packagesNuGet,
            packagesZip,
            publishedApplications,
            publishedLibraries,
            publishedWebsites,
            publishedNUnitTests,
            testCoverage,
            testResultsNUnit
            );

        var license = root.CombineWithFilePath("LICENSE.txt");
        var releaseNotes = root.CombineWithFilePath("RELEASENOTES.md");
        var gitReleaseNotes = artifacts.CombineWithFilePath("GITRELEASENOTES.md");
        var buildLog = log.CombineWithFilePath("MSBuild.log");
        var testCoverageOutput = testCoverage.CombineWithFilePath("OpenCover.xml");

        Files = new BuildFiles(
            license,
            releaseNotes,
            gitReleaseNotes,
            buildLog,
            testCoverageOutput
            );
    }
}

public class BuildDirectories
{
    public DirectoryPath Root { get; }
    public DirectoryPath Nuspec { get; }
    public DirectoryPath Source { get; }
    public DirectoryPath Artifacts { get; }
    public DirectoryPath Docs { get; }
    public DirectoryPath Log { get; }
    public DirectoryPath Packages { get; }
    public DirectoryPath PackagesNuGet { get; }
    public DirectoryPath PackagesZip { get; }
    public DirectoryPath PublishedApplications { get; }
    public DirectoryPath PublishedLibraries { get; }
    public DirectoryPath PublishedWebsites { get; }
    public DirectoryPath PublishedNUnitTests { get; }
    public DirectoryPath TestCoverage { get; }
    public DirectoryPath TestResultsNUnit { get; }

    public BuildDirectories(
        DirectoryPath root,
        DirectoryPath nuspec,
        DirectoryPath source,
        DirectoryPath artifacts,
        DirectoryPath docs,
        DirectoryPath log,
        DirectoryPath packages,
        DirectoryPath packagesNuGet,
        DirectoryPath packagesZip,
        DirectoryPath publishedApplications,
        DirectoryPath publishedLibraries,
        DirectoryPath publishedWebsites,
        DirectoryPath publishedNUnitTests,
        DirectoryPath testCoverage,
        DirectoryPath testResultsNUnit
        )
    {
        Root = root;
        Nuspec = nuspec;
        Source = source;

        Artifacts = artifacts;
        Docs = docs;
        Log = log;
        Packages = packages;
        PackagesNuGet = packagesNuGet;
        PackagesZip = packagesZip;

        PublishedApplications = publishedApplications;
        PublishedLibraries = publishedLibraries;
        PublishedWebsites = publishedWebsites;
        PublishedNUnitTests = publishedNUnitTests;

        TestCoverage = testCoverage;
        TestResultsNUnit = testResultsNUnit;
    }

    public List<string> GetInfo()
    {
        return new List<string>
        {
            "Root: " + Root,
            "Nuspec: " + Nuspec,
            "Source: " + Source,
            "",
            "Artifacts: " + Artifacts,
            "Docs: " + Docs,
            "Log: " + Log,
            "Packages: " + Packages,
            "PackagesNuGet: " + PackagesNuGet,
            "PackagesZip: " + PackagesZip,
            "PublishedApplications: " + PublishedApplications,
            "PublishedLibraries: " + PublishedLibraries,
            "PublishedWebsites: " + PublishedWebsites,
            "PublishedNUnitTests: " + PublishedNUnitTests,
            "TestCoverage: " + TestCoverage,
            "TestResultsNUnit: " + TestResultsNUnit,
        };
    }
}

public class BuildFiles
{
    public FilePath License { get; }
    public FilePath ReleaseNotes { get; }
    public FilePath GitReleaseNotes { get; }
    public FilePath BuildLog { get; }
    public FilePath TestCoverageOutput { get; }

    public BuildFiles(
        FilePath license,
        FilePath releaseNotes,
        FilePath gitReleaseNotes,
        FilePath buildLog,
        FilePath testCoverageOutput
        )
    {
        License = license;
        ReleaseNotes = releaseNotes;
        GitReleaseNotes = gitReleaseNotes;
        BuildLog = buildLog;
        TestCoverageOutput = testCoverageOutput;
    }

    public List<string> GetInfo()
    {
        return new List<string>
        {
            "License: " + License,
            "ReleaseNotes: " + ReleaseNotes,
            "GitReleaseNotes: " + GitReleaseNotes,
            "BuildLog: " + BuildLog,
            "TestCoverageOutput: " + TestCoverageOutput,
        };
    }
}