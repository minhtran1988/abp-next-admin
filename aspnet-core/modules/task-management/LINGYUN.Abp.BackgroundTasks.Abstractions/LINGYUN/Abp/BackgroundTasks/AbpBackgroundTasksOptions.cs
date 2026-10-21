using System;
using System.Collections.Generic;
using Volo.Abp.Collections;

namespace LINGYUN.Abp.BackgroundTasks;

public class AbpBackgroundTasksOptions
{
    /// <summary>
    /// Job exception type conversion
    /// </summary>
    public IDictionary<string, JobExceptionType> ErrorCodeToExceptionTypeMappings { get; }
    /// <summary>
    /// Task monitoring type list
    /// </summary>
    /// <remarks>
    /// Users can implement event monitoring to implement custom logic
    /// </remarks>
    public ITypeList<IJobEvent> JobMonitors { get; }
    /// <summary>
    /// Job provider list
    /// </summary>
    /// <remarks>
    /// User-implemented jobs can be added to the collection
    /// </remarks>
    public ITypeList<IJobDefinitionProvider> DefinitionProviders { get; }
    /// <summary>
    /// Job release pre-configuration
    /// </summary>
    /// <remarks>
    /// Tips: <br />
    /// Only applies to job preconfiguration applicable to the IBackgroundJobManager and IBackgroundWorker adapter interfaces
    /// <br />
    /// <see cref="IJobRunnable"/> Please manage the job parameters of standard implementation directly through the interface.
    /// </remarks>
    public IJobDispatcherSelectorList JobDispatcherSelectors { get; }
    /// <summary>
    /// Enable cleanup tasks
    /// </summary>
    /// <remarks>
    /// Master node enabled
    /// </remarks>
    public bool JobCleanEnabled { get; set; }
    /// <summary>
    /// Task expiration time
    /// Default: 15 days
    /// </summary>
    /// <remarks>
    /// Task expiration time, clean up after exceeding the time period
    /// </remarks>
    public TimeSpan JobExpiratime { get; set; }
    /// <summary>
    /// Batch size of each cleaning task
    /// Default: 1000
    /// </summary>
    public int MaxJobCleanCount { get; set; }
    /// <summary>
    /// Clean up expired task batch Cron expression
    /// Default: 600 seconds (0 0/10 * * * ? * )
    /// </summary>
    /// <remarks>
    /// Cron expression
    /// </remarks>
    public string JobCleanCronExpression { get; set; }
    /// <summary>
    /// Enable polling task
    /// </summary>
    /// <remarks>
    /// Master node enabled
    /// </remarks>
    public bool JobFetchEnabled { get; set; }
    /// <summary>
    /// Each polling task batch size
    /// Default: 1000
    /// </summary>
    public int MaxJobFetchCount { get; set; }
    /// <summary>
    /// Polling task batch Cron expression
    /// Default: 30 seconds (0/30 * * * * ? )
    /// </summary>
    /// <remarks>
    /// Cron expression
    /// </remarks>
    public string JobFetchCronExpression { get; set; }
    /// <summary>
    /// Lock task timeout (seconds) when polling task batches
    /// Default: 120
    /// </summary>
    /// <remarks>
    /// The polling task is also a background task, and each polling needs to be locked to prevent repeated tasks from being stored in the database.
    /// </remarks>
    public int JobFetchLockTimeOut { get; set; }

    /// <summary>
    /// Enable inspection tasks
    /// </summary>
    /// <remarks>
    /// Master node enabled
    /// </remarks>
    public bool JobCheckEnabled { get; set; }
    /// <summary>
    /// Check the task batch size each time
    /// Default: 100
    /// </summary>
    public int MaxJobCheckCount { get; set; }
    /// <summary>
    /// Check task batch Cron expression
    /// Default: Execute every 2 hours (0 0 0/2 * * ? )
    /// </summary>
    /// <remarks>
    /// Cron expression
    /// </remarks>
    public string JobCheckCronExpression { get; set; }
    /// <summary>
    /// Lock task timeout (seconds) when checking task batches
    /// Default: 120
    /// </summary>
    public int JobCheckLockTimeOut { get; set; }
    /// <summary>
    /// Specify the running node
    /// </summary>
    public string? NodeName { get; set; }
    public AbpBackgroundTasksOptions()
    {
        JobFetchEnabled = false;
        MaxJobFetchCount = 1000;
        JobFetchLockTimeOut = 120;
        JobFetchCronExpression = "0/30 * * * * ? ";

        JobCheckEnabled = false;
        MaxJobCheckCount = 100;
        JobCheckLockTimeOut = 120;
        JobCheckCronExpression = "0 0 0/2 * * ? ";

        JobCleanEnabled = false;
        MaxJobCleanCount = 1000;
        JobExpiratime = TimeSpan.FromDays(15d);
        JobCleanCronExpression = "0 0/10 * * * ? *";

        JobMonitors = new TypeList<IJobEvent>();
        DefinitionProviders = new TypeList<IJobDefinitionProvider>();
        JobDispatcherSelectors = new JobDispatcherSelectorList();
        ErrorCodeToExceptionTypeMappings = new Dictionary<string, JobExceptionType>();
    }
    public void Map(string errorCode, JobExceptionType exceptionType)
    {
        ErrorCodeToExceptionTypeMappings[errorCode] = exceptionType;
    }
}
