# LINGYUN.Abp.MultiTenancy.Saas

The multi-tenant SaaS management module provides tenant management, version management and other functions.

## Features

* Tenant management: create, edit, delete tenants, manage tenant connection strings
* Version management: create, edit, delete versions, and manage version functions
* Tenant expiration management: supports tenant expiration time setting, expiration warning, expired resource recycling
* Tenant function management: supports assigning function permissions to tenants

## Configuration usage

### Module configuration

```json
{
  "AbpSaas": {
    "Tenants": {
      "RecycleStrategy": "1", // Resource recycling strategy: 0-retain, 1-recycle
      "ExpirationReminderDays": "15", // Number of expiration warning days, range 1-30 days
      "ExpiredRecoveryTime": "15" // Expired recovery time, range 1-30 days
    }
  }
}
```

### Permission configuration

* AbpSaas.Editions
  * AbpSaas.Editions.Create: Create version
  * AbpSaas.Editions.Update: updated version
  *AbpSaas.Editions.Delete: Delete editions
  * AbpSaas.Editions.ManageFeatures: Manage version functions

* AbpSaas.Tenants
  * AbpSaas.Tenants.Create: Create tenants
  * AbpSaas.Tenants.Update: Update tenants
  *AbpSaas.Tenants.Delete: delete tenant
  * AbpSaas.Tenants.ManageFeatures: Manage tenant functions
  * AbpSaas.Tenants.ManageConnectionStrings: Manage tenant connection strings

## More

[English](README.EN.md)
