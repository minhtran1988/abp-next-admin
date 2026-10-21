# LINGYUN.Abp.LocalizationManagement.Domain.Shared

The localization management domain layer shared module defines shared content such as error codes, localized resources, and constants.

## Features

* Define constants related to localization management
* Define localized error codes
* Provide localized resource files
* Support multi-language error messages

## Module reference

```csharp
[DependsOn(typeof(AbpLocalizationManagementDomainSharedModule))]
public class YouProjectModule : AbpModule
{
  // other
}
```

## Error code

* Localization:001100 - Language {CultureName} already exists
* Localization:001400 - The language name {CultureName} does not exist or the built-in language does not allow the operation
* Localization:002100 - Resource {Name} already exists
* Localization:002400 - The resource name {Name} does not exist or the built-in resource does not allow the operation

## Localized resources

The module defines the following localization resources:

* DisplayName:Enable - enable
* DisplayName:CreationTime - creation time
* DisplayName:LastModificationTime - modification time
* DisplayName:SaveAndNext - save and next step
* Permissions:LocalizationManagement - localization management
* Permissions:Language - Language management
* Permissions:Resource - Resource Management
* Permissions:Text - Document management
* Wait...

## More information

* [English documentation](./README.EN.md)
