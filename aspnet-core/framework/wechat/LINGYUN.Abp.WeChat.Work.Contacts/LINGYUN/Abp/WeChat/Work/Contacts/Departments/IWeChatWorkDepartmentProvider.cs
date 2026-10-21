using LINGYUN.Abp.WeChat.Work.Contacts.Departments.Request;
using LINGYUN.Abp.WeChat.Work.Contacts.Departments.Response;
using System.Threading;
using System.Threading.Tasks;

namespace LINGYUN.Abp.WeChat.Work.Contacts.Departments;
/// <summary>
/// Enterprise WeChat department management interface
/// </summary>
public interface IWeChatWorkDepartmentProvider
{
    /// <summary>
    /// Get the department list
    /// </summary>
    /// <remarks>
    /// For details, see: <see href="https://developer.work.weixin.qq.com/document/path/90208" />
    /// </remarks>
    /// <param name="request">Request parameters</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<WeChatWorkGetDepartmentListResponse> GetDepartmentListAsync(
        WeChatWorkGetDepartmentListRequest request,
        CancellationToken cancellationToken = default);
    /// <summary>
    /// Get the list of sub-department IDs
    /// </summary>
    /// <remarks>
    /// For details, see: <see href="https://developer.work.weixin.qq.com/document/path/95350" />
    /// </remarks>
    /// <param name="request">Request parameters</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<WeChatWorkGetSubDepartmentListResponse> GetSubDepartmentListAsync(
        WeChatWorkGetSubDepartmentListRequest request,
        CancellationToken cancellationToken = default);
    /// <summary>
    /// Get details of a single department
    /// </summary>
    /// <remarks>
    /// For details, see: <see href="https://developer.work.weixin.qq.com/document/path/95351" />
    /// </remarks>
    /// <param name="request">Request parameters</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<WeChatWorkGetDepartmentResponse> GetDepartmentAsync(
        WeChatWorkGetDepartmentRequest request,
        CancellationToken cancellationToken = default);
    /// <summary>
    /// Create department
    /// </summary>
    /// <remarks>
    /// This interface uses the address book application Token<br />
    /// For details, see: <see href="https://developer.work.weixin.qq.com/document/path/90205" />
    /// </remarks>
    /// <param name="request">Request parameters</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<WeChatWorkCreateDepartmentResponse> CreateDepartmentAsync(
        WeChatWorkCreateDepartmentRequest request,
        CancellationToken cancellationToken = default);
    /// <summary>
    /// Update department
    /// </summary>
    /// <remarks>
    /// This interface uses the address book application Token<br />
    /// For details, see: <see href="https://developer.work.weixin.qq.com/document/path/90206" />
    /// </remarks>
    /// <param name="request">Request parameters</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<WeChatWorkResponse> UpdateDepartmentAsync(
        WeChatWorkUpdateDepartmentRequest request,
        CancellationToken cancellationToken = default);
    /// <summary>
    /// Delete department
    /// </summary>
    /// <remarks>
    /// This interface uses the address book application Token<br />
    /// For details, see: <see href="https://developer.work.weixin.qq.com/document/path/90207" />
    /// </remarks>
    /// <param name="request">Request parameters</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<WeChatWorkResponse> DeleteDepartmentAsync(
        WeChatWorkDeleteDepartmentRequest request,
        CancellationToken cancellationToken = default);
}
