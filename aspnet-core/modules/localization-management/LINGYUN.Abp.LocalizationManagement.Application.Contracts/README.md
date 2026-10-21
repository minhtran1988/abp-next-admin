# LINGYUN.Abp.LocalizationManagement.Application.Contracts

The localization management application service layer contract module defines the application service interface, DTO object and permission definition of localization management.

## Features

* Define language management application service interface
* Define resource management application service interface
* Define text management application service interface
* Define permissions and authorizations
* Provide DTO object definition

## Module reference

```csharp
[DependsOn(typeof(AbpLocalizationManagementApplicationContractsModule))]
public class YouProjectModule : AbpModule
{
  // other
}
```

## Permission definition

* LocalizationManagement.Resource - whether the authorization object allows access to the resource
* LocalizationManagement.Resource.Create - whether the authorization object allows resource creation
* LocalizationManagement.Resource.Update - whether the authorization object allows modification of resources
* LocalizationManagement.Resource.Delete - Whether the authorization object allows resource deletion
* LocalizationManagement.Language - whether the authorization object allows access to the language
* LocalizationManagement.Language.Create - whether the authorization object allows language creation
* LocalizationManagement.Language.Update - whether the authorization object allows language modification
* LocalizationManagement.Language.Delete - whether the authorization object allows language deletion
* LocalizationManagement.Text - whether the authorization object allows access to the document
* LocalizationManagement.Text.Create - whether the authorization object allows document creation
* LocalizationManagement.Text.Update - whether the authorization object allows modification of the document
* LocalizationManagement.Text.Delete - Whether the authorization object allows deletion of documents

## Application service interface

* `ILanguageAppService`: Language management application service interface
* `IResourceAppService`: Resource management application service interface
* `ITextAppService`: Text management application service interface

## More information

* [English documentation](./README.EN.md)
