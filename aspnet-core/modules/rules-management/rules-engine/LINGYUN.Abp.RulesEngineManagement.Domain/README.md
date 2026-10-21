# LINGYUN.Abp.RulesEngineManagement.Domain

## 1. Introduction

The domain layer of the rule engine management module implements the core business logic of the rule engine, including workflow storage, rule recording, action recording and other functions.

## 2. Function

* Workflow storage
  * Supports workflow storage based on memory cache
  * Provide workflow acquisition and query functions
  * Supports obtaining workflow list by type
  
* Rule record
  * Define rules to record entities
  * Provide rule record warehousing interface
  * Support CRUD operations of rules
  
* Workflow rule records
  * Define workflow rules to record entities
  * Supports the association of workflow and rules
  
* Parameter record
  * Define parameter record entities
  * Support management of workflow parameters
  
* Action recording
  * Define action record entities
  * Supports recording of successful/failed actions

## 3. Domain services

* WorkflowStore
  * Implemented the IWorkflowStore interface
  * Provide workflow cache management
  * Support workflow query and mapping

## 4. Warehousing interface

* IRuleRecordRepository
  * Provide CRUD operations for rule records
  * Support querying rules by name
  
* IWorkflowRecordRepository
  * Provide CRUD operations for workflow records
  * Support querying workflows by name and type

## 5. Dependencies

* Volo.Abp.Domain
* LINGYUN.Abp.Rules.RulesEngine
* Microsoft.Extensions.Caching.Memory

[Click to view English document](README.EN.md)
