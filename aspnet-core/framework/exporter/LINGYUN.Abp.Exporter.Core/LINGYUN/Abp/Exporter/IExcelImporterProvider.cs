using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace LINGYUN.Abp.Exporter;
/// <summary>
/// Excel data import provider
/// </summary>
public interface IExcelImporterProvider
{
    /// <summary>
    /// Data import
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="stream"></param>
    /// <returns></returns>
    Task<IReadOnlyCollection<T>> ImportAsync<T>(Stream stream) where T : class, new();
}
