using LINGYUN.Abp.WeChat.Work.Contacts.Members.Models;
using LINGYUN.Abp.WeChat.Work.Contacts.Members.Request;
using LINGYUN.Abp.WeChat.Work.Contacts.Members.Response;
using System.Threading;
using System.Threading.Tasks;

namespace LINGYUN.Abp.WeChat.Work.Contacts.Members;
/// <summary>
/// Enterprise WeChat member management interface
/// </summary>
public interface IWeChatWorkMemberProvider
{
    /// <summary>
    /// Read members
    /// </summary>
    /// <remarks>
    /// For details, see: <see href="https://developer.work.weixin.qq.com/document/path/90196" />
    /// </remarks>
    /// <param name="userId">Member UserID. The account corresponding to the management end must be unique within the enterprise. Case-insensitive, length 1~64 bytes</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<WeChatWorkGetMemberResponse> GetMemberAsync(
        string userId,
        CancellationToken cancellationToken = default);
    /// <summary>
    /// Create members
    /// </summary>
    /// <remarks>
    /// This interface uses the address book application Token<br />
    /// For details, see: <see href="https://developer.work.weixin.qq.com/document/path/90195" />
    /// </remarks>
    /// <param name="request">Request parameters</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<WeChatWorkCreateMemberResponse> CreateMemberAsync(
        WeChatWorkCreateMemberRequest request,
        CancellationToken cancellationToken = default);
    /// <summary>
    /// Update members
    /// </summary>
    /// <remarks>
    /// This interface uses the address book application Token<br />
    /// For details, see: <see href="https://developer.work.weixin.qq.com/document/path/90197" />
    /// </remarks>
    /// <param name="request">Request parameters</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<WeChatWorkResponse> UpdateMemberAsync(
        WeChatWorkUpdateMemberRequest request,
        CancellationToken cancellationToken = default);
    /// <summary>
    /// Delete members
    /// </summary>
    /// <remarks>
    /// This interface uses the address book application Token<br />
    /// For details, see: <see href="https://developer.work.weixin.qq.com/document/path/90198" />
    /// </remarks>
    /// <param name="userId">Member UserID. Account corresponding to the management terminal</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<WeChatWorkResponse> DeleteMemberAsync(
        string userId,
        CancellationToken cancellationToken = default);
    /// <summary>
    /// Delete members in batches
    /// </summary>
    /// <remarks>
    /// This interface uses the address book application Token<br />
    /// For details, see: <see href="https://developer.work.weixin.qq.com/document/path/90199" />
    /// </remarks>
    /// <param name="request">Request parameters</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<WeChatWorkResponse> BulkDeleteMemberAsync(
        WeChatWorkBulkDeleteMemberRequest request,
        CancellationToken cancellationToken = default);
    /// <summary>
    /// Get department members
    /// </summary>
    /// <remarks>
    /// For details, see: <see href="https://developer.work.weixin.qq.com/document/path/90200" />
    /// </remarks>
    /// <param name="departmentId">The department id obtained</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<WeChatWorkGetSimpleMemberListResponse> GetSimpleMemberListAsync(
        int departmentId,
        CancellationToken cancellationToken = default);
    /// <summary>
    /// Get department member details
    /// </summary>
    /// <remarks>
    /// For details, see: <see href="https://developer.work.weixin.qq.com/document/path/90201" />
    /// </remarks>
    /// <param name="departmentId">The department id obtained</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<WeChatWorkGetMemberListResponse> GetMemberListAsync(
        int departmentId,
        CancellationToken cancellationToken = default);
    /// <summary>
    /// userid to openid
    /// </summary>
    /// <remarks>
    /// For details, see: <see href="https://developer.work.weixin.qq.com/document/path/90202" />
    /// </remarks>
    /// <param name="request">Request parameters</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<WeChatWorkConvertToOpenIdResponse> ConvertToOpenIdAsync(
        WeChatWorkConvertToOpenIdRequest request,
        CancellationToken cancellationToken = default);
    /// <summary>
    /// openid to userid
    /// </summary>
    /// <remarks>
    /// For details, see: <see href="https://developer.work.weixin.qq.com/document/path/90202" />
    /// </remarks>
    /// <param name="request">Request parameters</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<WeChatWorkConvertToUserIdResponse> ConvertToUserIdAsync(
        WeChatWorkConvertToUserIdRequest request,
        CancellationToken cancellationToken = default);
    /// <summary>
    /// Login two-step verification
    /// </summary>
    /// <remarks>
    /// For details, see: <see href="https://developer.work.weixin.qq.com/document/path/90203" />
    /// </remarks>
    /// <param name="userId">Member UserID. Account corresponding to the management terminal</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<WeChatWorkResponse> AuthSuccessAsync(
        string userId,
        CancellationToken cancellationToken = default);
    /// <summary>
    /// Invite members
    /// </summary>
    /// <remarks>
    /// For details, see: <see href="https://developer.work.weixin.qq.com/document/path/90975" />
    /// </remarks>
    /// <param name="request">Request parameters</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<WeChatWorkBulkInviteMemberResponse> BulkInviteMemberAsync(
        WeChatWorkBulkInviteMemberRequest request,
        CancellationToken cancellationToken = default);
    /// <summary>
    /// Get the QR code to join the enterprise
    /// </summary>
    /// <remarks>
    /// This interface uses the address book application Token<br />
    /// For details, see: <see href="https://developer.work.weixin.qq.com/document/path/91714" />
    /// </remarks>
    /// <param name="sizeType">QR code size type</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<WeChatWorkGetJoinQrCodeResponse> GetJoinQrCodeAsync(
        QrCodeSizeType sizeType,
        CancellationToken cancellationToken = default);
    /// <summary>
    /// Get userid from mobile phone number
    /// </summary>
    /// <remarks>
    /// For details, see: <see href="https://developer.work.weixin.qq.com/document/path/95402" />
    /// </remarks>
    /// <param name="request">Request parameters</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<WeChatWorkGetUserIdResponse> GetUserIdByMobileAsync(
        WeChatWorkGetUserIdByMobileRequest request,
        CancellationToken cancellationToken = default);
    /// <summary>
    /// Get userid from email
    /// </summary>
    /// <remarks>
    /// For details, see: <see href="https://developer.work.weixin.qq.com/document/path/95895" />
    /// </remarks>
    /// <param name="request">Request parameters</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<WeChatWorkGetUserIdResponse> GetUserIdByEmailAsync(
        WeChatWorkGetUserIdByEmailRequest request,
        CancellationToken cancellationToken = default);
    /// <summary>
    /// Get the member ID list
    /// </summary>
    /// <remarks>
    /// This interface uses the address book application Token<br />
    /// For details, see: <see href="https://developer.work.weixin.qq.com/document/path/96067" />
    /// </remarks>
    /// <param name="request">Request parameters</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<WeChatWorkGetUserIdListResponse> GetUserIdListAsync(
        WeChatWorkGetUserIdListRequest request,
        CancellationToken cancellationToken = default);
}
