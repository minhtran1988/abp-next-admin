using System;
using System.Threading.Tasks;

namespace LINGYUN.Abp.Calendar;

public interface IWorkdayCalendarStore
{
    /// <summary>
    /// Get special date
    /// </summary>
    /// <param name="date">Date</param>
    /// <returns></returns>
    Task<SpecialDateInfo?> FindSpecialDateAsync(DateOnly date);
    /// <summary>
    /// Get the default work calendar
    /// </summary>
    Task<WorkdayCalendarInfo?> FindDefaultCalendarAsync();
}
