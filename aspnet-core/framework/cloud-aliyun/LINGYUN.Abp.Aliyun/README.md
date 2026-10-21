# LINGYUN.Abp.Aliyun

Alibaba Cloud SDK integration module.

Reference: [Alibaba Cloud API Document](https://help.aliyun.com/document_detail/28763.html)

## Features

- Provide basic SDK integration of Alibaba Cloud services
- Support Alibaba Cloud RAM (Access Control) authentication
-Support STS Token access
-Support Alibaba Cloud SMS (SMS)
- Support Alibaba Cloud Object Storage (OSS)
- Provide distributed cache support and optimize high-concurrency scenarios

## Module reference

```csharp
[DependsOn(typeof(AbpAliyunModule))]
public class YouProjectModule : AbpModule
{
  // other
}
```

## Configuration item description

### Authentication configuration

- **AliyunSettingNames.Authorization.RegionId**

  - Description: Alibaba Cloud service area
  - Type: optional
  -Default value: default
  - Example: oss-cn-hangzhou

- **AliyunSettingNames.Authorization.AccessKeyId**

  - Description: AccessKey ID of Alibaba Cloud RAM account
  - Type: required
  - How to obtain: Visit Alibaba Cloud Console-Access Control

- **AliyunSettingNames.Authorization.AccessKeySecret**
  - Description: AccessKey Secret of RAM account
  - Type: required
  - How to obtain: Visit Alibaba Cloud Console-Access Control

### STS Token configuration

- **AliyunSettingNames.Authorization.UseSecurityTokenService**

  - Description: Whether to use STS Token for access
  - Type: optional
  -Default value: false
  - Recommendation: It is recommended to turn it on to improve security.

- **AliyunSettingNames.Authorization.RamRoleArn**

  - Description: Alibaba Cloud RAM role ARN
  - Type: required when enabling STS Token
  - Format: acs:ram::$accountID:role/$roleName

- **AliyunSettingNames.Authorization.RoleSessionName**

  - Description: User-defined token name
  - Type: optional
  - Purpose: for access auditing

- **AliyunSettingNames.Authorization.DurationSeconds**

  - Description: Expiration time of user token
  - Type: optional
  -Default: 3000
  - Unit: seconds

- **AliyunSettingNames.Authorization.Policy**
  - Description: Permission policy
  - Type: optional
  - Format: JSON string

### SMS service configuration

```json
{
  "Settings": {
    "Abp.Aliyun.Sms": {
      "Domain": "dysmsapi.aliyuncs.com", // API domain name, default is dysmsapi.aliyuncs.com
      "Version": "2017-05-25", // API version, default is 2017-05-25
      "ActionName": "SendSms", // API method name, default is SendSms
      "DefaultSignName": "", //Default SMS signature
      "DefaultTemplateCode": "", //Default SMS template code
      "DefaultPhoneNumber": "", // The default mobile phone number for receiving text messages
      "VisableErrorToClient": "false" // Whether to display error information to the client
    }
  }
}
```

## Available areas

This module supports the following Alibaba Cloud regions:

- China region

  - North China 1 (Qingdao) - cn-qingdao
  - North China 2 (Beijing) - cn-beijing
  - North China 3 (Zhangjiakou) - cn-zhangjiakou
  - North China 5 (Hohhot) - cn-huhehaote
  - East China 1 (Hangzhou) - cn-hangzhou
  - East China 2 (Shanghai) - cn-shanghai
  - South China 1 (Shenzhen) - cn-shenzhen
  - South China 2 (Heyuan) - cn-heyuan
  - South China 3 (Guangzhou) - cn-guangzhou
  - Southwest 1 (Chengdu) - cn-chengdu

- Hong Kong and overseas regions
  - Hong Kong - cn-hongkong
  - United States (Silicon Valley) - us-west-1
  - United States (Virginia) - us-east-1
  - Japan (Tokyo) - ap-northeast-1
  - South Korea (Seoul) - ap-northeast-2
  - Singapore - ap-southeast-1
  - Australia (Sydney) - ap-southeast-2
  - Malaysia (Kuala Lumpur) - ap-southeast-3
  - Indonesia (Jakarta) - ap-southeast-5
  - Philippines (Manila) - ap-southeast-6
  - Thailand (Bangkok) - ap-southeast-7
  - India (Mumbai) - ap-south-1
  - Germany (Frankfurt) - eu-central-1
  - United Kingdom (London) - eu-west-1
  - United Arab Emirates (Dubai) - me-east-1

## Performance optimization

- In high concurrency scenarios, it is recommended to enable distributed cache to improve performance
- When using STS Token, the Token will be automatically cached until it expires
- It is recommended to set DurationSeconds appropriately to avoid too frequent Token refreshes

## Related modules

- [LINGYUN.Abp.Aliyun.SettingManagement](../LINGYUN.Abp.Aliyun.SettingManagement/README.md) - Provides configuration management functions
- [LINGYUN.Abp.Aliyun.Features](../LINGYUN.Abp.Aliyun.Features/README.md) - Provides feature management functions

[Click to view English document](README.EN.md)
