using System;
using System.Collections.Generic;
using System.Threading;

namespace LINGYUN.Abp.BackgroundTasks;

public class JobEventData
{
    /// <summary>
    /// Task category
    /// </summary>
    public Type Type { get; }
    /// <summary>
    /// Task parameters
    /// </summary>
    public IReadOnlyDictionary<string, object> Args { get; }
    /// <summary>
    /// Task group
    /// </summary>
    public string Group { get; }
    /// <summary>
    /// Task name
    /// </summary>
    public string Name { get; }
    /// <summary>
    /// Task ID
    /// </summary>
    public string Key { get; }
    /// <summary>
    /// Task status
    /// </summary>
    public JobStatus Status { get; set; }
    /// <summary>
    /// Executor tenant
    /// </summary>
    public Guid? TenantId { get; set; }
    /// <summary>
    /// Error details
    /// </summary>
    public Exception? Exception { get; }
    /// <summary>
    /// Task description
    /// </summary>
    public string? Description { get; set; }
    /// <summary>
    /// Return parameters
    /// </summary>
    public string? Result { get; set; }
    /// <summary>
    /// Number of triggers
    /// </summary>
    public int Triggered { get; set; }
    /// <summary>
    /// Maximum number of executions
    /// </summary>
    public int RepeatCount { get; set; }
    /// <summary>
    /// Running time
    /// </summary>
    public DateTime RunTime { get; set; }
    /// <summary>
    /// Execution time (ms)
    /// </summary>
    public int? ExecutionDuration { get; set; }
    /// <summary>
    /// Last run time
    /// </summary>
    public DateTime? LastRunTime { get; set; }
    /// <summary>
    /// Next running time
    /// </summary>
    public DateTime? NextRunTime { get; set; }
    /// <summary>
    /// Job cancellation token
    /// </summary>
    public CancellationToken CancellationToken { get; }
    public JobEventData(
        string key,
        Type type, 
        string group, 
        string name,
        IReadOnlyDictionary<string, object> args,
        Exception? exception = null,
        CancellationToken cancellationToken = default)
    {
        Key = key;
        Type = type;
        Group = group;
        Name = name;
        Args = args;
        Exception = exception;
        CancellationToken = cancellationToken;
    }
}
