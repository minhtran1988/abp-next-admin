# LINGYUN.Abp.Exporter.Core

Data export/import core layer

## Configuration usage


```csharp

    [DependsOn(
        typeof(AbpExporterCoreModule)
        )]
    public class YouProjectModule : AbpModule
    {

    }
```

> Export data
```csharp
    public class ExportDemoClass
    {
        private readonly IExporterProvider _exporterProvider;

        public ExportDemoClass(IExporterProvider exporterProvider)
        {
            _exporterProvider = exporterProvider;
        }

        public async virtual Task<IRemoteStreamContent> ExportAsync()
        {
            var dataItems = new object[]
            {
                new
                {
                    Name = "name1",
                    Remakrs = "remarks1"
                }
            };

            var stream = await _exporterProvider.ExportAsync(dataItems);

            return new RemoteStreamContent(stream, "demo.xlsx");
        }
    }
```

> Import data
```csharp
    public class ImportDemoClass
    {
        private readonly IImporterProvider _importerProvider;

        public ImportDemoClass(IImporterProvider importerProvider)
        {
            _importerProvider = importerProvider;
        }

        public async virtual Task ImportAsync(Stream stream)
        {
            var demos = await _importerProvider.ImportAsync<DemoClass>(stream);

            //Other operations
        }
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
