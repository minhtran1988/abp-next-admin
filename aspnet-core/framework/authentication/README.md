# LINGYUN.Abp.Authentication authentication module

This module provides third-party social login authentication function, currently supports QQ and WeChat public account login, and is deeply integrated with the ABP identity system.

## Module overview

The authentication module contains two main sub-modules:

1. **QQ Internet Authentication Module**

   -Support QQ OAuth2.0 authentication
   - Suitable for mobile and PC login
   - Obtain basic user information (nickname, gender, avatar, etc.)

2. **WeChat public account authentication module**
   - Support WeChat public account OAuth2.0 authentication
   - Obtain user details (nickname, gender, region, avatar, etc.)
   - Support UnionId mechanism to connect the public account and mini program account system

## Features

- Log in with third-party social accounts
- Obtain basic user information
- Seamless integration with ABP identity system
- Flexible configuration options
-Supports multiple login scenarios (mobile terminal, PC terminal)

## Quick start

### Module reference

```csharp
[DependsOn(
    typeof(AbpAuthenticationQQModule),
    typeof(AbpAuthenticationWeChatModule)
)]
public class YourProjectModule : AbpModule
{
    //Other configuration
}
```

### Configuration example

Configure third-party login parameters in `appsettings.json`:

```json
{
  "Authentication": {
    "QQ": {
      "AppId": "Your QQ Internet AppId",
      "AppKey": "Your QQ Internet AppKey"
    },
    "WeChat": {
      "AppId": "Your WeChat official account AppId",
      "AppSecret": "Your WeChat official account AppSecret"
    }
  }
}
```

### Add login support

```csharp
public override void ConfigureServices(ServiceConfigurationContext context)
{
    context.Services.AddAuthentication()
        .AddQQConnect() // Add QQ login
        .AddWeChat(); // Add WeChat login
}
```

## Supported user information

### QQ login to obtain information

- OpenId - QQ user unique identifier
- NickName - user nickname
- Gender - user gender
- AvatarUrl - User avatar URL

### Log in with WeChat to obtain information

- OpenId - WeChat user's unique identifier
- UnionId - the unique identifier of WeChat open platform
- NickName - user nickname
- Sex - user gender
- Country, Province, City - Geographic information
- AvatarUrl - User avatar URL

## Reference documentation

- [QQ Connect Document](https://wiki.connect.qq.com/)
- [WeChat Public Platform Development Documents](https://developers.weixin.qq.com/doc/offiaccount/Getting_Started/Overview.html)
- [ABP Authentication Document](https://docs.abp.io/en/abp/latest/Authentication)

## License

Follow the project’s open source license
