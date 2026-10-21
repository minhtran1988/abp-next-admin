# LINGYUN.Abp.WeChat.MiniProgram

WeChat Mini Program SDK integration module provides functional support required for WeChat Mini Program development.

## Features

* WeChat applet login authentication
* Mini program code generation
*Subscribe to send messages
* Mini program data statistics
* Mini program live broadcast
* Mini program payment integration
* Unified service message

## Module reference

```csharp
[DependsOn(typeof(AbpWeChatMiniProgramModule))]
public class YouProjectModule : AbpModule
{
  // other
}
```

## Configuration items

```json
{
  "WeChat": {
    "MiniProgram": {
      "AppId": "", // Mini program AppId
      "AppSecret": "", // Mini program AppSecret
      "Token": "", // Mini program message Token
      "EncodingAESKey": "", // Mini program message encryption key
      "IsDebug": false, // Whether to enable debugging mode
      "DefaultEnvironment": "release" //Default environment, optional values: develop (development version), trial (experience version), release (official version)
    }
  }
}
```

#### Notes

There is a known issue in dynamic configuration: https://github.com/abpframework/abp/issues/6318
Therefore, it is necessary to recreate a method to dynamically change AbpWeChatMiniProgramOptions. Please use AbpWeChatMiniProgramOptionsFactory.CreateAsync()

## SettingsConfiguration

* `WeChat.MiniProgram.AppId`: Mini Program AppId
* `WeChat.MiniProgram.AppSecret`: Mini Program AppSecret
* `WeChat.MiniProgram.Token`: Mini Program Message Token
* `WeChat.MiniProgram.EncodingAESKey`: Mini program message encryption key
* `WeChat.MiniProgram.IsDebug`: Whether to enable debugging mode
* `WeChat.MiniProgram.DefaultEnvironment`: default environment

## More documentation

* [WeChat Mini Program Module Document](README.EN.md)
