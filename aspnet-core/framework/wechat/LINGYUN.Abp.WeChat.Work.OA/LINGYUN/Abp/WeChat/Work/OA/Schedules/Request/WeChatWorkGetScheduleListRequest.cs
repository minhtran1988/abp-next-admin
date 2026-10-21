using JetBrains.Annotations;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Volo.Abp;

namespace LINGYUN.Abp.WeChat.Work.OA.Schedules.Request;
/// <summary>
/// Get schedule details request parameters
/// </summary>
/// <remarks>
/// For details, see: <see href="https://developer.work.weixin.qq.com/document/path/97724"/>
/// </remarks>
public class WeChatWorkGetScheduleListRequest : WeChatWorkRequest
{
    private List<string> _scheduleIdList = new List<string>();
    /// <summary>
    /// Schedule ID list. A maximum of 1,000 items can be retrieved at one time
    /// </summary>
    [NotNull]
    [JsonProperty("schedule_id_list")]
    [JsonPropertyName("schedule_id_list")]
    public string[] ScheduleIdList => _scheduleIdList.ToArray();
    public WeChatWorkGetScheduleListRequest(string[] calIdList)
    {
        Check.NotNullOrEmpty(calIdList, nameof(calIdList));

        _scheduleIdList.AddIfNotContains(calIdList);
    }
    /// <summary>
    /// Add calendar ID
    /// </summary>
    /// <param name="calIdList"></param>
    public void WithCalId(params string[] calIdList)
    {
        _scheduleIdList.AddIfNotContains(calIdList);
    }

    protected override void Validate()
    {
        if (ScheduleIdList.Length > 2000)
        {
            throw new ArgumentException("A single schedule query can obtain up to 1,000 entries at most!", nameof(ScheduleIdList));
        }
    }
}
