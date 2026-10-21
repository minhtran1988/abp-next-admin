# LINGYUN.Abp.Saas.Domain

The SaaS domain layer module defines the core domain model, warehousing interface and domain services of tenants and versions.

## Core functions

* Tenant: Contains tenant basic information, status, expiration time, etc.
* Edition entity (Edition): contains basic version information
* Tenant connection string (TenantConnectionString): manages tenant database connection
* Tenant Manager (TenantManager): handles tenant-related business logic
* Edition Manager (EditionManager): handles version-related business logic
* Data seed (EditionDataSeeder): Provides default version data initialization

## Domain events

* Tenant creation, update, and deletion events
* Version creation, update, and deletion events
* Tenant connection string change event

## Cache management

* Tenant cache: cache tenant information to improve query performance
* Version cache: cache version information to improve query performance

## More

[English](README.EN.md)
