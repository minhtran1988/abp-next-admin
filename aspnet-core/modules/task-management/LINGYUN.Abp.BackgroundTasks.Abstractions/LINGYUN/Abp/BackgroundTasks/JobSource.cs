namespace LINGYUN.Abp.BackgroundTasks;
/// <summary>
/// Job source
/// </summary>
public enum JobSource
{
    /// <summary>
    /// undefined
    /// </summary>
    None = -1,
    /// <summary>
    /// User
    /// </summary>
    User = 0,
    /// <summary>
    /// Built-in system
    /// </summary>
    System = 10,
}
