using LINGYUN.Abp.DataProtection;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Specifications;

namespace PackageName.CompanyName.ProjectName;
/// <summary>
/// Basic warehousing interface
/// </summary>
/// <typeparam name="TEntity">Entity type</typeparam>
/// <typeparam name="TKey">Entity primary key type</typeparam>
public interface IProjectNameBasicRepository<TEntity, TKey> : IDataProtectionRepository<TEntity>
    where TEntity : class, IEntity<TKey>
{
    /// <summary>
    /// Get the number of filtered entities
    /// </summary>
    /// <param name="specification"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<int> GetCountAsync(
        ISpecification<TEntity> specification,
        CancellationToken cancellationToken = default);
    /// <summary>
    /// Get the filtered entity list (pagination)
    /// </summary>
    /// <param name="specification"></param>
    /// <param name="sorting"></param>
    /// <param name="maxResultCount"></param>
    /// <param name="skipCount"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<List<TEntity>> GetListAsync(
        ISpecification<TEntity> specification,
        string sorting = nameof(IEntity<TKey>.Id),
        int maxResultCount = 10,
        int skipCount = 0,
        CancellationToken cancellationToken = default);
    /// <summary>
    /// Get the filtered entity list
    /// </summary>
    /// <param name="specification"></param>
    /// <param name="sorting"></param>
    /// <param name="maxResultCount"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<List<TEntity>> GetListAsync(
        ISpecification<TEntity> specification,
        string sorting = nameof(IEntity<TKey>.Id),
        int maxResultCount = 10,
        CancellationToken cancellationToken = default);
}
