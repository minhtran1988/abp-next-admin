using System;

namespace LINGYUN.Abp.BackgroundWorkers.Hangfire;

public class CronGenerator
{
    public const long MilliSecondsOfYear = 315_3600_0000L;
    public const long MilliSecondsOfMonth = 26_7840_0000L;
    public const int MilliSecondsOfWeek = 6_0480_0000;
    public const int MilliSecondsOfDays = 8640_0000;
    public const int MilliSecondsOfHours = 360_0000;
    public const int MilliSecondsOfMinutes = 60000;

    /// <summary>
    /// Calculate Cron expression from milliseconds
    /// </summary>
    /// <param name="milliseconds"></param>
    /// <returns></returns>
    public static string FormMilliseconds(long milliseconds = 1000)
    {
        if (milliseconds <= 1000)
        {
            return Seconds(0, 1);
        }

        if (milliseconds >= MilliSecondsOfYear)
        {
            return Year(1, 1, 0, 0, 2001, (int)(milliseconds / MilliSecondsOfYear));
        }

        if (milliseconds >= MilliSecondsOfMonth)
        {
            return Month(1, 0, 0, 1, (int)(milliseconds / MilliSecondsOfMonth));
        }

        // TODO: Task Cron in weekly units
        if (milliseconds >= MilliSecondsOfWeek)
        {
            return Day(0, 0, 1, (int)(milliseconds / MilliSecondsOfWeek));
        }

        if (milliseconds >= MilliSecondsOfDays)
        {
            return Day(0, 0, 1, (int)(milliseconds / MilliSecondsOfDays));
        }

        if (milliseconds >= MilliSecondsOfHours)
        {
            return Hour(0, 0, (int)(milliseconds / MilliSecondsOfHours));
        }

        if (milliseconds >= MilliSecondsOfMinutes)
        {
            return Minute(0, 0, (int)(milliseconds / MilliSecondsOfMinutes));
        }

        return Seconds(0, (int)(milliseconds / 1000));
    }
    /// <summary>
    /// Tasks with periodicity of seconds
    /// </summary>
    /// <param name="second">The second to start, the default is 0th second</param>
    /// <param name="interval">The interval between execution cycles, the default is once every 5 seconds</param>
    /// <returns></returns>
    public static string Seconds(int second = 0, int interval = 5)
    {
        Check.Range(second, nameof(second), 0, 59);

        return $"{second}/{interval} * * * * ? ";
    }

    /// <summary>
    /// Tasks with a periodicity of minutes
    /// </summary>
    /// <param name="second">The second to start, the default is 0th second</param>
    /// <param name="minute">Start at minute number, default is minute 0</param>
    /// <param name="interval">The interval between execution cycles, the default is once per minute</param>
    /// <returns></returns>
    public static string Minute(int second = 0, int minute = 0, int interval = 1)
    {
        Check.Range(second, nameof(second), 0, 59);
        Check.Range(minute, nameof(minute), 0, 59);

        return $"{second} {minute}/{interval} * * * ? ";
    }

    /// <summary>
    /// Tasks with hourly periodicity
    /// </summary>
    /// <param name="minute">Start at minute number, default is minute 0</param>
    /// <param name="hour">What hour does it start, default is 0 o'clock</param>
    /// <param name="interval">The interval between execution cycles, the default is once an hour</param>
    /// <returns></returns>
    public static string Hour(int minute = 0, int hour = 0, int interval = 1)
    {
        Check.Range(hour, nameof(hour), 0, 23);
        Check.Range(minute, nameof(minute), 0, 59);

        return $"0 {minute} {hour}/{interval} * * ? ";
    }

    /// <summary>
    /// Tasks with daily periodicity
    /// </summary>
    /// <param name="hour">What hour does it start, default starts at 0 o'clock</param>
    /// <param name="minute">Start at minute number, default starts at minute 0</param>
    /// <param name="day">Number of days to start, default starts from day 1</param>
    /// <param name="interval">Interval of execution cycle, default is once a day</param>
    /// <returns></returns>
    public static string Day(int hour = 0, int minute = 0, int day = 1, int interval = 1)
    {
        Check.Range(hour, nameof(hour), 0, 23);
        Check.Range(minute, nameof(minute), 0, 59);
        Check.Range(day, nameof(day), 1, 31);

        return $"0 {minute} {hour} {day}/{interval} * ? ";
    }

    /// <summary>
    /// Weekly tasks
    /// </summary>
    /// <param name="dayOfWeek">The day of the week starts, default starts at Monday o'clock</param>
    /// <param name="hour">What hour does it start, default starts at 0 o'clock</param>
    /// <param name="minute">Start at minute number, default starts at minute 0</param>
    /// <returns></returns>
    public static string Week(DayOfWeek dayOfWeek = DayOfWeek.Monday, int hour = 0, int minute = 0)
    {
        Check.Range(hour, nameof(hour), 0, 23);
        Check.Range(minute, nameof(minute), 0, 59);

        return $"{minute} {hour} * * {dayOfWeek.ToString().Substring(0, 3)}";
    }

    /// <summary>
    /// Tasks with monthly periodicity
    /// </summary>
    /// <param name="day">What day does it start, default starts from the 1st</param>
    /// <param name="hour">What hour does it start, default starts at 0 o'clock</param>
    /// <param name="minute">Start at minute number, default starts at minute 0</param>
    /// <param name="month">What month does it start from? The default is month 1</param>
    /// <param name="interval">Month interval</param>
    /// <returns></returns>
    public static string Month(int day = 1, int hour = 0, int minute = 0, int month = 1, int interval = 1)
    {
        Check.Range(hour, nameof(hour), 0, 23);
        Check.Range(minute, nameof(minute), 0, 59);
        Check.Range(day, nameof(day), 1, 31);

        return $"0 {minute} {hour} {day} {month}/{interval} ?";
    }

    /// <summary>
    /// Tasks with annual periodicity
    /// </summary>
    /// <param name="month">The month to start, the default is January</param>
    /// <param name="day">What day does it start, default starts from the 1st</param>
    /// <param name="hour">What hour does it start, default starts at 0 o'clock</param>
    /// <param name="year">Start from the first year, default starts from 2001</param>
    /// <param name="minute">Start at minute number, default starts at minute 0</param>
    /// <returns></returns>
    public static string Year(int month = 1, int day = 1, int hour = 0, int minute = 0, int year = 2001, int interval = 1)
    {
        Check.Range(hour, nameof(hour), 0, 23);
        Check.Range(minute, nameof(minute), 0, 59);
        Check.Range(day, nameof(day), 1, 31);
        Check.Range(month, nameof(month), 1, 12);

        return $"0 {minute} {hour} {day} {month} ? {year}/{interval}";
    }
}
