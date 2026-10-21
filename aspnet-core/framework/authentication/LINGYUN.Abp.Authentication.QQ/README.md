# LINGYUN.Abp.Authentication.QQ

QQ Internet authentication module integrates QQ login function into ABP application.

## Features

* QQ OAuth2.0 authentication
* Supports mobile and PC login
* Obtain basic information of QQ users (nickname, gender, avatar, etc.)
* Support integration with ABP identity system

## Module reference

```csharp
[DependsOn(typeof(AbpAuthenticationQQModule))]
public class YouProjectModule : AbpModule
{
  // other
}
```

## Configuration items

```json
{
  "Authentication": {
    "QQ": {
      "AppId": "Your QQ Internet AppId",
      "AppKey": "Your QQ Internet AppKey",
      "IsMobile": false, // Whether to enable the mobile login page
      "ClaimsIssuer": "connect.qq.com", // Optional, default is connect.qq.com
      "CallbackPath": "/signin-qq", // Optional, default is /signin-qq
      "Scope": ["get_user_info"] // Optional, default is get_user_info
    }
  }
}
```

## Basic usage

1. Configure QQ interconnection parameters
   * Apply for an application on QQ Internet platform and obtain AppId and AppKey
   * Configure AppId and AppKey in appsettings.json

2. Add QQ login
   ```csharp
   public override void ConfigureServices(ServiceConfigurationContext context)
   {
       context.Services.AddAuthentication()
           .AddQQConnect(); // Add QQ login support
   }
   ```

## Obtained user information

* OpenId - QQ user unique identifier
* NickName - user nickname
* Gender - user gender
* AvatarUrl - User avatar URL

## More information

* [QQ Connect Document](https://wiki.connect.qq.com/)
* [ABP Authentication Document](https://docs.abp.io/en/abp/latest/Authentication)
