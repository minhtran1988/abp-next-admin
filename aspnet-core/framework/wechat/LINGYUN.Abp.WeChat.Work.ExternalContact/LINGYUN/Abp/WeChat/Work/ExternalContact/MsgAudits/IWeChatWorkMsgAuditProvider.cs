using LINGYUN.Abp.WeChat.Work.ExternalContact.MsgAudits.Request;
using LINGYUN.Abp.WeChat.Work.ExternalContact.MsgAudits.Response;
using System.Threading;
using System.Threading.Tasks;

namespace LINGYUN.Abp.WeChat.Work.ExternalContact.MsgAudits;
/// <summary>
/// Session archive interface
/// </summary>
public interface IWeChatWorkMsgAuditProvider
{
    /// <summary>
    /// Get the session content archive open member list
    /// </summary>
    /// <remarks>
    /// For details, see: <see href="https://developer.work.weixin.qq.com/document/path/91614" />
    /// </remarks>
    Task<WeChatWorkGetPermitUserListResponse> GetPermitUserListAsync(
        WeChatWorkGetPermitUserListRequest request,
        CancellationToken cancellationToken = default);
    /// <summary>
    /// Get single chat session consent status
    /// </summary>
    /// <remarks>
    /// For details, see: <see href="https://developer.work.weixin.qq.com/document/path/91782" />
    /// </remarks>
    Task<WeChatWorkCheckSingleAgreeResponse> CheckSingleAgreeAsync(
        WeChatWorkCheckSingleAgreeRequest request,
        CancellationToken cancellationToken = default);
    /// <summary>
    /// Get group chat session consent status
    /// </summary>
    /// <remarks>
    /// For details, see: <see href="https://developer.work.weixin.qq.com/document/path/91782" />
    /// </remarks>
    Task<WeChatWorkCheckRoomAgreeResponse> CheckRoomAgreeAsync(
        WeChatWorkCheckRoomAgreeRequest request,
        CancellationToken cancellationToken = default);
    /// <summary>
    /// Get session content archive internal group information
    /// </summary>
    /// <remarks>
    /// For details, see: <see href="https://developer.work.weixin.qq.com/document/path/92951" />
    /// </remarks>
    Task<WeChatWorkGetGroupChatResponse> GetGroupChatAsync(
        WeChatWorkGetGroupChatRequest request,
        CancellationToken cancellationToken = default);
}
