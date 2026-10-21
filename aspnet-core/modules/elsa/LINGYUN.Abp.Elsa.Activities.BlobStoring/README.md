# LINGYUN.Abp.Elsa.Activities.BlobStoring

Blob storage activity integration module for Elsa workflow

## Function

* Provides the following Blob storage activities:
  * **BlobExists**: Check if the Blob exists
  * **WriteBlob**: Write Blob data
  * **ReadBlob**: Read Blob data
  * **DeleteBlob**: Delete Blob data

## Configuration usage

```csharp
[DependsOn(
    typeof(AbpElsaActivitiesBlobStoringModule)
    )]
public class YouProjectModule : AbpModule
{
}
```

## appsettings.json

```json
{
    "Elsa": {
        "BlobStoring": true // Enable Blob storage activity
    }
}
```

[English](./README.EN.md)
