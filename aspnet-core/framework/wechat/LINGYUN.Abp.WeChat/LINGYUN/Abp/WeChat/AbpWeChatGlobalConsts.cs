namespace LINGYUN.Abp.WeChat;

public class AbpWeChatGlobalConsts
{
    /// <summary>
    /// WeChat authorized name
    /// </summary>
    public static string AuthenticationScheme { get; set; }= "WeChat";
    /// <summary>
    /// WeChat personal information identification
    /// </summary>
    public static string ProfileKey { get; set; } = "wechat.profile";
    /// <summary>
    /// WeChat authorization Token parameter name
    /// </summary>
    public static string TokenName  { get; set; }= "code";
    /// <summary>
    /// WeChat authorized display name
    /// </summary>
    public static string DisplayName { get; set; } = "WeChat";

    public static string HttpClient { get; set; } = "Abp.WeChat";
}
