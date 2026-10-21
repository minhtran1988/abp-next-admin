namespace LINGYUN.Abp.BackgroundTasks;
/// <summary>
/// Task category
/// </summary>
public enum JobType
{
    /// <summary>
    /// Disposable
    /// </summary>
    Once,
    /// <summary>
    /// Periodicity
    /// </summary>
    Period,
    /// <summary>
    /// persistence
    /// </summary>
    Persistent
}
