using JetBrains.Annotations;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.WeChat.Work.OA.Approvals.Models;
/// <summary>
/// Leave component - business trip/outing/overtime component value
/// </summary>
public class AttendanceControlValue : ControlValue
{
    /// <summary>
    /// Duration
    /// </summary>
    [NotNull]
    [JsonProperty("attendance")]
    [JsonPropertyName("attendance")]
    public AttendanceValue Attendance { get; set; } = default!;
    public AttendanceControlValue()
    {

    }

    public AttendanceControlValue(AttendanceValue attendance)
    {
        Attendance = attendance;
    }
}

public class AttendanceValue
{
    /// <summary>
    /// Time selection range of holiday component
    /// </summary>
    [NotNull]
    [JsonProperty("date_range")]
    [JsonPropertyName("date_range")]
    public DateRangeValue DateRange { get; set; } = default!;
    /// <summary>
    /// Leave and attendance component types: 1-leave; 3-business trip; 4-outing; 5-overtime
    /// </summary>
    [NotNull]
    [JsonProperty("type")]
    [JsonPropertyName("type")]
    public byte Type { get; set; }
    /// <summary>
    /// Optional. Duration slice information
    /// </summary>
    [CanBeNull]
    [JsonProperty("slice_info")]
    [JsonPropertyName("slice_info")]
    public AttendanceSliceInfo? SliceInfo { get; set; }
}

public class AttendanceSliceInfo
{
    /// <summary>
    /// Leave and attendance component types: 1-leave; 3-business trip; 4-outing; 5-overtime
    /// </summary>
    [NotNull]
    [JsonProperty("type")]
    [JsonPropertyName("type")]
    public byte Type { get; set; }
    /// <summary>
    /// Fragmentation on a certain day
    /// </summary>
    [NotNull]
    [JsonProperty("day_items")]
    [JsonPropertyName("day_items")]
    public List<AttendanceSliceDayItem> DayItems { get; set; } = default!;
    public AttendanceSliceInfo()
    {

    }

    public AttendanceSliceInfo(byte type, List<AttendanceSliceDayItem> dayItems)
    {
        Type = type;
        DayItems = dayItems;
    }
}

public class AttendanceSliceDayItem
{
    /// <summary>
    /// The zero o'clock timestamp of the day (the zero o'clock timestamp of the East Eighth District of the day)
    /// </summary>
    [NotNull]
    [JsonProperty("daytime")]
    [JsonPropertyName("daytime")]
    public long Daytime { get; set; }
    /// <summary>
    /// The length of a certain day, in seconds, can be 0, (when type is halfday: overtime: it needs to be an integer multiple of 8640, others: it needs to be an integer multiple of 43200, when type is hour, it needs to be an integer multiple of 360)
    /// </summary>
    [NotNull]
    [JsonProperty("duration")]
    [JsonPropertyName("duration")]
    public long Duration { get; set; }
    public AttendanceSliceDayItem()
    {

    }

    public AttendanceSliceDayItem(long daytime, long duration)
    {
        Daytime = daytime;
        Duration = duration;
    }
}