using LINGYUN.Abp.WeChat.Common.Messages;
using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Volo.Abp.EventBus;

namespace LINGYUN.Abp.WeChat.Work.Common.Messages.Models;
/// <summary>
/// Enterprise WeChat "Approval Application" approval status notification event
/// </summary>
[EventName("sys_approval_change")]
public class SysApprovalStatusChangeEvent : WeChatWorkEventMessage
{
    /// <summary>
    /// Approval information
    /// </summary>
    [XmlElement("ApprovalInfo")]
    public SysApprovalInfo ApprovalInfo { get; set; } = default!;

    public override WeChatMessageEto ToEto()
    {
        return new WeChatWorkEventMessageEto<SysApprovalStatusChangeEvent>(this);
    }
}

public class SysApprovalInfo
{
    /// <summary>
    /// Approval number (string type)
    /// </summary>
    [XmlElement("SpNoStr")]
    public string SpNoStr { get; set; } = default!;
    /// <summary>
    /// Approval application type name (approval template name)
    /// </summary>
    [XmlElement("SpName")]
    public string SpName { get; set; } = default!;
    /// <summary>
    /// Application status: 1-under approval; 2-passed; 3-rejected; 4-withdrawn; 6-withdrawn after approval; 7-deleted; 10-paid
    /// </summary>
    [XmlElement("SpStatus")]
    public byte SpStatus { get; set; }
    /// <summary>
    /// Approval template id. It can be obtained in "Get Approval Application Details", "Approval Status Change Callback Notification", or in the template editing page link of the approval template.
    /// </summary>
    [XmlElement("TemplateId")]
    public string TemplateId { get; set; } = default!;
    /// <summary>
    /// Approval application submission time, Unix timestamp
    /// </summary>
    [XmlElement("ApplyTime")]
    public int ApplyTime { get; set; }
    /// <summary>
    /// Applicant information
    /// </summary>
    [XmlElement("Applyer")]
    public SysApprovalApplyer Applyer { get; set; } = default!;
    /// <summary>
    /// Approval process information, there may be multiple approval nodes.
    /// </summary>
    [XmlElement("SpRecord")]
    public List<SysApprovalRecord>? SpRecord { get; set; }
    /// <summary>
    /// CC information, there may be multiple CC nodes
    /// </summary>
    [XmlElement("Notifyer")]
    public List<SysApprovalNotifyer>? Notifyer { get; set; }
    /// <summary>
    /// Approval application remark information, there may be multiple remark nodes
    /// </summary>
    [XmlElement("Comments")]
    public List<SysApprovalComment>? Comments { get; set; }
    /// <summary>
    /// Approval process list
    /// </summary>
    [XmlElement("ProcessList")]
    public List<SysApprovalProcess>? ProcessList { get; set; }
    /// <summary>
    /// Approval application status change types: 1-bill of lading; 2-agree; 3-reject; 4-transfer; 5-urge; 6-withdraw; 8-withdraw after approval; 10-add remarks; 11-return to designated approver; 12-add approver; 13-sign and agree; 14-processed; 15-transferred
    /// </summary>
    [XmlElement("StatuChangeEvent")]
    public byte StatuChangeEvent { get; set; }
    /// <summary>
    /// Approval number
    /// </summary>
    /// <remarks>
    /// The bureau school approval form does not return this field. Other types of approval forms will return this field. This field is not recommended.
    /// </remarks>
    [XmlElement("SpNo")]
    [Obsolete("This field will not be returned in the bureau school approval form. Other types of approval forms will return this field. This field is not recommended.")]
    public string? SpNo { get; set; }
}

public class SysApprovalApplyer
{
    /// <summary>
    /// Applicant userid
    /// </summary>
    [XmlElement("UserId")]
    public string UserId { get; set; } = default!;
    /// <summary>
    /// The pid of the applicant’s department
    /// </summary>
    [XmlElement("Party")]
    public string? Party { get; set; }
}

public class SysApprovalRecord
{
    /// <summary>
    /// Approval node status: 1-Approval; 2-Agreed; 3-Rejected; 4-Referred for review
    /// </summary>
    [XmlElement("SpStatus")]
    public byte SpStatus { get; set; }
    /// <summary>
    /// Node approval method: 1-or signature; 2-countersign
    /// </summary>
    [XmlElement("ApproverAttr")]
    public byte ApproverAttr { get; set; }
    /// <summary>
    /// Node approval method: 1-or signature; 2-countersign
    /// </summary>
    [XmlElement("Details")]
    public List<SysApprovalRecordDetail>? Details { get; set; }
}

