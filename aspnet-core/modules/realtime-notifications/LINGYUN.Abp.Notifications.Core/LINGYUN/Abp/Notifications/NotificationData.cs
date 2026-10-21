using LINGYUN.Abp.RealTime.Localization;
using System;
using System.Globalization;
using Volo.Abp.Data;
using Volo.Abp.EventBus;

namespace LINGYUN.Abp.Notifications;

/// <summary>
/// Notification data
/// </summary>
/// <remarks>
/// TODO: 2020-10-29 For users in different languages, it is a wrong design to localize the language during release.
/// Design the title and content of the notification as <see cref="LocalizableStringInfo"/> and let the client localize it by itself
/// </remarks>
[Serializable]
[EventName("notifications")]
public class NotificationData : IHasExtraProperties
{
    /// <summary>
    /// Information used to identify whether localization is required
    /// </summary>
    public const string LocalizerKey = "L";
    /// <summary>
    /// Used for localized culture
    /// </summary>
    public const string CultureKey = "C";

    public virtual string Type => GetType().FullName!;

    public object? this[string key]
    {
        get
        {
            return this.GetProperty(key);
        }
        set
        {
            this.SetProperty(key, value);
        }
    }

    public ExtraPropertyDictionary ExtraProperties { get; set; } = default!;

    public NotificationData()
    {
        ExtraProperties = new ExtraPropertyDictionary();
        this.SetDefaultsForExtraProperties();

        TrySetData(LocalizerKey, false);
    }
    /// <summary>
    /// Write localized message data
    /// </summary>
    /// <param name="title"></param>
    /// <param name="message"></param>
    /// <param name="createTime"></param>
    /// <param name="formUser"></param>
    /// <param name="description"></param>
    /// <returns></returns>
    public NotificationData WriteLocalizedData(
        LocalizableStringInfo title,
        LocalizableStringInfo message,
        DateTime createTime,
        string formUser,
        LocalizableStringInfo? description = null,
        string? culture = null)
    {
        TrySetData("title", title);
        TrySetData("message", message);
        TrySetData("formUser", formUser);
        TrySetData("createTime", createTime);
        TrySetData(LocalizerKey, true);
        TrySetData(CultureKey, culture ?? CultureInfo.CurrentCulture.Name);
        if (description != null)
        {
            TrySetData("description", description);
        }
        return this;
    }
    /// <summary>
    /// Write standard data
    /// </summary>
    /// <param name="title">Title</param>
    /// <param name="message">Content</param>
    /// <param name="createTime">Creation time</param>
    /// <param name="formUser">Source user</param>
    /// <param name="description">Additional instructions</param>
    /// <returns></returns>
    public NotificationData WriteStandardData(string title, string message, DateTime createTime, string formUser, string description = "")
    {
        TrySetData("title", title);
        TrySetData("message", message);
        TrySetData("description", description);
        TrySetData("formUser", formUser);
        TrySetData("createTime", createTime);
        TrySetData(LocalizerKey, false);
        return this;
    }
    /// <summary>
    /// Write standard data
    /// </summary>
    /// <param name="prefix">Data prefix</param>
    /// <param name="key">Identity</param>
    /// <param name="value">Data content</param>
    /// <returns></returns>
    public NotificationData WriteStandardData(string prefix, string key, object value)
    {
        TrySetData(string.Concat(prefix, key), value);
        TrySetData(LocalizerKey, false);
        return this;
    }
    /// <summary>
    /// Convert to standard data
    /// </summary>
    /// <param name="sourceData">Original data</param>
    /// <returns></returns>
    public static NotificationData ToStandardData(NotificationData sourceData)
    {
        var data = new NotificationData();
        data.TrySetData("title", sourceData.TryGetData("title"));
        data.TrySetData("message", sourceData.TryGetData("message"));
        data.TrySetData("description", sourceData.TryGetData("description"));
        data.TrySetData("formUser", sourceData.TryGetData("formUser"));
        data.TrySetData("createTime", sourceData.TryGetData("createTime"));
        data.TrySetData(LocalizerKey, sourceData.TryGetData(LocalizerKey));
        return data;
    }
    /// <summary>
    /// Convert to standard data
    /// </summary>
    /// <param name="prefix">Data prefix</param>
    /// <param name="sourceData">Original data</param>
    /// <returns></returns>
    public static NotificationData ToStandardData(string prefix, NotificationData sourceData)
    {
        var data = ToStandardData(sourceData);

        foreach (var property in sourceData.ExtraProperties)
        {
            if (property.Key.StartsWith(prefix))
            {
                var key = property.Key.Replace(prefix, "");
                data.TrySetData(key, property.Value);
            }
        }
        return data;
    }

    public object? TryGetData(string key)
    {
        return this.GetProperty(key);
    }
    public void TrySetData(string key, object? value)
    {
        this.SetProperty(key, value);
    }
    /// <summary>
    /// Needs localization
    /// </summary>
    /// <returns></returns>
    public bool NeedLocalizer()
    {
        var localizer = TryGetData(LocalizerKey);
        if (localizer != null && 
            bool.TryParse(localizer.ToString(), out var isLocalizer))
        {
            return isLocalizer;
        }
        return false;
    }
}
