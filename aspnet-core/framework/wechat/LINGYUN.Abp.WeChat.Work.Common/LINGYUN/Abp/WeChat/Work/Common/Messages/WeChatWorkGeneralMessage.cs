using LINGYUN.Abp.WeChat.Common.Messages;
using System.Xml.Serialization;

namespace LINGYUN.Abp.WeChat.Work.Common.Messages;
/// <summary>
/// Ordinary corporate WeChat messages
/// </summary>
public abstract class WeChatWorkGeneralMessage : WeChatGeneralMessage
{
    /// <summary>
    /// The id of the enterprise application, integer type. Can be viewed on the app’s settings page
    /// </summary>
    [XmlElement("AgentID")]
    public int AgentId { get; set; }
}
