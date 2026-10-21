# LINGYUN.Abp.Saas.EntityFrameworkCore

The SaaS EntityFrameworkCore module implements the data access layer for tenants and versions.

## Features

* Entity mapping configuration
  * Tenant entity mapping
  * Version entity mapping
  * Tenant connection string entity mapping

* Warehousing implementation
  * EfCoreTenantRepository: tenant storage implementation
  * EfCoreEditionRepository: version storage implementation

* Database table
  * AbpEditions: version table
  * AbpTenants: tenant table
  * AbpTenantConnectionStrings: Tenant connection string table

## Configuration instructions

The database table prefix and Schema can be modified through configuration:

```json
{
  "AbpSaas": {
    "EntityFrameworkCore": {
      "TablePrefix": "Abp", // Database table prefix
      "Schema": null // Database Schema
    }
  }
}
```

## More

[English](README.EN.md)
