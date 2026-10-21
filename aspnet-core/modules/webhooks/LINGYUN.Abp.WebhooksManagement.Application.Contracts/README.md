# LINGYUN.Abp.WebhooksManagement.Application.Contracts

Webhook management application service contract module defines the application service interface and DTO of Webhook management.

[English](README.EN.md)

## Features

* Webhook subscription service interface
* Webhook group service interface
* Webhook defines service interface
* Webhook log service interface
* Webhook permission definition
* Webhook DTO definition

## Module reference

```csharp
[DependsOn(typeof(AbpWebhooksManagementApplicationContractsModule))]
public class YouProjectModule : AbpModule
{
  // other
}
```

## Service interface

* IWebhookSubscriptionAppService - Webhook subscription management service
* IWebhookGroupAppService - Webhook group management service
* IWebhookDefinitionAppService - Webhook definition management service
* IWebhookSendAttemptAppService - Webhook send log service

## Basic usage

1. Implement Webhook subscription service
```csharp
public class WebhookSubscriptionAppService : 
    ApplicationService,
    IWebhookSubscriptionAppService
{
    public async Task<WebhookSubscriptionDto> CreateAsync(
        WebhookSubscriptionCreateDto input)
    {
        // Implement creation subscription logic
    }

    public async Task<PagedResultDto<WebhookSubscriptionDto>> GetListAsync(
        WebhookSubscriptionGetListInput input)
    {
        // Implement query subscription logic
    }
}
```

2. Use Webhook DTO
```csharp
public class YourDto
{
    public WebhookSubscriptionDto Subscription { get; set; }
    public WebhookGroupDto Group { get; set; }
    public WebhookDefinitionDto Definition { get; set; }
    public WebhookSendAttemptDto SendAttempt { get; set; }
}
```
