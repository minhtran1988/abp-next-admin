using LINGYUN.Platform.Routes;
using System;

namespace LINGYUN.Platform.Layouts;

public class LayoutDto : RouteDto
{
    /// <summary>
    /// frame
    /// </summary>
    public string Framework { get; set; } = default!;
    /// <summary>
    /// Which data dictionary is used for the agreed Meta, mainly to constrain the consistency of the fields required for routing
    /// </summary>
    public Guid DataId { get; set; }
}
