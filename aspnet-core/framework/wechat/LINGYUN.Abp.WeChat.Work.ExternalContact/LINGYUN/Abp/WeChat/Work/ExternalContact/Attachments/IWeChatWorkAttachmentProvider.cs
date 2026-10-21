using LINGYUN.Abp.WeChat.Work.ExternalContact.Attachments.Request;
using LINGYUN.Abp.WeChat.Work.ExternalContact.Attachments.Response;
using System.Threading;
using System.Threading.Tasks;

namespace LINGYUN.Abp.WeChat.Work.ExternalContact.Attachments;
/// <summary>
/// Attachment resource interface
/// </summary>
public interface IWeChatWorkAttachmentProvider
{
    /// <summary>
    /// Upload attachment resources
    /// </summary>
    /// <remarks>
    /// For details, see: <see href="https://developer.work.weixin.qq.com/document/path/90253"/>
    /// </remarks>
    /// <param name="request">Request parameters</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<WeChatWorkUploadAttachmentResponse> UploadAsync(
        WeChatWorkUploadAttachmentRequest request,
        CancellationToken cancellationToken = default);
}
