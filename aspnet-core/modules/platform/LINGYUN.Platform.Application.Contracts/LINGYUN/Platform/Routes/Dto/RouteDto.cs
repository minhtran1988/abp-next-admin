using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;

namespace LINGYUN.Platform.Routes;

public class RouteDto : EntityDto<Guid>
{
    /// <summary>
    /// path
    /// </summary>
    public string Path { get; set; } = default!;
    /// <summary>
    /// Name
    /// </summary>
    public string Name { get; set; } = default!;
    /// <summary>
    /// Display name
    /// </summary>
    public string DisplayName { get; set; } = default!;
    /// <summary>
    /// Description
    /// </summary>
    public string? Description { get; set; }
    /// <summary>
    /// Redirect path
    /// </summary>
    public string? Redirect { get; set; }
    /// <summary>
    /// Some auxiliary elements of routing depend on the design of the data dictionary
    /// </summary>
    public Dictionary<string, object> Meta { get; set; } = new Dictionary<string, object>();
}
