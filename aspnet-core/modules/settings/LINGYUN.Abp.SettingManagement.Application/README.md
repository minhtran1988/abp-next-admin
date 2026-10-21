# LINGYUN.Abp.SettingManagement.Application

## Module description

The settings management application service module implements business logic related to settings management.

### Basic module

* LINGYUN.Abp.SettingManagement.Application.Contracts
* Volo.Abp.SettingManagement.Application
* Volo.Abp.Ddd.Application

### Function definition

* Provide application service implementation for setting management
  * SettingAppService - Universal settings management service implementation
  * UserSettingAppService - User setting management service implementation
  * SettingDefinitionAppService - Setting definition management service implementation
* Implement the following application service interfaces
  * ISettingAppService
  * IUserSettingAppService
  * ISettingDefinitionAppService
* Provide settings cache management
  * DynamicSettingDefinitionStoreCacheInvalidator - dynamic setting definition cache invalidation processing

### Application Services

* SettingAppService
  * GetAllForGlobalAsync - Get global settings
  * GetAllForTenantAsync - Get tenant settings
  * GetAllForUserAsync - Get user settings
  * GetAllGroupsAsync - Get all settings groups
  * UpdateAsync - update settings
* UserSettingAppService
  * GetAsync - Get user settings
  * UpdateAsync - Update user settings
  * DeleteAsync - delete user settings
* SettingDefinitionAppService
  * GetAsync - Get setting definition
  * GetListAsync - Get the setting definition list
  * CreateAsync - Create settings definition
  * UpdateAsync - Update settings definition
  * DeleteAsync - delete settings definition

### Error code

* SettingManagement:010001 - Setting definition name already exists
* SettingManagement:010002 - Setting definition does not exist
* SettingManagement:010003 - The setting is defined as static and no modification is allowed
* SettingManagement:010004 - The setting is defined as static and does not allow deletion

### How to use

1. Add `AbpSettingManagementApplicationModule` dependency

```csharp
[DependsOn(typeof(AbpSettingManagementApplicationModule))]
public class YouProjectModule : AbpModule
{
}
```

2. Inject and use the settings service

```csharp
public class YourService
{
    private readonly ISettingAppService _settingAppService;
    private readonly IUserSettingAppService _userSettingAppService;

    public YourService(
        ISettingAppService settingAppService,
        IUserSettingAppService userSettingAppService)
    {
        _settingAppService = settingAppService;
        _userSettingAppService = userSettingAppService;
    }

    public async Task ManageSettingsAsync()
    {
        // Get global settings
        var settings = await _settingAppService.GetAllForGlobalAsync();

        //Update user settings
        await _userSettingAppService.UpdateAsync(
            "SettingName",
            "NewValue");
    }
}
```

[View in English](README.EN.md)
