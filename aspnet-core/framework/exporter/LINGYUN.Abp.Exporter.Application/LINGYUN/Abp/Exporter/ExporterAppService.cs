using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp.Content;

namespace LINGYUN.Abp.Exporter;

public abstract class ExporterAppService<TEntity, TEntityExportDto, TEntityListGetInput> : ApplicationService, IExporterAppService<TEntityExportDto, TEntityListGetInput>
    where TEntityExportDto : class, new()
{
    private readonly IExcelExporterProvider _exporterProvider;

    protected ExporterAppService(IExcelExporterProvider exporterProvider)
    {
        _exporterProvider = exporterProvider;
    }

    public async virtual Task<IRemoteStreamContent> ExportAsync(TEntityListGetInput input)
    {
        var fileName = GetExportFileName(input);

        var entities = await GetListAsync(input);
        var entitieDtoList = MapEntitiesToDto(entities);

        var stream = await _exporterProvider.ExportAsync(entitieDtoList);

        return new RemoteStreamContent(stream, fileName);
    }
    /// <summary>
    /// Implementation method to return the export file name
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    protected abstract string GetExportFileName(TEntityListGetInput input);
    /// <summary>
    /// Implementation method to query the list of entities that need to be exported
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    protected abstract Task<List<TEntity>> GetListAsync(TEntityListGetInput input);
    /// <summary>
    /// Implementation method for entity data transfer object mapping
    /// </summary>
    /// <param name="entities"></param>
    /// <returns></returns>
    protected virtual List<TEntityExportDto> MapEntitiesToDto(List<TEntity> entities)
    {
        return ObjectMapper.Map<List<TEntity>, List<TEntityExportDto>>(entities);
    }
}
