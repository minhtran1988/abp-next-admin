using LINGYUN.Abp.WeChat.Work.Chat.Request;
using LINGYUN.Abp.WeChat.Work.Chat.Response;
using System.Threading;
using System.Threading.Tasks;

namespace LINGYUN.Abp.WeChat.Work.Chat;
/// <summary>
/// Group chat operation interface
/// </summary>
/// <remarks>
/// Reference: https://developer.work.weixin.qq.com/document/path/90244
/// </remarks>
public interface IWeChatWorkAppChatManager
{
    /// <summary>
    /// Create a group chat session
    /// </summary>
    /// <remarks>
    /// Reference: https://developer.work.weixin.qq.com/document/path/90245
    /// </remarks>
    Task<WeChatWorkAppChatCreateResponse> CreateAsync(
        WeChatWorkAppChatCreateRequest request, 
        CancellationToken cancellationToken = default);
    /// <summary>
    /// Modify group chat session
    /// </summary>
    /// <remarks>
    /// Reference: https://developer.work.weixin.qq.com/document/path/98913
    /// </remarks>
    Task<WeChatWorkResponse> UpdateAsync(
        WeChatWorkAppChatUpdateRequest request,
        CancellationToken cancellationToken = default);
    /// <summary>
    /// Get group chat session
    /// </summary>
    /// <remarks>
    /// Reference: https://developer.work.weixin.qq.com/document/path/98914
    /// </remarks>
    Task<WeChatWorkAppChatInfoResponse> GetAsync(
        string agentId,
        string chatId,
        CancellationToken cancellationToken = default);
}
