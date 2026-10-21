using System;

namespace LINGYUN.Abp.Calendar;

/// <summary>
/// Work calendar information
/// </summary>
public class WorkdayCalendarInfo
{
    /// <summary>
    /// Calendar name
    /// </summary>
    public string Name { get; set; } = default!;
    /// <summary>
    /// Default calendar
    /// </summary>
    public bool IsDefault { get; set; }
    /// <summary>
    /// Working day range
    /// </summary>
    public DayOfWeek[] Workdays { get; set; } = default!;
    /// <summary>
    /// Working time range
    /// </summary>
    public WorkTimeRange[]? WorkTimes { get; set; }
    /// <summary>
    /// Whether to enable working time limits
    /// </summary>
    public bool EnableWorkTimeRestriction { get; set; }
    /// <summary>
    /// Special date list
    /// </summary>
    public SpecialDateInfo[]? SpecialDates { get; set; }
}
