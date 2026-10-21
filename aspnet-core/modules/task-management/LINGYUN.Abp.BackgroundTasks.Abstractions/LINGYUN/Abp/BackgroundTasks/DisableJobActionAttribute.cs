using System;

namespace LINGYUN.Abp.BackgroundTasks;
/// <summary>
/// Disable job scheduling behavior
/// </summary>
[AttributeUsage(AttributeTargets.Class)]
public class DisableJobActionAttribute : Attribute
{
}
