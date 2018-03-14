#tool "nuget:?package=GitVersion.CommandLine&version=4.0.0-beta0012"

public class BuildVersion
{
    public string CakeVersion { get; private set; }
    public string Milestone { get; private set; }
    public string SemVersion { get; private set; }
    public string Version { get; private set; }

    public static BuildVersion GetInstance(
        ICakeContext context
        )
    {
        var buildSystem = context.BuildSystem();

        string cakeVersion = typeof(ICakeContext).Assembly.GetName().Version.ToString();
        string milestone = null;
        string semVersion = null;
        string version = null;

        if (context.IsRunningOnWindows())
        {
            if (!buildSystem.IsLocalBuild)
            {
                context.GitVersion(new GitVersionSettings
                {
                    UpdateAssemblyInfo = true,
                    OutputType = GitVersionOutput.BuildServer
                });

                version = context.EnvironmentVariable("GitVersion_MajorMinorPatch");
                semVersion = context.EnvironmentVariable("GitVersion_LegacySemVerPadded");
            }
            else
            {
                GitVersion gitVersion = context.GitVersion(new GitVersionSettings
                {
                    OutputType = GitVersionOutput.Json
                });

                version = gitVersion.MajorMinorPatch;
                semVersion = gitVersion.LegacySemVerPadded;
            }

            milestone = string.Concat("v", version);
        }

        return new BuildVersion
        {
            CakeVersion = cakeVersion,
            Milestone = milestone,
            SemVersion = semVersion,
            Version = version,
        };
    }
}