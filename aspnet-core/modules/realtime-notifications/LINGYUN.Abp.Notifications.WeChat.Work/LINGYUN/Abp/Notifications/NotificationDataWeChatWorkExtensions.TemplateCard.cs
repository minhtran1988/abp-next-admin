using LINGYUN.Abp.WeChat.Work.Messages.Models;
using Newtonsoft.Json;

namespace LINGYUN.Abp.Notifications;

public static partial class NotificationDataWeChatWorkExtensions
{
    private const string TemplateCardMessage = Prefix + "template_card_message";

    /// <summary>
    /// Template card message notification
    /// </summary>
    /// <param name="notificationData">Notification content</param>
    /// <param name="templateCard">Template card message</param>
    public static void WithTemplateCardMessage(
        this NotificationData notificationData,
        TemplateCard templateCard)
    {
        notificationData.TrySetData(TemplateCardMessage, JsonConvert.SerializeObject(templateCard));
    }

    /// <summary>
    /// Try to get the template card message
    /// </summary>
    /// <param name="notificationData"></param>
    /// <param name="textTemplateCard"></param>
    /// <returns></returns>
    public static bool TryGetTextTemplateCardMessage(
        this NotificationData notificationData,
        out TextTemplateCard? textTemplateCard)
    {
        return notificationData.TryGetMessagePayload(TemplateCardMessage, out textTemplateCard);
    }
}
