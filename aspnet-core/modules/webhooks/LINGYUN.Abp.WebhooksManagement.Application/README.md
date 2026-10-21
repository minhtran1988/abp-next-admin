# LINGYUN.Abp.WebhooksManagement.Application

The Webhook management application service module provides application layer implementation of Webhook management.

[English](README.EN.md)

## Features

* Webhook subscription management
* Webhook group management
* Webhook definition management
* Webhook log query
* Webhook permission management

## Module reference

```csharp
[DependsOn(typeof(AbpWebhooksManagementApplicationModule))]
public class YouProjectModule : AbpModule
{
  // other
}
```

## Permission definition

* WebhooksManagement.Webhooks
  * WebhooksManagement.Webhooks.Create
  * WebhooksManagement.Webhooks.Update
  * WebhooksManagement.Webhooks.Delete
  * WebhooksManagement.Webhooks.ManagePermissions
* WebhooksManagement.Groups
  * WebhooksManagement.Groups.Create
  * WebhooksManagement.Groups.Update
  * WebhooksManagement.Groups.Delete
* WebhooksManagement.Subscriptions
  * WebhooksManagement.Subscriptions.Create
  * WebhooksManagement.Subscriptions.Update
  * WebhooksManagement.Subscriptions.Delete
* WebhooksManagement.Logs
  * WebhooksManagement.Logs.Default

## Basic usage

1. Manage Webhook subscriptions
```csharp
public class YourService
{
    private readonly IWebhookSubscriptionAppService _webhookSubscriptionAppService;

    public YourService(IWebhookSubscriptionAppService webhookSubscriptionAppService)
    {
        _webhookSubscriptionAppService = webhookSubscriptionAppService;
    }

    public async Task ManageSubscription()
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

2. Query Webhook logs
```csharp
public class YourService
{
    private readonly IWebhookSendAttemptAppService _webhookSendAttemptAppService;

    public YourService(IWebhookSendAttemptAppService webhookSendAttemptAppService)
    {
        _webhookSendAttemptAppService = webhookSendAttemptAppService;
    }

    public async Task QueryLogs()
    {
        var logs = await _webhookSendAttemptAppService.GetListAsync(
            new WebhookSendAttemptGetListInput());
    }
}
```
