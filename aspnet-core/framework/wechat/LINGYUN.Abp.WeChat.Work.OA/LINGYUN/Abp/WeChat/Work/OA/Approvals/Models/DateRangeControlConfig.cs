using JetBrains.Annotations;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.WeChat.Work.OA.Approvals.Models;
/// <summary>
/// Duration component configuration
/// </summary>
public class DateRangeControlConfig : ControlConfig
{
    /// <summary>
    /// Duration component
    /// </summary>
    [NotNull]
    [JsonProperty("date_range")]
    [JsonPropertyName("date_range")]
    public DateRangeConfig DateRange { get; set; } = default!;
    public DateRangeControlConfig()
    {

    }

    public DateRangeControlConfig(DateRangeConfig dateRange)
    {
        DateRange = dateRange;
    }
}
