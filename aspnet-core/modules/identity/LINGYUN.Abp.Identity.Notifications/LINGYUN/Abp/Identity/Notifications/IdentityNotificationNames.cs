namespace LINGYUN.Abp.Identity.Notifications;
public static class IdentityNotificationNames
{
    public const string GroupName = "AbpIdentity";

    public static class Session
    {
        public const string Prefix = GroupName + ".Session";
        /// <summary>
        /// Session expiration notification
        /// </summary>
        public const string ExpirationSession = Prefix + ".Expiration";
    }

    public static class IdentityUser
    {
        public const string Prefix = GroupName + ".IdentityUser";
        /// <summary>
        /// Account remains active notification
        /// </summary>
        public const string InactiveUserReminderNotifier = Prefix + ".InactiveUserReminderNotifier";
        /// <summary>
        /// Account deactivation notification
        /// </summary>
        public const string InactiveUserDeactivationNotifier = Prefix + ".InactiveUserDeactivationNotifier";
        /// <summary>
        /// Account deletion notification
        /// </summary>
        public const string InactiveUserDeletionNotifier = Prefix + ".InactiveUserDeletionNotifier";
    }
}
