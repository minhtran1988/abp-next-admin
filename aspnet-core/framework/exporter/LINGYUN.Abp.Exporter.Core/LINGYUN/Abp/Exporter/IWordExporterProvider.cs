using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace LINGYUN.Abp.Exporter;
/// <summary>
/// Word data export
/// </summary>
public interface IWordExporterProvider
{
    /// <summary>
    /// Data export
    /// </summary>
    /// <param name="data">Export data</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<Stream> ExportAsync(object data, CancellationToken cancellationToken = default);
    /// <summary>
    /// Data export
    /// </summary>
    /// <param name="template">Template data</param>
    /// <param name="data">Export data</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<Stream> ExportAsync(byte[] template, object data, CancellationToken cancellationToken = default);
}
