using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace LINGYUN.Abp.Exporter;

public abstract class ImporterAppService<TEntity, TEntityImportDto> : ApplicationService, IImporterAppService<TEntityImportDto>
    where TEntityImportDto : class, new()
{
    private readonly IExcelImporterProvider _importerProvider;
    protected ImporterAppService(IExcelImporterProvider importerProvider)
    {
        _importerProvider = importerProvider;
    }

    public async virtual Task ImportAsync(TEntityImportDto input)
    {
        var stream = await GetImportStream(input);
        var entitieDtoList = await _importerProvider.ImportAsync<TEntityImportDto>(stream);

        var entities = MapDtoToEntities([.. entitieDtoList]);

        await SaveManyEntities(entities);
    }
    /// <summary>
    /// Implement methods to extract file streams from imported data
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    protected abstract Task<Stream> GetImportStream(TEntityImportDto input);
    /// <summary>
    /// Implement method to save the entity list to the persistence facility
    /// </summary>
    /// <param name="entities"></param>
    /// <returns></returns>
    protected abstract Task SaveManyEntities(ICollection<TEntity> entities);
    /// <summary>
    /// Implementation method for entity data transfer object mapping
    /// </summary>
    /// <param name="entitieDtoList"></param>
    /// <returns></returns>
    protected virtual List<TEntity> MapDtoToEntities(List<TEntityImportDto> entitieDtoList)
    {
        return ObjectMapper.Map<List<TEntityImportDto>, List<TEntity>>(entitieDtoList);
    }
}
