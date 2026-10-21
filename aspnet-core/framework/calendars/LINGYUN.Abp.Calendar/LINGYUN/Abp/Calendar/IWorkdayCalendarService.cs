using System;
using System.Threading.Tasks;

namespace LINGYUN.Abp.Calendar;

public interface IWorkdayCalendarService
{
    /// <summary>
    /// Check whether the specified time is on a working day
    /// </summary>
    Task<bool> IsWorkdayAsync(DateOnly date);
    /// <summary>
    /// Check whether the specified time is within working hours
    /// </summary>
    /// <param name="dateTime"></param>
    /// <returns></returns>
    Task<bool> IsInWorkTimeAsync(DateTime dateTime);
    /// <summary>
    /// Get the next working day
    /// </summary>
    /// <param name="date"></param>
    /// <returns></returns>
    Task<DateOnly> GetNextWorkdayAsync(DateOnly date);
}
