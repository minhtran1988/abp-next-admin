namespace LINGYUN.Abp.WeChat.MiniProgram;

public class AbpWeChatMiniProgramConsts
{
    /// <summary>
    /// Provider name corresponding to WeChat applet
    /// </summary>
    public static string ProviderName { get; set; } = "WeChat.MiniProgram";

    /// <summary>
    /// WeChat applet authorization type
    /// </summary>
    public static string GrantType { get; set; } = "wx-mp";

    /// <summary>
    /// WeChat applet authorization method name
    /// </summary>
    public static string AuthenticationMethod { get; set; } = "wma";
    public static string HttpClient { get; set; } = "Abp.WeChat.MiniProgram";
}
