# LINGYUN.Abp.RulesManagement

The rule engine management module provides workflow and rule management functions based on the rule engine.

## 1. Module description

This module is implemented based on RulesEngine and provides persistence management, cache management and API interface of workflow rules. Mainly includes the following sub-modules:

### 1.1 Domain.Shared

* Defines basic constants such as workflow records, rule records, action records, etc.
* Provides enumeration definitions such as action types
* [View detailed description](rules-engine/LINGYUN.Abp.RulesEngineManagement.Domain.Shared/README.md)

### 1.2 Domain

* Implemented the core business logic of workflow storage
* Provides workflow storage implementation based on memory cache
* Defines domain models such as rule records and workflow records
* [View detailed description](rules-engine/LINGYUN.Abp.RulesEngineManagement.Domain/README.md)

### 1.3 EntityFrameworkCore

* Provides data access layer implementation based on EF Core
* Implemented database mapping of workflow, rules and other entities
* Implemented warehousing interface
* [View detailed description](rules-engine/LINGYUN.Abp.RulesEngineManagement.EntityFrameworkCore/README.md)

### 1.4 Application.Contracts

* Defines the application service interface managed by the rule engine
* Defines data transfer objects (DTOs)
*Permissions defined
* [View detailed description](rules-engine/LINGYUN.Abp.RulesEngineManagement.Application.Contracts/README.md)

### 1.5 Application

* Implemented application services for rule recording and workflow recording
* Provides automatic object mapping configuration
* [View detailed description](rules-engine/LINGYUN.Abp.RulesEngineManagement.Application/README.md)

### 1.6 HttpApi

* Provides RESTful style API interface
* Implemented CRUD operation API for rules and workflow
* [View detailed description](rules-engine/LINGYUN.Abp.RulesEngineManagement.HttpApi/README.md)

## 2. Features

* Workflow management
  * Support workflow creation, modification, deletion, and query
  * Provide workflow cache management
  * Support query workflow by type
  
* Rule management
  * Support the creation, modification, deletion and query of rules
  * Support the definition of rule expressions
  * Supports the association of rules and workflow
  
* Parameter management
  * Support the definition of workflow parameters
  * Support parameter value verification
  
* Action management
  * Support the definition of success/failure actions
  * Supports recording of action execution results

## 3. Configuration items

### 3.1 Workflow recording configuration

* MaxNameLength: Maximum length of workflow name, default 64
* MaxTypeFullNameLength: The maximum length of the full name of the type, default 255
* MaxInjectWorkflowsLength: Maximum length of injected workflow, default (MaxNameLength + 1) * 5

### 3.2 Rule record configuration

* MaxNameLength: Maximum length of rule name, default 64
* MaxOperatorLength: maximum length of operator, default 30
* MaxErrorMessageLength: Maximum error message length, default 255
* MaxExpressionLength: Maximum length of expression, default int.MaxValue
* MaxSuccessEventLength: Maximum length of success event, default 128

## 4. Permissions

* RulesEngineManagement.Rule
  *Rule management authority
  *Includes creation, modification, deletion, and query permissions
  
* RulesEngineManagement.Workflow
  * Workflow management permissions
  *Includes creation, modification, deletion, and query permissions

## 5. API interface

### 5.1 Rule Record API

* Base path: api/rules-engine-management/rules
* Provide CRUD operation API for rule records
* Support paging query API

### 5.2 Workflow Recording API

* Base path: api/rules-engine-management/workflows
* Provide CRUD operation API for workflow records
* Support paging query API

## 6. Dependencies

* Volo.Abp.Core
* LINGYUN.Abp.Rules.RulesEngine
* Microsoft.Extensions.Caching.Memory
* Volo.Abp.EntityFrameworkCore
* Volo.Abp.Ddd.Application
* Volo.Abp.AspNetCore.Mvc