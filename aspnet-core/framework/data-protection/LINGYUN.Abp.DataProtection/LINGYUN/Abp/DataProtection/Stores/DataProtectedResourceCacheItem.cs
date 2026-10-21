using System;
using System.Collections.Generic;
using Volo.Abp.Caching;

namespace LINGYUN.Abp.DataProtection.Stores;

[Serializable]
[CacheName("AbpDataProtectionResources")]
public class DataProtectedResourceCacheItem
{
    private const string CacheKeyFormat = "sn:{0};si:{1},e:{2},o:{3}";
    /// <summary>
    /// Authorization subject
    /// </summary>
    public string SubjectName { get; set; } = default!;

    /// <summary>
    /// Permission subject identification
    /// </summary>
    public string SubjectId { get; set; } = default!;

    /// <summary>
    /// Entity type full name
    /// </summary>
    public string EntityTypeFullName { get; set; } = default!;

    /// <summary>
    /// Data permission operations
    /// </summary>
    public DataAccessOperation Operation { get; set; }

    /// <summary>
    /// Get or set data filtering rules
    /// </summary>
    public DataAccessFilterGroup? FilterGroup { get; set; }

    /// <summary>
    /// List of properties that are allowed to operate
    /// </summary>
    public List<string> AccessdProperties { get; set; }
    public DataProtectedResourceCacheItem()
    {
        AccessdProperties = new List<string>();
    }

    public DataProtectedResourceCacheItem(
        string subjectName, 
        string subjectId, 
        string entityTypeFullName,
        DataAccessOperation operation, 
        DataAccessFilterGroup? filterGroup = null)
    {
        SubjectName = subjectName;
        SubjectId = subjectId;
        EntityTypeFullName = entityTypeFullName;
        Operation = operation;
        FilterGroup = filterGroup;
        AccessdProperties = new List<string>();
    }

    public static string CalculateCacheKey(string subjectName, string subjectId, string entityTypeFullName, DataAccessOperation operation = DataAccessOperation.Read)
    {
        return string.Format(CacheKeyFormat, subjectName, subjectId, entityTypeFullName, operation.ToString());
    }
}
