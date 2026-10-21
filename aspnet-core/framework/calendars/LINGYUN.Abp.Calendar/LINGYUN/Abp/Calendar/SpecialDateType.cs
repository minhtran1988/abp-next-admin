using System.ComponentModel;

namespace LINGYUN.Abp.Calendar;
/// <summary>
/// Special date type
/// </summary>
public enum SpecialDateType
{
    /// <summary>
    /// Legal holidays
    /// </summary>
    [Description("Legal Holidays")]
    Holiday = 0,
    /// <summary>
    /// Special working days
    /// </summary>
    [Description("Special working day")]
    SpecialWorkday = 1,
    /// <summary>
    /// Custom holidays
    /// </summary>
    [Description("Customized holidays")]
    CustomHoliday = 2
}
