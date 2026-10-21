# LINGYUN.Abp.BackgroundJobs.Hangfire

[English](README.EN.md) | Simplified Chinese

## 1. Introduction

`LINGYUN.Abp.BackgroundJobs.Hangfire` is an ABP background job module implemented based on [Hangfire](https://www.hangfire.io/). It provides a reliable background job execution framework, supporting the execution of immediate tasks, delayed tasks and periodic tasks.

## 2. Features

*Support instant task execution
* Support delayed task execution
* Support periodic tasks (using Cron expressions)
* Seamless integration with ABP background operating system
* Support job execution status tracking
* Support job retry mechanism

## 3. Installation

```bash
dotnet add package LINGYUN.Abp.BackgroundJobs.Hangfire
```

## 4. How to use

1. Add `AbpBackgroundJobsHangfireModule` to module dependencies:

```csharp
[DependsOn(typeof(AbpBackgroundJobsHangfireModule))]
public class YourModule : AbpModule
{
}
```

2. Configure background jobs:

```csharp
public override void ConfigureServices(ServiceConfigurationContext context)
{
    Configure<AbpBackgroundJobOptions>(options =>
    {
        options.IsJobExecutionEnabled = true; // Enable job execution
    });
}
```

3. Use background jobs:

```csharp
public class YourService
{
    private readonly IBackgroundJobManager _backgroundJobManager;

    public YourService(IBackgroundJobManager backgroundJobManager)
    {
        _backgroundJobManager = backgroundJobManager;
    }

    public async Task CreateJobAsync()
    {
        //Create immediate task
        await _backgroundJobManager.EnqueueAsync(new YourArgs());

        //Create a delayed task
        await _backgroundJobManager.EnqueueAsync(
            new YourArgs(),
            delay: TimeSpan.FromMinutes(5)
        );

        //Create periodic tasks
        await _backgroundJobManager.EnqueueAsync(
            "0 0 * * *", // Cron expression: executed at 0 o'clock every day
            new YourArgs()
        );
    }
}
```

## 5. Configuration items

### 5.1 Job configuration

* `AbpBackgroundJobOptions.IsJobExecutionEnabled`: Whether to enable job execution. Default value: `true`

## 6. Dependencies

* Volo.Abp.BackgroundJobs
* Hangfire.Core
* Hangfire.AspNetCore

## 7. Documentation and Resources

* [Hangfire official documentation](https://docs.hangfire.io/)
* [ABP Framework Documentation](https://docs.abp.io/)
