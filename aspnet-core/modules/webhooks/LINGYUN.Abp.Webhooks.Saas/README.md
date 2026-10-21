# LINGYUN.Abp.Webhooks.Saas

Webhook SaaS integration module provides integration support with the ABP SaaS system.

[English](README.EN.md)

## Features

* Integrate with ABP SaaS system
* Support multi-tenant Webhook
* SaaS related Webhook events

## Module reference

```csharp
[DependsOn(typeof(AbpWebhooksSaasModule))]
public class YouProjectModule : AbpModule
{
  // other
}
```

## Basic usage

1. Handle SaaS-related Webhooks
```csharp
public class YourSaasWebhookHandler : IWebhookHandler, ITransientDependency
{
    public async Task HandleWebhookAsync(WebhookPayload webhook)
    {
        if (webhook.WebhookName == "Tenant.Created")
        {
            // Handle tenant creation event
        }
    }
}
```

2. Publish SaaS-related Webhooks
```csharp
public class YourService
{
    private readonly IWebhookPublisher _webhookPublisher;

    public YourService(IWebhookPublisher webhookPublisher)
    {
        _webhookPublisher = webhookPublisher;
    }

    public async Task PublishSaasWebhook()
    {
        await _webhookPublisher.PublishAsync(
            webhookName: "Tenant.Created",
            data: new { /* tenant data */ }
        );
    }
}
```
