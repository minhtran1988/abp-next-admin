using LINGYUN.Abp.DataProtection;
using Volo.Abp.Domain.Entities;

namespace PackageName.CompanyName.ProjectName;
/// <summary>
/// Protected entity protection warehousing interface
/// </summary>
/// <typeparam name="TEntity">Entity type</typeparam>
/// <typeparam name="TKey">Entity primary key type</typeparam>
public interface IProjectNameDataProtectionRepository<TEntity, TKey> :
    IProjectNameBasicRepository<TEntity, TKey>,
    IDataProtectionRepository<TEntity>
    where TEntity : class, IEntity<TKey>
{
}
