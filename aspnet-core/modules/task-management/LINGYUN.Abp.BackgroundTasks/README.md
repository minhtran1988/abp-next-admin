# LINGYUN.Abp.BackgroundTasks

Background task (queue) module, the background jobs and background workers provided by Abp do not support Cron expressions, and provide manageable background task (queue) function.

Implemented **Volo.Abp.BackgroundJobs.IBackgroundJobManager**, which means you can also add new jobs through the framework background job interface.
Implemented **Volo.Abp.BackgroundWorkers.IBackgroundWorkerManager**, which means you can also add new jobs through the framework background worker interface.

## Task category

* JobType.Once: One-time task, this type will only be executed once, suitable for scenarios such as email notifications
* JobType.Period: Periodic tasks. This type of tasks will determine the running method based on Cron expressions. It is suitable for scenarios such as report analysis.
* JobType.Persistent: Persistent tasks. This type of tasks runs according to a given number of repetitions and repetition intervals. It is suitable for scenarios such as interface stress testing.

## Interface description

* [IJobPublisher](/LINGYUN/Abp/BackgroundTasks/IJobPublisher.cs): Job publishing interface, publishes the specified job to the current node
* [IJobDispatcher](/LINGYUN/Abp/BackgroundTasks/IJobDispatcher.cs): Job scheduling interface, schedules the specified job to the specified node
* [IJobScheduler](/LINGYUN/Abp/BackgroundTasks/IJobScheduler.cs): Scheduler interface, manages the current running node job scheduler
* [IJobLockProvider](/LINGYUN/Abp/BackgroundTasks/IJobLockProvider.cs): Job lock interface, specify job lock to prevent repeated running, for the lock duration, see job **LockTimeOut**
* [IJobEventTrigger](/LINGYUN/Abp/BackgroundTasks/IJobEventTrigger.cs): Job event trigger interface, job listening interface before and after running
* [IJobStore](/LINGYUN/Abp/BackgroundTasks/IJobStore.cs): Job persistence interface

## Configuration usage

Module reference on demand

```csharp
[DependsOn(typeof(AbpBackgroundTasksModule))]
public class YouProjectModule : AbpModule
{
  // other
}
```

```csharp
public class DemoClass
{
    protected IServiceProvider ServiceProvider { get; }

    public DemoClass(IServiceProvider serviceProvider)
    {
        ServiceProvider = serviceProvider;
    }

    public async Task Some()
    {
        var scheduler = ServiceProvider.GetRequiredService<IJobScheduler>();

        //Add periodic (every 5 seconds) tasks to the queue
        await scheduler.QueueAsync(new JobInfo
        {
            Type = typeof(ConsoleJob).AssemblyQualifiedName,
            Args = new Dictionary<string, object>(),
            Name = "Test-Console-Period",
            Group = "Test",
            Description = "Test-Console",
            Id = Guid.NewGuid(),
            JobType = JobType.Period,
            Priority = Volo.Abp.BackgroundJobs.BackgroundJobPriority.Low,
            Cron = "0/5 * * * * ? ",
            TryCount = 10,
            Status = JobStatus.Running,
            //Define this field to handle concurrency
            LockTimeOut = 120,
        });

        // Add a one-time task to the queue, which will be executed after 10 (Interval) seconds
        await scheduler.QueueAsync(new JobInfo
        {
            Type = typeof(ConsoleJob).AssemblyQualifiedName,
            Args = new Dictionary<string, object>(),
            Name = "Test-Console-Once",
            Group = "Test",
            Description = "Test-Console",
            Id = Guid.NewGuid(),
            JobType = JobType.Once,
            Priority = Volo.Abp.BackgroundJobs.BackgroundJobPriority.Low,
            Interval = 10,
            TryCount = 10,
            Status = JobStatus.Running,
        });

        //Add the persistent task to the queue, which will be executed after 10 (Interval) seconds, with a maximum execution of 5 (MaxCount) times
        await scheduler.QueueAsync(new JobInfo
        {
            Type = typeof(ConsoleJob).AssemblyQualifiedName,
            Args = new Dictionary<string, object>(),
            Name = "Test-Console-Persistent",
            Group = "Test",
            Description = "Test-Console",
            Id = Guid.NewGuid(),
            JobType = JobType.Persistent,
            Priority = Volo.Abp.BackgroundJobs.BackgroundJobPriority.Low,
            Interval = 10,
            TryCount = 10,
            MaxCount = 5,
            Status = JobStatus.Running,
        });

        // You can also add framework background jobs to the job scheduler without changing your usage habits.
        var backgroundJobManager = ServiceProvider.GetRequiredService<IBackgroundJobManager>();
        await jobManager.EnqueueAsync(
            new SmsJobArgs
            {
                PhoneNumber = "13800138000",
                Message = "Background worker from framework"
            },
            BackgroundJobPriority.High,
            TimeSpan.FromSeconds(10));

        // You can also add the framework background worker to the job scheduler without changing your usage habits.
        var backgroundWorkManager = ServiceProvider.GetRequiredService<IBackgroundWorkerManager>();
        //Console output every 20 seconds
        await backgroundWorkManager.AddAsync(ServiceProvider.GetRequiredService<ConsoleWorker>());
    }
}

public class SmsJobArgs
{
    public string PhoneNumber { get; set; }
    public string Message { get; set; }
}

public class SmsJob : AsyncBackgroundJob<SmsJobArgs>, ITransientDependency
{
    public override Task ExecuteAsync(SmsJobArgs args)
    {
        Console.WriteLine($"Send sms message: {args.Message}");

        return Task.CompletedTask;
    }
}

public class ConsoleWorker : AsyncPeriodicBackgroundWorkerBase, ISingletonDependency
{
    public ConsoleWorker(AbpAsyncTimer timer, IServiceScopeFactory serviceScopeFactory)
        : base(timer, serviceScopeFactory)
    {
        timer.Period = 20000;
    }

    protected override Task DoWorkAsync(PeriodicBackgroundWorkerContext workerContext)
    {
        Console.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] - ConsoleWorker Do Wrok.");
        return Task.CompletedTask;
    }
}
```
