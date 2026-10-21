using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace LINGYUN.Abp.DataProtection;
/// <summary>
/// Entity filter condition constructor
/// </summary>
public interface IEntityTypeFilterBuilder
{
    /// <summary>
    /// Get the query filter expression of the entity
    /// </summary>
    /// <param name="operation">Data permission operation</param>
    /// <param name="group">Query condition group</param>
    /// <typeparam name="TEntity">Entity type</typeparam>
    /// <returns></returns>
    Task<Expression<Func<TEntity, bool>>> Build<TEntity>(DataAccessOperation operation, DataAccessFilterGroup? group = null);

    Task<LambdaExpression> Build(Type entityType, DataAccessOperation operation, DataAccessFilterGroup? group = null);
}