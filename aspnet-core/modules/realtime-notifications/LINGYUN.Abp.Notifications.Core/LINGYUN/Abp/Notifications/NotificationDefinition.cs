using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using Volo.Abp;
using Volo.Abp.Localization;
using Volo.Abp.TextTemplating;

/*
 * 2020-10-29 Reconstruction Notice
 * INotificationSender specifies the receiver. If not specified, all subscriber users will be queried. If the publisher is specified, publish directly (check whether to subscribe)
 */

namespace LINGYUN.Abp.Notifications;

public class NotificationDefinition
{
    /// <summary>
    /// Notification name
    /// </summary>
    [NotNull]
    public string Name { get; set; }
    /// <summary>
    /// Notification display name
    /// </summary>
    [NotNull]
    public ILocalizableString DisplayName
    {
        get => _displayName;
        set => _displayName = Check.NotNull(value, nameof(value));
    }
    private ILocalizableString _displayName = default!;
    /// <summary>
    /// Notification description
    /// </summary>
    [CanBeNull]
    public ILocalizableString? Description { get; set; }
    /// <summary>
    /// Allow clients to display subscriptions
    /// </summary>
    public bool AllowSubscriptionToClients { get; set; }
    /// <summary>
    /// Survival type
    /// </summary>
    public NotificationLifetime NotificationLifetime { get; set; }
    /// <summary>
    /// Notification type
    /// </summary>
    public NotificationType NotificationType { get; set; }
    /// <summary>
    /// Notification content type
    /// </summary>
    public NotificationContentType ContentType { get; set; }
    /// <summary>
    /// Notify provider
    /// </summary>
    public List<string> Providers { get; }
    /// <summary>
    /// Notification template
    /// </summary>
    public TemplateDefinition? Template { get; private set; }
    /// <summary>
    /// Additional attributes
    /// </summary>
    [NotNull]
    public Dictionary<string, object?> Properties { get; }

    public object? this[string name] {
        get => Properties.GetOrDefault(name);
        set => Properties[name] = value;
    }

    public NotificationDefinition(
       string name,
       ILocalizableString? displayName = null,
       ILocalizableString? description = null,
       NotificationType notificationType = NotificationType.Application,
       NotificationLifetime lifetime = NotificationLifetime.Persistent,
       NotificationContentType contentType = NotificationContentType.Text,
       bool allowSubscriptionToClients = false)
    {
        Name = name;
        DisplayName = displayName ?? new FixedLocalizableString(name);
        Description = description;
        NotificationLifetime = lifetime;
        NotificationType = notificationType;
        ContentType = contentType;
        AllowSubscriptionToClients = allowSubscriptionToClients;

        Providers = new List<string>();
        Properties = new Dictionary<string, object?>();
    }

    public virtual NotificationDefinition WithProviders(params string[] providers)
    {
        if (!providers.IsNullOrEmpty())
        {
            Providers.AddIfNotContains(providers);
        }

        return this;
    }

    public virtual NotificationDefinition WithTemplate(
        Type localizationResource, 
        bool isLayout = false, 
        string? layout = null, 
        string? defaultCultureName = null)
    {
        Template = new TemplateDefinition(
            Name,
            localizationResource,
            DisplayName,
            isLayout,
            layout,
            defaultCultureName);

        return this;
    }

    public virtual NotificationDefinition WithTemplate(Action<TemplateDefinition> setup)
    {
        if (Template != null)
        {
            setup(Template);
        }
        else
        {
            var template = new TemplateDefinition(Name);
            setup(template);

            Template = template;
        }

        return this;
    }

    public virtual NotificationDefinition WithTemplate(TemplateDefinition? template)
    {
        Template = template;

        return this;
    }

    public virtual NotificationDefinition WithProperty(string key, object value)
    {
        Properties[key] = value;
        return this;
    }

    public override string ToString()
    {
        return $"[{nameof(NotificationDefinition)} {Name}]";
    }
}
