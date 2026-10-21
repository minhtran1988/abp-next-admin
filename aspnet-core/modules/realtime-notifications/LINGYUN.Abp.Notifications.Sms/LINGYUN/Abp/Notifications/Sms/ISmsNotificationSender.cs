using System.Threading.Tasks;

namespace LINGYUN.Abp.Notifications.Sms;

/// <summary>
/// SMS notification sending interface
/// </summary>
/// <remarks>
/// Rewrite to implement customized SMS message processing
/// </remarks>
public interface ISmsNotificationSender
{
    /// <summary>
    /// Send notification
    /// </summary>
    /// <param name="notification">Notification data</param>
    /// <param name="phoneNumbers">Mobile phone number list, multiple mobile phone numbers are passed and separated</param>
    /// <returns></returns>
    Task SendAsync(NotificationInfo notification, string phoneNumbers);
}
