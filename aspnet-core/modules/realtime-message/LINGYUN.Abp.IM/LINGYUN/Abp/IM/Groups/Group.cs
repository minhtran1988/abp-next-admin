namespace LINGYUN.Abp.IM.Groups;

public class Group
{
    /// <summary>
    /// Group ID
    /// </summary>
    public string Id { get; set; } = default!;
    /// <summary>
    /// Group name
    /// </summary>
    public string Name { get; set; } = default!;
    /// <summary>
    /// Group avatar
    /// </summary>
    public string? AvatarUrl { get; set; }
    /// <summary>
    /// Allow anonymous chat
    /// </summary>
    public bool AllowAnonymous { get; set; }
    /// <summary>
    /// Allow sending messages
    /// </summary>
    public bool AllowSendMessage { get; set; }
    /// <summary>
    /// Maximum number of users
    /// </summary>
    public int MaxUserLength { get; set; }
    /// <summary>
    /// Number of group users
    /// </summary>
    public int GroupUserCount { get; set; }
}
