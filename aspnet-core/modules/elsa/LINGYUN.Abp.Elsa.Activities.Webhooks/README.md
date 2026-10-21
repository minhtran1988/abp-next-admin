# LINGYUN.Abp.Elsa.Activities.Webhooks

Webhook activity integration module for Elsa workflow

## Function

* Provide **PublishWebhook** activity for publishing Webhook
  * Support sending data to subscribers
  * Support custom HTTP headers
  * Support JavaScript, JSON and Liquid syntax
  * Integrate the `IWebhookPublisher` interface of the ABP framework
  *Support multi-tenancy

## Configuration usage

```csharp
[DependsOn(
    typeof(AbpElsaActivitiesWebhooksModule)
    )]
public class YouProjectModule : AbpModule
{
}
```

## appsettings.json

```json
{
    "Elsa": {
        "PublishWebhook": true // Enable Webhook activity
    }
}
```

## Activity parameters

* **WebhooName**: The unique name of the webhook
* **WebhookData**: Data to be sent
* **SendExactSameData**: Whether to send the original data directly (if true, the data in the parameters will be sent directly to the client)
* **UseOnlyGivenHeaders**: Whether to use only the given headers (if true, only the given headers will be used; if false, the given headers will be added to the predefined headers in the subscription)
* **Headers**: HTTP headers to send (advanced options)

[English](./README.EN.md)
