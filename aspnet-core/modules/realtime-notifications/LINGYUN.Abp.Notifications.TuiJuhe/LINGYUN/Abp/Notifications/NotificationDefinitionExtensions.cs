using LINGYUN.Abp.TuiJuhe.Messages;

namespace LINGYUN.Abp.Notifications;

public static class NotificationDefinitionExtensions
{
    private const string Prefix = "tui-juhe:";
    private const string ServiceIdKey = Prefix + "serviceId";

    /// <summary>
    /// Get the message content type
    /// </summary>
    /// <param name="notification"></param>
    /// <param name="defaultContentType"></param>
    /// <returns></returns>
    public static MessageContentType GetContentTypeOrDefault(
        this NotificationDefinition notification,
        MessageContentType defaultContentType = MessageContentType.Text)
    {
        return notification.ContentType switch
        {
            NotificationContentType.Text => MessageContentType.Text,
            NotificationContentType.Html => MessageContentType.Html,
            NotificationContentType.Markdown => MessageContentType.Markdown,
            NotificationContentType.Json => MessageContentType.Text,
            _ => defaultContentType,
        };
    }
    /// <summary>
    /// Specify service number
    /// </summary>
    /// <param name="notification">Group encoding</param>
    /// <param name="serviceId">Service number, ServiceID is automatically generated after the service is created. It is the unique identifier of the service and can be viewed in the details page of each service</param>
    /// <returns>
    /// <see cref="NotificationDefinition"/>
    /// </returns>
    public static NotificationDefinition WithServiceId(
        this NotificationDefinition notification,
        string serviceId)
    {
        return notification.WithProperty(ServiceIdKey, serviceId);
    }
    /// <summary>
    /// Get service number
    /// </summary>
    /// <param name="notification"></param>
    public static string GetServiceIdOrNull(
        this NotificationDefinition notification)
    {
        if (notification.Properties.TryGetValue(ServiceIdKey, out var serviceIdDefine))
        {
            return serviceIdDefine.ToString();
        }

        return null;
    }
}
