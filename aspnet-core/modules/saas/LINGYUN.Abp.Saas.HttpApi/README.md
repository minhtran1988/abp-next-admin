# LINGYUN.Abp.Saas.HttpApi

The SaaS HTTP API module implements the HTTP API interface for tenant and version management.

## Features

* Tenant management Controller (TenantController)
  * GET /api/saas/tenants: Get the tenant list
  * GET /api/saas/tenants/{id}: Get tenant details
  * POST /api/saas/tenants: Create tenants
  * PUT /api/saas/tenants/{id}: update tenant
  * DELETE /api/saas/tenants/{id}: delete tenant
  * GET /api/saas/tenants/{id}/connection-strings: Get the tenant connection string
  * PUT /api/saas/tenants/{id}/connection-strings: Update tenant connection strings
  * DELETE /api/saas/tenants/{id}/connection-strings/{name}: Delete tenant connection string

* Version management Controller (EditionController)
  * GET /api/saas/editions: Get the version list
  * GET /api/saas/editions/{id}: Get version details
  * POST /api/saas/editions: Create version
  * PUT /api/saas/editions/{id}: updated version
  * DELETE /api/saas/editions/{id}: delete edition

## API documentation

All API interfaces support Swagger documentation. After starting the application, you can view detailed API documentation through the Swagger UI.

## More

[English](README.EN.md)
