using Microsoft.Extensions.Options;
using System;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace LINGYUN.Abp.Calendar;

public class WorkdayCalendarService : IWorkdayCalendarService, ITransientDependency
{
    private readonly AbpCalendarOptions _options;
    private readonly IWorkdayCalendarStore _workdayCalendarStore;

    public WorkdayCalendarService(
        IOptionsMonitor<AbpCalendarOptions> options, 
        IWorkdayCalendarStore workdayCalendarStore)
    {
        _options = options.CurrentValue;
        _workdayCalendarStore = workdayCalendarStore;
    }

    public async virtual Task<DateOnly> GetNextWorkdayAsync(DateOnly date)
    {
        var nextDay = date.AddDays(1);
        // limited to 31 days
        var maxAttempts = 30;

        for (var i = 0; i < maxAttempts; i++)
        {
            if (await IsWorkdayAsync(nextDay))
            {
                return nextDay;
            }
            nextDay = nextDay.AddDays(1);
        }

        // If the working day cannot be found within 30 days, return the original date + 1 day
        return date.AddDays(1);
    }

    public async virtual Task<bool> IsWorkdayAsync(DateOnly date)
    {
        // Special working day definition
        var specialDate = await _workdayCalendarStore.FindSpecialDateAsync(date);
        if (specialDate != null)
        {
            return specialDate.Type == SpecialDateType.SpecialWorkday;
        }
        // Get the default work calendar definition
        var calendar = await _workdayCalendarStore.FindDefaultCalendarAsync();
        if (calendar == null)
        {
            // The default working calendar is not defined, get the system-defined working day range
            return _options.DefaultWorkdays.Contains(date.DayOfWeek);
        }
        // Special working day range
        if (calendar.SpecialDates != null &&
            calendar.SpecialDates.Any(x => x.Date == date && x.Type == SpecialDateType.SpecialWorkday))
        {
            return true;
        }
        // working day range
        if (!calendar.Workdays.Contains(date.DayOfWeek))
        {
            return false;
        }

        return false;
    }

    public async virtual Task<bool> IsInWorkTimeAsync(DateTime time)
    {
        var nowDate = DateOnly.FromDateTime(time);
        // Special working day definition
        var specialDate = await _workdayCalendarStore.FindSpecialDateAsync(nowDate);
        if (specialDate != null)
        {
            return specialDate.Type == SpecialDateType.SpecialWorkday;
        }
        // Get the default work calendar definition
        var calendar = await _workdayCalendarStore.FindDefaultCalendarAsync();
        if (calendar == null)
        {
            // The default working calendar is not defined, get the system-defined working day range
            if (!_options.DefaultWorkdays.Contains(nowDate.DayOfWeek))
            {
                return false;
            }
            if (_options.EnableWorkTimeRestriction &&
                !_options.DefaultWorkTimes.Any(timeRange => time.TimeOfDay >= timeRange.StartTime && time.TimeOfDay <= timeRange.EndTime))
            {
                return false;
            }

            return true;
        }
        // Special working day range
        if (calendar.SpecialDates != null &&
            calendar.SpecialDates.Any(x => x.Date == nowDate && x.Type == SpecialDateType.SpecialWorkday))
        {
            return true;
        }
        // working day range
        if (!calendar.Workdays.Contains(nowDate.DayOfWeek))
        {
            return false;
        }
        // working time range
        if (calendar.EnableWorkTimeRestriction && 
            calendar.WorkTimes != null)
        {
            return calendar.WorkTimes.Any(timeRange => time.TimeOfDay >= timeRange.StartTime && time.TimeOfDay <= timeRange.EndTime);
        }

        return false;
    }
}
