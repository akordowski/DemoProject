#load nuget:https://www.myget.org/F/arkord/api/v2?package=Cake.Bakery&prerelease

Build
    .SetParameters(
        "DemoProject",
        "akordowski",
        "DemoProject",
        "master",
        shouldCreateCoverageReport: false,
        shouldPublishToMyGet: null
        )
    .Run();