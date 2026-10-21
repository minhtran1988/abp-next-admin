using System;
using System.Collections.Generic;
using System.Linq;

namespace LINGYUN.Abp.Notifications;

public class NotificationDataMappingDictionary : Dictionary<string, List<NotificationDataMappingDictionaryItem>>
{
    public static string DefaultKey { get; set; } = "Default";
    /// <summary>
    /// Process the data of a notification
    /// Specific to a provider
    /// </summary>
    /// <param name="provider"></param>
    /// <param name="name"></param>
    /// <param name="func"></param>
    public void Mapping(string provider, string name, Func<NotificationData, NotificationData> func)
    {
        if (!ContainsKey(provider))
        {
            this[provider] = new List<NotificationDataMappingDictionaryItem>();
        }

        var mapItem = this[provider].FirstOrDefault(item => item.Name.Equals(name));

        if (mapItem == null)
        {
            this[provider].Add(new NotificationDataMappingDictionaryItem(name, func));
        }
        else
        {
            mapItem.Replace(func);
        }
    }
    /// <summary>
    /// Process all notification data
    /// Specific to a provider
    /// </summary>
    /// <param name="provider"></param>
    /// <param name="func"></param>
    public void MappingDefault(string provider, Func<NotificationData, NotificationData> func)
    {
        Mapping(provider, DefaultKey, func);
    }
    /// <summary>
    /// Get the method that needs to process the data
    /// </summary>
    /// <param name="name"></param>
    /// <param name="provider"></param>
    /// <returns></returns>
    public NotificationDataMappingDictionaryItem? GetMapItemOrDefault(string provider, string name)
    {
        if (ContainsKey(provider))
        {
            return this[provider].GetOrNullDefault(name);
        }
        return null;
    }
}
