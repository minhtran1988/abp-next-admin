# LINGYUN.Abp.BackgroundTasks.Activities

Background task (queue) module behavior processing module

## Interface parameters

* IJobActionStore implements this interface to obtain job management behavior
* JobActionDefinitionProvider implements this interface to customize job behavior
* JobExecutedProvider implements this interface to extend job behavior

## Configuration usage

Module reference on demand

```csharp
[DependsOn(typeof(AbpBackgroundTasksActivitiesModule))]
public class YouProjectModule : AbpModule
{
  // other
}
```
