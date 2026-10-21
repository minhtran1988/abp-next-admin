using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using Volo.Abp;
using Volo.Abp.Data;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace LINGYUN.Platform.Routes;

/// <summary>
/// Whether it is layout, view or page, they all serve as the implementation of routing, so a routing entity is abstracted<br/>
/// Note: This is an entity designed based on the routing rules of Vue Router. Details: https://router.vuejs.org/zh/api/#routes
/// </summary>
public abstract class Route : FullAuditedAggregateRoot<Guid>, IMultiTenant
{
    public virtual Guid? TenantId { get; protected set; }
    /// <summary>
    /// path
    /// </summary>
    public virtual string? Path { get; set; }
    /// <summary>
    /// Name
    /// </summary>
    public virtual string Name { get; set; } = default!;
    /// <summary>
    /// Display name
    /// </summary>
    public virtual string DisplayName { get; set; } = default!;
    /// <summary>
    /// Description
    /// </summary>
    public virtual string? Description { get; set; }
    /// <summary>
    /// Redirect path
    /// </summary>
    public virtual string? Redirect { get; set; }

    protected Route() { }

    protected Route(
        [NotNull] Guid id,
        [NotNull] string path,
        [NotNull] string name,
        [NotNull] string displayName,
        [CanBeNull] string? redirect = null,
        [CanBeNull] string? description = null,
        [CanBeNull] Guid? tenantId = null)
        : base(id)
    {
        Check.NotNullOrWhiteSpace(path, nameof(path));
        Check.NotNullOrWhiteSpace(name, nameof(name));
        Check.NotNullOrWhiteSpace(displayName, nameof(displayName));

        Path = path;
        Name = name;
        DisplayName = displayName;
        Redirect = redirect;
        Description = description;
        TenantId = tenantId;
    }

    public void WithRequiredFeatures(string[] features)
    {
        var requiresFeatures = features?.JoinAsString(",") ?? "";

        this.SetProperty("RequiredFeatures", requiresFeatures);
    }

    public string[] GetRequiredFeatures()
    {
        var requiresFeaturesObj = this.GetProperty("RequiredFeatures");
        return requiresFeaturesObj?.ToString()?.Split(",") ?? Array.Empty<string>();
    }

    public void WithRequiredPermissions(string[] permissions)
    {
        var requiresPermissions = permissions?.JoinAsString(",") ?? "";

        this.SetProperty("RequiredPermissions", requiresPermissions);
    }

    public string[] GetRequiredPermissions()
    {
        var requiresPermissionsObj = this.GetProperty("RequiredPermissions");
        return requiresPermissionsObj?.ToString()?.Split(",") ?? Array.Empty<string>();
    }

    public override int GetHashCode()
    {
        return Name.GetHashCode();
    }

    public override bool Equals(object? obj)
    {
        if (obj == null)
        {
            return false;
        }
        if (obj is Route route)
        {
            return route.Name.Equals(Name, StringComparison.InvariantCultureIgnoreCase);
        }
        return base.Equals(obj);
    }
}
