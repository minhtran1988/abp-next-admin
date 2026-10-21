using System;

namespace LINGYUN.Abp.Gdpr;
/// <summary>
/// AbpCookie consent options
/// See: https://abp.io/docs/latest/modules/gdpr#abpcookieconsentoptions
/// </summary>
public class AbpCookieConsentOptions
{
    /// <summary>
    /// Enable or disable the Cookie Consent feature
    /// </summary>
    public bool IsEnabled { get; set; }
    /// <summary>
    /// Define cookie policy page URL.<br />
    /// After setting, the "Cookie Policy" page URL will be automatically added to the Cookie Consent Statement.<br />
    /// Therefore, users can check the cookie policy before accepting cookie consent.<br />
    /// You can set this to a local address or a full URL, for example:<br />
    /// /CookiePolicy<br />
    /// https://example.com/cookie-policy
    /// </summary>
    public string CookiePolicyUrl { get; set; }
    /// <summary>
    /// Define privacy policy page URL.<br />
    /// After setting, the "Privacy Policy" page URL will be automatically added to the Cookie Consent Statement.<br />
    /// Therefore, users can review the privacy policy before accepting cookie consent.<br />
    /// You can set this to a local address or a full URL, for example:<br />
    /// /PrivacyPolicy<br />
    /// https://example.com/privacy-policy
    /// </summary>
    public string PrivacyPolicyUrl { get; set; }
    /// <summary>
    /// Define cookie expiration time for cookie consent.<br />
    /// By default, when accepting cookie consent, it sets the cookie to be valid for 6 months.
    /// </summary>
    public TimeSpan Expiration { get; set; }
    public AbpCookieConsentOptions()
    {
        IsEnabled = true;
        CookiePolicyUrl = "/CookiePolicy";
        PrivacyPolicyUrl = "/PrivacyPolicy";
        Expiration = TimeSpan.FromDays(180);
    }
}
