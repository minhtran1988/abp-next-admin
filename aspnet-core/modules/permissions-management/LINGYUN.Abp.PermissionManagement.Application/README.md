# LINGYUN.Abp.PermissionManagement.Application

The permission management application service module provides application layer implementation of permission management.

## Features

* Permission group definition management
  * Create, update, and delete permission group definitions
  * Support enabling/disabling permission groups
  * Support static/dynamic configuration of permission groups
* Permission definition management
  * Create, update, delete permission definitions
  * Support enabling/disabling permissions
  * Support static/dynamic configuration of permissions
  * Support parent-child hierarchical relationship of permissions
*Multi-tenant support
  * Supports two multi-tenant modes: Host and Tenant
  * Support multi-tenant side configuration of permissions

## Module reference

```csharp
[DependsOn(
    typeof(AbpPermissionManagementApplicationContractsModule),
    typeof(VoloAbpPermissionManagementApplicationModule))]
public class YouProjectModule : AbpModule
{
    // other
}
```

## Basic usage

1. Permission group definition management
```csharp
public class YourService
{
    private readonly IPermissionGroupDefinitionAppService _groupDefinitionAppService;

    public YourService(IPermissionGroupDefinitionAppService groupDefinitionAppService)
    {
        _groupDefinitionAppService = groupDefinitionAppService;
    }

    public async Task ManageGroupDefinitionAsync()
    {
        //Create permission group
        await _groupDefinitionAppService.CreateAsync(new PermissionGroupDefinitionCreateDto
        {
            Name = "MyPermissionGroup",
            DisplayName = "My Permission Group"
        });

        //Update permission group
        await _groupDefinitionAppService.UpdateAsync("MyPermissionGroup", 
            new PermissionGroupDefinitionUpdateDto
            {
                DisplayName = "Updated Permission Group"
            });
    }
}
```

2. Permission definition management
```csharp
public class YourService
{
    private readonly IPermissionDefinitionAppService _permissionDefinitionAppService;

    public YourService(IPermissionDefinitionAppService permissionDefinitionAppService)
    {
        _permissionDefinitionAppService = permissionDefinitionAppService;
    }

    public async Task ManagePermissionDefinitionAsync()
    {
        //Create permissions
        await _permissionDefinitionAppService.CreateAsync(new PermissionDefinitionCreateDto
        {
            GroupName = "MyPermissionGroup",
            Name = "MyPermission",
            DisplayName = "My Permission",
            Providers = new[] { "Role", "User" }
        });

        //Update permissions
        await _permissionDefinitionAppService.UpdateAsync("MyPermission",
            new PermissionDefinitionUpdateDto
            {
                DisplayName = "Updated Permission"
            });
    }
}
```

## See also

* [LINGYUN.Abp.PermissionManagement.Application.Contracts](../LINGYUN.Abp.PermissionManagement.Application.Contracts/README.md)
* [LINGYUN.Abp.PermissionManagement.Domain.OrganizationUnits](../LINGYUN.Abp.PermissionManagement.Domain.OrganizationUnits/README.md)
* [LINGYUN.Abp.PermissionManagement.HttpApi](../LINGYUN.Abp.PermissionManagement.HttpApi/README.md)
