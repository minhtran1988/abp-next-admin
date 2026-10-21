# LINGYUN.Abp.Elsa.Activities.Sms

SMS activity integration module for Elsa workflow

## Function

* Provide **SendSms** activity for sending text messages
  *Supports sending to multiple mobile phone numbers
  * Support custom message attributes
  * Support JavaScript, JSON and Liquid syntax
  * Integrate the `ISmsSender` interface of the ABP framework

## Configuration usage

```csharp
[DependsOn(
    typeof(AbpElsaActivitiesSmsModule)
    )]
public class YouProjectModule : AbpModule
{
}
```

## appsettings.json

```json
{
    "Elsa": {
        "Sms": true // Enable SMS activity
    }
}
```

## Activity parameters

* **To**: Receive a list of mobile phone numbers
* **Message**: SMS content
* **Properties**: Additional properties (advanced options)

[English](./README.EN.md)
