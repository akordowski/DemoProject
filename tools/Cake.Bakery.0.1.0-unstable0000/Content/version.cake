public class BuildVersion
{
    public string CakeVersion { get; }
    public string FullSemVersion { get; private set; }
    public string InformationalVersion { get; private set; }
    public string Milestone { get; private set; }
    public string SemVersion { get; private set; }
    public string Version { get; private set; }

    private readonly Builder _builder;
    private readonly ICakeContext _context;

    public BuildVersion(Builder builder)
    {
        _builder = builder;
        _context = builder.Context;

        CakeVersion = typeof(ICakeContext).Assembly.GetName().Version.ToString();
    }

    public void CalculateVersion()
    {
        if (_builder.Parameters.ShouldRunGitVersion)
        {
            _context.Information("");
            _context.Information("Calculating Semantic Version...");

            var noFetch = !_builder.Parameters.IsPublicRepository && _builder.Parameters.IsRunningOnAppVeyor;

            if (!_builder.BuildSystem.IsLocalBuild)
            {
                _context.GitVersion(new GitVersionSettings
                {
                    // UpdateAssemblyInfoFilePath = BuildParameters.Paths.Files.SolutionInfoFilePath,
                    UpdateAssemblyInfo = true,
                    OutputType = GitVersionOutput.BuildServer,
                    NoFetch = noFetch
                });
            }

            GitVersion gitVersion = _context.GitVersion(new GitVersionSettings
            {
                OutputType = GitVersionOutput.Json,
                NoFetch = noFetch
            });

            Version = gitVersion.MajorMinorPatch;
            SemVersion = gitVersion.LegacySemVerPadded;
            FullSemVersion = gitVersion.FullSemVer;
            Milestone = Version;
            InformationalVersion = gitVersion.InformationalVersion;

            _context.Information("Calculated Semantic Version: {0}", SemVersion);

            // _context.Information("");
            // _context.Information(gitVersion.Dump());
        }

        if (string.IsNullOrEmpty(Version) || string.IsNullOrEmpty(SemVersion))
        {
            _context.Information("Fetching version from SolutionInfo...");
        }
    }

    public List<string> GetInfo()
    {
        return new List<string>
        {
            "CakeVersion: " + CakeVersion,
            "Version: " + Version,
            "SemVersion: " + SemVersion,
            "FullSemVersion: " + FullSemVersion,
            "Milestone: " + Milestone,
            "InformationalVersion: " + InformationalVersion
        };
    }
}