# LINGYUN.Abp.PermissionManagement.Domain.OrganizationUnits

The organizational unit permission management domain module provides permission management functions based on organizational units.

## Features

* Organization unit permission management
  * Support assigning permissions to organizational units
  * Support inheritance of organizational unit permissions
* Permission provider
  * Implement OrganizationUnit permission provider
  * Support role organizational unit permission check
  * Support user organizational unit permission check
* Automatic permission cleaning
  * Automatically clear related permissions when the organizational unit is deleted

## Module reference

```csharp
[DependsOn(
    typeof(AbpPermissionManagementDomainOrganizationUnitsModule))]
public class YouProjectModule : AbpModule
{
    // other
}
```

## Basic usage

1. Organization unit permission management
```csharp
public class YourService
{
    private readonly IPermissionManager _permissionManager;

    public YourService(IPermissionManager permissionManager)
    {
        _permissionManager = permissionManager;
    }

    public async Task ManageOrganizationUnitPermissionAsync(string organizationUnitCode)
    {
        //Set organizational unit permissions
        await _permissionManager.SetAsync(
            "MyPermission",
            OrganizationUnitPermissionValueProvider.ProviderName,
            organizationUnitCode);

        // Check organizational unit permissions
        var result = await _permissionManager.GetAsync(
            "MyPermission",
            OrganizationUnitPermissionValueProvider.ProviderName,
            organizationUnitCode);
    }
}
```

## See also

* [LINGYUN.Abp.PermissionManagement.Application](../LINGYUN.Abp.PermissionManagement.Application/README.md)
* [LINGYUN.Abp.PermissionManagement.Application.Contracts](../LINGYUN.Abp.PermissionManagement.Application.Contracts/README.md)
* [LINGYUN.Abp.PermissionManagement.HttpApi](../LINGYUN.Abp.PermissionManagement.HttpApi/README.md)
