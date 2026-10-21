# LINGYUN.Abp.WebhooksManagement.Domain

The Webhook management domain module provides Webhook storage and management functions.

[English](README.EN.md)

## Features

* Support static Webhook storage to database
* Support dynamic Webhook storage
* Webhook cache management
* Timestamp expiration mechanism

## Module reference

```csharp
[DependsOn(typeof(AbpWebhooksManagementDomainModule))]
public class YouProjectModule : AbpModule
{
  // other
}
```

## Configuration items

```json
{
  "WebhooksManagement": {
    "SaveStaticWebhooksToDatabase": true, // Whether to save static Webhooks to the database, default true
    "IsDynamicWebhookStoreEnabled": false, // Whether to enable dynamic Webhook storage, default false
    "WebhooksCacheRefreshInterval": "00:00:30", // Cache refresh time, default 30 seconds
    "WebhooksCacheStampTimeOut": "00:02:00", // Application timestamp timeout, default is 2 minutes
    "WebhooksCacheStampExpiration": "00:30:00" // timestamp expiration time, default 30 minutes
  }
}
```

## Basic usage

1. Configure Webhook management options
```csharp
Configure<WebhooksManagementOptions>(options =>
{
    options.SaveStaticWebhooksToDatabase = true;
    options.IsDynamicWebhookStoreEnabled = true;
    options.WebhooksCacheRefreshInterval = TimeSpan.FromMinutes(1);
});
```

2. Use Webhook storage
```csharp
public class YourService
{
    private readonly IWebhookDefinitionManager _webhookDefinitionManager;

    public YourService(IWebhookDefinitionManager webhookDefinitionManager)
    {
        _webhookDefinitionManager = webhookDefinitionManager;
    }

    public async Task DoSomething()
    {
        var webhooks = await _webhookDefinitionManager.GetAllAsync();
        // handle webhook
    }
}
```
