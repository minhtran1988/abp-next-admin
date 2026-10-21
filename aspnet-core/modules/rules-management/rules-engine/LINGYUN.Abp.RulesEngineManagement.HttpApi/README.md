# LINGYUN.Abp.RulesEngineManagement.HttpApi

## 1. Introduction

The HTTP API layer of the rule engine management module provides a RESTful-based API interface.

## 2. Function

* Implemented the HTTP API interface for rule recording
* Implemented the HTTP API interface for workflow recording
* Provides routing configuration for API interface

## 3. API interface

### 3.1 Rule recording API

* RuleRecordController
  * Base path: api/rules-engine-management/rules
  * Provide CRUD operation API for rule records
  * Support paging query API
  * Implemented permission verification

### 3.2 Workflow Recording API

* WorkflowRecordController
  * Base path: api/rules-engine-management/workflows
  * Provide CRUD operation API for workflow records
  * Support paging query API
  * Implemented permission verification

## 4. Dependencies

* Volo.Abp.AspNetCore.Mvc
* LINGYUN.Abp.RulesEngineManagement.Application.Contracts

[Click to view English document](README.EN.md)
