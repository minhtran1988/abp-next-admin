using LINGYUN.Abp.WeChat.Work.Messages.Models;
using Newtonsoft.Json;

namespace LINGYUN.Abp.Notifications;

public static partial class NotificationDataWeChatWorkExtensions
{
    private const string FileMessage = Prefix + "file_message";

    /// <summary>
    /// File message notification
    /// </summary>
    /// <param name="notificationData">Notification content</param>
    /// <param name="fileMessage">File message</param>
    public static void WithFileMessage(
        this NotificationData notificationData,
        MediaMessage fileMessage)
    {
        notificationData.TrySetData(FileMessage, JsonConvert.SerializeObject(fileMessage));
    }

    /// <summary>
    /// Try to get the file message
    /// </summary>
    /// <param name="notificationData"></param>
    /// <param name="fileMessage"></param>
    /// <returns></returns>
    public static bool TryGetFileMessage(
        this NotificationData notificationData,
        out MediaMessage? fileMessage)
    {
        return notificationData.TryGetMessagePayload(FileMessage, out fileMessage);
    }
}
