public class Solution
{
    public string Name { get; private set; }
    public FilePath SolutionFilePath { get; private set; }

    public Solution()
    {
        Config(null, null);
    }

    public void Config(
        string name,
        FilePath solutionFilePath
        )
    {
        Name = name;
        SolutionFilePath = solutionFilePath;
    }

    public List<string> GetInfo()
    {
        return new List<string>
        {
            "Name: " + Name,
            "SolutionFilePath: " + SolutionFilePath,
        };
    }
}