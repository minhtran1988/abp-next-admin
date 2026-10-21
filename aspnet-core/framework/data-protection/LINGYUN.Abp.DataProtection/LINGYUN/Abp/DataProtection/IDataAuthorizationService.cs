using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LINGYUN.Abp.DataProtection;
/// <summary>
/// Data permission verification service
/// </summary>
public interface IDataAuthorizationService
{
    /// <summary>
    /// Verify permission to operate entity data
    /// </summary>
    /// <param name="operation">Data permission operation</param>
    /// <param name="entities">Check entity list</param>
    /// <typeparam name="TEntity">Entity type</typeparam>
    /// <returns></returns>
    Task<AuthorizationResult> AuthorizeAsync<TEntity>(DataAccessOperation operation, IEnumerable<TEntity> entities);
}