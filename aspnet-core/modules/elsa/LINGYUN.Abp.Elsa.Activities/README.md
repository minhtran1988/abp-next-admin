# LINGYUN.Abp.Elsa.Activities

Common Activity Integration

## Configuration usage

```csharp

    [DependsOn(
        typeof(AbpElsaActivitiesModule)
        )]
    public class YouProjectModule : AbpModule
    {
    }
```

## appsettings.json
```json
    {
        "Elsa": {
            "BlobStoring": true,
            "Emailing": true,
            "Notification": true,
            "Sms": true,
            "IM": true,
            "PublishWebhook": true
        }
    }

```