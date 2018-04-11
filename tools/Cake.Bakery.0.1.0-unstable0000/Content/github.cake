/* ---------------------------------------------------------------------------------------------------- */
/* Task Definitions */

Task("PublishGitHubRelease")
    .WithCriteria(() => Build.Parameters.ShouldPublishToGitHub)
    .Does(() =>
    {
        if (Build.Parameters.CanPublishToGitHub)
        {
            var files = GetFiles(Build.Paths.Directories.Packages + "/**/*");

            if (files.Any())
            {
                var version = Build.Version.SemVersion;
                var assets = String.Join(",", files);

                new GitHub(Build)
                    .CreateReleaseNotes(null, null)
                    .Create(new GitReleaseManagerCreateSettings
                    {
                        Name = version,
                        InputFilePath = Build.Paths.Files.GitReleaseNotes,
                        Prerelease = true,
                        Assets = assets,
                        TargetCommitish = "master" // parameters.Repository.Branch
                    })
                    .Publish(version);
            }
            else
            {
                Warning("Unable to publish to GitHub, as necessary assets are not available");
            }
        }
        else
        {
            Warning("Unable to publish to GitHub, as necessary credentials are not available");
        }
    })
    .OnError(ex =>
    {
        Error(ex.Message);
        Information("{0} Task failed, but continuing with next Task...", "PublishGitHubRelease");
        publishingError = true;
    });

/* ---------------------------------------------------------------------------------------------------- */
/* Classes */

public class GitHub
{
    private readonly ICakeContext _context;
    private readonly GitHubCredentials _credentials;
    private readonly string _owner;
    private readonly string _repo;

    public GitHub(Builder builder)
    {
        _context = builder.Context;
        _credentials = builder.Credentials.GitHub;
        _owner = "";
        _repo = "";
    }

    public GitHub AddAssets(string tagName, string assets)
    {
        _context.GitReleaseManagerAddAssets(
            _credentials.Username,
            _credentials.Password,
            _owner,
            _repo,
            tagName,
            assets);

        return this;
    }

    public GitHub AddAssets(string tagName, string assets, GitReleaseManagerAddAssetsSettings settings)
    {
        _context.GitReleaseManagerAddAssets(
            _credentials.Username,
            _credentials.Password,
            _owner,
            _repo,
            tagName,
            assets,
            settings);

        return this;
    }

    public GitHub Close(string milestone)
    {
        _context.GitReleaseManagerClose(
            _credentials.Username,
            _credentials.Password,
            _owner,
            _repo,
            milestone);

        return this;
    }

    public GitHub Close(string milestone, GitReleaseManagerCloseMilestoneSettings settings)
    {
        _context.GitReleaseManagerClose(
            _credentials.Username,
            _credentials.Password,
            _owner,
            _repo,
            milestone,
            settings);

        return this;
    }

    public GitHub Create()
    {
        _context.GitReleaseManagerCreate(
            _credentials.Username,
            _credentials.Password,
            _owner,
            _repo);

        return this;
    }

    public GitHub Create(GitReleaseManagerCreateSettings settings)
    {
        _context.GitReleaseManagerCreate(
            _credentials.Username,
            _credentials.Password,
            _owner,
            _repo,
            settings);

        return this;
    }

    public GitHub CreateReleaseNotes(ReleaseNotes releaseNotes, FilePath fileOutputPath)
    {
        System.IO.File.WriteAllLines(fileOutputPath.FullPath, releaseNotes.Notes.ToArray());

        return this;
    }

    public GitHub Export(FilePath fileOutputPath)
    {
        _context.GitReleaseManagerExport(
            _credentials.Username,
            _credentials.Password,
            _owner,
            _repo,
            fileOutputPath);

        return this;
    }

    public GitHub Export(FilePath fileOutputPath, GitReleaseManagerExportSettings settings)
    {
        _context.GitReleaseManagerExport(
            _credentials.Username,
            _credentials.Password,
            _owner,
            _repo,
            fileOutputPath,
            settings);

        return this;
    }

    public GitHub Publish(string tagName)
    {
        _context.GitReleaseManagerPublish(
            _credentials.Username,
            _credentials.Password,
            _owner,
            _repo,
            tagName);

        return this;
    }

    public GitHub Publish(string tagName, GitReleaseManagerPublishSettings settings)
    {
        _context.GitReleaseManagerPublish(
            _credentials.Username,
            _credentials.Password,
            _owner,
            _repo,
            tagName,
            settings);

        return this;
    }
}