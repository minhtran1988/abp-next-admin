# LINGYUN.Abp.RulesEngineManagement.EntityFrameworkCore

## 1. Introduction

The EntityFrameworkCore implementation of the rule engine management module provides a data access layer implementation based on EF Core.

## 2. Function

* Implemented the DbContext of the rule engine management module
* Provides database mapping configuration for entities
* Implemented warehousing interface

## 3. Database implementation

### 3.1 DbContext

* RulesEngineManagementDbContext
  * Implemented the IRulesEngineManagementDbContext interface
  * Contains DbSet definitions for all entities
  *Support multi-tenancy

### 3.2 Warehousing implementation

* EfCoreRuleRecordRepository
  * Implemented the IRuleRecordRepository interface
  * Provide CRUD operations for rule records
  * Support querying rules by name
  
* EfCoreWorkflowRecordRepository
  * Implemented the IWorkflowRecordRepository interface
  * Provide CRUD operations for workflow records
  * Support querying workflows by name and type

### 3.3 Entity mapping

* Workflow record mapping
  * Configured primary keys and indexes
  * Configured field length limit
  * Configured associations
  
* Rule record mapping
  * Configured primary keys and indexes
  * Configured field length limit
  * Configured associations
  
* Parameter record mapping
  * Configured primary key
  * Configured field length limit
  
* Action record mapping
  * Configured primary key
  * Configured field length limit

## 4. Dependencies

* Volo.Abp.EntityFrameworkCore
* LINGYUN.Abp.RulesEngineManagement.Domain

[Click to view English document](README.EN.md)
