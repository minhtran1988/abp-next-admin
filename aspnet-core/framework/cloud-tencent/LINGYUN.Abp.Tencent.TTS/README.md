# LINGYUN.Abp.Tencent.TTS

Tencent Cloud speech synthesis service module integrates Tencent Cloud speech synthesis service into ABP applications.

## Features

*Support Tencent Cloud Speech Synthesis Service (TTS)
* Support multi-tenant configuration
*Based on Tencent Cloud TTS SDK V20190823
* Provide TTS client factory to support dynamic creation of TTS clients

## Basic usage

1. Add module dependencies
```csharp
[DependsOn(typeof(AbpTencentTTSModule))]
public class YourModule : AbpModule
{
    // ...
}
```

2. Configure Tencent Cloud services
```json
{
  "Settings": {
    "LINGYUN.Abp.Tencent": {
      "SecretId": "Your Tencent Cloud SecretId",
      "SecretKey": "Your Tencent Cloud SecretKey"
    }
  }
}
```

3. Example of using TTS service
```csharp
public class YourService
{
    private readonly TencentCloudTTSClientFactory _ttsClientFactory;

    public YourService(TencentCloudTTSClientFactory ttsClientFactory)
    {
        _ttsClientFactory = ttsClientFactory;
    }

    public async Task TextToSpeechAsync(string text)
    {
        var ttsClient = await _ttsClientFactory.CreateAsync();
        // Use ttsClient to call Tencent Cloud TTS service API
        // For detailed API usage, please refer to Tencent Cloud TTS SDK documentation
    }
}
```

## Configuration item description

### Basic configuration

```json
{
  "Settings": {
    "Abp.TencentCloud": {
      "SecretId": "Your Tencent Cloud SecretId", // Get it from the Tencent Cloud console
      "SecretKey": "Your Tencent Cloud SecretKey", // Get it from the Tencent Cloud console
      "DurationSecond": "600" // Session duration (seconds)
    }
  }
}
```

### TTS service configuration

```json
{
  "Settings": {
    "Abp.TencentCloud.TTS": {
      "AppId": "", // TTS application ID
      "VoiceType": "", // Voice, default is "0"
      "Language": "1", // Language, 1-Chinese, 2-English
      "Speed": "0", // Speech speed, range: -2~2
      "Volume": "0", // Volume, range: 0~10
      "ProjectId": "0" // Project ID
    }
  }
}
```

## More documentation

* [Tencent Cloud Speech Synthesis Service](https://cloud.tencent.com/document/product/1073)
* [Tencent Cloud TTS SDK Document](https://cloud.tencent.com/document/product/1073/37927)

[English](./README.EN.md)
