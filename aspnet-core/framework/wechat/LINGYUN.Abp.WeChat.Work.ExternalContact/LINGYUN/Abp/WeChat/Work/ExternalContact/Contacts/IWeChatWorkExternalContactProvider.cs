using LINGYUN.Abp.WeChat.Work.ExternalContact.Contacts.Request;
using LINGYUN.Abp.WeChat.Work.ExternalContact.Contacts.Response;
using System.Threading;
using System.Threading.Tasks;

namespace LINGYUN.Abp.WeChat.Work.ExternalContact.Contacts;
/// <summary>
/// External contact interface
/// </summary>
public interface IWeChatWorkExternalContactProvider
{
    /// <summary>
    /// Get the serviced external contact
    /// </summary>
    /// <remarks>
    /// For details, see: <see href="https://developer.work.weixin.qq.com/document/path/99434" />
    /// </remarks>
    /// <param name="request">Request parameters</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<WeChatWorkGetExternalContactListResponse> GetExternalContactListAsync(
        WeChatWorkGetExternalContactListRequest request,
        CancellationToken cancellationToken = default);
}
