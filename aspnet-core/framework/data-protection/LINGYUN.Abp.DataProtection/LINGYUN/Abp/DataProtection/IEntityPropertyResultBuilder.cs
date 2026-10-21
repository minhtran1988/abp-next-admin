using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace LINGYUN.Abp.DataProtection;
/// <summary>
/// Entity property return value constructor
/// </summary>
public interface IEntityPropertyResultBuilder
{
    /// <summary>
    /// Get the attribute return value filter expression of the entity
    /// </summary>
    /// <typeparam name="TEntity">Entity type</typeparam>
    /// <param name="operation">Data operation mode</param>
    /// <returns></returns>
    Task<Expression<Func<TEntity, TEntity>>> Build<TEntity>(DataAccessOperation operation);
    /// <summary>
    /// Get the attribute return value filter expression of the entity
    /// </summary>
    /// <param name="entityType">Entity type</param>
    /// <param name="operation">Data operation mode</param>
    /// <returns></returns>
    Task<LambdaExpression> Build(Type entityType, DataAccessOperation operation);
}
