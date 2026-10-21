# LINGYUN.Abp.Exporter.Application.Contracts

Data export/import interface contract layer defines a common data import/import service interface

## Configuration usage


> Configure module dependencies
```csharp

[DependsOn(
    typeof(AbpExporterApplicationContractsModule)
    )]
public class YouProjectModule : AbpModule
{

}
```

> Define export type
```csharp
public class DemoClassDto
{
    public string Name { get; set; }
    public string Remarks { get; set; }

    public DemoClassDto()
    {
    }
}
```

> Define import type
```csharp
public class DemoClassImportInput
{
    [Required]
    public IRemoteStreamContent Content { get; set; }   

    public DemoClassImportInput()
    {
    }
}
```

> Define data filtering type
```csharp
public class DemoClassExportListInput
{
    public string Filter { get; set; }

    [Required]
    public string FileName { get; set; }

    public DemoClassExportListInput()
    {
    }
}
```

> Export data
```csharp
    public class ExportDemoClassExportAppService : IExporterAppService<DemoClassDto, DemoClassExportListInput>
    {
        private readonly IExporterProvider _exporterProvider;

        public ExportDemoClassExportAppService(IExporterProvider exporterProvider)
        {
            _exporterProvider = exporterProvider;
        }

        public async virtual Task<IRemoteStreamContent> ExportAsync(DemoClassExportListInput input)
        {
            var dtos = Query the data list through the warehousing interface;

            var stream = await _exporterProvider.ExportAsync(dtos);

            return new RemoteStreamContent(stream, input.FileName);
        }
    }
```

> Import data
```csharp
    public class ExportDemoClassImportAppService : IImporterAppService<DemoClassImportInput>
    {
        private readonly IImporterProvider _importerProvider;

        public ExportDemoClassImportAppService(IImporterProvider importerProvider)
        {
            _importerProvider = importerProvider;
        }

        public async virtual Task ImportAsync(DemoClassImportInput input)
        {
            var stream = input.Content.GetStream();

            var demos = await _importerProvider.ImportAsync<DemoClass>(stream);

            //Other operations
        }
    }
```
