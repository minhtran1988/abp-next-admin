using LINGYUN.Abp.Tests;
using System;
using System.IO;
using System.Linq;
using Volo.Abp.Modularity;

namespace LINGYUN.Abp.IP2Region;

/// <summary>
/// Test module used to verify the "physical file first" loading logic
/// </summary>
/// <remarks>
/// After releasing the built-in offline libraries into physical files, point to these files through <see cref="AbpIP2RegionOptions"/>
/// </remarks>
[DependsOn(
    typeof(AbpIP2RegionModule),
    typeof(AbpTestsBaseModule))]
public class AbpIP2RegionPhysicalFileTestModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpIP2RegionOptions>(options =>
        {
            options.IPv4DatabaseFile = ExtractDatabaseFile("ip2region_v4.xdb");
            options.IPv6DatabaseFile = ExtractDatabaseFile("ip2region_v6.xdb");
        });
    }

    private static string ExtractDatabaseFile(string fileName)
    {
        var directory = Path.Combine(Path.GetTempPath(), "LINGYUN.Abp.IP2Region.Tests");
        Directory.CreateDirectory(directory);

        var filePath = Path.Combine(directory, fileName);

        var assembly = typeof(AbpIP2RegionModule).Assembly;
        var resourceName = assembly.GetManifestResourceNames()
            .FirstOrDefault(name => name.EndsWith(fileName, StringComparison.OrdinalIgnoreCase));
        if (resourceName == null)
        {
            throw new InvalidOperationException($"Embedded resource not found: {fileName}.");
        }

        using var resourceStream = assembly.GetManifestResourceStream(resourceName)!;
        if (File.Exists(filePath) && new FileInfo(filePath).Length == resourceStream.Length)
        {
            // Already released and of the same size, reuse directly
            return filePath;
        }

        using (var fileStream = File.Create(filePath))
        {
            resourceStream.CopyTo(fileStream);
        }

        return filePath;
    }
}
