using System;

namespace LINGYUN.Abp.TextTemplating;

public class AbpTextTemplatingCachingOptions
{
    /// <summary>
    /// Minimum expiration time of text template cache
    /// </summary>
    public TimeSpan? MinimumCacheDuration { get; set; }
    /// <summary>
    /// Text template cache absolute expiration time
    /// </summary>
    public TimeSpan? MaximumCacheDuration { get; set; }
    /// <summary>
    /// Default: true.
    /// </summary>
    public bool SaveStaticTemplateDefinitionToDatabase { get; set; }
    /// <summary>
    /// Default: false.
    /// </summary>
    public bool IsDynamicTemplateDefinitionStoreEnabled { get; set; }
    /// <summary>
    /// Cache refresh time
    /// default: 2 hours
    /// </summary>
    public TimeSpan TemplateDefinitionsCacheRefreshInterval { get; set; }
    /// <summary>
    /// Application timestamp timeout
    /// default: 1 minutes
    /// </summary>
    public TimeSpan TemplateDefinitionsCacheStampTimeOut { get; set; }
    /// <summary>
    /// Timestamp expiration time
    /// default: 30 days
    /// </summary>
    public TimeSpan TemplateDefinitionsCacheStampExpiration { get; set; }

    public AbpTextTemplatingCachingOptions()
    {
        MinimumCacheDuration = TimeSpan.FromHours(1);
        MaximumCacheDuration = TimeSpan.FromDays(30);

        SaveStaticTemplateDefinitionToDatabase = true;
        IsDynamicTemplateDefinitionStoreEnabled = false;
        TemplateDefinitionsCacheRefreshInterval = TimeSpan.FromHours(2);
        TemplateDefinitionsCacheStampTimeOut = TimeSpan.FromMinutes(1);
        TemplateDefinitionsCacheStampExpiration = TimeSpan.FromDays(30);
    }
}
