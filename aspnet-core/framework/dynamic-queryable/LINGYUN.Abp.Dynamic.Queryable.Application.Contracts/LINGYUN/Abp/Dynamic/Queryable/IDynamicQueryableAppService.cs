using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace LINGYUN.Abp.Dynamic.Queryable;

public interface IDynamicQueryableAppService<TEntityDto>
{
    /// <summary>
    /// Get the list of available fields
    /// </summary>
    /// <returns></returns>
    Task<ListResultDto<DynamicParamterDto>> GetAvailableFieldsAsync();
    /// <summary>
    /// Query data based on dynamic conditions
    /// </summary>
    /// <param name="dynamicInput"></param>
    /// <returns></returns>
    Task<PagedResultDto<TEntityDto>> SearchAsync(GetListByDynamicQueryableInput dynamicInput);
}
