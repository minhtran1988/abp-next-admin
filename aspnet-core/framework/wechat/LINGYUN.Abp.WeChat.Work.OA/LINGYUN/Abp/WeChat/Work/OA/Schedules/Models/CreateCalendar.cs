namespace LINGYUN.Abp.WeChat.Work.OA.Schedules.Models;
/// <summary>
/// Add calendar
/// </summary>
public class CreateCalendar : CreateOrUpdateCalendar
{
    public CreateCalendar(string summary, string color, string? description)
        : base(summary, color, description)
    {
    }
}
