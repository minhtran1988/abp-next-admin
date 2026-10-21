# LINGYUN.Abp.WeChat.Work

The enterprise WeChat integration module provides functional support required for enterprise WeChat application development.

## Features

* Enterprise WeChat identity verification
* Address book management
* Message push
* Material management
* Customer contact
* Application management
* Authentication
*Business payment
* Electronic invoice

## Module reference

```csharp
[DependsOn(typeof(AbpWeChatWorkModule))]
public class YouProjectModule : AbpModule
{
  // other
}
```

## Configuration items

```json
{
  "WeChat": {
    "Work": {
      "CorpId": "", //CorpId
      "AgentId": 0, // Application ID
      "Secret": "", // Application secret
      "Token": "", // Message Token
      "EncodingAESKey": "", // Message encryption key
      "ApiUrl": "https://qyapi.weixin.qq.com" // API address
    }
  }
}
```

## SettingsConfiguration

* `WeChat.Work.CorpId`: corporate ID
* `WeChat.Work.AgentId`: application ID
* `WeChat.Work.Secret`: application secret
* `WeChat.Work.Token`: Message Token
* `WeChat.Work.EncodingAESKey`: message encryption key
* `WeChat.Work.ApiUrl`: API address

## More documentation

* [Enterprise WeChat Integration Module Document](README.EN.md)
