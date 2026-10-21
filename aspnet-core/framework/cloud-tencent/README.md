# LINGYUN.Abp.Tencent module overview

## Introduction
The LINGYUN.Abp.Tencent module integrates various services of Tencent Cloud and provides comprehensive support for Tencent Cloud services, including object storage, SMS service, QQ interconnection, speech synthesis and other functions.

## List of included items
- **LINGYUN.Abp.Tencent**
- **LINGYUN.Abp.BlobStoring.Tencent**
- **LINGYUN.Abp.Sms.Tencent**
- **LINGYUN.Abp.Tencent.QQ**
- **LINGYUN.Abp.Tencent.SettingManagement**
- **LINGYUN.Abp.Tencent.TTS**

## Overview of the main features of each project

### LINGYUN.Abp.Tencent
- Provides Tencent Cloud SDK client factory to support dynamic creation of clients for Tencent Cloud services.
- Supports multi-tenant configuration and multi-language localization.
- Provide unified Tencent Cloud service configuration management.

### LINGYUN.Abp.BlobStoring.Tencent
- Supports Tencent Cloud object storage service and automatically creates storage buckets.
- Supports multi-region configuration and file size limits.

### LINGYUN.Abp.Sms.Tencent
- Supports the sending function of Tencent Cloud SMS service and supports batch sending to multiple mobile phone numbers.
- Built-in error handling and logging.

### LINGYUN.Abp.Tencent.QQ
- Support QQ Internet quick login and support multi-tenant configuration.

### LINGYUN.Abp.Tencent.SettingManagement
- Provides a configuration management interface for Tencent Cloud services, supporting global and tenant-level configuration management.

### LINGYUN.Abp.Tencent.TTS
- Support Tencent Cloud speech synthesis service and provide TTS client factory.

## Overall purpose and importance of the module
This module provides developers with seamless integration with Tencent Cloud services, simplifies the use and management of cloud services, and improves the flexibility and scalability of applications.

## How to use or integrate this module
Reference the corresponding modules in the project and configure relevant parameters of Tencent Cloud as needed. Make sure to use it with other modules of the ABP framework for best results.
