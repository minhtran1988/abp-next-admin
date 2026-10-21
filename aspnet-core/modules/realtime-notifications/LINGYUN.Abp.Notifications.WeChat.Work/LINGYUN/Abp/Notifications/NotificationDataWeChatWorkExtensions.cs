using LINGYUN.Abp.WeChat.Work.Messages.Models;
using Newtonsoft.Json;

namespace LINGYUN.Abp.Notifications;
public static partial class NotificationDataWeChatWorkExtensions
{
    private const string Prefix = "wx-work:";
    private const string AgentIdKey = Prefix + "agent_id";
    private const string ToTagKey = Prefix + "to_tag";
    private const string ToPartyKey = Prefix + "to_party";
    private const string SafeKey = Prefix + "safe";
    private const string EnableIdTransKey = Prefix + "enable_id_trans";
    private const string EnableDuplicateCheckKey = Prefix + "enable_duplicate_check";
    private const string DuplicateCheckIntervalKey = Prefix + "duplicate_check_interval";

    /// <summary>
    /// Set to send to all applications
    /// </summary>
    /// <param name="notificationData"></param>
    /// <returns></returns>
    public static void WithAllAgent(
        this NotificationData notificationData)
    {
        notificationData.SetAgentId("@all");
    }

    /// <summary>
    /// Set message application ID
    /// </summary>
    /// <param name="notificationData"></param>
    /// <param name="agentId"></param>
    /// <returns></returns>
    public static void SetAgentId(
        this NotificationData notificationData,
        string agentId)
    {
        notificationData.TrySetData(AgentIdKey, agentId);
    }
    /// <summary>
    /// Get the message application ID
    /// </summary>
    /// <param name="notificationData"></param>
    public static string? GetAgentIdOrNull(
        this NotificationData notificationData)
    {
        return notificationData.TryGetData(AgentIdKey)?.ToString();
    }
    /// <summary>
    /// Specify the tag to receive the message, tag ID list, multiple receivers separated by '|', up to 100 are supported.
    /// </summary>
    /// <param name="notificationData"></param>
    /// <param name="tag"></param>
    /// <returns></returns>
    public static void SetTag(
        this NotificationData notificationData,
        string tag)
    {
        notificationData.TrySetData(ToTagKey, tag);
    }
    /// <summary>
    /// Get the label of the received message
    /// </summary>
    /// <param name="notificationData"></param>
    public static string? GetTagOrNull(
        this NotificationData notificationData)
    {
        return notificationData.TryGetData(ToTagKey)?.ToString();
    }
    /// <summary>
    /// Specify the department to receive the message, department ID list, multiple recipients separated by '|', up to 100 are supported.
    /// </summary>
    /// <param name="notificationData"></param>
    /// <param name="party"></param>
    /// <returns></returns>
    public static void SetParty(
        this NotificationData notificationData,
        string party)
    {
        notificationData.TrySetData(ToPartyKey, party);
    }
    /// <summary>
    /// Get the department that received the message
    /// </summary>
    /// <param name="notificationData"></param>
    public static string? GetPartyOrNull(
        this NotificationData notificationData)
    {
        return notificationData.TryGetData(ToPartyKey)?.ToString();
    }

    /// <summary>
    /// Set whether to keep the message confidential (only supported by text messages)
    /// </summary>
    /// <param name="notificationData"></param>
    /// <param name="safe"></param>
    public static void SetSafe(
        this NotificationData notificationData,
        bool safe)
    {
        notificationData.TrySetData(SafeKey, safe ? 1 : 0);
    }

    /// <summary>
    /// Get whether the message is confidential, default 0 (not confidential)
    /// </summary>
    /// <remarks>
    /// Enterprise WeChat requirements int: 0 means no, 1 means yes
    /// </remarks>
    /// <param name="notificationData"></param>
    public static int GetSafe(
        this NotificationData notificationData)
    {
        return notificationData.TryGetData(SafeKey) is bool safe
            ? (safe ? 1 : 0)
            : 0;
    }

