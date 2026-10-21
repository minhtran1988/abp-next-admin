# LINGYUN.Abp.Saas.Application

The SaaS application service layer module implements the application service interface for tenant and version management.

## Features

* Tenant management service (TenantAppService)
  * Create tenant
  * Update tenant
  * Delete tenant
  * Get tenant list
  * Get tenant details
  * Manage tenant connection strings
  * Manage tenant functions

* Version management service (EditionAppService)
  * Create version
  *Updated version
  * delete version
  * Get version list
  * Get version details
  *Manage version function

## Permission verification

Corresponding permission verification has been added to all application service methods to ensure that only users with corresponding permissions can access.

## Object mapping

The following object mapping is implemented using AutoMapper:
* Edition <-> EditionDto
* Tenant <-> TenantDto
* TenantConnectionString <-> TenantConnectionStringDto

## More

[English](README.EN.md)
