# LINGYUN.Abp.RulesEngineManagement.Application.Contracts

## 1. Introduction

The application service contract layer of the rule engine management module defines the interfaces, DTOs, etc. required for rule engine management.

## 2. Function

* Defines the application service interface managed by the rule engine
* Defines data transfer objects (DTOs)
*Permissions defined

## 3. Application service interface

* IRuleRecordAppService
  * Provide CRUD operation interface for rule records
  * Support querying rules by name
  * Support paging query
  
* IWorkflowRecordAppService
  * Provide CRUD operation interface for workflow records
  * Support querying workflows by name and type
  * Support paging query

## 4. Data transfer object

### 4.1 Rule record DTOs

* RuleRecordDto
* CreateRuleRecordDto
* UpdateRuleRecordDto
* RuleRecordGetListInput

### 4.2 Workflow record DTOs

* WorkflowRecordDto
* CreateWorkflowRecordDto
* UpdateWorkflowRecordDto
* WorkflowRecordGetListInput

### 4.3 Parameter record DTOs

* ParamRecordDto
* CreateParamRecordDto
* UpdateParamRecordDto

### 4.4 Action Record DTOs

* ActionRecordDto
* CreateActionRecordDto
* UpdateActionRecordDto

## 5. Permission definition

* RulesEngineManagement.Rule
  *Rule management authority
  *Includes creation, modification, deletion, and query permissions
  
* RulesEngineManagement.Workflow
  * Workflow management permissions
  *Includes creation, modification, deletion, and query permissions

## 6. Dependencies

* Volo.Abp.Ddd.Application.Contracts
* LINGYUN.Abp.RulesEngineManagement.Domain.Shared

[Click to view English document](README.EN.md)
