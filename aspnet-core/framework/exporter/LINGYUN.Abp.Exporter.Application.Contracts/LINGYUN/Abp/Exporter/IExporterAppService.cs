using System.Threading.Tasks;
using Volo.Abp.Content;

namespace LINGYUN.Abp.Exporter;
/// <summary>
/// Data export service interface
/// </summary>
/// <typeparam name="TEntityExportDto">Exported entity data transfer object</typeparam>
/// <typeparam name="TEntityListGetInput">Entity data filter data object</typeparam>
public interface IExporterAppService<TEntityExportDto, TEntityListGetInput>
     where TEntityExportDto : class, new()
{
    /// <summary>
    /// Export data (Excel is implemented by default)
    /// </summary>
    /// <param name="input">Data filter conditions</param>
    /// <returns></returns>
    Task<IRemoteStreamContent> ExportAsync(TEntityListGetInput input);
}
