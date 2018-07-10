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


var RepositoryOwner = "akordowski";
var RepositoryName = "DemoProject";
var Repository = $"{repositoryOwner}/{repositoryName}";
var IsMainRepository = repo.Name.Equals(Repository, StringComparison.OrdinalIgnoreCase);

Information("RepositoryOwner: " + RepositoryOwner);
Information("RepositoryName: " + RepositoryName);
Information("Repository: " + Repository);
Information("IsMainRepository: " + IsMainRepository);