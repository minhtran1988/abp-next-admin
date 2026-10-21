# LINGYUN.Abp.WeChat.Official

WeChat public account SDK integration module provides functional support required for WeChat public account development.

## Features

* WeChat public account OAuth2.0 authentication
* Custom menu management
* Template message sending
* Material management
* User management
*Customer service message
* WeChat payment integration
* Message encryption and decryption
*Event handling

## Module reference

```csharp
[DependsOn(typeof(AbpWeChatOfficialModule))]
public class YouProjectModule : AbpModule
{
  // other
}
```

## Configuration items

```json
{
  "WeChat": {
    "Official": {
      "AppId": "", // Official account AppId
      "AppSecret": "", // Official account AppSecret
      "Token": "", // Official account message Token
      "EncodingAESKey": "", // Official account message encryption key
      "IsSandBox": false, // Whether it is a sandbox environment
      "Url": "" // Official account server URL
    }
  }
}
```

#### Notes

There is a known issue in dynamic configuration: https://github.com/abpframework/abp/issues/6318
Therefore, it is necessary to recreate a method to dynamically change AbpWeChatOfficialOptions, please use AbpWeChatOfficialOptionsFactory.CreateAsync()

## SettingsConfiguration

* `WeChat.Official.AppId`: Official account AppId
* `WeChat.Official.AppSecret`: Official account AppSecret
* `WeChat.Official.Token`: Official account message Token
* `WeChat.Official.EncodingAESKey`: public account message encryption key
* `WeChat.Official.IsSandBox`: whether it is a sandbox environment
* `WeChat.Official.Url`: Official account server URL

## More documentation

* [WeChat public account module document](README.EN.md)
