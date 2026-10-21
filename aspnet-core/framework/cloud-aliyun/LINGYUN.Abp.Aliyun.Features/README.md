# LINGYUN.Abp.Aliyun.Features

Alibaba Cloud service function management module.

## Features

* Provide function definition and management of Alibaba Cloud services
* Support enabling/disabling Alibaba Cloud service functions
* Integrate with ABP function management system

## Module reference

```csharp
[DependsOn(typeof(AbpAliyunFeaturesModule))]
public class YouProjectModule : AbpModule
{
  // other
}
```

## Function items

* **Features:AlibabaCloud** - Alibaba Cloud Service Function Group
  * **Features:AlibabaCloud:IsEnabled** - Whether to enable Alibaba Cloud services
    *Default value: false
    * Description: Enable the ability for applications to have Alibaba Cloud services

## Configuration items

This module is mainly used for function definition and does not contain configuration items.

## Note

* This module needs to be used in conjunction with the LINGYUN.Abp.Aliyun module
* After enabling the Alibaba Cloud service function, you also need to configure the corresponding service parameters in the LINGYUN.Abp.Aliyun module

[Click to view English document](README.EN.md)
