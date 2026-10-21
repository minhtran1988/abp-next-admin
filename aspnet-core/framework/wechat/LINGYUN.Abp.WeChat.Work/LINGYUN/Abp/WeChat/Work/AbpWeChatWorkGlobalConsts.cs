namespace LINGYUN.Abp.WeChat.Work;

public class AbpWeChatWorkGlobalConsts
{
    /// <summary>
    /// Provider name corresponding to Enterprise WeChat
    /// </summary>
    public static string ProviderName { get; set; } = "WorkWeixin";
    /// <summary>
    /// Enterprise WeChat OAuth authorization type
    /// </summary>
    public static string OAuthGrantType { get; set; } = "wecom_oauth";
    /// <summary>
    /// Enterprise WeChat applet authorization type
    /// </summary>
    public static string MiniProgramGrantType { get; set; } = "wecom_mp";
    /// <summary>
    /// Enterprise WeChat personal information identification
    /// </summary>
    public static string ProfileKey { get; set; } = "wecom.profile";
    /// <summary>
    /// Enterprise WeChat authorized application identification parameters
    /// </summary>
    public static string AgentId { get; set; } = "agent_id";
    /// <summary>
    /// Enterprise WeChat authorization code parameters
    /// </summary>
    public static string Code  { get; set; }= "code";
    /// <summary>
    /// Enterprise WeChat authorized display name
    /// </summary>
    public static string DisplayName { get; set; } = "Enterprise WeChat";
    /// <summary>
    /// Enterprise WeChat authorization method name
    /// </summary>
    public static string AuthenticationMethod { get; set; } = "wecom";

    internal static string ApiClient { get; set; } = "Abp.WeChat.Work";
    internal static string OAuthClient { get; set; } = "Abp.WeChat.Work.OAuth";
    internal static string LoginClient { get; set; } = "Abp.WeChat.Work.Login";
}
