using LINGYUN.Abp.WeChat.Work.Contacts.Tags.Request;
using LINGYUN.Abp.WeChat.Work.Contacts.Tags.Response;
using System.Threading;
using System.Threading.Tasks;

namespace LINGYUN.Abp.WeChat.Work.Contacts.Tags;
/// <summary>
/// Tag management interface
/// </summary>
public interface IWeChatWorkTagProvider
{
    /// <summary>
    /// Create tag
    /// </summary>
    /// <remarks>
    /// For details, see: <see href="https://developer.work.weixin.qq.com/document/path/90210"/>
    /// </remarks>
    /// <param name="request">Create tag request parameters</param>
    /// <param name="cancellationToken"></param>
    /// <returns>Create tag response parameters</returns>
    Task<WeChatWorkTagCreateResponse> CreateAsync(
        WeChatWorkTagCreateRequest request, 
        CancellationToken cancellationToken = default);
    /// <summary>
    /// Update tag name
    /// </summary>
    /// <remarks>
    /// For details, see: <see href="https://developer.work.weixin.qq.com/document/path/90211"/>
    /// </remarks>
    /// <param name="request">Update tag name request parameters</param>
    /// <param name="cancellationToken"></param>
    /// <returns>Update tag name response parameters</returns>
    Task<WeChatWorkResponse> UpdateAsync(
        WeChatWorkTagUpdateRequest request,
        CancellationToken cancellationToken = default);
    /// <summary>
    /// Delete tag
    /// </summary>
    /// <remarks>
    /// For details, see: <see href="https://developer.work.weixin.qq.com/document/path/90212"/>
    /// </remarks>
    /// <param name="request">Delete tag request parameters</param>
    /// <param name="cancellationToken"></param>
    /// <returns>Delete tag response parameters</returns>
    Task<WeChatWorkResponse> DeleteAsync(
        WeChatWorkGetTagRequest request,
        CancellationToken cancellationToken = default);
    /// <summary>
    /// Get tag members
    /// </summary>
    /// <remarks>
    /// For details, see: <see href="https://developer.work.weixin.qq.com/document/path/90213"/>
    /// </remarks>
    /// <param name="request">Get tag member request parameters</param>
    /// <param name="cancellationToken"></param>
    /// <returns>Get tag member response parameters</returns>
    Task<WeChatWorkTagMemberInfoResponse> GetMemberAsync(
        WeChatWorkGetTagRequest request,
        CancellationToken cancellationToken = default);
    /// <summary>
    /// Add tag members
    /// </summary>
    /// <remarks>
    /// For details, see: <see href="https://developer.work.weixin.qq.com/document/path/90214"/>
    /// </remarks>
    /// <param name="request">Add tag member request parameters</param>
    /// <param name="cancellationToken"></param>
    /// <returns>Add tag member response parameters</returns>
    Task<WeChatWorkTagChangeMemberResponse> AddMemberAsync(
        WeChatWorkTagChangeMemberRequest request,
        CancellationToken cancellationToken = default);
    /// <summary>
    /// Delete tag members
    /// </summary>
    /// <remarks>
    /// For details, see: <see href="https://developer.work.weixin.qq.com/document/path/90215"/>
    /// </remarks>
    /// <param name="request">Add tag member request parameters</param>
    /// <param name="cancellationToken"></param>
    /// <returns>Add tag member response parameters</returns>
    Task<WeChatWorkTagChangeMemberResponse> DeleteMemberAsync(
        WeChatWorkTagChangeMemberRequest request,
        CancellationToken cancellationToken = default);
    /// <summary>
    /// Get the tag list
    /// </summary>
    /// <remarks>
    /// For details, see: <see href="https://developer.work.weixin.qq.com/document/path/90216"/>
    /// </remarks>
    /// <param name="cancellationToken"></param>
    /// <returns>Get tag list response parameters</returns>
    Task<WeChatWorkTagListResponse> GetListAsync(CancellationToken cancellationToken = default);
}
