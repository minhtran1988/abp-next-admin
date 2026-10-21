using LINGYUN.Abp.BackgroundTasks;
using System.Collections.Generic;

namespace LINGYUN.Abp.TaskManagement;

public class BackgroundJobActionDefinitionDto
{
    /// <summary>
    /// Name
    /// </summary>
    public string Name { get; set; } = default!;
    /// <summary>
    /// Type
    /// </summary>
    public JobActionType Type { get; set; }
    /// <summary>
    /// Display name
    /// </summary>
    public string? DisplayName { get; set; }
    /// <summary>
    /// Description
    /// </summary>
    public string? Description { get; set; }
    /// <summary>
    /// Parameter list
    /// </summary>
    public IList<BackgroundJobActionParamterDto> Paramters { get; set; } = new List<BackgroundJobActionParamterDto>();
}
