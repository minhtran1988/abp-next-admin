using System;

namespace LINGYUN.Abp.Calendar;
/// <summary>
/// Working time range
/// </summary>
public class WorkTimeRange
{
    /// <summary>
    /// Start time
    /// </summary>
    public TimeSpan StartTime { get; set; }
    /// <summary>
    /// Deadline
    /// </summary>
    public TimeSpan EndTime { get; set; }
    public WorkTimeRange()
    {

    }

    public WorkTimeRange(TimeSpan startTime, TimeSpan endTime)
    {
        StartTime = startTime;
        EndTime = endTime;
    }
}
