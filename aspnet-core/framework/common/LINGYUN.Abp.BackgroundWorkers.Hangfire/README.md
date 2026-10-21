# LINGYUN.Abp.BackgroundWorkers.Hangfire

[English](README.EN.md) | Simplified Chinese

## 1. Introduction

`LINGYUN.Abp.BackgroundWorkers.Hangfire` is an ABP background worker module implemented based on Hangfire. It provides a reliable way to manage and execute long-running background tasks, supporting functions such as automatic start, stop, and periodic execution.

## 2. Features

* Support automatic starting and stopping of background workers
*Supports background tasks that are executed regularly
* Seamless integration with ABP background worker system
* Support worker status management
* Support dependency injection

## 3. Installation

```bash
dotnet add package LINGYUN.Abp.BackgroundWorkers.Hangfire
```

## 4. How to use

1. Add `AbpBackgroundWorkersHangfireModule` to module dependencies:

```csharp
[DependsOn(typeof(AbpBackgroundWorkersHangfireModule))]
public class YourModule : AbpModule
{
}
```

2. Create a background worker:

```csharp
public class YourBackgroundWorker : AsyncPeriodicBackgroundWorkerBase
{
    public YourBackgroundWorker(
        AbpAsyncTimer timer,
        IServiceScopeFactory serviceScopeFactory) 
        : base(timer, serviceScopeFactory)
    {
        Timer.Period = 5000; // Execute every 5 seconds
    }

    protected override async Task DoWorkAsync(PeriodicBackgroundWorkerContext workerContext)
    {
        //Implement your background task logic here
        await Task.CompletedTask;
    }
}
```

3. Register background worker:

```csharp
public override void ConfigureServices(ServiceConfigurationContext context)
{
    context.Services.AddTransient<YourBackgroundWorker>();
    Configure<AbpBackgroundWorkerOptions>(options =>
    {
        options.IsEnabled = true; // Enable background workers
    });
}
```

## 5. Configuration items

### 5.1 Worker configuration

* `AbpBackgroundWorkerOptions.IsEnabled`: Whether to enable background workers. Default value: `true`

## 6. Dependencies

* Volo.Abp.BackgroundWorkers
* LINGYUN.Abp.BackgroundJobs.Hangfire

## 7. Documentation and Resources

* [Hangfire official documentation](https://docs.hangfire.io/)
* [ABP Framework Documentation](https://docs.abp.io/)
