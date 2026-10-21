using Hangfire;
using System;

namespace Volo.Abp.BackgroundJobs;

public class CronGenerator
{
    /// <summary>
    /// Tasks with a periodicity of minutes
    /// </summary>
    /// <param name="interval">The interval between execution cycles, the default is once per minute</param>
    /// <returns></returns>
    public static string Minute(int interval = 1)
    {
        return $"1 0/{interval} * * * ? ";
    }

    /// <summary>
    /// Tasks with hourly periodicity
    /// </summary>
    /// <param name="minute">Start at the minute, the default is the first minute</param>
    /// <param name="interval">The interval between execution cycles, the default is once an hour</param>
    /// <returns></returns>
    public static string Hour(int minute = 1, int interval = 1)
    {
        return $"1 {minute} 0/ {interval} * * ? ";
    }

    /// <summary>
    /// Tasks with daily periodicity
    /// </summary>
    /// <param name="hour">What hour does it start, default starts at 1 o'clock</param>
    /// <param name="minute">Start at the minute, the default is from the 1st minute</param>
    /// <param name="interval">Interval of execution cycle, default is once a day</param>
    /// <returns></returns>
    public static string Day(int hour = 1, int minute = 1, int interval = 1)
    {
        return $"1 {minute} {hour} 1/ {interval} * ? ";
    }

    /// <summary>
    /// Weekly tasks
    /// </summary>
    /// <param name="dayOfWeek">The day of the week starts, default starts at Monday o'clock</param>
    /// <param name="hour">What hour does it start, default starts at 1 o'clock</param>
    /// <param name="minute">Start at the minute, the default is from the 1st minute</param>
    /// <returns></returns>
    public static string Week(DayOfWeek dayOfWeek = DayOfWeek.Monday, int hour = 1, int minute = 1)
    {
        return Cron.Weekly(dayOfWeek, hour, minute);
    }

    /// <summary>
    /// Tasks with monthly periodicity
    /// </summary>
    /// <param name="day">What day does it start, default starts from the 1st</param>
    /// <param name="hour">What hour does it start, default starts at 1 o'clock</param>
    /// <param name="minute">Start at the minute, the default is from the 1st minute</param>
    /// <returns></returns>
    public static string Month(int day = 1, int hour = 1, int minute = 1)
    {
        return Cron.Monthly(day, hour, minute);
    }

    /// <summary>
    /// Tasks with annual periodicity
    /// </summary>
    /// <param name="month">The month to start, the default is January</param>
    /// <param name="day">What day does it start, default starts from the 1st</param>
    /// <param name="hour">What hour does it start, default starts at 1 o'clock</param>
    /// <param name="minute">Start at the minute, the default is from the 1st minute</param>
    /// <returns></returns>
    public static string Year(int month = 1, int day = 1, int hour = 1, int minute = 1)
    {
        return Cron.Yearly(month, day, hour, minute);
    }
}
