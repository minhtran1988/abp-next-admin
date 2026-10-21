# LINGYUN.Abp.BackgroundTasks.Abstractions

Background task (queue) module abstraction layer, defining some basic structures and interfaces

## Feature parameters

* DisableJobActionAttribute marks this attribute as not handling post-job triggering behavior
* DisableJobStatusAttribute marks that this attribute does not handle job status
* DisableAuditingAttribute marks this attribute as not recording job logs

## Configuration usage

Module reference on demand

```csharp
[DependsOn(typeof(AbpBackgroundTasksAbstractionsModule))]
public class YouProjectModule : AbpModule
{
  // other
}
```
