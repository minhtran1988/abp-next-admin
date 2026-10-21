using System.Collections.Generic;

namespace LINGYUN.Abp.BackgroundTasks;

/// <summary>
/// Task event provider
/// </summary>
public interface IJobEventProvider
{
    /// <summary>
    /// Returns all task event registration interfaces
    /// </summary>
    /// <returns></returns>
    IReadOnlyCollection<IJobEvent> GetAll();
}
