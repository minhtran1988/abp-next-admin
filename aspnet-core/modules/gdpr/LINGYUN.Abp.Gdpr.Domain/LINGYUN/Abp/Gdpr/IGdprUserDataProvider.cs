using System.Threading.Tasks;

namespace LINGYUN.Abp.Gdpr;
/// <summary>
/// User personal data provider<br />
/// Implementation: https://abp.io/docs/latest/modules/gdpr#gdpruserdatarequestedeto
/// </summary>
public interface IGdprUserDataProvider
{
    /// <summary>
    /// Provider name
    /// </summary>
    string Name { get; }
    /// <summary>
    /// Prepare user data
    /// </summary>
    Task PorepareAsync(GdprPrepareUserDataContext context);
    /// <summary>
    /// Delete user data
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    Task DeleteAsync(GdprDeleteUserDataContext context);
}
