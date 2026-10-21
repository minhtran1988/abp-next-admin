# LINGYUN.Abp.BackgroundTasks.Quartz

Quartz implementation of background task (queue) module.
And add a listener to notify the manager of the task status

## Configuration usage

The module is referenced on demand. For specific configuration, please refer to the Volo.Abp.Quartz module.

```csharp
[DependsOn(typeof(AbpBackgroundTasksQuartzModule))]
public class YouProjectModule : AbpModule
{
  // other
}
```
