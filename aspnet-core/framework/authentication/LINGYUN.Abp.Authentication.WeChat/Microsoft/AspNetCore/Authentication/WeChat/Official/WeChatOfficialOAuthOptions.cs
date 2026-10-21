using LINGYUN.Abp.Authentication.WeChat;
using LINGYUN.Abp.WeChat.Common.Security.Claims;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using System.Text.Json;

namespace Microsoft.AspNetCore.Authentication.WeChat.Official;

public class WeChatOfficialOAuthOptions : OAuthOptions
{
    public WeChatOfficialOAuthOptions()
    {
        // Used to prevent initialization errors, it will be rewritten in OAuthHandler.InitializeHandlerAsync
        ClientId = "WeChatOfficial";
        ClientSecret = "WeChatOfficial";

        ClaimsIssuer = AbpAuthenticationWeChatConsts.ProviderKey;
        CallbackPath = new PathString(AbpAuthenticationWeChatConsts.CallbackPath);

        AuthorizationEndpoint = AbpAuthenticationWeChatConsts.AuthorizationEndpoint;
        TokenEndpoint = AbpAuthenticationWeChatConsts.TokenEndpoint;
        UserInformationEndpoint = AbpAuthenticationWeChatConsts.UserInformationEndpoint;

        Scope.Add(AbpAuthenticationWeChatConsts.LoginScope);
        Scope.Add(AbpAuthenticationWeChatConsts.UserInfoScope);

        // This original attribute must be written in. The framework is associated with UserLogin.ProviderKey to determine whether to bind WeChat.
        ClaimActions.MapJsonKey(ClaimTypes.NameIdentifier, "openid");
        ClaimActions.MapJsonKey(ClaimTypes.Name, "nickname");

        // Write the custom identity into the token
        ClaimActions.MapJsonKey(AbpWeChatClaimTypes.OpenId, "openid");
        ClaimActions.MapJsonKey(AbpWeChatClaimTypes.UnionId, "unionid");//If the official account is associated with the mini program, this can be used
        ClaimActions.MapJsonKey(AbpWeChatClaimTypes.NickName, "nickname");
        ClaimActions.MapJsonKey(AbpWeChatClaimTypes.Sex, "sex", ClaimValueTypes.Integer);
        ClaimActions.MapJsonKey(AbpWeChatClaimTypes.Country, "country");
        ClaimActions.MapJsonKey(AbpWeChatClaimTypes.Province, "province");
        ClaimActions.MapJsonKey(AbpWeChatClaimTypes.City, "city");
        ClaimActions.MapJsonKey(AbpWeChatClaimTypes.AvatarUrl, "headimgurl");
        ClaimActions.MapCustomJson(AbpWeChatClaimTypes.Privilege, user =>
        {
            return string.Join(",", user.GetStrings("privilege"));
        });
    }
}
