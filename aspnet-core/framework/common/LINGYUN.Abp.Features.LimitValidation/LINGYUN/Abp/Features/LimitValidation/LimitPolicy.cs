namespace LINGYUN.Abp.Features.LimitValidation;

public enum LimitPolicy : byte
{
    /// <summary>
    /// Limit by minutes
    /// </summary>
    Minute = 0,
    /// <summary>
    /// Limit by hour
    /// </summary>
    Hours = 10,
    /// <summary>
    /// Limited by day
    /// </summary>
    Days = 20,
    /// <summary>
    /// Limited by week
    /// </summary>
    Weeks = 30,
    /// <summary>
    /// Monthly limit
    /// </summary>
    Month = 40,
    /// <summary>
    /// Limited by year
    /// </summary>
    Years = 50
}
