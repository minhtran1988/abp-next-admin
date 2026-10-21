using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.PermissionManagement;

namespace LINGYUN.Abp.PermissionManagement;
public interface IMultiplePermissionManager : IPermissionManager
{
    /// <summary>
    /// Set permissions in batches
    /// </summary>
    /// <param name="providerName">Permission provider</param>
    /// <param name="providerKey">Permission provider Key</param>
    /// <param name="permissions">Permission collection</param>
    /// <returns></returns>
    Task SetManyAsync(string providerName, string providerKey, IEnumerable<PermissionChangeState> permissions);
}
