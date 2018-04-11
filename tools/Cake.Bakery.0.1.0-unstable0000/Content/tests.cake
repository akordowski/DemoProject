/* ---------------------------------------------------------------------------------------------------- */
/* Task Definitions */

Task("TestNUnit")
    .WithCriteria(() => DirectoryExists(Build.Paths.Directories.PublishedNUnitTests))
    .Does(() => Require(new[] { NUnitTool, OpenCoverTool, ReportGeneratorTool }, () =>
    {
        if (Build.Parameters.IsRunningOnWindows)
        {
            CleanDirectory(Build.Paths.Directories.TestCoverage);

            OpenCover(tool =>
            {
                var files = GetFiles(Build.Paths.Directories.PublishedNUnitTests + "/**/*Tests.dll");

                tool.NUnit3(files, new NUnit3Settings
                {
                    NoResults = true
                });
            },
            Build.Paths.Files.TestCoverageOutput,
            new OpenCoverSettings
            {
                OldStyle = true,
                ReturnTargetCodeOffset = 0
            }
            // .WithFilter(ToolSettings.TestCoverageFilter)
            // .ExcludeByAttribute(ToolSettings.TestCoverageExcludeByAttribute)
            // .ExcludeByFile(ToolSettings.TestCoverageExcludeByFile)
            );
        }
    }));

Task("Test")
    .IsDependentOn("TestNUnit");