public class Environment
{
    public string AppVeyorApiTokenVariable { get; private set; }
    public string GitHubUsernameVariable { get; private set; }
    public string GitHubPasswordVariable { get; private set; }
    public string MyGetApiKeyVariable { get; private set; }
    public string MyGetSourceVariable { get; private set; }
    public string NuGetApiKeyVariable { get; private set; }
    public string NuGetSourceVariable { get; private set; }
    public string TwitterAccessTokenVariable { get; private set; }
    public string TwitterAccessTokenSecretVariable { get; private set; }
    public string TwitterConsumerKeyVariable { get; private set; }
    public string TwitterConsumerSecretVariable { get; private set; }

    public Environment()
    {
        SetVariableNames();
    }

    public void SetVariableNames(
        string appVeyorApiTokenVariable = null,
        string githubUsernameVariable = null,
        string githubPasswordVariable = null,
        string mygetApiKeyVariable = null,
        string mygetSourceVariable = null,
        string nugetApiKeyVariable = null,
        string nugetSourceVariable = null,
        string twitterConsumerKeyVariable = null,
        string twitterConsumerSecretVariable = null,
        string twitterAccessTokenVariable = null,
        string twitterAccessTokenSecretVariable = null
        )
    {
        AppVeyorApiTokenVariable = appVeyorApiTokenVariable ?? "APPVEYOR_API_TOKEN";
        GitHubUsernameVariable = githubUsernameVariable ?? "GITHUB_USERNAME";
        GitHubPasswordVariable = githubPasswordVariable ?? "GITHUB_PASSWORD";
        MyGetApiKeyVariable = mygetApiKeyVariable ?? "MYGET_API_KEY";
        MyGetSourceVariable = mygetSourceVariable ?? "MYGET_SOURCE";
        NuGetApiKeyVariable = nugetApiKeyVariable ?? "NUGET_API_KEY";
        NuGetSourceVariable = nugetSourceVariable ?? "NUGET_SOURCE";
        TwitterConsumerKeyVariable = twitterConsumerKeyVariable ?? "TWITTER_CONSUMER_KEY";
        TwitterConsumerSecretVariable = twitterConsumerSecretVariable ?? "TWITTER_CONSUMER_SECRET";
        TwitterAccessTokenVariable = twitterAccessTokenVariable ?? "TWITTER_ACCESS_TOKEN";
        TwitterAccessTokenSecretVariable = twitterAccessTokenSecretVariable ?? "TWITTER_ACCESS_TOKEN_SECRET";
    }

    public List<string> GetInfo()
    {
        return new List<string>
        {
            "AppVeyorApiTokenVariable: " + AppVeyorApiTokenVariable,
            "GitHubUsernameVariable: " + GitHubUsernameVariable,
            "GitHubPasswordVariable: " + GitHubPasswordVariable,
            "MyGetApiKeyVariable: " + MyGetApiKeyVariable,
            "MyGetSourceVariable: " + MyGetSourceVariable,
            "NuGetApiKeyVariable: " + NuGetApiKeyVariable,
            "NuGetSourceVariable: " + NuGetSourceVariable,
            "TwitterConsumerKeyVariable: " + TwitterConsumerKeyVariable,
            "TwitterConsumerSecretVariable: " + TwitterConsumerSecretVariable,
            "TwitterAccessTokenVariable: " + TwitterAccessTokenVariable,
            "TwitterAccessTokenSecretVariable: " + TwitterAccessTokenSecretVariable,
        };
    }
}