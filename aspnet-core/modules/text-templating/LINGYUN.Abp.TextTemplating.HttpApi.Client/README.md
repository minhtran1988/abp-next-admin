# LINGYUN.Abp.TextTemplating.HttpApi.Client

## Module description

The text template HTTP API client module provides an HTTP client proxy implementation for text template management.

### Basic module

* LINGYUN.Abp.TextTemplating.Application.Contracts
* Volo.Abp.Http.Client

### Function definition

* HTTP client proxy that provides text template management
  * TextTemplateDefinitionClientProxy - template definition management client proxy
  * TextTemplateContentClientProxy - Template content management client proxy
* Implement the following application service interfaces
  * ITextTemplateDefinitionAppService
  * ITextTemplateContentAppService

### Configuration items

* AbpTextTemplatingRemoteServiceConsts
  * RemoteServiceName - remote service name (default: "AbpTextTemplating")

### How to use

1. Add `AbpTextTemplatingHttpApiClientModule` dependency

```csharp
[DependsOn(typeof(AbpTextTemplatingHttpApiClientModule))]
public class YouProjectModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        //Configure remote service
        Configure<AbpRemoteServiceOptions>(options =>
        {
            options.RemoteServices.Default.BaseUrl = "http://localhost:44315/";
        });
    }
}
```

2. Inject and use client proxy

```csharp
public class YourService
{
    private readonly ITextTemplateDefinitionAppService _templateDefinitionAppService;

    public YourService(ITextTemplateDefinitionAppService templateDefinitionAppService)
    {
        _templateDefinitionAppService = templateDefinitionAppService;
    }

    public async Task ManageTemplateAsync()
    {
        // Get the template definition list
        var templates = await _templateDefinitionAppService.GetListAsync(
            new TextTemplateDefinitionGetListInput());
    }
}
```

[View in English](README.EN.md)
