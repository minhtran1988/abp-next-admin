using JetBrains.Annotations;
using LINGYUN.Abp.WeChat.Work.OA.Schedules.Models;
using Newtonsoft.Json;
using System.Text.Json.Serialization;
using Volo.Abp;

namespace LINGYUN.Abp.WeChat.Work.OA.Schedules.Request;
/// <summary>
/// Update calendar request parameters
/// </summary>
/// <remarks>
/// For details, see: <see href="https://developer.work.weixin.qq.com/document/path/97716"/>
/// </remarks>
public class WeChatWorkUpdateCalendarRequest : WeChatWorkRequest
{
    /// <summary>
    /// Calendar information
    /// </summary>
    [NotNull]
    [JsonProperty("calendar")]
    [JsonPropertyName("calendar")]
    public UpdateCalendar Calendar { get; }
    /// <summary>
    /// Whether not to update the subscribeable range. 0-no; 1-yes. The default will be 0, and the subscriber range will be updated.
    /// </summary>
    [CanBeNull]
    [JsonProperty("skip_public_range")]
    [JsonPropertyName("skip_public_range")]
    public uint? SkipPublicRange { get; }
    public WeChatWorkUpdateCalendarRequest(UpdateCalendar calendar, bool skipPublicRange = false)
    {
        Calendar = calendar;
        SkipPublicRange = skipPublicRange ? 1u : 0u;
    }

    protected override void Validate()
    {
        Check.NotNull(Calendar, nameof(Calendar));

        Calendar.Validate();
    }
}
