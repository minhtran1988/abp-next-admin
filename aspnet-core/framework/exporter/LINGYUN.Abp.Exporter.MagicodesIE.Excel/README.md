# LINGYUN.Abp.Exporter.MagicodesIE.Excel

[Magicodes.IE.Excel](https://github.com/dotnetcore/Magicodes.IE) implementation of data export interface

Import and export general library, support Dto import and export, template export, fancy export and dynamic export, support Excel, Csv, Word, Pdf and Html.

For details, please refer to [Magicodes.IE.Excel documentation](https://github.com/dotnetcore/Magicodes.IE/blob/master/docs/2.%E5%9F%BA%E7%A1%80%E6%95%99%E7%A8%8B%E4%B9%8B%E5%AF%BC%E5%87%BAExcel.md)

## Configuration usage


```csharp

    [DependsOn(
        typeof(AbpExporterMagicodesIEExcelModule)
        )]
    public class YouProjectModule : AbpModule
    {
        Configure<AbpExporterMagicodesIEExcelOptions>(options =>
        {
            // Configuration type data export
            options.MapExportSetting(typeof(DemoClass), config =>
            {
                //The header position starts from the second row
                config.HeaderRowIndex = 2;
                //Other configuration
            });
        });
    }

    public class DemoClass
    {
        public string Name { get; set; }
        public string Remarks { get; set; }

        public DemoClass()
        {
        }
    }
```

