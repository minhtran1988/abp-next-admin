# LINGYUN.Abp.Localization.Xml

## Module description

The XML document integration module of the localization component provides localization resource support based on XML files. Built-in implementation of the physical file provider (PhysicalFileProvider) and virtual file provider (VirtualFileProvider).

## Features

* Support reading localized resources from XML files
* Support XML files in virtual file system
* Support XML files in physical file system
* Supports serialization and deserialization of XML files
* Support UTF-8 encoded XML files

## Installation

```bash
dotnet add package LINGYUN.Abp.Localization.Xml
```

## Basic module

* Volo.Abp.Localization

## How to use

1. Add module dependencies:

```csharp
[DependsOn(
    typeof(AbpLocalizationXmlModule))]
public class YouProjectModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<YouProjectModule>();
        });

        Configure<AbpLocalizationOptions>(options =>
        {
            options.Resources
                .Add<YouResource>("en")
                //The virtual file system directory in the current project
                // For details, see: https://docs.abp.io/zh-Hans/abp/latest/Virtual-File-System
                .AddVirtualXml("/LINGYUN/Abp/Localization/Xml/Resources")
                // Generally configured in the host project, write the absolute path to the xml file stored in the host project
                // For details, see: https://docs.microsoft.com/zh-cn/dotnet/api/microsoft.extensions.fileproviders.physicalfileprovider
                .AddPhysicalXml(Path.Combine(Directory.GetCurrentDirectory(), "Resources"));
        });
    }
}
```

## XML file format

This module uses the [XmlLocalizationFile](./LINGYUN/Abp/Localization/Xml/XmlLocalizationFile.cs) type to serialize and deserialize XML files.

Example XML file format:

```xml
<?xml version="1.0" encoding="utf-8"?>
<localization>
    <culture name="zh-Hans"/>
    <texts>
        <text name="Welcome">Welcome</text>
        <text name="HelloWorld">Hello, world! </text>
        <text name="ThisFieldIsRequired">This is a required field</text>
    </texts>
</localization>
```

##Extension methods

The module provides two extension methods to add XML localization resources:

1. AddVirtualXml: Add XML files in the virtual file system
```csharp
localizationResource.AddVirtualXml("/YourVirtualPath/Localization");
```

2. AddPhysicalXml: Add XML files in the physical file system
```csharp
localizationResource.AddPhysicalXml("C:/YourPath/Localization");
```

## Best Practices

1. Recommended usage of virtual files:
   * Embed XML file into assembly
   * Applicable to default localized resources that do not require dynamic modification

2. Recommended usage of physical files:
   * Stored in a specific directory of the host project
   * Suitable for localized resources that need to be dynamically modified or managed by external systems

## More information

* [English Documentation](./README.EN.md)
* [ABP Localization Document](https://docs.abp.io/zh-Hans/abp/latest/Localization)
* [ABP Virtual File System](https://docs.abp.io/zh-Hans/abp/latest/Virtual-File-System)
