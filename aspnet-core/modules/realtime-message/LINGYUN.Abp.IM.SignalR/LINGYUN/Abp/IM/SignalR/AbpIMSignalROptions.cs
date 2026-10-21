namespace LINGYUN.Abp.IM.SignalR;

public class AbpIMSignalROptions
{
    /// <summary>
    /// Customized client message receiving method name
    /// </summary>
    public string GetChatMessageMethod { get; set; }
    /// <summary>
    /// Customized client withdrawal message method name
    /// </summary>
    public string ReCallChatMessageMethod { get; set; }
    /// <summary>
    /// User online receiving method name
    /// </summary>
    public string UserOnlineMethod { get; set; }
    /// <summary>
    /// User offline reception method name
    /// </summary>
    public string UserOfflineMethod { get; set; }

    public AbpIMSignalROptions()
    {
        GetChatMessageMethod = "get-chat-message";
        ReCallChatMessageMethod = "recall-chat-message";
        UserOnlineMethod = "on-user-onlined";
        UserOfflineMethod = "on-user-offlined";
    }
}
