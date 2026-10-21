using JetBrains.Annotations;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Volo.Abp;

namespace LINGYUN.Abp.WeChat.Work.OA.Schedules.Request;
/// <summary>
/// Get calendar details request parameters
/// </summary>
/// <remarks>
/// For details, see: <see href="https://developer.work.weixin.qq.com/document/path/97717"/>
/// </remarks>
public class WeChatWorkGetCalendarListRequest : WeChatWorkRequest
{
    private List<string> _calIdList = new List<string>();
    /// <summary>
    /// Calendar ID list, obtained after calling the create calendar interface. Up to 1000 items can be obtained at one time
    /// </summary>
    [NotNull]
    [JsonProperty("cal_id_list")]
    [JsonPropertyName("cal_id_list")]
    public string[] CalIdList => _calIdList.ToArray();
    public WeChatWorkGetCalendarListRequest(string[] calIdList)
    {
        Check.NotNullOrEmpty(calIdList, nameof(calIdList));

        _calIdList.AddIfNotContains(calIdList);
    }
    /// <summary>
    /// Add calendar ID
    /// </summary>
    /// <param name="calIdList"></param>
    public void WithCalId(params string[] calIdList)
    {
        _calIdList.AddIfNotContains(calIdList);
    }

    protected override void Validate()
    {
        if (CalIdList.Length > 2000)
        {
            throw new ArgumentException("A single calendar query can obtain up to 1,000 entries at most!", nameof(CalIdList));
        }
    }
}
