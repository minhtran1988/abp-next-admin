namespace LINGYUN.Abp.Notifications.WeChat.MiniProgram;

/// <summary>
/// TODO: Later improvements, configuration items are integrated into <see cref="LINGYUN.Abp.WeChat.MiniProgram.AbpWeChatMiniProgramOptions"/>
/// </summary>
public class AbpNotificationsWeChatMiniProgramOptions
{
    /// <summary>
    /// Default applet template
    /// </summary>
    public string DefaultTemplateId { get; set; } = default!;
    /// <summary>
    /// Default jump applet type
    /// </summary>
    public string DefaultState { get; set; } = "developer";
    /// <summary>
    /// Default applet language
    /// </summary>
    public string DefaultLanguage { get; set; } = "zh_CN";
}
