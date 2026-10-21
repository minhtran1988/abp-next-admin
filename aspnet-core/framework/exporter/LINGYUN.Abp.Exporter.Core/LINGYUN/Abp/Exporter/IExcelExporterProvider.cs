using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace LINGYUN.Abp.Exporter;
/// <summary>
/// Excel data export
/// </summary>
public interface IExcelExporterProvider
{
    /// <summary>
    /// Data export
    /// </summary>
    /// <param name="data">Export data</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<Stream> ExportAsync(object data, CancellationToken cancellationToken = default);
}
