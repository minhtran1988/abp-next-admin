using JetBrains.Annotations;
using LINGYUN.Abp.WeChat.Work.ExternalContact.Attachments.Models;
using Volo.Abp;
using Volo.Abp.Content;

namespace LINGYUN.Abp.WeChat.Work.ExternalContact.Attachments.Request;
/// <summary>
/// Upload attachment resource request parameters
/// </summary>
/// <remarks>
/// For details, see: <see href="https://developer.work.weixin.qq.com/document/path/95098" />
/// </remarks>
public class WeChatWorkUploadAttachmentRequest : WeChatWorkRequest
{
    /// <summary>
    /// Media file type
    /// </summary>
    [NotNull]
    public MediaType MediaType { get; }
    /// <summary>
    /// Attachment type
    /// </summary>
    [NotNull]
    public AttachmentType AttachmentType { get; }
    /// <summary>
    /// Media files
    /// </summary>
    [NotNull]
    public IRemoteStreamContent Content { get; }
    public WeChatWorkUploadAttachmentRequest(
        MediaType mediaType, 
        AttachmentType attachmentType,
        IRemoteStreamContent content)
    {
        Check.NotNull(content, nameof(content));

        MediaType = mediaType;
        AttachmentType = attachmentType;
        Content = content;
    }
}
