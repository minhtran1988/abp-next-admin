using JetBrains.Annotations;
using LINGYUN.Platform.Routes;
using System;

namespace LINGYUN.Platform.Layouts;

/// <summary>
/// Layout view entity
/// </summary>
public class Layout : Route
{
    /// <summary>
    /// frame
    /// </summary>
    public virtual string Framework { get; protected set; } = default!;
    /// <summary>
    /// Which data dictionary is used for the agreed Meta, mainly to constrain the consistency of the fields required for routing
    /// </summary>
    public virtual Guid DataId { get; protected set; }

    protected Layout() { }

    public Layout(
        [NotNull] Guid id, 
        [NotNull] string path, 
        [NotNull] string name,
        [NotNull] string displayName,
        [NotNull] Guid dataId,
        [NotNull] string framework,
        [CanBeNull] string? redirect = null,
        [CanBeNull] string? description = null,
        [CanBeNull] Guid? tenantId = null) 
        : base(id, path, name, displayName, redirect, description, tenantId)
    {
        DataId = dataId;
        Framework = framework;
    }
}
