#load nuget:https://www.myget.org/F/arkord/api/v2?package=Cake.Bakery&prerelease

Build.Parameters.SolutionFile = "./src/DemoProject.sln";

Build.Parameters.SetParameters(
    "DemoProject",
    "akordowski",
    "DemoProject",
    "master"
    );

Build.Run();