# LINGYUN.Abp.Webhooks.EventBus

Webhook event bus integration module provides integration support with the ABP event bus.

[English](README.EN.md)

## Features

* Support publishing Webhook events to the event bus
* Seamless integration with ABP event bus
* Support distributed event bus

## Module reference

```csharp
[DependsOn(typeof(AbpWebhooksEventBusModule))]
public class YouProjectModule : AbpModule
{
  // other
}
```

## Basic usage

1. Define Webhook event handler
```csharp
public class YourWebhookEventHandler : 
    IDistributedEventHandler<WebhookEventData>,
    ITransientDependency
{
    public async Task HandleEventAsync(WebhookEventData eventData)
    {
        // Handle webhook events
    }
}
```

2. Publish Webhook events
```csharp
public class YourService
{
    private readonly IDistributedEventBus _eventBus;

    public YourService(IDistributedEventBus eventBus)
    {
        _eventBus = eventBus;
    }

    public async Task PublishWebhook()
    {
        await _eventBus.PublishAsync(new WebhookEventData
        {
            WebhookName = "YourWebhook",
            Data = new { /* webhook data */ }
        });
    }
}
```
