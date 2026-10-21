# LINGYUN.Abp.MicroService.TaskService.EntityFrameworkCore

The task management data migration module provides database migration functions related to the task management system.

[English](./README.EN.md)

## Features

* Integrated SaaS multi-tenant data migration
* Integrated task management data migration
* Integrated settings to manage data migration
* Integrated rights management data migration
* Integrated features to manage data migration
* Support MySQL database

## Module dependencies

```csharp
[DependsOn(
    typeof(AbpSaasEntityFrameworkCoreModule),
    typeof(TaskManagementEntityFrameworkCoreModule),
    typeof(AbpEntityFrameworkCoreMySQLModule),
    typeof(AbpSettingManagementEntityFrameworkCoreModule),
    typeof(AbpPermissionManagementEntityFrameworkCoreModule),
    typeof(AbpFeatureManagementEntityFrameworkCoreModule),
    typeof(AbpDataDbMigratorModule)
)]
```

## Configuration items

```json
{
  "ConnectionStrings": {
    "TaskManagementDbMigrator": "Your database connection string"
  }
}
```

## Basic usage

1. Configure database connection string
   * Configure the TaskManagementDbMigrator connection string in appsettings.json

2. Add module dependencies
   ```csharp
   [DependsOn(typeof(TaskManagementMigrationsEntityFrameworkCoreModule))]
   public class YourModule : AbpModule
   {
       // ...
   }
   ```

## Database table description

* AbpTasks - Task basic information table
* AbpTaskCategories - task classification table
* AbpTaskStatuses - task status table
* AbpTaskAssignments - task assignment table
* Saas related tables - multi-tenant related data such as tenants and versions
* AbpSettings - system setting data
* AbpPermissionGrants - permission authorization data
* AbpFeatures - Functional feature data

## More information

* [ABP Documentation](https://docs.abp.io)
* [ABP task management module](https://github.com/colinin/abp-next-admin/tree/master/aspnet-core/modules/task-management)
