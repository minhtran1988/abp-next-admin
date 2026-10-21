using System;

namespace LINGYUN.Abp.AuditLogging.OpenObserve;

/// <summary>
/// OpenObserve timestamp tool
/// </summary>
public static class OpenObserveTimestampHelper
{
    /// <summary>
    /// Convert to microsecond timestamp
    /// </summary>
    public static long ToMicroseconds(DateTime time)
    {
        return new DateTimeOffset(time.ToUniversalTime()).ToUnixTimeMilliseconds() * 1000L;
    }

    /// <summary>
    /// Try to overwrite <c>_timestamp</c> with log time
    /// </summary>
    /// <remarks>
    /// By default, OpenObserve only allows writing data within <c>ZO_INGEST_ALLOWED_UPTO</c> (default 5) hours.
    /// Data that exceeds the window will be discarded by the server, so when it exceeds, null is returned and the writing time is used by the server.
    /// </remarks>
    /// <param name="time">The business time of the log itself (such as AuditLog.ExecutionTime / SecurityLog.CreationTime)</param>
    /// <param name="now">Current time</param>
    /// <param name="allowedUptoHours">The number of hours allowed to be backtracked must be consistent with the server ZO_INGEST_ALLOWED_UPTO</param>
    public static long? TryGetTimestamp(DateTime time, DateTime now, int allowedUptoHours)
    {
        if (time == default)
        {
            return null;
        }

        if (allowedUptoHours > 0 && time.ToUniversalTime() < now.ToUniversalTime().AddHours(-allowedUptoHours))
        {
            return null;
        }

        return ToMicroseconds(time);
    }
}
