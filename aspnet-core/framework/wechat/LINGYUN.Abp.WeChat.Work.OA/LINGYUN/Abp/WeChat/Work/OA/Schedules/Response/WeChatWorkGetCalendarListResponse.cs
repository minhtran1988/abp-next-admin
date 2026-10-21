using JetBrains.Annotations;
using LINGYUN.Abp.WeChat.Work.OA.Schedules.Models;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.WeChat.Work.OA.Schedules.Response;
/// <summary>
/// Get calendar details response parameters
/// </summary>
/// <remarks>
/// For details, see: <see href="https://developer.work.weixin.qq.com/document/path/97717"/>
/// </remarks>
public class WeChatWorkGetCalendarListResponse : WeChatWorkResponse
{
    /// <summary>
    /// Calendar list
    /// </summary>
    [NotNull]
    [JsonProperty("calendar_list")]
    [JsonPropertyName("calendar_list")]
    public CalendarInfo[] CalendarList { get; set; } = default!;
}
