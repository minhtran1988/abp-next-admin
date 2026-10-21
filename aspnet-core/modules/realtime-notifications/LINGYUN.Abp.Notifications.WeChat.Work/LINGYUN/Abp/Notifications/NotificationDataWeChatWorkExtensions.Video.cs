using LINGYUN.Abp.WeChat.Work.Messages.Models;
using Newtonsoft.Json;

namespace LINGYUN.Abp.Notifications;

public static partial class NotificationDataWeChatWorkExtensions
{
    private const string VideoMessage = Prefix + "video_message";

    /// <summary>
    /// Video message notification
    /// </summary>
    /// <param name="notificationData">Notification content</param>
    /// <param name="videoMessage">Video message</param>
    public static void WithVideoMessage(
        this NotificationData notificationData,
        VideoMessage videoMessage)
    {
        notificationData.TrySetData(VideoMessage, JsonConvert.SerializeObject(videoMessage));
    }

    /// <summary>
    /// Try to get the video message
    /// </summary>
    /// <param name="notificationData"></param>
    /// <param name="videoMessage"></param>
    /// <returns></returns>
    public static bool TryGetVideoMessage(
        this NotificationData notificationData,
        out VideoMessage? videoMessage)
    {
        return notificationData.TryGetMessagePayload(VideoMessage, out videoMessage);
    }
}
