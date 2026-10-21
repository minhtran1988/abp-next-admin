namespace LINGYUN.Abp.Notifications.WeChat.Work;

/// <summary>
/// Enterprise WeChat channel data (load) base class
/// </summary>
/// <remarks>
/// Each payload corresponds to an enterprise WeChat message carrier, and the form is explicitly declared through <see cref="Kind"/>.
/// The channel side uses the factory dictionary for O(1) dispatch, and no longer relies on the if/else detection sequence.
/// <para>
/// Existence is declaratively derived from <see cref="NotificationChannelDataAttribute.Presence"/>:
/// Each payload has only one "identification key", so the default "all attributes participating in the matching exist" is the correct semantics.
/// There is no need to implement the determination method in the payload type.
/// </para>
/// </remarks>
public abstract class WeChatWorkChannelData : NotificationChannelData
{
    /// <summary>
    /// Message carrier type
    /// </summary>
    public abstract WeChatWorkMessageKind Kind { get; }
}
