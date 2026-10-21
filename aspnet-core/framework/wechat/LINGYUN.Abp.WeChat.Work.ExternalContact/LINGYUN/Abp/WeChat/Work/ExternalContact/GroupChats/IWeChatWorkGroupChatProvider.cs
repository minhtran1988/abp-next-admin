using LINGYUN.Abp.WeChat.Work.ExternalContact.GroupChats.Request;
using LINGYUN.Abp.WeChat.Work.ExternalContact.GroupChats.Response;
using System.Threading;
using System.Threading.Tasks;

namespace LINGYUN.Abp.WeChat.Work.ExternalContact.GroupChats;
/// <summary>
/// Customer group management interface
/// </summary>
public interface IWeChatWorkGroupChatProvider
{
    /// <summary>
    /// Get the customer group list
    /// </summary>
    /// <remarks>
    /// For details, see: <see href="https://developer.work.weixin.qq.com/document/path/92120" />
    /// </remarks>
    /// <param name="request">Request parameters</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<WeChatWorkGetGroupChatListResponse> GetGroupChatListAsync(
        WeChatWorkGetGroupChatListRequest request,
        CancellationToken cancellationToken = default);
    /// <summary>
    /// Get customer group details
    /// </summary>
    /// <remarks>
    /// For details, see: <see href="https://developer.work.weixin.qq.com/document/path/92122" />
    /// </remarks>
    /// <param name="request">Request parameters</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<WeChatWorkGetGroupChatResponse> GetGroupChatAsync(
        WeChatWorkGetGroupChatRequest request,
        CancellationToken cancellationToken = default);
    /// <summary>
    /// Customer group opengid conversion
    /// </summary>
    /// <remarks>
    /// For details, see: <see href="https://developer.work.weixin.qq.com/document/path/94822" />
    /// </remarks>
    /// <param name="request">Request parameters</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<WeChatWorkOpengIdToChatIdResponse> OpengIdToChatIdAsync(
        WeChatWorkOpengIdToChatIdRequest request,
        CancellationToken cancellationToken = default);
}
