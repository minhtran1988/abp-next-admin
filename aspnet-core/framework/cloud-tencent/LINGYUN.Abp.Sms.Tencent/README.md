# LINGYUN.Abp.Sms.Tencent

Tencent Cloud SMS service module integrates Tencent Cloud SMS service into ABP applications.

## Features

* Support the sending function of Tencent Cloud SMS service
* Support multi-tenant configuration
* Support default signature and template configuration
* Support batch sending to multiple mobile phone numbers
* Support SMS template parameter passing
* Built-in error handling and logging

## Configuration item description

### Basic configuration

```json
{
  "Settings": {
    "Abp.TencentCloud": {
      "SecretId": "Your Tencent Cloud SecretId", // Get it from the Tencent Cloud console
      "SecretKey": "Your Tencent Cloud SecretKey", // Get it from the Tencent Cloud console
      "DurationSecond": "600" // Session duration, in seconds
    }
  }
}
```

### SMS service configuration

```json
{
  "Settings": {
    "Abp.TencentCloud.Sms": {
      "AppId": "", // SMS application ID, generated after adding the application in the SMS console
      "DefaultSignName": "", //Default SMS signature
      "DefaultTemplateId": "" //Default SMS template ID
    }
  }
}
```

## Basic usage

1. Add module dependencies
```csharp
[DependsOn(typeof(AbpSmsTencentModule))]
public class YourModule : AbpModule
{
    // ...
}
```

2. Configure Tencent Cloud SMS service
```json
{
  "Settings": {
    "Abp.TencentCloud": {
      "SecretId": "Your Tencent Cloud SecretId",
      "SecretKey": "Your Tencent Cloud SecretKey"
    },
    "Abp.TencentCloud.Sms": {
      "AppId": "Your SMS application ID",
      "DefaultSignName": "Your SMS signature",
      "DefaultTemplateId": "Your default template ID"
    }
  }
}
```

3. Example of sending SMS
```csharp
public class YourService
{
    private readonly ISmsSender _smsSender;

    public YourService(ISmsSender smsSender)
    {
        _smsSender = smsSender;
    }

    //Send using default signature and template
    public async Task SendSmsAsync(string phoneNumber, Dictionary<string, object> templateParams)
    {
        await _smsSender.SendAsync(
            phoneNumber,
            nameof(TencentCloudSmsSender),
            templateParams);
    }

    //Send using specified signature and template
    public async Task SendSmsAsync(
        string signName,
        string templateCode,
        string phoneNumber,
        Dictionary<string, object> templateParams)
    {
        await _smsSender.SendAsync(
            signName,
            templateCode,
            phoneNumber,
            templateParams);
    }
}
```

## Advanced features

### Feature switch

The module provides the following feature switches:

* TencentSms - Control the enabling/disabling of Tencent Cloud SMS service

### Error handling

* When all text messages fail to be sent, an exception will be thrown
* When some text messages fail to be sent, a warning log will be recorded
* Supports viewing detailed information of failed delivery, including serial number, mobile phone number, error code and error message

## More documentation

* [Tencent Cloud SMS Service](https://cloud.tencent.com/document/product/382)
* [SMS Console](https://console.cloud.tencent.com/smsv2)

[English](./README.EN.md)
