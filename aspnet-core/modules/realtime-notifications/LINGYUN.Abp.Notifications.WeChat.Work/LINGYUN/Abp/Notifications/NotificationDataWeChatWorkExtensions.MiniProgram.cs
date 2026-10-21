using LINGYUN.Abp.WeChat.Work.Messages.Models;
using Newtonsoft.Json;

namespace LINGYUN.Abp.Notifications;

public static partial class NotificationDataWeChatWorkExtensions
{
    private const string MiniProgramMessage = Prefix + "mini_program_message";

    /// <summary>
    /// Mini program notification
    /// </summary>
    /// <param name="notificationData">Notification content</param>
    /// <param name="miniProgramMessage">Mini program notification message</param>
    public static void WithMiniProgramMessage(
        this NotificationData notificationData,
        MiniProgramMessage miniProgramMessage)
    {
        notificationData.TrySetData(MiniProgramMessage, JsonConvert.SerializeObject(miniProgramMessage));
    }

    /// <summary>
    /// Try to get mini program messages
    /// </summary>
    /// <param name="notificationData"></param>
    /// <param name="workMiniProgramMessage"></param>
    /// <returns></returns>
    public static bool TryGetMiniProgramMessage(
        this NotificationData notificationData,
        out MiniProgramMessage? miniProgramMessage)
    {
        return notificationData.TryGetMessagePayload(MiniProgramMessage, out miniProgramMessage);
    }
}
