# LINGYUN.Abp.Saas.HttpApi.Client

The SaaS HTTP API client module provides HTTP client proxy implementation for tenant and version management.

## Features

* HTTP client proxy
  * HTTP client implementation of ITenantAppService
  * HTTP client implementation of IEditionAppService

## Usage

1. Install the module

```csharp
[DependsOn(typeof(AbpSaasHttpApiClientModule))]
public class YourModule : AbpModule
{
}
```

2. Configure remote services

```json
{
  "RemoteServices": {
    "Default": {
      "BaseUrl": "http://localhost:44388/"
    }
  }
}
```

3. Inject and use the service

```csharp
public class YourService
{
    private readonly ITenantAppService _tenantAppService;
    private readonly IEditionAppService _editionAppService;

    public YourService(
        ITenantAppService tenantAppService,
        IEditionAppService editionAppService)
    {
        _tenantAppService = tenantAppService;
        _editionAppService = editionAppService;
    }
}
```

## More

[English](README.EN.md)
