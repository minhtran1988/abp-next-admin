using LINGYUN.Abp.WeChat.Common.Messages;
using LINGYUN.Abp.WeChat.Work.Common.Messages;
using System.Xml.Serialization;
using Volo.Abp.EventBus;

namespace LINGYUN.Abp.WeChat.Work.Contacts.Messages.Models;
/// <summary>
/// Add department event
/// </summary>
[EventName("create_party")]
public class CreateDepartmentEvent : DepartmentUpdateEvent
{
    /// <summary>
    /// Department sorting
    /// </summary>
    [XmlElement("Order", IsNullable = true)]
    public int? Order { get; set; }
    public override WeChatMessageEto ToEto()
    {
        return new WeChatWorkEventMessageEto<CreateDepartmentEvent>(this);
    }
}
