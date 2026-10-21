using JetBrains.Annotations;
using LINGYUN.Abp.WeChat.Work.OA.Schedules.Models;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.WeChat.Work.OA.Schedules.Response;
/// <summary>
/// Create calendar response parameters
/// </summary>
/// <remarks>
/// For details, see: <see href="https://developer.work.weixin.qq.com/document/path/93647"/>
/// </remarks>
public class WeChatWorkCreateCalendarResponse : WeChatWorkResponse
{
    /// <summary>
    /// Calendar ID
    /// </summary>
    [NotNull]
    [JsonProperty("cal_id")]
    [JsonPropertyName("cal_id")]
    public string CalId { get; set; } = default!;
    /// <summary>
    /// Invalid input content
    /// </summary>
    [NotNull]
    [JsonProperty("fail_result")]
    [JsonPropertyName("fail_result")]
    public CalendarFailResult FailResult { get; set; } = default!;
}
