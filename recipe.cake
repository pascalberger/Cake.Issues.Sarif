#load nuget:?package=Cake.Recipe&version=1.1.1

Environment.SetVariableNames();

BuildParameters.SetParameters(
    context: Context, 
    buildSystem: BuildSystem,
    sourceDirectoryPath: "./src",
    title: "Cake.Issues.Sarif",
    repositoryOwner: "cake-contrib",
    repositoryName: "Cake.Issues.Sarif",
    appVeyorAccountName: "cakecontrib",
    shouldGenerateDocumentation: false,
    shouldRunCodecov: true,
    shouldRunGitVersion: true);

BuildParameters.PackageSources.Add(
    new PackageSourceData(
        Context,
        "GPR",
        "https://nuget.pkg.github.com/cake-contrib/index.json",
        FeedType.NuGet,
        false));

BuildParameters.PrintParameters(Context);

ToolSettings.SetToolSettings(
    context: Context,
    dupFinderExcludePattern: new string[] { BuildParameters.RootDirectoryPath + "/src/Cake.Issues.Sarif.Tests/*.cs" },
    testCoverageFilter: "+[*]* -[xunit.*]* -[Cake.Core]* -[Cake.Testing]* -[*.Tests]* -[Cake.Issues]* -[Cake.Issues.Testing]* -[Shouldly]* -[Newtonsoft.Json]*",
    testCoverageExcludeByAttribute: "*.ExcludeFromCodeCoverage*",
    testCoverageExcludeByFile: "*/*Designer.cs;*/*.g.cs;*/*.g.i.cs");

BuildParameters.Tasks.PublishPreReleasePackagesTask.IsDependentOn(BuildParameters.Tasks.PublishGitHubReleaseTask);
BuildParameters.Tasks.PublishReleasePackagesTask.IsDependentOn(BuildParameters.Tasks.PublishGitHubReleaseTask);

Build.RunDotNetCore();
