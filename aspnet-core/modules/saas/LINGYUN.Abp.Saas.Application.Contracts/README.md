# LINGYUN.Abp.Saas.Application.Contracts

The SaaS application service contract module defines the application service interface, DTO object and permission definition for tenant and version management.

## Features

* Tenant management interface (ITenantAppService)
  * Defines all service interfaces managed by tenants
  * Contains tenant-related DTO object definitions
  * Tenant connection string management interface

* Version management interface (IEditionAppService)
  * Defines all service interfaces for version management
  * Contains version-related DTO object definitions

* Permission definition (AbpSaasPermissions)
  * Version management permissions
  * Tenant management rights
  * Function management permissions
  * Connection string management permissions

*DTO object
  * EditionCreateDto/EditionUpdateDto
  * TenantCreateDto/TenantUpdateDto
  * TenantConnectionStringCreateDto/TenantConnectionStringUpdateDto

## More

[English](README.EN.md)
