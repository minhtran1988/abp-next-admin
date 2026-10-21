# LINGYUN.Abp.Sms.Aliyun

Alibaba Cloud implementation of abp framework SMS sending interface **ISmsSender**

## Configuration usage

Module reference on demand

```csharp
[DependsOn(typeof(AbpAliyunSmsModule))]
public class YouProjectModule : AbpModule
{
  // other
}
```
## Configuration item description

* AliyunSettingNames.Sms.Domain optional, Alibaba Cloud SMS service domain name, default dysmsapi.aliyuncs.com
* AliyunSettingNames.Sms.ActionName is required, the name of the calling method, the default is SendSms
* AliyunSettingNames.Sms.Version is required, R default version number, default 2017-05-25
* AliyunSettingNames.Sms.DefaultSignName Optional, default signature, the SMS signature used by default when the user calls without passing a signature
* AliyunSettingNames.Sms.DefaultTemplateCode Optional, the default SMS template number, the SMS template used by default when the user calls the SMS template that is not passed
* AliyunSettingNames.Sms.DefaultPhoneNumber is optional, the default sending number. When the user calls without passing the recipient number, the default recipient number is used.
* AliyunSettingNames.Sms.VisableErrorToClient Optional, displays errors to the client. When an error occurs when calling the SMS interface, whether to return error details to the client, default false

## Others

Network factors may cause unexpected exceptions under high concurrency. Consider using second-level cache.
