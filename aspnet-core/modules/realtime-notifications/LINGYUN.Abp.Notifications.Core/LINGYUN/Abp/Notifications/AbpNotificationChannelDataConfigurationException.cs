using System;

namespace LINGYUN.Abp.Notifications;

/// <summary>
/// Channel data configuration abnormality
/// </summary>
/// <remarks>
/// Thrown when the contract verification fails during the registration period. Purpose: to expose configuration errors during the startup phase.
/// Instead of silently discarding notifications in production environments.
/// </remarks>
public class AbpNotificationChannelDataConfigurationException : Exception
{
    public AbpNotificationChannelDataConfigurationException(string message)
        : base(message)
    {
    }

    public AbpNotificationChannelDataConfigurationException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}
