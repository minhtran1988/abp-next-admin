using JetBrains.Annotations;
using LINGYUN.Abp.WeChat.Work.OA.Schedules.Models;
using Newtonsoft.Json;
using System.Text.Json.Serialization;
using Volo.Abp;

namespace LINGYUN.Abp.WeChat.Work.OA.Schedules.Request;
/// <summary>
/// Create schedule request parameters
/// </summary>
/// <remarks>
/// For details, see: <see href="https://developer.work.weixin.qq.com/document/path/93648"/>
/// </remarks>
public class WeChatWorkCreateScheduleRequest : WeChatWorkRequest
{
    /// <summary>
    /// Calendar information
    /// </summary>
    [NotNull]
    [JsonProperty("schedule")]
    [JsonPropertyName("schedule")]
    public CreateSchedule Schedule { get; }
    /// <summary>
    /// The agentid of the application installed by the authorized party. Only old third-party multi-application suites need to fill in this parameter
    /// </summary>
    [CanBeNull]
    [JsonProperty("agentid")]
    [JsonPropertyName("agentid")]
    public int? AgentId { get; set; }
    public WeChatWorkCreateScheduleRequest(CreateSchedule schedule)
    {
        Schedule = schedule;
    }

    protected override void Validate()
    {
        Check.NotNull(Schedule, nameof(Schedule));

        Schedule.Validate();
    }
}
