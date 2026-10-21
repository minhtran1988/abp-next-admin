using System.Threading.Tasks;

namespace LINGYUN.Abp.DataProtection.Stores;
public interface IDataProtectedResourceCache
{
    /// <summary>
    /// Set the cache for the specified data permissions
    /// </summary>
    /// <param name="item">Data permission cache item to be updated</param>
    void SetCache(DataAccessResource resource);
    /// <summary>
    /// Set the cache for the specified data permissions
    /// </summary>
    /// <param name="item">Data permission cache item to be updated</param>
    Task SetCacheAsync(DataAccessResource resource);
    /// <summary>
    /// Remove cache items of the specified body and entity type
    /// </summary>
    /// <param name="item">Data permission cache item information to be removed</param>
    void RemoveCache(DataAccessResource resource);
    /// <summary>
    /// Remove cache items of the specified body and entity type
    /// </summary>
    /// <param name="item">Data permission cache item information to be removed</param>
    Task RemoveCacheAsync(DataAccessResource resource);
    /// <summary>
    /// Get the data permission filtering rules of the specified subject and entity type
    /// </summary>
    /// <param name="subjectName">Permission subject</param>
    /// <param name="subjectId">Permission subject identification</param>
    /// <param name="entityTypeFullName">Entity type name</param>
    /// <param name="operation">Data permission operation</param>
    /// <returns>Data filter condition group</returns>
    DataProtectedResourceCacheItem? GetCache(string subjectName, string subjectId, string entityTypeFullName, DataAccessOperation operation);
    /// <summary>
    /// Get the data permission filtering rules of the specified subject and entity type
    /// </summary>
    /// <param name="subjectName">Permission subject</param>
    /// <param name="subjectId">Permission subject identification</param>
    /// <param name="entityTypeFullName">Entity type name</param>
    /// <param name="operation">Data permission operation</param>
    /// <returns>Data filter condition group</returns>
    Task<DataProtectedResourceCacheItem?> GetCacheAsync(string subjectName, string subjectId, string entityTypeFullName, DataAccessOperation operation);
}
