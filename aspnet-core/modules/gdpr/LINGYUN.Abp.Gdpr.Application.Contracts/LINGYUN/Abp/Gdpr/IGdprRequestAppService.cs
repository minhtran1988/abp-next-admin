using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Content;

namespace LINGYUN.Abp.Gdpr;

public interface IGdprRequestAppService : IApplicationService
{
    /// <summary>
    /// Get personal data request details
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<GdprRequestDto> GetAsync(Guid id);
    /// <summary>
    /// Get the personal data request list
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    Task<PagedResultDto<GdprRequestDto>> GetListAsync(GdprRequestGetListInput input);
    /// <summary>
    /// Download collected user data
    /// </summary>
    /// <param name="requestId"></param>
    /// <returns></returns>
    Task<IRemoteStreamContent> DownloadPersonalDataAsync(Guid requestId);
    /// <summary>
    /// Delete personal data request
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task DeleteAsync(Guid id);
    /// <summary>
    /// Prepare user data
    /// </summary>
    /// <returns></returns>
    Task PreparePersonalDataAsync();
    /// <summary>
    /// Delete user data
    /// </summary>
    /// <returns></returns>
    Task DeletePersonalDataAsync();
    /// <summary>
    /// Delete user account
    /// </summary>
    /// <returns></returns>
    Task DeletePersonalAccountAsync();
}
