# LINGYUN.Abp.SettingManagement.HttpApi

## Module description

The setting management HTTP API module provides a RESTful API interface for setting management.

### Basic module

* LINGYUN.Abp.SettingManagement.Application.Contracts
* Volo.Abp.AspNetCore.Mvc

### Function definition

* API controller that provides settings management
  * SettingController - Universal settings management controller
  * UserSettingController - User setting management controller
  * SettingDefinitionController - Setting definition management controller

### API interface

* /api/setting-management/settings
  * GET /by-global - Get global settings
  * GET /by-tenant - Get tenant settings
  * GET /by-user - Get user settings
  * GET /groups - Get all settings groups
  * PUT /{providerName}/{providerKey} - update settings
* /api/setting-management/users
  * GET - Get user settings
  * PUT - Update user settings
  * DELETE - delete user settings
* /api/setting-management/definitions
  * GET - Get a list of settings definitions
  * POST - Create settings definition
  * PUT - Update settings definition
  * DELETE - delete a setting definition
  * GET /{name} - Get the specified setting definition

### Permission requirements

* SettingManagement.Settings
  * Update - update settings
  * ManageGroup - Management settings group
* SettingManagement.ManageFeatures
  * ManageHostFeatures - Manage host features

### How to use

1. Add `AbpSettingManagementHttpApiModule` dependency

```csharp
[DependsOn(typeof(AbpSettingManagementHttpApiModule))]
public class YouProjectModule : AbpModule
{
}
```

2. Use API interface

```csharp
public class YourService
{
    private readonly HttpClient _httpClient;

    public YourService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task ManageSettingsAsync()
    {
        // Get global settings
        var response = await _httpClient.GetAsync("/api/setting-management/settings/by-global");
        var settings = await response.Content.ReadFromJsonAsync<ListResultDto<SettingGroupDto>>();

        //Update user settings
        await _httpClient.PutAsJsonAsync("/api/setting-management/users", new UpdateSettingDto
        {
            Name = "SettingName",
            Value = "NewValue"
        });
    }
}
```

[View in English](README.EN.md)
