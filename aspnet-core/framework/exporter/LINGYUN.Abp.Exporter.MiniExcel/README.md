# LINGYUN.Abp.Exporter.MiniExcel

[MiniExcel](https://github.com/mini-software/MiniExcel) implementation of data export interface

MiniExcel is a simple and efficient .NET tool for Excel query, writing and filling data that avoids OOM.

Most of the current mainstream frameworks need to load all the data into memory for easy operation, but this will lead to memory consumption problems. MiniExcel tries to write the underlying algorithm logic from a Stream perspective, which can reduce the original 1,000 MB usage to a few MB and avoid insufficient memory.

For details, please refer to [MiniExcel documentation](https://github.com/mini-software/MiniExcel/blob/master/README.md)

## Configuration usage


```csharp

    [DependsOn(
        typeof(AbpExporterMiniExcelModule)
        )]
    public class YouProjectModule : AbpModule
    {
        Configure<AbpExporterMiniExcelOptions>(options =>
        {
            // Configuration type data export
            options.MapExportSetting(typeof(DemoClass), config =>
            {
                config.DynamicColumns = new[]
                {
                    // Ignore the Name field
                    new DynamicExcelColumn(nameof(DemoClass.Name)){ Ignore = true },
                    //Other configuration
                };
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

