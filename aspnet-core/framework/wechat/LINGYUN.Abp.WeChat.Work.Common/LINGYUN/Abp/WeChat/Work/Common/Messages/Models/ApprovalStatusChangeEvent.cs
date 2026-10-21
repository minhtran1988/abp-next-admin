using LINGYUN.Abp.WeChat.Common.Messages;
using System.Collections.Generic;
using System.Xml.Serialization;
using Volo.Abp.EventBus;

namespace LINGYUN.Abp.WeChat.Work.Common.Messages.Models;
/// <summary>
/// Approval status notification event
/// </summary>
[EventName("open_approval_change")]
public class ApprovalStatusChangeEvent : WeChatWorkEventMessage
{
    /// <summary>
    /// Event KEY value
    /// </summary>
    [XmlElement("EventKey")]
    public string EventKey { get; set; } = default!;
    /// <summary>
    /// Approval information
    /// </summary>
    [XmlElement("ApprovalInfo")]
    public ApprovalInfo ApprovalInfo { get; set; } = default!;
    public override WeChatMessageEto ToEto()
    {
        return new WeChatWorkEventMessageEto<ApprovalStatusChangeEvent>(this);
    }
}

public class ApprovalInfo
{
    /// <summary>
    /// Approval order number, customized by the developer when initiating the application
    /// </summary>
    [XmlElement("ThirdNo")]
    public string ThirdNo { get; set; } = default!;
    /// <summary>
    /// Approval template name
    /// </summary>
    [XmlElement("OpenSpName")]
    public string OpenSpName { get; set; } = default!;
    /// <summary>
    /// Approval template id
    /// </summary>
    [XmlElement("OpenTemplateId")]
    public string OpenTemplateId { get; set; } = default!;
    /// <summary>
    /// Current approval status of the application:
    /// 1-Under approval;
    /// 2-passed;
    /// 3-Rejected;
    /// 4-Cancelled
    /// </summary>
    [XmlElement("OpenSpStatus")]
    public byte OpenSpStatus { get; set; }
    /// <summary>
    /// Submit application time
    /// </summary>
    [XmlElement("ApplyTime")]
    public int ApplyTime { get; set; }
    /// <summary>
    /// Submitter's name
    /// </summary>
    [XmlElement("ApplyUserName")]
    public string ApplyUserName { get; set; } = default!;
    /// <summary>
    /// Submitter userid
    /// </summary>
    [XmlElement("ApplyUserId")]
    public string ApplyUserId { get; set; } = default!;
    /// <summary>
    /// The submitter's department
    /// </summary>
    [XmlElement("ApplyUserParty")]
    public string? ApplyUserParty { get; set; }
    /// <summary>
    /// Submitter avatar
    /// </summary>
    [XmlElement("ApplyUserImage")]
    public string? ApplyUserImage { get; set; }
    /// <summary>
    /// Approval process information
    /// </summary>
    [XmlElement("ApprovalNodes")]
    public List<ApprovalNode> ApprovalNodes { get; set; } = default!;
    /// <summary>
    /// Cc information, there may be multiple cc people
    /// </summary>
    [XmlElement("NotifyNodes")]
    public List<NotifyNode>? NotifyNodes { get; set; }
    /// <summary>
    /// Current approval node: 0-the first approval node; 1-the second approval node...and so on
    /// </summary>
    [XmlElement("approverstep")]
    public int ApproverStep { get; set; }
}

/// <summary>
/// Approval process information, there can be multiple approval nodes
/// </summary>
[XmlRoot("ApprovalNode")]
public class ApprovalNode
{
    /// <summary>
    /// Node approval operation status:
    /// 1-Under approval;
    /// 2-Agreed;
    /// 3-Rejected;
    /// 4-Referred for review
    /// </summary>
    [XmlElement("NodeStatus")]
    public byte NodeStatus { get; set; }
    /// <summary>
    /// Approval node attributes:
    /// 1-or sign;
    /// 2-Countersign
    /// </summary>
    [XmlElement("NodeAttr")]
    public byte NodeAttr { get; set; }
    /// <summary>
    /// Approval node type:
    /// 1-Fixed member;
    /// 2-label;
    /// 3-Superior
    /// </summary>
    [XmlElement("NodeType")]
    public byte NodeType { get; set; }
    /// <summary>
    /// Approval node information. When the node is a label or superior, a node may have multiple branches.
    /// </summary>
    [XmlElement("Items")]
    public List<ApprovalNodeItem> Items { get; set; } = default!;
}
/// <summary>
/// Approval node branches. When the node is a label or superior, a node may have multiple branches.
/// </summary>
[XmlRoot("Item")]
public class ApprovalNodeItem
{
    /// <summary>
    /// Name of branch approver
    /// </summary>
    [XmlElement("ItemName")]
    public string ItemName { get; set; } = default!;
    /// <summary>
    /// Branch approver userid
    /// </summary>
    [XmlElement("ItemUserId")]
    public string ItemUserId { get; set; } = default!;
    /// <summary>
    /// Branch approver avatar
    /// </summary>
    [XmlElement("ItemImage")]
    public string? ItemImage { get; set; }
    /// <summary>
    /// Branch approval approval operation status:
    /// 1-Under approval;
    /// 2-Agreed;
    /// 3-Rejected;
    /// 4-Referred for review
    /// </summary>
    [XmlElement("ItemStatus")]
    public byte ItemStatus { get; set; }
    /// <summary>
    /// Approval opinions of branch approvers
    /// </summary>
    [XmlElement("ItemSpeech")]
    public string? ItemSpeech { get; set; }
    /// <summary>
    /// Approval opinions of branch approvers
    /// </summary>
    [XmlElement("ItemOpTime")]
    public int ItemOpTime { get; set; }
}
/// <summary>
/// Cc information
/// </summary>
[XmlRoot("NotifyNode")]
public class NotifyNode
{
    /// <summary>
    /// Cc’s name
    /// </summary>
    [XmlElement("ItemName")]
    public string ItemName { get; set; } = default!;
    /// <summary>
    /// CC person userid
    /// </summary>
    [XmlElement("ItemUserId")]
    public string ItemUserId { get; set; } = default!;
    /// <summary>
    /// Cc person's avatar
    /// </summary>
    [XmlElement("ItemImage")]
    public string? ItemImage { get; set; }
}