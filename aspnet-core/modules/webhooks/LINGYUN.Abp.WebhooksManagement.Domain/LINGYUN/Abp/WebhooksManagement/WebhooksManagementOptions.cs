using System;

namespace LINGYUN.Abp.WebhooksManagement;
public class WebhooksManagementOptions
{
    /// <summary>
    /// Default: true.
    /// </summary>
    public bool SaveStaticWebhooksToDatabase { get; set; }
    /// <summary>
    /// Default: false.
    /// </summary>
    public bool IsDynamicWebhookStoreEnabled { get; set; }
    /// <summary>
    /// Cache refresh time
    /// default: 30 seconds
    /// </summary>
    public TimeSpan WebhooksCacheRefreshInterval { get; set; }
    /// <summary>
    /// Application timestamp timeout
    /// default: 2 minutes
    /// </summary>
    public TimeSpan WebhooksCacheStampTimeOut { get; set; }
    /// <summary>
    /// Timestamp expiration time
    /// default: 30 minutes
    /// </summary>
    public TimeSpan WebhooksCacheStampExpiration { get; set; }
    public WebhooksManagementOptions()
    {
        SaveStaticWebhooksToDatabase = true;

        WebhooksCacheRefreshInterval = TimeSpan.FromSeconds(30);
        WebhooksCacheStampTimeOut = TimeSpan.FromMinutes(2);
        // Refresh the cache after 30 minutes expiration
        WebhooksCacheStampExpiration = TimeSpan.FromMinutes(30);
    }
}
