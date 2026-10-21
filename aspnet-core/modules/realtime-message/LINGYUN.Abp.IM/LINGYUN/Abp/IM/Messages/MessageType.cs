namespace LINGYUN.Abp.IM.Messages;

public enum MessageType
{
    /// <summary>
    /// Text message
    /// </summary>
    Text = 0,
    /// <summary>
    /// Picture message
    /// </summary>
    Image = 10,
    /// <summary>
    /// link
    /// </summary>
    Link = 20,
    /// <summary>
    /// Video
    /// </summary>
    Video = 30,
    /// <summary>
    /// Audio
    /// </summary>
    Voice = 40,
    /// <summary>
    /// File
    /// </summary>
    File = 50,
    /// <summary>
    /// Notification
    /// Generally used for error handling
    /// </summary>
    Notifier = 100,
}
