# LINGYUN.Abp.Webhooks.Core

Webhook core module provides Webhook definition, configuration and basic function support.

[English](README.EN.md)

## Features

* Webhook definition management
* Configurable Webhook timeout and retry mechanism
* Automatic subscription failure protection
* Customizable HTTP request headers
* Support multiple Webhook providers

## Module reference

```csharp
[DependsOn(typeof(AbpWebhooksCoreModule))]
public class YouProjectModule : AbpModule
{
  // other
}
```

## Configuration items

```json
{
  "Webhooks": {
    "TimeoutDuration": "00:01:00", //Default timeout time, default 60 seconds
    "MaxSendAttemptCount": 5, //Default maximum number of sends
    "IsAutomaticSubscriptionDeactivationEnabled": true, // Whether to automatically cancel the subscription when the maximum number of consecutive failures is reached
    "MaxConsecutiveFailCountBeforeDeactivateSubscription": 15, //The maximum number of consecutive failures before canceling the subscription, the default is MaxSendAttemptCount * 3
    "DefaultAgentIdentifier": "Abp-Webhooks", //Default sender identifier
    "DefaultHttpHeaders": { //Default request headers
      "_AbpDontWrapResult": "true",
      "X-Requested-From": "abp-webhooks"
    }
  }
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
                name: "TestWebhook",
                displayName: L("DisplayName:TestWebhook"),
                description: L("Description:TestWebhook")
            )
        );
    }
}
```

2. Configure Webhook options
```csharp
Configure<AbpWebhooksOptions>(options =>
{
    options.TimeoutDuration = TimeSpan.FromMinutes(2);
    options.MaxSendAttemptCount = 3;
    options.AddHeader("Custom-Header", "Value");
});
```
