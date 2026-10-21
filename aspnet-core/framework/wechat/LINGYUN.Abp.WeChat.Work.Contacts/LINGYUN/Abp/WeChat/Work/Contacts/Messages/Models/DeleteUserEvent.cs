using LINGYUN.Abp.WeChat.Common.Messages;
using LINGYUN.Abp.WeChat.Work.Common.Messages;
using System.Xml.Serialization;
using Volo.Abp.EventBus;

namespace LINGYUN.Abp.WeChat.Work.Contacts.Messages.Models;
/// <summary>
/// Delete member event
/// </summary>
[EventName("delete_user")]
public class DeleteUserEvent : WeChatWorkEventMessage
{
    /// <summary>
    /// Member UserID of change information
    /// </summary>
    [XmlElement("UserID")]
    public string UserId { get; set; } = default!;

    public override WeChatMessageEto ToEto()
    {
        return new WeChatWorkEventMessageEto<DeleteUserEvent>(this);
    }
}
