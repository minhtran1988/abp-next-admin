using System;

namespace LINGYUN.Abp.IM.Contract;

public class UserFriend : UserCard
{
    /// <summary>
    /// Friend ID
    /// </summary>
    public Guid FriendId { get; set; }
    /// <summary>
    /// Blacklist added
    /// </summary>
    public bool Black { get; set; }
    /// <summary>
    /// Special attention
    /// </summary>
    public bool SpecialFocus { get; set; }
    /// <summary>
    /// Do not disturb messages
    /// </summary>
    public bool DontDisturb { get; set; }
    /// <summary>
    /// Remark name
    /// </summary>
    public string? RemarkName { get; set; }

    public override int GetHashCode()
    {
        return FriendId.GetHashCode();
    }

    public override bool Equals(object? obj)
    {
        if (obj == null)
        {
            return false;
        }
        if (obj is UserFriend friend)
        {
            return friend.FriendId.Equals(FriendId);
        }
        return false;
    }
}
