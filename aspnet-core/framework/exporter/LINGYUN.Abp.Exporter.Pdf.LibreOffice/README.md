# LINGYUN.Abp.Exporter.Pdf.LibreOffice

> LibreOffice is Free and Open Source Software. Development is open to new talent and new ideas, and our software is tested and used daily by a large and devoted user community.  

This module uses the local `LibreOffice` command line to convert Excel to Pdf. Please ensure that `LibreOffice` is installed before using this module. If it is not installed in the default directory, please manually specify the installation directory before use.

## Configuration usage


```csharp

    [DependsOn(
        typeof(AbpExporterPdfLibreOfficeModule)
        )]
    public class YouProjectModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            // Manually specify the installation directory
            LibreOfficeCommands.WindowsCliDir = "path\\to\\libreoffice";
            LibreOfficeCommands.UnixCliDir = "path/to/libreoffice";
        }
    }
```


