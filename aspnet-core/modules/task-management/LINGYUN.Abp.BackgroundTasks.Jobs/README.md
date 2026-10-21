# LINGYUN.Abp.BackgroundTasks.Jobs

Common job modules for background tasks (queues)

## Job list

* [ConsoleJob](./LINGYUN/Abp/BackgroundTasks/Jobs/ConsoleJob): Console output
* [HttpRequestJob](./LINGYUN/Abp/BackgroundTasks/Jobs/HttpRequestJob): Http request
* [SendEmailJob](./LINGYUN/Abp/BackgroundTasks/Jobs/SendEmailJob): Send email
* [SendSmsJob](./LINGYUN/Abp/BackgroundTasks/Jobs/SendSmsJob): Send SMS
* [ServiceInvocationJob](./LINGYUN/Abp/BackgroundTasks/Jobs/ServiceInvocationJob): Inter-service invocation (extension of HTTP request)
* [SleepJob](./LINGYUN/Abp/BackgroundTasks/Jobs/SleepJob): Sleep to delay job execution

## Configuration usage

Module reference on demand

```csharp
[DependsOn(typeof(AbpBackgroundTasksJobsModule))]
public class YouProjectModule : AbpModule
{
  // other
}
```
