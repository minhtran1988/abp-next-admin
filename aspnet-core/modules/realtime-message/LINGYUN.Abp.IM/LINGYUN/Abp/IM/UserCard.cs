using System;

namespace LINGYUN.Abp.IM;

public class UserCard
{
    public Guid? TenantId { get; set; }

    public Guid UserId { get; set; }

    #region Fine-grained user profiles

    public string UserName { get; set; } = default!;
    /// <summary>
    /// Avatar
    /// </summary>
    public string? AvatarUrl { get; set; }
    /// <summary>
    /// Nickname
    /// </summary>
    public string? NickName { get; set; }
    /// <summary>
    /// age
    /// </summary>
    public int Age { get; set; }
    /// <summary>
    /// Gender
    /// </summary>
    public Sex Sex { get; set; }
    /// <summary>
    /// Signature
    /// </summary>
    public string? Sign { get; set; }
    /// <summary>
    /// Description
    /// </summary>
    public string? Description { get; set; }
    /// <summary>
    /// birthday
    /// </summary>
    public DateTime? Birthday { get; set; }

    public bool Online { get; set; }

    #endregion
}
