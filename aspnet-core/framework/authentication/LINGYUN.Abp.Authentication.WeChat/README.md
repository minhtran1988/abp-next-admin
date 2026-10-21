# LINGYUN.Abp.Authentication.WeChat

The WeChat public account authentication module integrates the WeChat public account login function into the ABP application.

## Features

* WeChat public account OAuth2.0 authentication
* Supports obtaining basic information of WeChat users (nickname, gender, region, avatar, etc.)
* Support UnionId mechanism to connect public account and mini program account system
* Support WeChat server message verification
* Support integration with ABP identity system

## Module reference

```csharp
[DependsOn(typeof(AbpAuthenticationWeChatModule))]
public class YouProjectModule : AbpModule
{
  // other
}
```

## Configuration items

```json
{
  "Authentication": {
    "WeChat": {
      "AppId": "Your WeChat official account AppId",
      "AppSecret": "Your WeChat official account AppSecret",
      "ClaimsIssuer": "WeChat", // Optional, default is WeChat
      "CallbackPath": "/signin-wechat", // Optional, default is /signin-wechat
      "Scope": ["snsapi_login", "snsapi_userinfo"], // Optional, default includes snsapi_login and snsapi_userinfo
      "QrConnect": {
        "Enabled": false, // Whether to enable PC scanning QR code login
        "Endpoint": "https://open.weixin.qq.com/connect/qrconnect" // Scan the QR code login address on the PC side
      }
    }
  }
}
```

## Basic usage

1. Configure WeChat public account parameters
   * Apply for a public account on the WeChat public platform and obtain AppId and AppSecret
   * Configure AppId and AppSecret in appsettings.json

2. Add WeChat login
   ```csharp
   public override void ConfigureServices(ServiceConfigurationContext context)
   {
       context.Services.AddAuthentication()
           .AddWeChat(); // Add WeChat login support
   }
   ```

3. Enable WeChat server message verification (optional)
   ```csharp
   public void Configure(IApplicationBuilder app)
   {
       app.UseWeChatSignature(); // Enable WeChat server message verification middleware
   }
   ```

## Obtained user information

* OpenId - WeChat user’s unique identifier
* UnionId - the unique identifier of the WeChat open platform (needs to be bound to the open platform)
* NickName - user nickname
* Sex - user gender
* Country - country
*Province - Province
* City - city
* AvatarUrl - User avatar URL
* Privilege - User privilege information

## More information

* [WeChat Public Platform Development Document](https://developers.weixin.qq.com/doc/offiaccount/Getting_Started/Overview.html)
* [ABP Authentication Document](https://docs.abp.io/en/abp/latest/Authentication)
