# LINGYUN.Abp.PermissionManagement.HttpApi

The permission management HTTP API module provides a RESTful API interface for permission management.

## Features

*Permission group definition API
  * Provide CRUD operation interface for permission groups
  * Support permission group paging query
* Permission definition API
  * Provide CRUD operation interface for permissions
  *Support permission paging query
* Unified API base class
  * PermissionManagementControllerBase - Permission management controller base class
  * Standardized API response format

## Module reference

```csharp
[DependsOn(
    typeof(AbpPermissionManagementApplicationContractsModule),
    typeof(AbpPermissionManagementHttpApiModule))]
public class YouProjectModule : AbpModule
{
    // other
}
```

## API interface

1. Permission group definition
```
GET    /api/permission-management/groups
POST   /api/permission-management/groups
PUT    /api/permission-management/groups/{name}
DELETE /api/permission-management/groups/{name}
```

2. Permission definition
```
GET    /api/permission-management/permissions
POST   /api/permission-management/permissions
PUT    /api/permission-management/permissions/{name}
DELETE /api/permission-management/permissions/{name}
```

## Basic usage

1. Permission group definition management
```http
### Create permission group
POST /api/permission-management/groups
{
    "name": "MyPermissionGroup",
    "displayName": "My Permission Group"
}

### Update permission group
PUT /api/permission-management/groups/MyPermissionGroup
{
    "displayName": "Updated Permission Group"
}
```

2. Permission definition management
```http
### Create permissions
POST /api/permission-management/permissions
{
    "groupName": "MyPermissionGroup",
    "name": "MyPermission",
    "displayName": "My Permission",
    "providers": ["Role", "User"]
}

### Update permissions
PUT /api/permission-management/permissions/MyPermission
{
    "displayName": "Updated Permission"
}
```

## See also

* [LINGYUN.Abp.PermissionManagement.Application](../LINGYUN.Abp.PermissionManagement.Application/README.md)
* [LINGYUN.Abp.PermissionManagement.Application.Contracts](../LINGYUN.Abp.PermissionManagement.Application.Contracts/README.md)
* [LINGYUN.Abp.PermissionManagement.Domain.OrganizationUnits](../LINGYUN.Abp.PermissionManagement.Domain.OrganizationUnits/README.md)
