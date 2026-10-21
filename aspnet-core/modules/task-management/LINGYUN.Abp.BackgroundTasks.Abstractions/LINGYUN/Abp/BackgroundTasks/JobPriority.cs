namespace LINGYUN.Abp.BackgroundTasks;
/// <summary>
/// Task priority
/// </summary>
/// <remarks>
/// Keep consistent with the framework
/// </remarks>
public enum JobPriority
{
    Low = 5,
    BelowNormal = 10,
    Normal = 0xF,
    AboveNormal = 20,
    High = 25
}
