namespace LINGYUN.Abp.BackgroundTasks.Jobs;

public static class DefaultJobNames
{
    /// <summary>
    /// Send email
    /// </summary>
    public const string SendEmailJob = "SendEmail";
    /// <summary>
    /// Send SMS
    /// </summary>
    public const string SendSmsJob = "SendSms";
    /// <summary>
    /// Console output
    /// </summary>
    public const string ConsoleJob = "Console";
    /// <summary>
    /// sleep
    /// </summary>
    public const string SleepJob = "Sleep";
    /// <summary>
    /// Call between services
    /// </summary>
    public const string ServiceInvocationJob = "ServiceInvocation";
    /// <summary>
    /// Http request
    /// </summary>
    public const string HttpRequestJob = "HttpRequest";
}
