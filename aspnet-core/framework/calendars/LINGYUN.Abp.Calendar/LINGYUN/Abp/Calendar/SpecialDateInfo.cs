using System;

namespace LINGYUN.Abp.Calendar;
/// <summary>
/// Special date
/// </summary>
public class SpecialDateInfo
{
    /// <summary>
    /// Date
    /// </summary>
    public DateOnly Date { get; set; }
    /// <summary>
    /// Special date type
    /// </summary>
    public SpecialDateType Type { get; set; }
    /// <summary>
    /// Special date description
    /// </summary>
    public string? Description { get; set; }
}