public class SysApprovalRecordDetail
{
    /// <summary>
    /// Branch approver
    /// </summary>
    [XmlElement("Approver")]
    public SysApprovalApplyer Approver { get; set; } = default!;
    /// <summary>
    /// Approval opinion field
    /// </summary>
    [XmlElement("Speech")]
    public string? Speech { get; set; }
    /// <summary>
    /// Approval status of branch approver: 1-Approval; 2-Agreed; 3-Rejected; 4-Referred for review
    /// </summary>
    [XmlElement("SpStatus")]
    public byte SpStatus { get; set; }
    /// <summary>
    /// Node branch approver's approval operation time, 0 means it has not been operated yet
    /// </summary>
    [XmlElement("SpTime")]
    public int SpTime { get; set; }
    /// <summary>
    /// Attachment of approval opinions of the node branch approver, assigned the value media_id. For specific usage, please refer to: Documentation - Obtaining temporary materials
    /// </summary>
    [XmlElement("Attach")]
    public string? Attach { get; set; }
}

public class SysApprovalNotifyer
{
    /// <summary>
    /// Node CC person userid
    /// </summary>
    [XmlElement("UserId")]
    public string UserId { get; set; } = default!;
}

public class SysApprovalComment
{
    /// <summary>
    /// Note person information
    /// </summary>
    [XmlElement("CommentUserInfo")]
    public SysApprovalCommenter CommentUserInfo { get; set; } = default!;
    /// <summary>
    /// Note submission time
    /// </summary>
    [XmlElement("CommentTime")]
    public int CommentTime { get; set; }
    /// <summary>
    /// Remark text content
    /// </summary>
    [XmlElement("CommentContent")]
    public string? CommentContent { get; set; }
    /// <summary>
    /// Remark id
    /// </summary>
    [XmlElement("CommentId")]
    public string CommentId { get; set; } = default!;
    /// <summary>
    /// Note the attachment, the value is the attachment media_id
    /// </summary>
    [XmlElement("Attach")]
    public string? Attach { get; set; }
}

public class SysApprovalCommenter
{
    /// <summary>
    /// Node CC person userid
    /// </summary>
    [XmlElement("UserId")]
    public string UserId { get; set; } = default!;
}

public class SysApprovalProcess
{
    /// <summary>
    /// Process node
    /// </summary>
    [XmlElement("NodeList")]
    public List<SysApprovalProcessNode>? NodeList { get; set; }
}

public class SysApprovalProcessNode
{
    /// <summary>
    /// Node type 1 approver 2 carbon copy person 3 handler
    /// </summary>
    [XmlElement("NodeType")]
    public byte NodeType { get; set; }
    /// <summary>
    /// Node status 1-approval; 2-agree; 3-reject; 4-transfer; 11-return to designated approver; 12-sign; 13-agree and sign; 14-process; 15-transfer
    /// </summary>
    [XmlElement("SpStatus")]
    public byte SpStatus { get; set; }
    /// <summary>
    /// Multi-person processing method 1-countersign; 2-or sign 3-approval in sequence
    /// </summary>
    [XmlElement("ApvRel")]
    public byte ApvRel { get; set; }
    /// <summary>
    /// Child node list
    /// </summary>
    [XmlElement("SubNodeList")]
    public List<SysApprovalProcessSubNode>? SubNodeList { get; set; }
}

public class SysApprovalProcessSubNode
{
    /// <summary>
    /// Processor information
    /// </summary>
    [XmlElement("UserInfo")]
    public SysApprovalProcesser UserInfo { get; set; } = default!;
    /// <summary>
    /// Approval/Processing Opinions
    /// </summary>
    [XmlElement("Speech")]
    public string? Speech { get; set; }
    /// <summary>
    /// Sub-node status 1-under approval; 2-agree; 3-reject; 4-transfer; 11-return to designated approver; 12-sign; 13-agree and sign; 14-process; 15-transfer
    /// </summary>
    [XmlElement("SpYj")]
    public byte SpYj { get; set; }
    /// <summary>
    /// Operation time
    /// </summary>
    [XmlElement("Sptime")]
    public int Sptime { get; set; }
    /// <summary>
    /// Note the attachment, the value is the attachment media_id
    /// </summary>
    [XmlElement("MediaIds")]
    public string? MediaIds { get; set; }
}

public class SysApprovalProcesser
{
    /// <summary>
    /// Processor userid
    /// </summary>
    [XmlElement("UserId")]
    public string UserId { get; set; } = default!;
}