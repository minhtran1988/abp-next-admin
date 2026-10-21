# LINGYUN.Abp.Aliyun.SettingManagement

Alibaba Cloud configuration management module. This module can be used to manage Alibaba Cloud-related configurations and aggregate publicly announced API interfaces through the gateway.

## Features

* Provide query interface for Alibaba Cloud service configuration
* Supports obtaining Alibaba Cloud configuration information through API interface
* Integrated with ABP settings management system

## API interface

* **GET api/setting-management/aliyun** - Get Alibaba Cloud configuration information

## Module reference

The module is referenced on demand. It is recommended to reference this module in the configuration management hosting service.

```csharp
[DependsOn(typeof(AbpAliyunSettingManagementModule))]
public class YouProjectModule : AbpModule
{
  // other
}
```

## Note

* Since the background management module is responsible for managing all configurations, this module only provides a query interface
* Need to be used in conjunction with LINGYUN.Abp.Aliyun module
* It is recommended to reference this module in the configuration management hosting service

[Click to view English document](README.EN.md)
