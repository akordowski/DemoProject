#tool "nuget:?package=GitVersion.CommandLine&version=4.0.0-beta0012"

/* ---------------------------------------------------------------------------------------------------- */
/* Tools */

private const string GitReleaseManagerTool = "#tool nuget:?package=gitreleasemanager&version=0.7.0";
private const string MSBuildExtensionPackTool = "#tool nuget:?package=MSBuild.Extension.Pack&version=1.9.1";
private const string NUnitTool = "#tool nuget:?package=NUnit.ConsoleRunner&version=3.8.0";
private const string OpenCoverTool = "#tool nuget:?package=OpenCover&version=4.6.519";
private const string ReportGeneratorTool = "#tool nuget:?package=ReportGenerator&version=3.1.2";