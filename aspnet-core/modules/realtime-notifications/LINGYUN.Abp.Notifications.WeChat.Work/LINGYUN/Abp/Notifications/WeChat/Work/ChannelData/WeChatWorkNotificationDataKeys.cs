using System;

namespace LINGYUN.Abp.Notifications.WeChat.Work;

/// <summary>
/// Enterprise WeChat notification data key
/// </summary>
/// <remarks>
/// The key name is consistent with the historical implementation (<c>wx-work:</c> prefix) to ensure that online producers are not affected.
/// </remarks>
public static class WeChatWorkNotificationDataKeys
{
    public const string Prefix = "wx-work:";

    /// <summary>
    /// Specify the member to receive the message
    /// </summary>
    public const string ToUser = Prefix + "to_user";
    /// <summary>
    /// Specify the department to receive the message
    /// </summary>
    public const string ToParty = Prefix + "to_party";
    /// <summary>
    /// Specify the label to receive the message
    /// </summary>
    public const string ToTag = Prefix + "to_tag";
    /// <summary>
    /// Application ID
    /// </summary>
    public const string AgentId = Prefix + "agent_id";
    /// <summary>
    /// Is it confidential information?
    /// </summary>
    public const string Safe = Prefix + "safe";
    /// <summary>
    /// Whether to enable id translation
    /// </summary>
    public const string EnableIdTrans = Prefix + "enable_id_trans";
    /// <summary>
    /// Whether to enable duplicate message checking
    /// </summary>
    public const string EnableDuplicateCheck = Prefix + "enable_duplicate_check";
    /// <summary>
    /// The time interval for repeated message checking
    /// </summary>
    public const string DuplicateCheckInterval = Prefix + "duplicate_check_interval";

    public const string MiniProgramMessage = Prefix + "mini_program_message";
    public const string MpNewMessage = Prefix + "mp_new_message";
    public const string TemplateCardMessage = Prefix + "template_card_message";
    public const string TextCardMessage = Prefix + "text_card_message";
    public const string VideoMessage = Prefix + "video_message";
    public const string VoiceMessage = Prefix + "voice_message";
    public const string FileMessage = Prefix + "file_message";
}
