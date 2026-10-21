# LINGYUN.Abp.OpenIddict.Application

[![ABP version](https://img.shields.io/badge/dynamic/xml?style=flat-square&color=yellow&label=abp&query=%2F%2FProject%2FPropertyGroup%2FAbpVersion&url=https%3A%2F%2Fraw.githubusercontent.com%2Fcolinin%2Fabp-next-admin%2Fmaster%2Faspnet-core%2Fmodules%2FopenIddict%2FLINGYUN.Abp.OpenIddict.Application%2FLINGYUN.Abp.OpenIddict.Application.csproj)](https://abp.io)
[![NuGet](https://img.shields.io/nuget/v/LINGYUN.Abp.OpenIddict.Application.svg?style=flat-square)](https://www.nuget.org/packages/LINGYUN.Abp.OpenIddict.Application)

## Introduction

`LINGYUN.Abp.OpenIddict.Application` is an application service layer implementation based on ABP OpenIddict, which provides management functions for OpenIddict clients, authorization, tokens and scopes.

[English](./README.EN.md)

## Features

* OpenIddict client application management
  * Create, update, delete client applications
  * Manage basic client information (client ID, key, name, etc.)
  * Configure authorization type, response type, scope and redirect URI
  * Support custom configuration of client properties and requirements

* OpenIddict authorization management
  * Query and delete authorization records
  * Filter authorization based on client ID, creation time, status, etc.

* OpenIddict token management
  * Query and delete tokens
  * Filter tokens by client ID, creation time, expiration time, status, etc.

* OpenIddict scope management
  * Create, update, delete scopes
  * Management scope basic information (name, description, display name, etc.)
  *Support multi-language display name and description
  * Configure resources associated with the scope

## Installation

```bash
dotnet add package LINGYUN.Abp.OpenIddict.Application
```

## Use

1. Add `[DependsOn(typeof(AbpOpenIddictApplicationModule))]` to your module class.

2. Configure permissions:

The module comes with the following permissions predefined:

* OpenIddict.Applications: Client application management
  * OpenIddict.Applications.Create: Create client
  * OpenIddict.Applications.Update: Update client
  * OpenIddict.Applications.Delete: Delete the client
* OpenIddict.Authorizations: Authorization management
  * OpenIddict.Authorizations.Delete: Delete authorization
* OpenIddict.Scopes: scope management
  * OpenIddict.Scopes.Create: Create a scope
  * OpenIddict.Scopes.Update: Update scope
  * OpenIddict.Scopes.Delete: delete scope
* OpenIddict.Tokens: Token management
  * OpenIddict.Tokens.Delete: Delete tokens

## Notes

* The ClientId of the client application cannot be modified after creation.
* Deleting the client application will also delete related authorizations and tokens
* The scope name cannot be modified after creation
