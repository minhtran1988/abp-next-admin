namespace LINGYUN.Abp.EventBus.CAP;

/// <summary>
/// Expired message cleaning configuration items
/// </summary>
public class AbpCAPEventBusOptions
{
    /// <summary>
    /// Publish message processing failure notification
    /// default: false
    /// </summary>
    public bool NotifyFailedCallback { get; set; } = false;
}
