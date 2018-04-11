public class Credentials
{
    public GitHubCredentials GitHub { get; }
    public MyGetCredentials MyGet { get; }
    public NuGetCredentials NuGet { get; }
    public TwitterCredentials Twitter { get; }

    private ICakeContext _context;
    private Environment _environment;

    public Credentials(Builder builder)
    {
        _context = builder.Context;
        _environment = builder.Environment;

        GitHub = GetGitHubCredentials();
        MyGet = GetMyGetCredentials();
        NuGet = GetNuGetCredentials();
        Twitter = GetTwitterCredentials();
    }

    private GitHubCredentials GetGitHubCredentials()
    {
        return new GitHubCredentials(
            _context.EnvironmentVariable(_environment.GitHubUsernameVariable),
            _context.EnvironmentVariable(_environment.GitHubPasswordVariable));
    }

    private MyGetCredentials GetMyGetCredentials()
    {
        return new MyGetCredentials(
            _context.EnvironmentVariable(_environment.MyGetApiKeyVariable),
            _context.EnvironmentVariable(_environment.MyGetSourceVariable));
    }

    private NuGetCredentials GetNuGetCredentials()
    {
        return new NuGetCredentials(
            _context.EnvironmentVariable(_environment.NuGetApiKeyVariable),
            _context.EnvironmentVariable(_environment.NuGetSourceVariable));
    }

    private TwitterCredentials GetTwitterCredentials()
    {
        return new TwitterCredentials(
            _context.EnvironmentVariable(_environment.TwitterConsumerKeyVariable),
            _context.EnvironmentVariable(_environment.TwitterConsumerSecretVariable),
            _context.EnvironmentVariable(_environment.TwitterAccessTokenVariable),
            _context.EnvironmentVariable(_environment.TwitterAccessTokenSecretVariable));
    }
}

/* ---------------------------------------------------------------------------------------------------- */
/* Classes */

public class GitHubCredentials
{
    public string Username { get; }
    public string Password { get; }

    public GitHubCredentials(string username, string password)
    {
        Username = username;
        Password = password;
    }
}

public class MyGetCredentials : NuGetCredentials
{
    public MyGetCredentials(string apiKey, string source)
        : base(apiKey, source)
    {
    }
}

public class NuGetCredentials
{
    public string ApiKey { get; }
    public string Source { get; }

    public NuGetCredentials(string apiKey, string source)
    {
        ApiKey = apiKey;
        Source = source;
    }
}

public class TwitterCredentials
{
    public string ConsumerKey { get; }
    public string ConsumerSecret { get; }
    public string AccessToken { get; }
    public string AccessTokenSecret { get; }

    public TwitterCredentials(string consumerKey, string consumerSecret, string accessToken, string accessTokenSecret)
    {
        ConsumerKey = consumerKey;
        ConsumerSecret = consumerSecret;
        AccessToken = accessToken;
        AccessTokenSecret = accessTokenSecret;
    }
}