# LINGYUN.Abp.WebhooksManagement.HttpApi

Webhook management HTTP API module provides a REST API interface for Webhook management.

[English](README.EN.md)

## Features

* Webhook subscribe to REST API
* Webhook Group REST API
* Webhook defines REST API
* Webhook log REST API
* Automatic API routing
* API permission control

## Module reference

```csharp
[DependsOn(typeof(AbpWebhooksManagementHttpApiModule))]
public class YouProjectModule : AbpModule
{
  // other
}
```

## API routing

* /api/webhooks-management/subscriptions - Webhook subscription management
* /api/webhooks-management/groups - Webhook group management
* /api/webhooks-management/definitions - Webhook definition management
* /api/webhooks-management/logs - Webhook log query

## Basic usage

1. Configure API routing
```csharp
public override void ConfigureServices(ServiceConfigurationContext context)
{
    Configure<AbpAspNetCoreMvcOptions>(options =>
    {
        options.ConventionalControllers.Create(
            typeof(AbpWebhooksManagementHttpApiModule).Assembly,
            opts =>
            {
                opts.RootPath = "webhooks-management";
            });
    });
}
```

2. API call example
```http
### Create Webhook subscription
POST /api/webhooks-management/subscriptions
{
    "webhookUri": "https://your-webhook-endpoint",
    "webhooks": ["YourWebhook"]
}

### Query Webhook subscription
GET /api/webhooks-management/subscriptions?maxResultCount=10&skipCount=0

### Query Webhook logs
GET /api/webhooks-management/logs?maxResultCount=10&skipCount=0
```
