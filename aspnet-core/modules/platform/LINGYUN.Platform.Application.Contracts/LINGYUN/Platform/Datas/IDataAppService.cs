using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace LINGYUN.Platform.Datas;
/// <summary>
/// Data dictionary application service interface
/// </summary>
public interface IDataAppService : 
    ICrudAppService<
        DataDto,
        Guid,
        GetDataListInput,
        DataCreateDto,
        DataUpdateDto>
{
    /// <summary>
    /// Get data dictionary
    /// </summary>
    /// <param name="name">Name</param>
    /// <returns></returns>
    Task<DataDto> GetAsync(string name);
    /// <summary>
    /// Get all data dictionaries
    /// </summary>
    /// <returns></returns>
    Task<ListResultDto<DataDto>> GetAllAsync();
    /// <summary>
    /// Remove data dictionary
    /// </summary>
    /// <param name="id">DictionaryId</param>
    /// <param name="input"></param>
    /// <returns></returns>
    Task<DataDto> MoveAsync(Guid id, DataMoveDto input);
    /// <summary>
    /// Add new data dictionary item
    /// </summary>
    /// <param name="id">DictionaryId</param>
    /// <param name="input"></param>
    /// <returns></returns>
    Task CreateItemAsync(Guid id, DataItemCreateDto input);
    /// <summary>
    /// Update data dictionary items
    /// </summary>
    /// <param name="id">DictionaryId</param>
    /// <param name="name">Project name</param>
    /// <param name="input"></param>
    /// <returns></returns>
    Task UpdateItemAsync(Guid id, string name, DataItemUpdateDto input);
    /// <summary>
    /// Delete data dictionary item
    /// </summary>
    /// <param name="id">DictionaryId</param>
    /// <param name="name">Project name</param>
    /// <returns></returns>
    Task DeleteItemAsync(Guid id, string name);
}
