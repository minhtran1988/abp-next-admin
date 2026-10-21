using JetBrains.Annotations;
using LINGYUN.Platform.Routes;
using System;
using Volo.Abp;

namespace LINGYUN.Platform.Menus;

/// <summary>
/// Menu
/// </summary>
public class Menu : Route
{
    /// <summary>
    /// frame
    /// </summary>
    public virtual string Framework { get; set; } = default!;
    /// <summary>
    /// Menu number
    /// </summary>
    public virtual string Code { get; set; } = default!;
    /// <summary>
    /// Menu layout page, Layout path
    /// </summary>
    public virtual string Component { get; set; } = default!;
    /// <summary>
    /// The parent menu it belongs to
    /// </summary>
    public virtual Guid? ParentId { get; set; }
    /// <summary>
    /// The layout identifier to which it belongs
    /// </summary>
    public virtual Guid LayoutId { get; set; }
    /// <summary>
    /// Public menu
    /// </summary>
    public virtual bool IsPublic { get; set; }
    protected Menu() 
    {
    }

    public Menu(
        [NotNull] Guid id,
        [NotNull] Guid layoutId,
        [NotNull] string path, 
        [NotNull] string name,
        [NotNull] string code,
        [NotNull] string component,
        [NotNull] string displayName,
        [NotNull] string framework,
        string? redirect = null,
        string? description = null,
        Guid? parentId = null,
        Guid? tenantId = null) 
        : base(id, path, name, displayName, redirect, description, tenantId)
    {
        Check.NotNullOrWhiteSpace(code, nameof(code));

        LayoutId = layoutId;
        Code = code;
        Component = component; // The Component of the subordinate first-level menu should be the layout page, such as component: Layout in vue-admin. It is the same with other front-end frameworks. The path of the layout page should be passed here for the front-end to import.
        Framework = framework;
        ParentId = parentId;

        IsPublic = false;
    }
}
