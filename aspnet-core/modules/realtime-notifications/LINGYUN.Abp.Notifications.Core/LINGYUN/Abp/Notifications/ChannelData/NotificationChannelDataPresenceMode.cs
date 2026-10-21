namespace LINGYUN.Abp.Notifications;

/// <summary>
/// Channel data (load) existence determination mode
/// </summary>
public enum NotificationChannelDataPresenceMode
{
    /// <summary>
    /// Payload is always considered present
    /// </summary>
    /// <remarks>
    /// Suitable for payloads that do not rely on notification data (constructed by the channel at runtime).
    /// </remarks>
    None = 0,

    /// <summary>
    /// When all attributes participating in the match exist, the payload is considered to exist (default)
    /// </summary>
    /// <remarks>
    /// Suitable for scenarios where "a certain key is the payload identifier", such as various message types of Enterprise WeChat.
    /// </remarks>
    DeclaredProperties = 1,

    /// <summary>
    /// When any of the attributes participating in the match exists, the payload is considered to exist.
    /// </summary>
    /// <remarks>
    /// Suitable for scenarios where "any one of multiple optional keys is enough",
    /// For example, PushPlus's webhook or callback.
    /// </remarks>
    AnyDeclaredProperty = 2
}
