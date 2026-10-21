namespace LINGYUN.Abp.Notifications;

public static class DefaultNotifications
{
    public const string GroupName = "Abp.Notifications";
    /// <summary>
    /// Site notification
    /// </summary>
    public const string OnsideNotice = GroupName + ".OnsideNotice";
    /// <summary>
    /// Event notification
    /// </summary>
    public const string ActivityNotice = GroupName + ".ActivityNotice";
    /// <summary>
    /// System notification
    /// </summary>
    public const string SystemNotice = GroupName + ".SystemNotice";
}
