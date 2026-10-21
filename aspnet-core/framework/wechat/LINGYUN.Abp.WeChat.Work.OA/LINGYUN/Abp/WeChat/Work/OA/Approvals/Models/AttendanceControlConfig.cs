using JetBrains.Annotations;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.WeChat.Work.OA.Approvals.Models;
/// <summary>
/// Leave component - business trip/outing/overtime component configuration
/// </summary>
public class AttendanceControlConfig : ControlConfig
{
    /// <summary>
    /// Leave component - business trip/outing/overtime component
    /// </summary>
    [NotNull]
    [JsonProperty("attendance")]
    [JsonPropertyName("attendance")]
    public AttendanceConfig Attendance { get; set; } = default!;
    public AttendanceControlConfig()
    {

    }

    public AttendanceControlConfig(AttendanceConfig attendance)
    {
        Attendance = attendance;
    }
}

public class AttendanceConfig
{
    /// <summary>
    /// Leave component type: 3-business trip; 4-outing; 5-overtime
    /// </summary>
    [NotNull]
    [JsonProperty("type")]
    [JsonPropertyName("type")]
    public byte Type { get; set; }
    /// <summary>
    /// Time selection range of holiday component
    /// </summary>
    [NotNull]
    [JsonProperty("date_range")]
    [JsonPropertyName("date_range")]
    public DateRangeConfig DateRange { get; set; } = default!;
    public AttendanceConfig()
    {

    }

    public AttendanceConfig(byte type, DateRangeConfig dateRange)
    {
        Type = type;
        DateRange = dateRange;
    }
}