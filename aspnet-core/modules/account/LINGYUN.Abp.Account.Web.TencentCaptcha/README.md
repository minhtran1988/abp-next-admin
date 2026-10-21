# LINGYUN.Abp.Account.Web.TencentCaptcha

Tencent Cloud Tianyu verification code component integration module can use Tencent Cloud verification code capabilities

## Module reference

```csharp
[DependsOn(typeof(AbpAccountWebTencentCaptchaModule))]
public class YouProjectModule : AbpModule
{
  // other
}
```

## Notes

` Please configure Tencent Cloud related parameters in the startup project or system settings, otherwise the login page verification code component cannot be loaded.

* Abp.TencentCloud.SecretId Tencent Cloud account SecretId, needs to be encrypted
* Abp.TencentCloud.SecretKey Tencent Cloud account SecretKey, needs to be encrypted
* Abp.TencentCloud.Captcha.CaptchaAppId Verification code business ID, needs to be encrypted
* Abp.TencentCloud.Captcha.AppSecretKey original key (25 digits), needs to be encrypted
* Abp.TencentCloud.Captcha.CaptchaAppIdEncryptedType encryption method, case insensitive: "cbc", "gcm", if not specified, the front-end validator component will not be encrypted
* Abp.TencentCloud.Captcha.CaptchaAppIdEncryptedExpireTime expiration time (seconds), the maximum value is 86400 seconds, which is 24 hours

Example configuration:

```json
{
  "Settings": {
    "Abp.TencentCloud.SecretId": "XXXXXXXXXXXXXX",
    "Abp.TencentCloud.SecretKey": "YYYYYYYYYYYYY",
    "Abp.TencentCloud.Captcha.CaptchaAppId": "XXXXXXXXXXXX",
    "Abp.TencentCloud.Captcha.AppSecretKey": "XXXXXXXXXXXXX",
    "Abp.TencentCloud.Captcha.CaptchaAppIdEncryptedType": "gcm",
    "Abp.TencentCloud.Captcha.CaptchaAppIdEncryptedExpireTime": 300
  }
}
```
