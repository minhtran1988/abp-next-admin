# LINGYUN.Abp.RulesEngineManagement.Application

## 1. Introduction

The application service implementation layer of the rule engine management module implements the business logic of rule engine management.

## 2. Function

* Implemented application services for rule recording
* Implemented application services for workflow recording
* Provides automatic mapping configuration managed by rule engine

## 3. Application service implementation

### 3.1 Rule Record Application Service

* RuleRecordAppService
  * Implemented the IRuleRecordAppService interface
  * Provide CRUD operations for rule records
  * Implemented the query function of rule records
  * Support paging query
  * Implemented permission verification

### 3.2 Workflow recording application service

* WorkflowRecordAppService
  * Implemented the IWorkflowRecordAppService interface
  * Provide CRUD operations for workflow records
  * Implemented the query function of workflow records
  * Support paging query
  * Implemented permission verification

## 4. Object mapping

* RulesEngineManagementApplicationAutoMapperProfile
  * Configured automatic mapping between DTO and entities
  * Contains all mapping configurations related to rule engine management

## 5. Dependencies

* Volo.Abp.AutoMapper
* Volo.Abp.Ddd.Application
* LINGYUN.Abp.RulesEngineManagement.Application.Contracts
* LINGYUN.Abp.RulesEngineManagement.Domain

[Click to view English document](README.EN.md)
