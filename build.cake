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

var repo = "akordowski/DemoProject";
var repoName = BuildSystem.AppVeyor.Environment.Repository.Name;
var isEqual = repoName.Equals(repo, StringComparison.OrdinalIgnoreCase);

Information("repo: " + repo);
Information("repoName: " + repoName);
Information("isEqual: " + isEqual);