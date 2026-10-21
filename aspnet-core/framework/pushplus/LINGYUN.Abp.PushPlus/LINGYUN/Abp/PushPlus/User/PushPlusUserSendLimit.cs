namespace LINGYUN.Abp.PushPlus.User;

public enum PushPlusUserSendLimit
{
    /// <summary>
    /// Unlimited
    /// </summary>
    None = 1,
    /// <summary>
    /// Short term limit
    /// </summary>
    Short = 2,
    /// <summary>
    /// Permanent restrictions
    /// </summary>
    Permanent = 3,
}
