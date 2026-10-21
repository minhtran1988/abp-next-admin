# LINGYUN.Abp.Webhooks.ClientProxies

Webhook client proxy module provides proxy implementation of Webhook client.

[English](README.EN.md)

## Features

* Webhook client proxy
* HTTP client configuration
* Automatic retry mechanism
* Error handling

## Module reference

```csharp
[DependsOn(typeof(AbpWebhooksClientProxiesModule))]
public class YouProjectModule : AbpModule
{
  // other
}
```

## Configuration items

```json
{
  "Webhooks": {
    "ClientProxies": {
      "RetryCount": 3, //Number of retries
      "RetryInterval": "00:00:05", // Retry interval
      "HttpTimeout": "00:00:30" // HTTP request timeout
    }
  }
}
```

## Basic usage

1. Configure client proxy
```csharp
public override void ConfigureServices(ServiceConfigurationContext context)
{
    Configure<AbpWebhooksClientProxiesOptions>(options =>
    {
        options.RetryCount = 5;
        options.RetryInterval = TimeSpan.FromSeconds(10);
    });
}
```

2. Use client proxies
```csharp
public class YourService
{
    private readonly IWebhookClientProxy _webhookClientProxy;

    public YourService(IWebhookClientProxy webhookClientProxy)
    {
        _webhookClientProxy = webhookClientProxy;
    }

    public async Task SendWebhook()
    {
        await _webhookClientProxy.SendAsync(
            new WebhookSendArgs
            {
                WebhookUri = "https://your-webhook-endpoint",
                Data = new { /* webhook data */ }
            });
    }
}
```
