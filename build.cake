#load nuget:https://www.myget.org/F/arkord/api/v2?package=Cake.Bakery&prerelease

Build.Solution.Config(
    "DemoProject",
    "./src/DemoProject.sln"
    );

Build.Run();