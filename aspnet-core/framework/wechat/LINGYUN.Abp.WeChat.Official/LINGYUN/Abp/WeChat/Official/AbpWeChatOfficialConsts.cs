namespace LINGYUN.Abp.WeChat.Official;

public class AbpWeChatOfficialConsts
{
    /// <summary>
    /// Provider name corresponding to WeChat official account
    /// </summary>
    public static string ProviderName { get; set; } = "WeChat.Official";

    /// <summary>
    /// WeChat public platform authorization type
    /// </summary>
    public static string GrantType { get; set; } = "wx-op";

    /// <summary>
    /// WeChat public platform authorization method name
    /// </summary>
    public static string AuthenticationMethod { get; set; } = "woa";

    public static string HttpClient { get; set; } = "Abp.WeChat.Official";
}
