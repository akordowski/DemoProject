Task("CreateZipPackages")
    .Does(() =>
    {
        Information("Creating Zip Packages...");
        CleanDirectory(Build.Paths.Directories.PackagesZip);
    });