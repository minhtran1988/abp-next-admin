namespace LINGYUN.Abp.BackgroundTasks;

public enum JobStatus
{
    /// <summary>
    /// Unknown
    /// </summary>
    None = -1,
    /// <summary>
    /// Completed
    /// </summary>
    Completed = 0,
    /// <summary>
    /// In queue
    /// </summary>
    Queuing = 5,
    /// <summary>
    /// Running
    /// </summary>
    Running = 10,
    /// <summary>
    /// Retry on failure
    /// </summary>
    FailedRetry = 15,
    /// <summary>
    /// Paused
    /// </summary>
    Paused = 20,
    /// <summary>
    /// Stopped
    /// </summary>
    Stopped = 30
}
