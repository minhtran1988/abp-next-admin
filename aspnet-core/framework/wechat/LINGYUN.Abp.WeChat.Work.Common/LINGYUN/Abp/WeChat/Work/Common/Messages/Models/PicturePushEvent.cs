using System.Xml.Serialization;

namespace LINGYUN.Abp.WeChat.Work.Common.Messages.Models;
/// <summary>
/// Pop up the event push of the system picture
/// </summary>
public abstract class PicturePushEvent : WeChatWorkEventMessage
{
    /// <summary>
    /// Event KEY value
    /// </summary>
    [XmlElement("EventKey")]
    public string EventKey { get; set; } = default!;
    /// <summary>
    /// Event KEY value
    /// </summary>
    [XmlElement("SendPicsInfo")]
    public PictureInfo SendPicsInfo { get; set; } = default!;
}

public class PictureInfo
{
    /// <summary>
    /// Number of pictures sent
    /// </summary>
    [XmlElement("Count")]
    public int Count { get; set; }
    /// <summary>
    /// Number of pictures sent
    /// </summary>
    [XmlArrayItem("Item")]
    public Picture? Picture { get; set; }
}

[XmlRoot("PicList")]
public class Picture
{
    /// <summary>
    /// The MD5 value of the image can be used to verify the received image if the developer needs it.
    /// </summary>
    [XmlElement("PicMd5Sum")]
    public string PicMd5Sum { get; set; } = default!;
}
