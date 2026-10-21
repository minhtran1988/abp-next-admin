using LINGYUN.Abp.WeChat.Work.Messages.Models;
using Newtonsoft.Json;

namespace LINGYUN.Abp.Notifications;

public static partial class NotificationDataWeChatWorkExtensions
{
    private const string VoiceMessage = Prefix + "voice_message";

    /// <summary>
    /// Language message notification
    /// </summary>
    /// <param name="notificationData">Notification content</param>
    /// <param name="voiceMessage">Language message</param>
    public static void WithVoiceMessage(
        this NotificationData notificationData,
        MediaMessage voiceMessage)
    {
        notificationData.TrySetData(VoiceMessage, JsonConvert.SerializeObject(voiceMessage));
    }

    /// <summary>
    /// Try to get the language message
    /// </summary>
    /// <param name="notificationData"></param>
    /// <param name="voiceMessage"></param>
    /// <returns></returns>
    public static bool TryGetVoiceMessage(
        this NotificationData notificationData,
        out MediaMessage? voiceMessage)
    {
        return notificationData.TryGetMessagePayload(VoiceMessage, out voiceMessage);
    }
}
