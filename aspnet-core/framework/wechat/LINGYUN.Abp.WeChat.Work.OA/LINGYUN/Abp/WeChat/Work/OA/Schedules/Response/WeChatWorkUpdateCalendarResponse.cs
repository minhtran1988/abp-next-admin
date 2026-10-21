using JetBrains.Annotations;
using LINGYUN.Abp.WeChat.Work.OA.Schedules.Models;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.WeChat.Work.OA.Schedules.Response;
/// <summary>
/// Update calendar response parameters
/// </summary>
/// <remarks>
/// For details, see: <see href="https://developer.work.weixin.qq.com/document/path/97716"/>
/// </remarks>
public class WeChatWorkUpdateCalendarResponse : WeChatWorkResponse
{
    /// <summary>
    /// Invalid input content
    /// </summary>
    [NotNull]
    [JsonProperty("fail_result")]
    [JsonPropertyName("fail_result")]
    public CalendarFailResult FailResult { get; set; } = default!;
}
