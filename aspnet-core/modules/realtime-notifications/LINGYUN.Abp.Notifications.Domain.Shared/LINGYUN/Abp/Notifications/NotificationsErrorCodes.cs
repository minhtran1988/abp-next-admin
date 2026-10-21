namespace LINGYUN.Abp.Notifications;

/// <summary>
/// Message system error code design
/// The status code is divided into two parts: the first 2 digits field and the last 3 digits status
/// <br />
/// Domain part:
/// <list type="table">
/// <item>000-019 input</item>
/// <item>020-029 Group</item>
/// <item>030-039 User</item>
/// <item>040-049 Application</item>
/// <item>050-059 Internal</item>
/// <item>100-199 output</item>
/// </list>
/// 
/// Status part:
/// <list type="table">
/// <item>200-299 Success</item>
/// <item>300-399 Success but follow-up operations</item>
/// <item>400-499 Business exception</item>
/// <item>500-599 Internal exception</item>
/// <item>900-999 Input and output exception</item>
/// </list>
/// 
/// </summary>
public class NotificationsErrorCodes
{
    public const string Namespace = "Notifications";
    /// <summary>
    /// The notification template does not exist!
    /// </summary>
    public const string NotificationTemplateNotFound = Namespace + ":001404";

    public static class GroupDefinition
    {
        private const string Prefix = Namespace + ":002";

        public const string StaticGroupNotAllowedChanged = Prefix + "400";

        public const string AlreayNameExists = Prefix + "403";

        public const string NameNotFount = Prefix + "404";
    }

    public static class Definition
    {
        private const string Prefix = Namespace + ":003";

        public const string StaticFeatureNotAllowedChanged = Prefix + "400";

        public const string FailedGetGroup = Prefix + "401";

        public const string AlreayNameExists = Prefix + "403";

        public const string NameNotFount = Prefix + "404";
    }
}
