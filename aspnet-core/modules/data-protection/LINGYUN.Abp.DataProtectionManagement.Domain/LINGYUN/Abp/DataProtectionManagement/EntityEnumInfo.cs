using System;
using Volo.Abp;
using Volo.Abp.Domain.Entities;

namespace LINGYUN.Abp.DataProtectionManagement;

public class EntityEnumInfo : Entity<Guid>
{
    /// <summary>
    /// Name
    /// </summary>
    public virtual string Name { get; protected set; } = default!;
    /// <summary>
    /// Display name
    /// </summary>
    public virtual string DisplayName { get; protected set; } = default!;
    /// <summary>
    /// enumeration value
    /// </summary>
    public virtual string Value { get; protected set; } = default!;
    /// <summary>
    /// Attributes
    /// </summary>
    public virtual EntityPropertyInfo PropertyInfo { get; protected set; } = default!;
    /// <summary>
    /// Attribute identification
    /// </summary>
    public virtual Guid PropertyInfoId { get; protected set; }
    protected EntityEnumInfo()
    {

    }

    public EntityEnumInfo(
        Guid id,
        Guid propertyInfoId,
        string name, 
        string displayName, 
        string value)
        : base(id)
    {
        PropertyInfoId = propertyInfoId;
        Name = Check.NotNullOrWhiteSpace(name, nameof(name), EntityEnumInfoConsts.MaxNameLength);
        DisplayName = Check.NotNullOrWhiteSpace(displayName, nameof(displayName), EntityEnumInfoConsts.MaxDisplayNameLength);
        Value = Check.NotNullOrWhiteSpace(value, nameof(value), EntityEnumInfoConsts.MaxValueLength);
    }
}
