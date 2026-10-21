using System.ComponentModel;

namespace LINGYUN.Abp.WeChat.Work.ExternalContact.Attachments.Models;
/// <summary>
/// Attachment type
/// </summary>
[Description("Attachment Type")]
public enum AttachmentType
{
    /// <summary>
    /// Moments
    /// </summary>
    [Description("Moments")]
    Moments = 1,
    /// <summary>
    /// Product album
    /// </summary>
    [Description("Product Album")]
    ProductCatalogue = 2,
}
