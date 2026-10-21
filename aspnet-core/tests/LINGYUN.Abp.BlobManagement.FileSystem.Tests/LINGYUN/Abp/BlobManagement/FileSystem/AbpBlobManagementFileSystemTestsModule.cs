#nullable enable
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Volo.Abp;
using Volo.Abp.Modularity;

namespace LINGYUN.Abp.BlobManagement.FileSystem;

[DependsOn(
    typeof(AbpBlobManagementFileSystemModule),
    typeof(AbpBlobManagementDomainTestsModule))]
public class AbpBlobManagementFileSystemTestsModule : AbpModule
{
    /// <summary>
    /// Test root directory (one for each test host)
    /// </summary>
    public static string RootPath { get; } = Path.Combine(
        Path.GetTempPath(),
        "LINGYUN.Abp.BlobManagement.FileSystem.Tests",
        Guid.NewGuid().ToString("N"));

    /// <summary>
    /// Storage root directory for testing (BlobManagement:FileSystem:BasePath)
    /// </summary>
    public static string BasePath { get; } = Path.Combine(RootPath, "blobs");

    /// <summary>
    /// Storage directories other than the root directory, used to verify traversal/unauthorized access will be denied
    /// </summary>
    public static string OutsidePath { get; } = Path.Combine(RootPath, "outside");

    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        // Let the production module enable FileSystem provider(BlobManagement:Provider) as configured
        var configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string?>
            {
                ["BlobManagement:Provider"] = "FileSystem",
                ["BlobManagement:FileSystem:BasePath"] = BasePath,
            })
            .Build();

        context.Services.ReplaceConfiguration(configuration);

        Directory.CreateDirectory(BasePath);
        Directory.CreateDirectory(OutsidePath);
    }

    public override void OnApplicationShutdown(ApplicationShutdownContext context)
    {
        // Clean up the test directory
        if (Directory.Exists(RootPath))
        {
            Directory.Delete(RootPath, true);
        }

        // The outer directory will be removed together when it is empty after all test hosts are finished.
        var parentPath = Path.GetDirectoryName(RootPath);
        if (parentPath != null &&
            Directory.Exists(parentPath) &&
            !Directory.EnumerateFileSystemEntries(parentPath).Any())
        {
            Directory.Delete(parentPath);
        }
    }
}
