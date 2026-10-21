# LINGYUN.Abp.Webhooks.Identity

Webhook identity integration module provides integration support with the ABP identity system.

[English](README.EN.md)

## Features

* Integrated with ABP identity system
* Supports user and tenant level webhooks
* Identity-related Webhook events

## Module reference

```csharp
[DependsOn(typeof(AbpWebhooksIdentityModule))]
public class YouProjectModule : AbpModule
{
  // other
}
```

## Basic usage

1. Handle identity-related Webhooks
```csharp
public class YourIdentityWebhookHandler : IWebhookHandler, ITransientDependency
{
    public async Task HandleWebhookAsync(WebhookPayload webhook)
    {
        if (webhook.WebhookName == "User.Created")
        {
            // Handle user creation event
        }
    }
}
```

2. Publish identity-related Webhooks
```csharp
public class YourService
{
    private readonly IWebhookPublisher _webhookPublisher;

    public YourService(IWebhookPublisher webhookPublisher)
    {
        _webhookPublisher = webhookPublisher;
    }

    public async Task PublishIdentityWebhook()
    {
        await _webhookPublisher.PublishAsync(
            webhookName: "User.Created",
            data: new { /* user data */ }
        );
    }
}
```
