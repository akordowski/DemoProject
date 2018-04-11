public class BuildParameters
{
    public string Target { get; }
    public string Configuration { get; }
    public Cake.Core.Configuration.ICakeConfiguration CakeConfiguration { get; }

    public string PublishMessage => $"Version {_builder.Version.Version} of {_builder.Solution.Name} Addin has just been released, https://www.nuget.org/packages/{_builder.Solution.Name}.";

    public bool IsLocalBuild { get; }
    public bool IsRunningOnUnix { get; }
    public bool IsRunningOnWindows { get; }
    public bool IsRunningOnAppVeyor { get; }

    public bool IsMasterBranch { get; }
    public bool IsDevelopBranch { get; }
    public bool IsReleaseBranch { get; }
    public bool IsHotFixBranch { get; }
    public bool IsPublicRepository => true;

    public bool CanPostToTwitter =>
        !String.IsNullOrEmpty(_builder.Credentials.Twitter.ConsumerKey) &&
        !String.IsNullOrEmpty(_builder.Credentials.Twitter.ConsumerSecret) &&
        !String.IsNullOrEmpty(_builder.Credentials.Twitter.AccessToken) &&
        !String.IsNullOrEmpty(_builder.Credentials.Twitter.AccessTokenSecret);

    public bool CanPublishToGitHub =>
        !String.IsNullOrEmpty(_builder.Credentials.GitHub.Username) &&
        !String.IsNullOrEmpty(_builder.Credentials.GitHub.Password);

    public bool CanPublishToMyGet =>
        !String.IsNullOrEmpty(_builder.Credentials.MyGet.ApiKey) &&
        !String.IsNullOrEmpty(_builder.Credentials.MyGet.Source);

    public bool CanPublishToNuGet =>
        !String.IsNullOrEmpty(_builder.Credentials.NuGet.ApiKey) &&
        !String.IsNullOrEmpty(_builder.Credentials.NuGet.Source);

    public bool ShouldPostToTwitter => true;
    public bool ShouldPublishToGitHub => true;
    public bool ShouldPublishToMyGet => true;
    public bool ShouldPublishToNuGet => true;

    public bool ShouldRunGitVersion => true;

    private readonly Builder _builder;
    private readonly ICakeContext _context;
    private readonly BuildSystem _buildSystem;

    public BuildParameters(Builder builder)
    {
        _builder = builder;
        _context = builder.Context;
        _buildSystem = builder.BuildSystem;

        Target = _context.Argument("target", "Default");
        Configuration = _context.Argument("configuration", "Release");
        CakeConfiguration = _context.GetCakeConfiguration();

        IsLocalBuild = _buildSystem.IsLocalBuild;
        IsRunningOnUnix = _context.IsRunningOnUnix();
        IsRunningOnWindows = _context.IsRunningOnWindows();
        IsRunningOnAppVeyor = _buildSystem.AppVeyor.IsRunningOnAppVeyor;

        var branch = _buildSystem.AppVeyor.Environment.Repository.Branch;
        IsMasterBranch = branch.Equals("master", StringComparison.OrdinalIgnoreCase);
        IsDevelopBranch = branch.Equals("develop", StringComparison.OrdinalIgnoreCase);
        IsReleaseBranch = branch.StartsWith("release", StringComparison.OrdinalIgnoreCase);
        IsHotFixBranch = branch.StartsWith("hotfix", StringComparison.OrdinalIgnoreCase);
    }

    public List<string> GetInfo()
    {
        return new List<string>
        {
            "Target: " + Target,
            "Configuration: " + Configuration,
            "",
            "IsLocalBuild: " + CanPostToTwitter,
            "IsRunningOnUnix: " + IsRunningOnUnix,
            "IsRunningOnWindows: " + IsRunningOnWindows,
            "IsRunningOnAppVeyor: " + IsRunningOnAppVeyor,
            "",
            "IsMasterBranch: " + IsMasterBranch,
            "IsDevelopBranch: " + IsDevelopBranch,
            "IsReleaseBranch: " + IsReleaseBranch,
            "IsHotFixBranch: " + IsHotFixBranch,
            "IsPublicRepository: " + IsPublicRepository,
            "",
            "CanPostToTwitter: " + CanPostToTwitter,
            "CanPublishToMyGet: " + CanPublishToMyGet,
            "CanPublishToNuGet: " + CanPublishToNuGet,
            "ShouldPostToTwitter: " + ShouldPostToTwitter,
            "ShouldPublishToMyGet: " + ShouldPublishToMyGet,
            "ShouldPublishToNuGet: " + ShouldPublishToNuGet,
            "ShouldRunGitVersion: " + ShouldRunGitVersion,
        };
    }
}