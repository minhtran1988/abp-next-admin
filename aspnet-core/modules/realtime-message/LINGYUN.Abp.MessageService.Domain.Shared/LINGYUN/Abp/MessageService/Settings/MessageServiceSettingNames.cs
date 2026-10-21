namespace LINGYUN.Abp.MessageService.Settings;

public class MessageServiceSettingNames
{
    public const string GroupName = "Abp.MessageService";

    public class Notifications
    {
        public const string Default = GroupName + ".Notifications";
        /// <summary>
        /// Clean up expired message batches
        /// </summary>
        public const string CleanupExpirationBatchCount = Default + ".CleanupExpirationBatchCount";
    }

    public class Messages
    {
        public const string Default = GroupName + ".Messages";
        /// <summary>
        /// Recall message expiration time (minutes)
        /// </summary>
        public const string RecallExpirationTime = Default + ".RecallExpirationTime";
    }
}
