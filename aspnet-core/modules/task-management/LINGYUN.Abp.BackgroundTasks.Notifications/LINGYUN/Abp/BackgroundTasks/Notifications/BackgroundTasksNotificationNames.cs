namespace LINGYUN.Abp.BackgroundTasks.Notifications;

public static class BackgroundTasksNotificationNames
{
    public const string GroupName = "Abp.Notifications.BackgroundTasks";
    /// <summary>
    /// Job execution successful
    /// </summary>
    public const string JobExecuteSucceeded = GroupName + ".ExecuteSucceeded";
    /// <summary>
    /// Job execution failed
    /// </summary>
    public const string JobExecuteFailed = GroupName + ".ExecuteFailed";
    /// <summary>
    /// Job execution completed
    /// </summary>
    public const string JobExecuteCompleted = GroupName + ".ExecuteCompleted";
}
