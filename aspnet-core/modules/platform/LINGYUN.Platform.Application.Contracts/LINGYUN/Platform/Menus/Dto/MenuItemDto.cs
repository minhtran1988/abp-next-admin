using LINGYUN.Platform.Routes;
using System.Collections.Generic;

namespace LINGYUN.Platform.Menus;

public class MenuItemDto : RouteDto
{
    /// <summary>
    /// Menu number
    /// </summary>
    public string Code { get; set; } = default!;
    /// <summary>
    /// Menu component
    /// </summary>
    public string Component { get; set; } = default!;
    /// <summary>
    /// Submenu list
    /// </summary>
    public List<MenuItemDto> Children { get; set; } = new List<MenuItemDto>();
}
