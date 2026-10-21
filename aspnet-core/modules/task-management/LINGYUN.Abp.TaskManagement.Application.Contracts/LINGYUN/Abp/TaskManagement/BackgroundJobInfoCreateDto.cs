using LINGYUN.Abp.BackgroundTasks;
using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Validation;

namespace LINGYUN.Abp.TaskManagement;

public class BackgroundJobInfoCreateDto : BackgroundJobInfoCreateOrUpdateDto
{
    /// <summary>
    /// Task name
    /// </summary>
    [Required]
    [DynamicStringLength(typeof(BackgroundJobInfoConsts), nameof(BackgroundJobInfoConsts.MaxNameLength))] 
    public string Name { get; set; } = default!;
    /// <summary>
    /// Task grouping
    /// </summary>
    [Required]
    [DynamicStringLength(typeof(BackgroundJobInfoConsts), nameof(BackgroundJobInfoConsts.MaxGroupLength))] 
    public string Group { get; set; } = default!;
    /// <summary>
    /// Task type
    /// </summary>
    [Required]
    [DynamicStringLength(typeof(BackgroundJobInfoConsts), nameof(BackgroundJobInfoConsts.MaxTypeLength))] 
    public string Type { get; set; } = default!;

    [DynamicStringLength(typeof(BackgroundJobInfoConsts), nameof(BackgroundJobInfoConsts.MaxNodeNameLength))]
    public string? NodeName { get; set; }
    /// <summary>
    /// Start time
    /// </summary>
    [Required]
    public DateTime BeginTime { get; set; }
    /// <summary>
    /// End time
    /// </summary>
    public DateTime? EndTime { get; set; }
    /// <summary>
    /// Job source
    /// </summary>
    public JobSource Source { get; set; } = JobSource.User;
}
