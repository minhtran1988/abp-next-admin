using LINGYUN.Abp.WeChat.Common.Messages;
using System.Xml.Serialization;

namespace LINGYUN.Abp.WeChat.Work.Common.Messages;
/// <summary>
/// Enterprise WeChat event message
/// </summary>
public abstract class WeChatWorkEventMessage : WeChatEventMessage
{
    /// <summary>
    /// The id of the enterprise application, integer type. Can be viewed on the app’s settings page
    /// </summary>
    [XmlElement("AgentID")]
    public int AgentId { get; set; }
}
