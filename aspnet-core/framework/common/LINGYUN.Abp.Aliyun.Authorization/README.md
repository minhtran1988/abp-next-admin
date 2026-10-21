# LINGYUN.Abp.Aliyun.Authorization

Alibaba Cloud's basic authentication module currently only requires access control functions, so only AppKeyId and AccessKeySecret are defined.

## Configuration usage

Modules are referenced on demand. Modules that use Alibaba Cloud basically need to rely on this.

Define the **appsettings.json** file in advance

```json
{
  "Aliyun": {
    "Auth": {
      "AccessKeyId": "Your Alibaba Cloud access ID",
      "AccessKeySecret": "Your Alibaba Cloud access key"
    }
  }
}

```

```csharp
[DependsOn(typeof(AbpAliyunAuthorizationModule))]
public class YouProjectModule : AbpModule
{
  // other
}
```