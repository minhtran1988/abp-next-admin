using System.Threading.Tasks;

namespace LINGYUN.Abp.Gdpr;
/// <summary>
/// User personal account provider
/// </summary>
public interface IGdprUserAccountProvider
{
    /// <summary>
    /// Provider name
    /// </summary>
    string Name { get; }
    /// <summary>
    /// Delete user account
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    Task DeleteAsync(GdprDeleteUserAccountContext context);
}
