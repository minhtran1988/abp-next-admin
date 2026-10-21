# LINGYUN.Abp.Webhooks

Webhook basic module provides the basic definition and functions of Webhook.

[English](README.EN.md)

## Features

* Basic Webhook definition
* Webhook publish and subscribe
* Webhook event data processing
* Webhook configuration management

## Module reference

```csharp
[DependsOn(typeof(AbpWebhooksModule))]
public class YouProjectModule : AbpModule
{
  // other
}
```

## Basic usage

1. Define Webhook
```csharp
public class YourWebhookDefinitionProvider : WebhookDefinitionProvider
{
    public override void Define(IWebhookDefinitionContext context)
    {
        context.Add(
            new WebhookDefinition(
                name: "YourWebhook",
                displayName: L("DisplayName:YourWebhook"),
                description: L("Description:YourWebhook")
            )
        );
    }
}
```

2. Publish Webhook
```csharp
public class YourService
{
    private readonly IWebhookPublisher _webhookPublisher;

    public YourService(IWebhookPublisher webhookPublisher)
    {
        _webhookPublisher = webhookPublisher;
    }

    public async Task PublishWebhook()
    {
        await _webhookPublisher.PublishAsync(
            webhookName: "YourWebhook",
            data: new { /* webhook data */ }
        );
    }
}
```

3. Handle Webhook events
```csharp
public class YourWebhookHandler : IWebhookHandler, ITransientDependency
{
    public async Task HandleWebhookAsync(WebhookPayload webhook)
    {
        // Handle webhook events
    }
}
```
