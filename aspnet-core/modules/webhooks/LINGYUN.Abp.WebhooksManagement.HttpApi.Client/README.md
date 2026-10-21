# LINGYUN.Abp.WebhooksManagement.HttpApi.Client

Webhook management HTTP API client module provides a dynamic proxy client for Webhook management HTTP API.

[English](README.EN.md)

## Features

* Dynamic API client proxy
* Automatic HTTP client configuration
* Support remote service calling
* Integrate ABP dynamic C# API client

## Module reference

```csharp
[DependsOn(typeof(AbpWebhooksManagementHttpApiClientModule))]
public class YouProjectModule : AbpModule
{
  // other
}
```

## Configuration items

```json
{
  "RemoteServices": {
    "WebhooksManagement": {
      "BaseUrl": "http://localhost:44315/" //The base URL of the Webhook management service
    }
  }
}
```

## Basic usage

1. Configure remote services
```csharp
public override void ConfigureServices(ServiceConfigurationContext context)
{
    var configuration = context.Services.GetConfiguration();
    
    Configure<AbpRemoteServiceOptions>(options =>
    {
        options.RemoteServices.Default = new RemoteServiceConfiguration(
            configuration["RemoteServices:WebhooksManagement:BaseUrl"]);
    });
}
```

2. Use HTTP client
```csharp
public class YourService
{
    private readonly IWebhookSubscriptionAppService _webhookSubscriptionAppService;

    public YourService(IWebhookSubscriptionAppService webhookSubscriptionAppService)
    {
        _webhookSubscriptionAppService = webhookSubscriptionAppService;
    }

    public async Task CallRemoteApi()
    {
        //Create subscription
        await _webhookSubscriptionAppService.CreateAsync(new WebhookSubscriptionCreateDto
        {
            WebhookUri = "https://your-webhook-endpoint",
            Webhooks = new[] { "YourWebhook" }
        });

        // Query subscription
        var subscriptions = await _webhookSubscriptionAppService.GetListAsync(
            new WebhookSubscriptionGetListInput());
    }
}
```
