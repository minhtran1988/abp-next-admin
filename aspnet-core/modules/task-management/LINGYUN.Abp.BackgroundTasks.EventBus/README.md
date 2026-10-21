# LINGYUN.Abp.BackgroundTasks.EventBus

Background task (queue) module distributed event module, integrated module enables applications to process job events

## Configuration usage

Module reference on demand

```csharp
[DependsOn(typeof(AbpBackgroundTasksEventBusModule))]
public class YouProjectModule : AbpModule
{
  // other
}
```
