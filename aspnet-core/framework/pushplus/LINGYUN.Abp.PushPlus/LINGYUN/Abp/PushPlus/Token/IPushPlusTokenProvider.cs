using System.Threading;
using System.Threading.Tasks;

namespace LINGYUN.Abp.PushPlus.Token;
/// <summary>
/// Get AccessKey
/// </summary>
public interface IPushPlusTokenProvider
{
    /// <summary>
    /// Get the globally available Token
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<PushPlusToken> GetTokenAsync(CancellationToken cancellationToken = default);
}
