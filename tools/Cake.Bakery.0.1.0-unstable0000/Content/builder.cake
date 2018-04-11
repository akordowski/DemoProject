public class Builder
{
    public ICakeContext Context { get; }
    public BuildSystem BuildSystem { get; }

    public BuildParameters Parameters { get; }
    public BuildPaths Paths { get; }
    public BuildVersion Version { get; }
    public Credentials Credentials { get; }
    public Environment Environment { get; }
    public Solution Solution { get; }

    private readonly Action<string> _runTarget;

    public Builder(ICakeContext context, BuildSystem buildSystem, Action<string> runTarget)
    {
        Context = context;
        BuildSystem = buildSystem;
        _runTarget = runTarget;

        Environment = new Environment();
        Credentials = new Credentials(this);
        Paths = new BuildPaths(context);
        Solution = new Solution();
        Version = new BuildVersion(this);
        Parameters = new BuildParameters(this);
    }

    public void Run()
    {
        _runTarget(Parameters.Target);
    }
}