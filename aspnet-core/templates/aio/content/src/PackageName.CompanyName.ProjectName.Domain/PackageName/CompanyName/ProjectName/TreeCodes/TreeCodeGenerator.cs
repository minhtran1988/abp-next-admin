using System;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories;

namespace PackageName.CompanyName.ProjectName.TreeCodes
{
    /// <summary>
    /// Tree encoding generator
    /// </summary>
    public class TreeCodeGenerator : ITreeCodeGenerator, ISingletonDependency
    {
        /// <summary>
        /// Generate tree encoding
        /// </summary>
        /// <typeparam name="TEntity">Entity type</typeparam>
        /// <param name="repository">Repository</param>
        /// <param name="parentId">ParentId</param>
        /// <returns>Generated tree encoding</returns>
        public async virtual Task<string> GenerateAsync<TEntity>(
            IRepository<TEntity, Guid> repository,
            Guid? parentId)
            where TEntity : class, IEntity<Guid>, IHaveTreeCode
        {
            if (!parentId.HasValue)
            {
                // Generate root node code
                return await GenerateRootCodeAsync(repository);
            }
            else
            {
                // Generate child node code
                return await GenerateChildCodeAsync(repository, parentId.Value);
            }
        }

        private async Task<string> GenerateRootCodeAsync<TEntity>(
            IRepository<TEntity, Guid> repository)
            where TEntity : class, IEntity<Guid>, IHaveTreeCode
        {
            var query = await repository.GetQueryableAsync();
            query = query.Where(e => e.ParentId == null).OrderByDescending(e => e.CreationTime);
            var maxCodeEntity = await repository.AsyncExecuter.FirstOrDefaultAsync(query);

            if (maxCodeEntity == null)
            {
                return "0001";
            }

            int maxCode = int.Parse(maxCodeEntity.TreeCode.Split('.').LastOrDefault("0"));
            return (maxCode + 1).ToString("D4");
        }

        private async Task<string> GenerateChildCodeAsync<TEntity>(
            IRepository<TEntity, Guid> repository,
            Guid parentId)
            where TEntity : class, IEntity<Guid>, IHaveTreeCode
        {
            var parent = await repository.GetAsync(parentId);
            if (parent == null)
            {
                throw new EntityNotFoundException(typeof(TEntity), parentId);
            }

            var query = await repository.GetQueryableAsync();
            query = query.Where(e => e.ParentId == parentId).OrderByDescending(e => e.CreationTime);
            var maxCodeEntity = await repository.AsyncExecuter.FirstOrDefaultAsync(query);

            string newCode;
            if (maxCodeEntity == null)
            {
                newCode = "0001";
            }
            else
            {
                int maxCode = int.Parse(maxCodeEntity.TreeCode.Split('.').Last());
                newCode = (maxCode + 1).ToString("D4");
            }

            // Build a complete TreeCode: parent TreeCode.New encoding
            return $"{parent.TreeCode}.{newCode}";
        }

        /// <summary>
        /// Update the TreeCode of the node and all its child nodes
        /// </summary>
        public async virtual Task UpdateTreeCodesAsync<TEntity>(
            IRepository<TEntity, Guid> repository,
            Guid entityId)
            where TEntity : class, IEntity<Guid>, IHaveTreeCode
        {
            var entity = await repository.GetAsync(entityId);
            var query = await repository.GetQueryableAsync();
            var children = await repository.AsyncExecuter.ToListAsync(
                query.Where(e => e.ParentId == entityId));

            foreach (var child in children)
            {
                // Get the child node code (the last part of TreeCode) or generate a new code
                string childCode = child.TreeCode.Contains('.')
                    ? child.TreeCode.Split('.').Last()
                    : child.TreeCode;

                child.TreeCode = $"{entity.TreeCode}.{childCode}";
                await repository.UpdateAsync(child);

                // Recursively update child nodes
                await UpdateTreeCodesAsync(repository, child.Id);
            }
        }
    }
}