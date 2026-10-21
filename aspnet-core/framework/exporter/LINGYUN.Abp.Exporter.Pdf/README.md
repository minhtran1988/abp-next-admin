# LINGYUN.Abp.Exporter.Pdf

PDF export module

## Configuration usage


```csharp

    [DependsOn(
        typeof(AbpExporterPdfModule)
        )]
    public class YouProjectModule : AbpModule
    {

    }
```

> Export data
```csharp
    public class ExportDemoClass
    {
        private readonly IExcelToPdfProvider _exporterProvider;

        public ExportDemoClass(IExcelToPdfProvider exporterProvider)
        {
            _exporterProvider = exporterProvider;
        }

        public async virtual Task<IRemoteStreamContent> ExportAsync(Stream excelStream)
        {
            var stream = await _exporterProvider.ParseAsync(excelStream);

            return new RemoteStreamContent(stream, "demo.pdf");
        }
    }
```

