using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Data;

namespace LINGYUN.Abp.TaskManagement;

public class BackgroundJobActionDto : EntityDto<Guid>
{
    /// <summary>
    /// Job ID
    /// </summary>
    public string JobId { get; set; } = default!;
    /// <summary>
    /// Name
    /// </summary>
    public string Name { get; set; } = default!;
    /// <summary>
    /// Whether to enable
    /// </summary>
    public bool IsEnabled { get; set; }
    /// <summary>
    /// Parameters
    /// </summary>
    public ExtraPropertyDictionary Paramters { get; set; } = new ExtraPropertyDictionary();
}
