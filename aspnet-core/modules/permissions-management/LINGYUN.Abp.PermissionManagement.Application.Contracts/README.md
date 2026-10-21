# LINGYUN.Abp.PermissionManagement.Application.Contracts

The permission management application service contract module defines the interface and DTO of permission management.

## Features

*Permission group definition interface
  * Provide CRUD operation interface for permission groups
  * Support permission group paging query
* Permission definition interface
  * Provide CRUD operation interface for permissions
  *Support permission paging query
* Permission definition DTO
  * PermissionGroupDefinitionDto - permission group definition DTO
  * PermissionDefinitionDto - permission definition DTO
  * Support permission provider configuration (Role, User, OrganizationUnit, etc.)
* Permission error code definition
  * 001100 - Permission group already exists
  * 001010 - Static permission groups do not allow modification
  * 001404 - Permission group does not exist
  * 002100 - Permission already exists
  * 002010 - Static permissions do not allow modification
  * 002101 - Unable to obtain permissions for group definition
  * 002404 - Permission does not exist

## Module reference

```csharp
[DependsOn(
    typeof(AbpPermissionManagementApplicationContractsModule))]
public class YouProjectModule : AbpModule
{
    // other
}
```

## Permission definition

```csharp
public class YourPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var permissionGroup = context.AddGroup(
            "YourPermissionGroup",
            "Your Permission Group");

        var permission = permissionGroup.AddPermission(
            "YourPermission",
            "Your Permission",
            MultiTenancySides.Both);

        permission.AddChild(
            "Create",
            "Create Permission");
    }
}
```

## See also

* [LINGYUN.Abp.PermissionManagement.Application](../LINGYUN.Abp.PermissionManagement.Application/README.md)
* [LINGYUN.Abp.PermissionManagement.Domain.OrganizationUnits](../LINGYUN.Abp.PermissionManagement.Domain.OrganizationUnits/README.md)
* [LINGYUN.Abp.PermissionManagement.HttpApi](../LINGYUN.Abp.PermissionManagement.HttpApi/README.md)
