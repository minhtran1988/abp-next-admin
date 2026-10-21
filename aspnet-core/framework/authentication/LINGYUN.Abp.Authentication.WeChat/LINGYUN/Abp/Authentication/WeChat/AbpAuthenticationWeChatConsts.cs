using LINGYUN.Abp.WeChat;
using LINGYUN.Abp.WeChat.Official;

namespace LINGYUN.Abp.Authentication.WeChat;

public static class AbpAuthenticationWeChatConsts
{
    /// <summary>
    /// WeChat personal information identification
    /// </summary>
    public static string ProfileKey => AbpWeChatGlobalConsts.ProfileKey;
    /// <summary>
    /// WeChat provider identification
    /// </summary>
    public static string ProviderKey => AbpWeChatOfficialConsts.ProviderName;
    /// <summary>
    /// WeChat provider display name
    /// </summary>
    public static string DisplayName => AbpWeChatGlobalConsts.DisplayName;
    /// <summary>
    /// Callback address
    /// </summary>
    public static string CallbackPath { get; set; } = "/signin-wechat";

    /// <summary>
    /// Web page login outside WeChat client
    /// </summary>
    public const string QrConnectEndpoint = "https://open.weixin.qq.com/connect/qrconnect";

    /// <summary>
    /// Web page login in WeChat client
    /// </summary>
    public const string AuthorizationEndpoint = "https://open.weixin.qq.com/connect/oauth2/authorize";

    /// <summary>
    /// After the user allows authorization, the returned code is exchanged for the access_token address.
    /// </summary>
    public const string TokenEndpoint = "https://api.weixin.qq.com/sns/oauth2/access_token";

    /// <summary>
    /// Use access_token to obtain the user's personal information address
    /// </summary>
    public const string UserInformationEndpoint = "https://api.weixin.qq.com/sns/userinfo";
    /// <summary>
    /// The authorization page pops up, and you can get the nickname, gender, and location through openid.
    /// Moreover, even if the user is not paying attention, the information can be obtained as long as the user authorizes it.
    /// <br />
    /// <br />
    /// For detailed inquiries: <see cref="https://developers.weixin.qq.com/doc/offiaccount/OA_Web_Apps/Wechat_webpage_authorization.html"/>
    /// </summary>
    /// <remarks>
    /// Web page authorization initiated with snsapi_userinfo as the scope is used to obtain the user's basic information.
    /// But this kind of authorization requires the user to manually agree, and since the user has agreed, there is no need to pay attention, and the basic information of the user can be obtained after authorization.
    /// </remarks>
    public const string UserInfoScope = "snsapi_userinfo";
    /// <summary>
    /// Does not pop up the authorization page, jumps directly, and can only obtain the user's openid
    /// <br />
    /// <br />
    /// For detailed inquiries: <see cref="https://developers.weixin.qq.com/doc/offiaccount/OA_Web_Apps/Wechat_webpage_authorization.html"/>
    /// </summary>
    /// <remarks>
    /// The web page authorization initiated with snsapi_base as the scope is used to obtain the openid of the user entering the page, and is authorized silently and automatically jumps to the callback page.
    /// What the user perceives is that he directly enters the callback page (often a business page)
    /// </remarks>
    public const string LoginScope = "snsapi_login";
}
