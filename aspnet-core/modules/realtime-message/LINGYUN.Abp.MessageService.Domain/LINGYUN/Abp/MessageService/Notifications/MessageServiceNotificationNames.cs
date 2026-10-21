namespace LINGYUN.Abp.MessageService.Notifications;

public static class MessageServiceNotificationNames
{
    public const string GroupName = "LINGYUN.Abp.Messages";

    public class IM
    {
        public const string GroupName = MessageServiceNotificationNames.GroupName + ".IM";
        /// <summary>
        /// Friend verification notification
        /// </summary>
        public const string FriendValidation = GroupName + ".FriendValidation";
        /// <summary>
        /// New friend notification
        /// </summary>
        public const string NewFriend = GroupName + ".NewFriend";
        /// <summary>
        /// Join group notification
        /// </summary>
        public const string JoinGroup = GroupName + ".JoinGroup";
        /// <summary>
        /// Exit group notification
        /// </summary>
        public const string ExitGroup = GroupName + ".ExitGroup";
        /// <summary>
        /// Group disbandment notification
        /// </summary>
        public const string DissolveGroup = GroupName + ".DissolveGroup";
    }

}
