using System.Threading.Tasks;

namespace LINGYUN.Abp.Exporter;
/// <summary>
/// Data import service interface
/// </summary>
/// <typeparam name="TEntityImportDto"></typeparam>
public interface IImporterAppService<TEntityImportDto>
    where TEntityImportDto: class, new()
{
    /// <summary>
    /// Import data (default from Excel)
    /// </summary>
    /// <param name="input">Imported data file stream</param>
    /// <returns></returns>
    Task ImportAsync(TEntityImportDto input);
}
