# LINGYUN.Abp.WebhooksManagement.Dapr.Client

Webhook manages the Dapr client integration module and provides integration support with the Dapr service call building block.

[English](README.EN.md)

## Features

* Support access to Webhook management services through Dapr service calls
* Seamless integration with Dapr service call building blocks
* Support distributed service calls

## Module reference

```csharp
[DependsOn(typeof(AbpWebhooksManagementDaprClientModule))]
public class YouProjectModule : AbpModule
{
  // other
}
```

## Configuration items

```json
{
  "WebhooksManagement": {
    "Dapr": {
      "AppId": "webhooks-management", // Dapr application ID of Webhook management service
      "HttpEndpoint": "http://localhost:3500" // Dapr sidecar HTTP endpoint
    }
  }
}
```

## Basic usage

1. Configure Dapr client
```csharp
Configure<AbpWebhooksManagementDaprClientOptions>(options =>
{
    options.AppId = "webhooks-management";
    options.HttpEndpoint = "http://localhost:3500";
});
```

2. Use Webhook to manage clients
```csharp
public class YourService
{
    private readonly IWebhookSubscriptionAppService _webhookSubscriptionAppService;

    public YourService(IWebhookSubscriptionAppService webhookSubscriptionAppService)
    {
        _webhookSubscriptionAppService = webhookSubscriptionAppService;
    }

    public async Task SubscribeWebhook()
    {
        await _webhookSubscriptionAppService.SubscribeAsync(new WebhookSubscriptionCreateDto
        {
            WebhookUri = "https://your-webhook-endpoint",
            Webhooks = new[] { "YourWebhook" }
        });
    }
}
```
