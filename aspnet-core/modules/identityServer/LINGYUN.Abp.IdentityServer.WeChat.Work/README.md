# LINGYUN.Abp.IdentityServer.WeChat.Work

The IdentityServer Enterprise WeChat authentication module provides identity authentication functions based on Enterprise WeChat.

## Features

* Enterprise WeChat certification
  * `WeChatWorkGrantValidator` - Enterprise WeChat authorization validator
    *Support corporate WeChat login
    *Support multi-tenancy
    * Automatic user registration
    * Security logging
    *Event notification
    * Localization support

* Certification process
  1. The user initiates a login request through Enterprise WeChat
  2. Verify the validity of AgentId and Code
  3. Obtain enterprise WeChat user information
  4. Verify user registration status
     * Registered users log in directly
     * Unregistered users are automatically registered according to configuration
  5. Generate access token
  6. Record security logs and events

## Module reference

```csharp
[DependsOn(
    typeof(AbpIdentityServerWeChatWorkModule)
)]
public class YourModule : AbpModule
{
    // ...
}
```

## Dependent modules

* `AbpIdentityServerDomainModule` - ABP IdentityServer domain module
* `AbpWeChatWorkModule` - ABP enterprise WeChat module

## Configuration usage

### Configure Enterprise WeChat Authentication

```csharp
public override void PreConfigureServices(ServiceConfigurationContext context)
{
    PreConfigure<IIdentityServerBuilder>(builder =>
    {
        builder.AddExtensionGrantValidator<WeChatWorkGrantValidator>();
    });
}
```

### Authentication request parameters

* `grant_type`: "wechat_work" (required)
* `agent_id`: Enterprise WeChat application ID (required)
* `code`: Enterprise WeChat authorization code (required)
* `scope`: request scope (optional)

### Authentication response

* Authentication successful:
```json
{
    "access_token": "Access token",
    "expires_in": validity period,
    "token_type": "Bearer",
    "refresh_token": "Refresh token"
}
```

*Authentication failed:
```json
{
    "error": "invalid_grant",
    "error_description": "Error description"
}
```

### Configuration options

* Quick login
```csharp
Configure<AbpSettingOptions>(options =>
{
    // Enable quick login for unregistered users
    options.SetDefault(WeChatWorkSettingNames.EnabledQuickLogin, true);
});
```

### Error type

* `invalid_grant`: Authorization verification failed
  *AgentId or Code is invalid
  * User is not registered and quick login is not enabled
  * Enterprise WeChat API call failed

Related documents:
* [IdentityServer4 Documentation](https://identityserver4.readthedocs.io/)
* [Enterprise WeChat Development Documentation](https://work.weixin.qq.com/api/doc)

[View English document](README.EN.md)
