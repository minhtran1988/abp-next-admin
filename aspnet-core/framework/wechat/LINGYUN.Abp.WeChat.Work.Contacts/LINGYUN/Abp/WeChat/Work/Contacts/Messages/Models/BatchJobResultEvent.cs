using LINGYUN.Abp.WeChat.Common.Messages;
using LINGYUN.Abp.WeChat.Work.Common.Messages;
using System.Xml.Serialization;
using Volo.Abp.EventBus;

namespace LINGYUN.Abp.WeChat.Work.Contacts.Messages.Models;
/// <summary>
/// Asynchronous task completion notification
/// </summary>
/// <remarks>
/// This event is used by members to receive notification of the completion of task execution when members use the asynchronous task interface.
/// </remarks>
[EventName("batch_job_result")]
public class BatchJobResultEvent : WeChatWorkEventMessage
{
    /// <summary>
    /// Asynchronous task information
    /// </summary>
    [XmlElement("BatchJob")]
    public BatchJob BatchJob { get; set; } = default!;

    public override WeChatMessageEto ToEto()
    {
        return new WeChatWorkEventMessageEto<BatchJobResultEvent>(this);
    }
}

public class BatchJob
{
    /// <summary>
    /// Asynchronous task id, the maximum length is 64 characters
    /// </summary>
    [XmlElement("JobId")]
    public string JobId { get; set; } = default!;
    /// <summary>
    /// Operation type, string, currently includes: sync_user (incremental update of members), replace_user (full coverage of members), invite_user (invite members to follow), replace_party (full coverage of departments)
    /// </summary>
    [XmlElement("JobType")]
    public string JobType { get; set; } = default!;
    /// <summary>
    /// return code
    /// </summary>
    [XmlElement("ErrCode")]
    public int ErrCode { get; set; }
    /// <summary>
    /// Text description of the return code
    /// </summary>
    [XmlElement("ErrMsg")]
    public string? ErrMsg { get; set; }
}