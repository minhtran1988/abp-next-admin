# LINGYUN.Abp.SettingManagement.Application.Contracts

## Module description

The setting management application service contract module provides interface definitions and DTOs related to setting management.

### Basic module

* Volo.Abp.SettingManagement.Application.Contracts
* Volo.Abp.Ddd.Application.Contracts

### Function definition

* Provide application service interface definition for setting management
  * ISettingAppService - Universal settings management service
  * IUserSettingAppService - User settings management service
  * IReadonlySettingAppService - read-only setting service
* Provide setting-related DTO definitions
  * SettingGroupDto - Setting group DTO
  * SettingDto - Set DTO
  * SettingDetailsDto - Setting details DTO
  * UpdateSettingDto - update setting DTO
* Provide permission definitions related to setting management

### Permission definition

* SettingManagement.ManageHostFeatures - Management host features
* SettingManagement.ManageFeatures - Management features
* SettingManagement.Settings - Settings management
* SettingManagement.Settings.Update - update settings
* SettingManagement.Settings.ManageGroup - Management settings group

### Configuration definition

* SettingManagementMergeOptions
  * EnableCustomize - whether to enable custom settings
  * EnableHost - whether to enable host settings
  * EnableTenant - whether to enable tenant settings
  * EnableUser - whether to enable user settings

### How to use

1. Add `AbpSettingManagementApplicationContractsModule` dependency

```csharp
[DependsOn(typeof(AbpSettingManagementApplicationContractsModule))]
public class YouProjectModule : AbpModule
{
}
```

2. Inject and use the settings service

```csharp
public class YourService
{
    private readonly ISettingAppService _settingAppService;

    public YourService(ISettingAppService settingAppService)
    {
        _settingAppService = settingAppService;
    }

    public async Task ManageSettingsAsync()
    {
        // Get the settings group
        var groups = await _settingAppService.GetAllGroupsAsync();
        
        //Update settings
        await _settingAppService.UpdateAsync("GroupName", new UpdateSettingsDto
        {
            Settings = new List<UpdateSettingDto>
            {
                new UpdateSettingDto
                {
                    Name = "SettingName",
                    Value = "NewValue"
                }
            }
        });
    }
}
```

[Return to directory](../../../README.md)
