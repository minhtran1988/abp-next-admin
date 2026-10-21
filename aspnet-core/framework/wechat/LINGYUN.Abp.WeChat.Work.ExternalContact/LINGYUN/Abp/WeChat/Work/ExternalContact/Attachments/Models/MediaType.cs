using System.ComponentModel;

namespace LINGYUN.Abp.WeChat.Work.ExternalContact.Attachments.Models;
/// <summary>
/// Media file type
/// </summary>
[Description("Media File Type")]
public enum MediaType
{
    /// <summary>
    /// Picture
    /// </summary>
    [Description("Picture")]
    Image = 1,
    /// <summary>
    /// Video
    /// </summary>
    [Description("video")]
    Video = 2,
    /// <summary>
    /// Ordinary file
    /// </summary>
    [Description("Normal file")]
    File = 3,
}
