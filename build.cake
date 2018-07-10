#load nuget:https://www.myget.org/F/arkord/api/v2?package=Cake.Baker

Build
    .SetParameters(
        "DemoProject",
        "akordowski",
        //printAllInfo: false,
        shouldPublish: false,
        shouldPost: false
        )
    .Run();

Information(BuildSystem.AppVeyor.Environment.Repository.Name);