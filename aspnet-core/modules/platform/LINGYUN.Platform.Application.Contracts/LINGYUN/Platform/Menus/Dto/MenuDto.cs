using LINGYUN.Platform.Routes;
using System;
using System.Collections.Generic;

namespace LINGYUN.Platform.Menus;

public class MenuDto : RouteDto
{
    /// <summary>
    /// Menu number
    /// </summary>
    public string Code { get; set; } = default!;
    /// <summary>
    /// Menu layout page
    /// </summary>
    public string Component { get; set; } = default!;
    /// <summary>
    /// frame
    /// </summary>
    public string Framework { get; set; } = default!;
    /// <summary>
    /// Parent node
    /// </summary>
    public Guid? ParentId { get; set; }
    /// <summary>
    /// The layout identifier to which it belongs
    /// </summary>
    public Guid LayoutId { get; set; }

    public bool IsPublic { get; set; }
    public bool Startup { get; set; }
}
