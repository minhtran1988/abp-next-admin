# LINGYUN.Abp.Localization.Json

## Module description

Json local file system integration for localized components

This component is based on PhysicalFileProvider

### Basic module

### High-level modules

### Permission definition

### Function definition

### Configuration definition

### How to use


```csharp

    [DependsOn(
       typeof(AbpLocalizationJsonModule))]
    public class YouProjectModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpLocalizationOptions>(options =>
            {
                options.Resources
                    .Add<YouResource>("en")
                    // Generally configured in the host project, write the absolute path to the json file stored in the host project (limited by PhysicalFileProvider)
                    // The json file format is consistent with Abp's default json format.
                    // For details, see: https://docs.microsoft.com/en-us/dotnet/api/microsoft.extensions.fileproviders.physicalfileprovider?view=dotnet-plat-ext-5.0
                    .AddPhysicalJson(Path.Combine(Directory.GetCurrentDirectory(), "Resources"));
            });

            // Equivalent to the following method
            Configure<AbpVirtualFileSystemOptions>(options =>
            {
                // Map the local file system to a virtual file system
                options.FileSets.AddPhysical(Path.Combine(Directory.GetCurrentDirectory(), "Resources"));
            });

            Configure<AbpLocalizationOptions>(options =>
            {
                options.Resources
                    .Add<YouResource>("en")
                    .AddVirtualJson("/Resources"); // json localized virtual file path
            });
        }
    }

```

```json

{
  "culture": "en",
  "texts": {
    "Hello China": "Hello China!"
  }
}

```

### Update log
