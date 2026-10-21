using LINGYUN.Abp.WeChat.Work.Messages.Models;
using Newtonsoft.Json;

namespace LINGYUN.Abp.Notifications;
public static partial class NotificationDataWeChatWorkExtensions
{
    private const string MpNewMessage = Prefix + "mp_new_message";

    /// <summary>
    /// Graphic message
    /// </summary>
    /// <param name="notificationData">Notification content</param>
    /// <param name="mpNewMessagePayload">Picture and text message (mp) carrier</param>
    public static void WithMpNewMessage(
        this NotificationData notificationData,
        MpNewMessagePayload mpNewMessagePayload)
    {
        notificationData.TrySetData(MpNewMessage, JsonConvert.SerializeObject(mpNewMessagePayload));
    }

    /// <summary>
    /// Try graphic messages
    /// </summary>
    /// <param name="notificationData"></param>
    /// <param name="workMiniProgramMessage"></param>
    /// <returns></returns>
    public static bool TryGetMpNewMessage(
        this NotificationData notificationData,
        out MpNewMessagePayload? mpNewMessagePayload)
    {
        return notificationData.TryGetMessagePayload(MpNewMessage, out mpNewMessagePayload);
    }
}
