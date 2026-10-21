# LINGYUN.Abp.WeChat.SettingManagement

The WeChat settings management module provides management functions for WeChat related configurations.

## Features

* WeChat configuration management
* Official account configuration management
* Mini program configuration management
* Enterprise WeChat configuration management
* Configuration interface integration
*Multi-tenant support

## Module reference

```csharp
[DependsOn(typeof(AbpWeChatSettingManagementModule))]
public class YouProjectModule : AbpModule
{
  // other
}
```

## SettingsConfiguration

### Official account configuration

* `WeChat.Official.AppId`: Official account AppId
* `WeChat.Official.AppSecret`: Official account AppSecret
* `WeChat.Official.Token`: Official account message Token
* `WeChat.Official.EncodingAESKey`: public account message encryption key
* `WeChat.Official.IsSandBox`: whether it is a sandbox environment
* `WeChat.Official.Url`: Official account server URL

### Mini program configuration

* `WeChat.MiniProgram.AppId`: Mini Program AppId
* `WeChat.MiniProgram.AppSecret`: Mini Program AppSecret
* `WeChat.MiniProgram.Token`: Mini Program Message Token
* `WeChat.MiniProgram.EncodingAESKey`: Mini program message encryption key
* `WeChat.MiniProgram.IsDebug`: Whether to enable debugging mode
* `WeChat.MiniProgram.DefaultEnvironment`: default environment

### Enterprise WeChat configuration

* `WeChat.Work.CorpId`: corporate ID
* `WeChat.Work.AgentId`: application ID
* `WeChat.Work.Secret`: application secret
* `WeChat.Work.Token`: Message Token
* `WeChat.Work.EncodingAESKey`: message encryption key

## Permissions

* `WeChat.Setting`: WeChat settings management
* `WeChat.Setting.Official`: Official account setting management
* `WeChat.Setting.MiniProgram`: Mini program settings management
* `WeChat.Setting.Work`: Enterprise WeChat settings management

## More documentation

* [WeChat Settings Management Module Document](README.EN.md)
