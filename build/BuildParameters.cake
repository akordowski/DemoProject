#load "./BuildVersion.cake"

public class BuildParameters
{
    public ICakeContext Context { get; private set; }
    public string Target { get; private set; }
    public string Configuration { get; private set; }

    public BuildVersion Version { get; private set; }

    public static BuildParameters GetInstance(
        ICakeContext context
        )
    {
        return new BuildParameters
        {
            Context = context,
            Target = context.Argument("target", "Default"),
            Configuration = context.Argument("configuration", "Release"),

            Version = BuildVersion.GetInstance(context),
        };
    }

    public void ShowInfo()
    {
        var sb = new StringBuilder()
            .AppendLine("Version:")
            .AppendLine("  CakeVersion: " + Version.CakeVersion)
            .AppendLine("  Version:     " + Version.Version)
            .AppendLine("  SemVersion:  " + Version.SemVersion)
            .AppendLine("  Milestone:   " + Version.Milestone)
            ;

        Context.Information(sb.ToString());
    }
}