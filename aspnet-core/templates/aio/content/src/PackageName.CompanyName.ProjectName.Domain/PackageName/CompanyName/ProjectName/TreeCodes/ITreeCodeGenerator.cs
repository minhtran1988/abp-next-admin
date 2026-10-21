using System;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories;

namespace PackageName.CompanyName.ProjectName.TreeCodes
{
    /// <summary>
    /// Tree coding generator interface
    /// </summary>
    public interface ITreeCodeGenerator
    {
        /// <summary>
        /// Generate tree encoding
        /// </summary>
        /// <typeparam name="TEntity">Entity type</typeparam>
        /// <param name="repository">Repository</param>
        /// <param name="parentId">ParentId</param>
        /// <returns>Generated tree encoding</returns>
        Task<string> GenerateAsync<TEntity>(
            IRepository<TEntity, Guid> repository,
            Guid? parentId)
            where TEntity : class, IEntity<Guid>, IHaveTreeCode;

        /// <summary>
        /// Update the TreeCode of the node and all its child nodes
        /// </summary>
        Task UpdateTreeCodesAsync<TEntity>(
            IRepository<TEntity, Guid> repository,
            Guid entityId)
            where TEntity : class, IEntity<Guid>, IHaveTreeCode;
    }
}