    /// <summary>
    /// Set whether to enable id translation
    /// </summary>
    /// <param name="notificationData"></param>
    /// <param name="enableIdTrans"></param>
    public static void SetEnableIdTrans(
        this NotificationData notificationData,
        bool enableIdTrans)
    {
        notificationData.TrySetData(EnableIdTransKey, enableIdTrans ? (byte)1 : (byte)0);
    }

    /// <summary>
    /// Get whether ID translation is enabled, default 0 (not enabled)
    /// </summary>
    /// <remarks>
    /// Enterprise WeChat requires byte: 0 means no, 1 means yes
    /// </remarks>
    /// <param name="notificationData"></param>
    public static byte GetEnableIdTrans(
        this NotificationData notificationData)
    {
        return notificationData.TryGetData(EnableIdTransKey) is bool enable
            ? (enable ? (byte)1 : (byte)0)
            : (byte)0;
    }

    /// <summary>
    /// Set whether to enable duplicate message checking
    /// </summary>
    /// <param name="notificationData"></param>
    /// <param name="enableDuplicateCheck"></param>
    public static void SetEnableDuplicateCheck(
        this NotificationData notificationData,
        bool enableDuplicateCheck)
    {
        notificationData.TrySetData(EnableDuplicateCheckKey, enableDuplicateCheck ? (byte)1 : (byte)0);
    }

    /// <summary>
    /// Get whether duplicate message checking is enabled, default 0 (not enabled)
    /// </summary>
    /// <remarks>
    /// Enterprise WeChat requires byte: 0 means no, 1 means yes
    /// </remarks>
    /// <param name="notificationData"></param>
    public static byte GetEnableDuplicateCheck(
        this NotificationData notificationData)
    {
        return notificationData.TryGetData(EnableDuplicateCheckKey) is bool enable
            ? (enable ? (byte)1 : (byte)0)
            : (byte)0;
    }

    /// <summary>
    /// Set the time interval (seconds) for duplicate message checking
    /// </summary>
    /// <param name="notificationData"></param>
    /// <param name="interval"></param>
    public static void SetDuplicateCheckInterval(
        this NotificationData notificationData,
        int interval)
    {
        notificationData.TrySetData(DuplicateCheckIntervalKey, interval);
    }

    /// <summary>
    /// Get the time interval (seconds) for duplicate message checking, default 1800
    /// </summary>
    /// <param name="notificationData"></param>
    public static int GetDuplicateCheckInterval(
        this NotificationData notificationData)
    {
        return notificationData.TryGetData(DuplicateCheckIntervalKey) is int interval
            ? interval
            : 1800;
    }

    /// <summary>
    /// Try to get the message payload
    /// </summary>
    /// <remarks>
    /// The message payload is written into the property bag as a JSON string (see each <c>With*Message</c> method),
    /// Therefore, it is deserialized and restored according to JSON. <c>TMessage</c> can be restored directly if it is a specific type;
    /// Abstract types (such as <c>TemplateCard</c>) require a JSON converter to participate in type determination.
    /// </remarks>
    /// <param name="notificationData"></param>
    /// <param name="messageKey">Message key</param>
    /// <param name="message">Message payload</param>
    /// <returns></returns>
    public static bool TryGetMessagePayload<TMessage>(
        this NotificationData notificationData,
        string messageKey,
        out TMessage? message)
        where TMessage : class
    {
        message = null;

        var payload = notificationData.TryGetData(messageKey);
        if (payload == null)
        {
            return false;
        }

        // The value in the property bag may be a JSON string, or it may have been deserialized into a JSON node
        var json = payload as string ?? payload.ToString();

        if (string.IsNullOrWhiteSpace(json))
        {
            return false;
        }

        try
        {
            message = JsonConvert.DeserializeObject<TMessage>(json, JsonSettings);
        }
        catch (JsonException)
        {
            // Return false when the payload is illegal, and it is up to the caller to decide whether to fall back
            return false;
        }

        return message != null;
    }

    private static readonly JsonSerializerSettings JsonSettings = new()
    {
        Converters = { new TemplateCardJsonConverter() }
    };
}
