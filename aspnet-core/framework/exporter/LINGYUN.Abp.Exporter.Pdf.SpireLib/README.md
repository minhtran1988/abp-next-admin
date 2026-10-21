# LINGYUN.Abp.Exporter.Pdf.SpireLib

> Spire.XLS for .NET is a professional .NET Excel component that can be used in various .NET frameworks, including .NET Core, .NET 5.0, .NET 6.0, .NET 7.0, .NET Standard, Xamarin, Mono Android, ASP.NET and Windows Forms and other related .NET applications. Spire.XLS for .NET provides an object model Excel API that allows developers to quickly complete various programming operations on Excel on the .NET platform, such as creating new Excel documents based on templates, editing existing Excel documents, and converting Excel documents.  

This module uses [Spire.XLS](https://www.e-iceblue.cn/spirexls/spire-xls-for-net-program-guide-content.html) to convert Excel to Pdf. Please configure the permission before use.

## Configuration usage


```csharp

    [DependsOn(
        typeof(AbpExporterPdfModule)
        )]
    public class YouProjectModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            // Configure permissions
            Spire.Xls.License.LicenseProvider.SetLicense("xxx");
        }
    }
```


