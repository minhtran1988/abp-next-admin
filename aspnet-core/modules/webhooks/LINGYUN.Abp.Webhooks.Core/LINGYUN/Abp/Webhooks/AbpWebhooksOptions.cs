using System;
using System.Collections.Generic;
using System.Reflection;
using Volo.Abp.Collections;

namespace LINGYUN.Abp.Webhooks;

public class AbpWebhooksOptions
{
    /// <summary>
    /// Default timeout
    /// </summary>
    public TimeSpan TimeoutDuration { get; set; }
    /// <summary>
    /// Default maximum number of sending times
    /// </summary>
    public int MaxSendAttemptCount { get; set; }
    /// <summary>
    /// Whether to automatically cancel the subscription when the maximum number of consecutive failures is reached
    /// </summary>
    public bool IsAutomaticSubscriptionDeactivationEnabled { get; set; }
    /// <summary>
    /// Maximum number of consecutive failures before canceling subscription
    /// </summary>
    public int MaxConsecutiveFailCountBeforeDeactivateSubscription { get; set; }

    public ITypeList<IWebhookDefinitionProvider> DefinitionProviders { get; }

    public HashSet<string> DeletedWebhooks { get; }

    public HashSet<string> DeletedWebhookGroups { get; }
    /// <summary>
    /// Default request header
    /// </summary>
    public IDictionary<string, string> DefaultHttpHeaders { get; }
    /// <summary>
    /// Default sender ID
    /// </summary>
    public string DefaultAgentIdentifier { get; set; }
    public AbpWebhooksOptions()
    {
        TimeoutDuration = TimeSpan.FromSeconds(60);
        MaxSendAttemptCount = 5;
        MaxConsecutiveFailCountBeforeDeactivateSubscription = MaxSendAttemptCount * 3;

        DefinitionProviders = new TypeList<IWebhookDefinitionProvider>();

        DeletedWebhooks = new HashSet<string>();
        DeletedWebhookGroups = new HashSet<string>();

        DefaultHttpHeaders = new Dictionary<string, string>
        {
            // Cancel response content packaging
            { "_AbpDontWrapResult", "true" },
            // TODO: Possible cross-domain impact
            // { "X-Requested-With", "XMLHttpRequest" },
            // identify source
            { "X-Requested-From", "abp-webhooks" },
        };

        DefaultAgentIdentifier = "Abp-Webhooks";

        var assembly = typeof(AbpWebhooksOptions).Assembly;
        var versionAttr = assembly.GetCustomAttribute<AssemblyFileVersionAttribute>();
        if (versionAttr != null)
        {
            DefaultAgentIdentifier += "/" + versionAttr.Version;
        }
    }

    public void AddHeader(string key, string value)
    {
        if (value.IsNullOrWhiteSpace())
        {
            return;
        }
        DefaultHttpHeaders[key] = value;
    }
}
