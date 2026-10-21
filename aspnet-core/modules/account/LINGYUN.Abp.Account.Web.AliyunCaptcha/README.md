# LINGYUN.Abp.Account.Web.AliyunCaptcha

Alibaba Cloud verification code component integration module, which can use Alibaba Cloud verification code capabilities

## Module reference

```csharp
[DependsOn(typeof(AbpAccountWebAliyunCaptchaModule))]
public class YouProjectModule : AbpModule
{
  // other
}
```

## Notes

` Please configure Alibaba Cloud related parameters in the startup project or system settings, otherwise the login page verification code component cannot be loaded.

* Abp.Aliyun.Authorization.RegionId Alibaba Cloud access control related configuration, please refer to the AbpAliyunModule module for details
* Abp.Aliyun.Authorization.AccessKeyId Alibaba Cloud access control related configuration, please refer to the AbpAliyunModule module for details
* Abp.Aliyun.Authorization.AccessKeySecret Alibaba Cloud access control related configuration, please refer to the AbpAliyunModule module for details
* Abp.Aliyun.Authorization.UseSecurityTokenService Alibaba Cloud access control related configuration, please refer to the AbpAliyunModule module for details
* Abp.Aliyun.Authorization.RamRoleArn Alibaba Cloud access control related configuration, please refer to the AbpAliyunModule module for details
* Abp.Aliyun.Authorization.RoleSessionName Alibaba Cloud access control related configuration, please refer to the AbpAliyunModule module for details
* Abp.Aliyun.Authorization.DurationSeconds Alibaba Cloud access control related configuration, please refer to the AbpAliyunModule module for details
* Abp.Aliyun.Authorization.Policy Alibaba Cloud access control related configuration, please refer to the AbpAliyunModule module for details
* Abp.Aliyun.Captcha.SceneId The unique identifier of the verification code scene, which needs to be encrypted
* Abp.Aliyun.Captcha.Prefix The identity obtained by the console needs to be encrypted
* Abp.Aliyun.Captcha.UseEncryptedSceneId Use encrypted verification code scene identification
* Abp.Aliyun.Captcha.EKey The ekey obtained by the console needs to be encrypted
* Abp.Aliyun.Captcha.EncryptedExpireTimeSec ciphertext expiration time, unit seconds, range 1~86400

Example configuration:

```json
{
  "Settings": {
    "Abp.Aliyun.Authorization.RegionId": "XXXXXXXXXXXXXX",
    "Abp.Aliyun.Authorization.AccessKeyId": "XXXXXXXXXXXXXX",
    "Abp.Aliyun.Authorization.AccessKeySecret": "XXXXXXXXXXXXXX",
    "Abp.Aliyun.Authorization.UseSecurityTokenService": true,
    "Abp.Aliyun.Authorization.RamRoleArn": "XXXXXXXXXXXXXX",
    "Abp.Aliyun.Authorization.RoleSessionName": "XXXXXXXXXXXXXX",
    "Abp.Aliyun.Authorization.DurationSeconds": 3600,
    "Abp.Aliyun.Authorization.Policy": null,
    "Abp.Aliyun.Captcha.SceneId": "YYYYYYYYYYYYY",
    "Abp.Aliyun.Captcha.Prefix": "YYYYYYYYYYYYY",
    "Abp.Aliyun.Captcha.UseEncryptedSceneId": true,
    "Abp.Aliyun.Captcha.EKey": "YYYYYYYYYYYYY",
    "Abp.Aliyun.Captcha.EncryptedExpireTimeSec": 300
  }
}
```
