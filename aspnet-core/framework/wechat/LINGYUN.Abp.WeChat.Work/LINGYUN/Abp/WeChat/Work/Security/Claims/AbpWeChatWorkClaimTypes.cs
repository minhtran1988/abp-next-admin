namespace LINGYUN.Abp.WeChat.Work.Security.Claims;
public static class AbpWeChatWorkClaimTypes
{
    /// <summary>
    /// unique identifier
    /// </summary>
    public static string UserId { get; set; } = "wecom_userid";
    /// <summary>
    /// QR code business card
    /// </summary>
    public static string QrCode { get; set; } = "wecom_qr_code";
    /// <summary>
    /// Business email
    /// </summary>
    public static string BizMail { get; set; } = "wecom_biz_mail";
    /// <summary>
    /// address
    /// </summary>
    public static string Address { get; set; } = "wecom_address";
}
