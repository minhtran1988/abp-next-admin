# LINGYUN.Abp.BackgroundTasks.DistributedLocking

Background task (queue) module distributed lock module

See: [Distributed-Locking](https://docs.abp.io/en/abp/latest/Distributed-Locking)

## Configuration usage

Module reference on demand

```csharp
[DependsOn(typeof(AbpBackgroundTasksDistributedLockingModule))]
public class YouProjectModule : AbpModule
{
  // other
}
```
