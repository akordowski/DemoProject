#load nuget:https://www.myget.org/F/arkord/api/v2?package=Cake.Bakery&prerelease

Build
    .SetParameters(
        "DemoProject",
        "akordowski",
        isPrerelease: true,

        shouldCreateCoverageReport: false,
        shouldPostToTwitter: true,
        shouldPublishToGitHub: true,
        shouldPublishToMyGet: true,
        shouldPublishToNuGet: true,
        shouldRunGitVersion: true,

        printEnvironmentInfo: false,
        printDirectoriesInfo: false,
        printFilesInfo: false,
        printToolSettingsInfo: false
        )
    .Run();