# LINGYUN.Abp.BackgroundTasks.ExceptionHandling

Implementation of background job execution exception notification. By default, email is used to send notifications.

## Configuration usage

Module reference on demand

```csharp
[DependsOn(typeof(AbpBackgroundTasksExceptionHandlingModule))]
public class YouProjectModule : AbpModule
{
  // other
}
```

## Action parameters

** Specify the following parameters in the parameters when defining the job, and a notification will be sent when the job execution fails.

* to required, recipient email address
* from optional, email header sender name
* body optional, email content (required parameter if template name is not specified)
* subject optional, email title
* template optional, email template
* context optional, context parameter when using templates
* culture optional, template culture when using templates
