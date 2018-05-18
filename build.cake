#load nuget:https://www.myget.org/F/arkord/api/v2?package=Cake.Baker.0.1.0

Build
    .SetParameters(
        "DemoProject",
        "akordowski",

        shouldCreateCoverageReport: false,
        shouldPostToTwitter: true,
        shouldPublishToGitHub: true,
        shouldPublishToMyGet: true,
        shouldPublishToNuGet: true
        )
    .Run();