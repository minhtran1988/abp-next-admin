using System;
using Volo.Abp.Application.Dtos;

namespace LINGYUN.Abp.TaskManagement;

public class BackgroundJobLogGetListInput : PagedAndSortedResultRequestDto
{
    public string? JobId { get; set; }
    /// <summary>
    /// Other filter conditions
    /// </summary>
    public string? Filter { get; set; }
    /// <summary>
    /// There is an exception
    /// </summary>
    public bool? HasExceptions { get; set; }
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
    /// Start trigger time
    /// </summary>
    public DateTime? BeginRunTime { get; set; }
    /// <summary>
    /// End trigger time
    /// </summary>
    public DateTime? EndRunTime { get; set; }
}
