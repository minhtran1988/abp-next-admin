# LINGYUN.Abp.RulesEngineManagement.Domain.Shared

## 1. Introduction

The shared domain layer of the rule engine management module includes constant definitions, enumeration types and other infrastructure required for rule engine management.

## 2. Function

* Defines constants related to workflow records
* Defines constants related to rule records
* Defines constants related to action recording
* Defines constants related to parameter recording
* Defines action type enumeration

## 3. Constant configuration

### 3.1 Workflow record constants

* MaxNameLength: Maximum length of workflow name, default 64
* MaxTypeFullNameLength: The maximum length of the full name of the type, default 255
* MaxInjectWorkflowsLength: The maximum length of the injected workflow, the default is (MaxNameLength + 1) * 5

### 3.2 Rule record constants

* MaxNameLength: Maximum length of rule name, default 64
* MaxOperatorLength: maximum length of operator, default 30
* MaxErrorMessageLength: Maximum error message length, default 255
* MaxInjectWorkflowsLength: The maximum length of the injected workflow, the default is (MaxNameLength + 1) * 5
* MaxExpressionLength: Maximum length of expression, default is int.MaxValue
* MaxSuccessEventLength: Maximum length of success event, default 128

### 3.3 Action type enumeration

* Success = 0: Success
* Failure = 1: Failure

## 4. Dependencies

* Volo.Abp.Validation
* LINGYUN.Abp.Rules.RulesEngine

[Click to view English document](README.EN.md)
