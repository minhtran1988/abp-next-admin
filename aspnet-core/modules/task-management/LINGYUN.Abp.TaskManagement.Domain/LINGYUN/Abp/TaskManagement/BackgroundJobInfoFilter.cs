using LINGYUN.Abp.BackgroundTasks;
using System;

namespace LINGYUN.Abp.TaskManagement;

/// <summary>
/// Background task filtering
/// </summary>
public class BackgroundJobInfoFilter
{
    /// <summary>
    /// Other filter conditions
    /// </summary>
    public string? Filter { get; set; }
    /// <summary>
    /// Task name
    /// </summary>
    public string? Name { get; set; }
    /// <summary>
    /// Task grouping
    /// </summary>
    public string? Group { get; set; }
    /// <summary>
    /// Task type
    /// </summary>
    public string? Type { get; set; }
    /// <summary>
    /// Node name
    /// </summary>
    public string? NodeName { get; set; }
    /// <summary>
    /// Task status
    /// </summary>
    public JobStatus? Status { get; set; }
    /// <summary>
    /// Start time
    /// </summary>
    public DateTime? BeginTime { get; set; }
    /// <summary>
    /// End time
    /// </summary>
    public DateTime? EndTime { get; set; }
    /// <summary>
    /// Last start trigger time
    /// </summary>
    public DateTime? BeginLastRunTime { get; set; }
    /// <summary>
    /// Last trigger time
    /// </summary>
    public DateTime? EndLastRunTime { get; set; }
    /// <summary>
    /// Starting creation time
    /// </summary>
    public DateTime? BeginCreationTime { get; set; }
    /// <summary>
    /// Deadline creation time
    /// </summary>
    public DateTime? EndCreationTime { get; set; }
    /// <summary>
    /// Whether the task has been abandoned
    /// </summary>
    public bool? IsAbandoned { get; set; }
    /// <summary>
    /// Whether it is a persistent task
    /// </summary>
    public JobType? JobType { get; set; }
    /// <summary>
    /// Priority
    /// </summary>
    public JobPriority? Priority { get; set; }
    /// <summary>
    /// Job source
    /// </summary>
    public JobSource? Source { get; set; }
}
