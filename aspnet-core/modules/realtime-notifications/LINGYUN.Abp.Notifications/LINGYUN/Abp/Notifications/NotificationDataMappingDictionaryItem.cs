using System;

namespace LINGYUN.Abp.Notifications;

public class NotificationDataMappingDictionaryItem
{
    /// <summary>
    /// Notification name
    /// </summary>
    public string Name { get; }
    /// <summary>
    /// Conversion method
    /// </summary>
    public Func<NotificationData, NotificationData> MappingFunc { get; private set; }

    public NotificationDataMappingDictionaryItem(string name, Func<NotificationData, NotificationData> func)
    {
        Name = name;
        MappingFunc = func;
    }

    public void Replace(Func<NotificationData, NotificationData> func)
    {
        MappingFunc = func;
    }
}
