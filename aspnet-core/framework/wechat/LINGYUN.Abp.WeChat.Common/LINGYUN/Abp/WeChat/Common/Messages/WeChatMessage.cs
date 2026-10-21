using System;
using System.Xml.Serialization;

namespace LINGYUN.Abp.WeChat.Common.Messages;
/// <summary>
/// WeChat message
/// </summary>
[Serializable]
[XmlRoot("xml")]
public abstract class WeChatMessage
{
    /// <summary>
    /// Developer WeChat ID
    /// </summary>
    [XmlElement("ToUserName")]
    public string ToUserName { get; set; } = default!;
    /// <summary>
    /// Sender account (an OpenID)
    /// </summary>
    [XmlElement("FromUserName")]
    public string FromUserName { get; set; } = default!;
    /// <summary>
    /// Message creation time (integer)
    /// </summary>
    [XmlElement("CreateTime")]
    public int CreateTime { get; set; }
    /// <summary>
    /// Message type, event
    /// </summary>
    [XmlElement("MsgType")]
    public string MsgType { get; set; } = default!;

    public abstract WeChatMessageEto ToEto();

    public virtual string SerializeToJson()
    {
        return WeChatObjectSerializeExtensions.SerializeToJson(this);
    }

    public virtual string SerializeToXml()
    {
        return this.SerializeWeChatMessage();
    }
}
