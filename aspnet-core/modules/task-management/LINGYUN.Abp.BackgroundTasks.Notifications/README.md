# LINGYUN.Abp.BackgroundTasks.Notifications

Notification events after background job execution

## Configuration usage

Module reference on demand

```csharp
[DependsOn(typeof(AbpBackgroundTasksNotificationsModule))]
public class YouProjectModule : AbpModule
{
  // other
}
```

## Action parameters

** Notification content formatting parameters, real-time notification will be sent after the job

* push-provider optional, user-specified message push provider;
* use-template optional, the name of the template used to format notification content
* content optional, notification content (required parameter if template name is not specified)
* culture optional, template culture when using templates
