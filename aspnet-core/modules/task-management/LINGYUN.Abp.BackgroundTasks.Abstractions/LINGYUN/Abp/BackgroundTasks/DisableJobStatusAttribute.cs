using System;

namespace LINGYUN.Abp.BackgroundTasks;
/// <summary>
/// Disable job scheduling status
/// </summary>
[AttributeUsage(AttributeTargets.Class)]
public class DisableJobStatusAttribute : Attribute
{
}
