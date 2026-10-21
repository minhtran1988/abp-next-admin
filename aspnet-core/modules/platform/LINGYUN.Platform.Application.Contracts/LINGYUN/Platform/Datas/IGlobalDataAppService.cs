using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace LINGYUN.Platform.Datas;
/// <summary>
/// Public data dictionary application service interface
/// </summary>
public interface IGlobalDataAppService : IApplicationService
{
    /// <summary>
    /// Get data dictionary
    /// </summary>
    /// <param name="name">Name</param>
    /// <returns></returns>
    Task<DataDto> GetByNameAsync(string name);
    /// <summary>
    /// Get data dictionary
    /// </summary>
    /// <param name="id">Id</param>
    /// <returns></returns>
    Task<DataDto> GetAsync(Guid id);
    /// <summary>
    /// Get all data dictionaries
    /// </summary>
    /// <returns></returns>
    Task<ListResultDto<DataDto>> GetAllAsync();
}
