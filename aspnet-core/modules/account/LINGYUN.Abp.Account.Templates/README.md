# LINGYUN.Abp.Account.Templates

The email template definition module of the ABP account module provides account-related email template functions.

[English](./README.EN.md)

## Features

* Provide standard account email templates
* Support email template localization
* Integrated ABP virtual file system
* Use ABP text template system

## Module reference

```csharp
[DependsOn(
    typeof(AbpEmailingModule),
    typeof(AbpAccountApplicationContractsModule))]
public class YouProjectModule : AbpModule
{
  // other
}
```

## Email template

### Template definition

The module defines the following email templates:

* `Abp.Account.MailConfirmLink` - Email address confirmation template
  * Used for user email address verification
  * Template file: `/LINGYUN/Abp/Account/Emailing/Templates/MailConfirm.tpl`
  * Use standard email layout

* `Abp.Account.MailSecurityVerifyLink` - Email security verification template
  * Used for email security verification
  * Template file: `/LINGYUN/Abp/Account/Emailing/Templates/MailSecurityVerify.tpl`
  * Use standard email layout

### Template system

* Use ABP text template system (TextTemplating)
* Support template localization
* Template definition provider: `AccountTemplateDefinitionProvider`

## Localization

Modules contain localized resources:
* Resource type: `AccountResource`
* Resource path: `/LINGYUN/Abp/Account/Templates/Localization/Resources`

## Virtual file system

The module uses the ABP virtual file system to manage template files:
* All template files are embedded through the virtual file system
* Supports overwriting and customization of template files
