using LINGYUN.Abp.WeChat.Work.Messages.Models;
using Newtonsoft.Json;

namespace LINGYUN.Abp.Notifications;

public static partial class NotificationDataWeChatWorkExtensions
{
    private const string TextCardMessage = Prefix + "text_card_message";

    /// <summary>
    /// Card message notification
    /// </summary>
    /// <param name="notificationData">Notification content</param>
    /// <param name="textCardMessage">Card message</param>
    public static void WithTextCardMessage(
        this NotificationData notificationData,
        TextCardMessage textCardMessage)
    {
        notificationData.TrySetData(TextCardMessage, JsonConvert.SerializeObject(textCardMessage));
    }

    /// <summary>
    /// Try to get the card message
    /// </summary>
    /// <param name="notificationData"></param>
    /// <param name="textCardMessage"></param>
    /// <returns></returns>
    public static bool TryGetTextCardMessage(
        this NotificationData notificationData,
        out TextCardMessage? textCardMessage)
    {
        return notificationData.TryGetMessagePayload(TextCardMessage, out textCardMessage);
    }
}
