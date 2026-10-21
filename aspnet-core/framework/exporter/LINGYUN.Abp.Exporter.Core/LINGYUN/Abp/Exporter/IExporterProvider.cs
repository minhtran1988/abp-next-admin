using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace LINGYUN.Abp.Exporter;
/// <summary>
/// Data export provider
/// </summary>
[Obsolete("This interface will be deprecated in future versions. Please use IExcelExporterProvider instead.")]
public interface IExporterProvider
{
    /// <summary>
    /// Data export
    /// </summary>
    /// <param name="data">Export data</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<Stream> ExportAsync<T>(ICollection<T> data, CancellationToken cancellationToken = default)
        where T : class, new();
}
