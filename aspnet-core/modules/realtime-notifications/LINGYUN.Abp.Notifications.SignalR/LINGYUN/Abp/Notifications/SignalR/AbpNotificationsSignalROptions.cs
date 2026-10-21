namespace LINGYUN.Abp.Notifications.SignalR;

public class AbpNotificationsSignalROptions
{
    /// <summary>
    /// Customized client subscription notification method name
    /// </summary>
    public string MethodName { get; set; }

    public AbpNotificationsSignalROptions()
    {
        MethodName = "get-notification";
    }
}
