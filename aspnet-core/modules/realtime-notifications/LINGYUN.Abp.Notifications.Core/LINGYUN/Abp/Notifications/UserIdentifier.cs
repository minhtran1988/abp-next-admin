using System;

namespace LINGYUN.Abp.Notifications;

/// <summary>
/// User information
/// </summary>
public class UserIdentifier
{
    /// <summary>
    /// User ID
    /// </summary>
    public Guid UserId { get; set; }
    /// <summary>
    /// Username
    /// </summary>
    public string? UserName { get; set; }

    public UserIdentifier()
    {

    }

    public UserIdentifier(Guid userId, string? userName)
    {
        UserId = userId;
        UserName = userName;
    }
}
