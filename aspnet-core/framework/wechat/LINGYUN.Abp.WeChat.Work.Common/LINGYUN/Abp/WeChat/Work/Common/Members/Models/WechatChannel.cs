using JetBrains.Annotations;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.WeChat.Work.Common.Members.Models;
/// <summary>
/// Video number attribute
/// </summary>
public class WechatChannel
{
    /// <summary>
    /// Video account name (after setting, members will display the video account to the outside world)
    /// </summary>
    [NotNull]
    [JsonProperty("nickname")]
    [JsonPropertyName("nickname")]
    public string NickName { get; set; } = default!;
    /// <summary>
    /// Display the video account status to the outside world
    /// </summary>
    [NotNull]
    [JsonProperty("status")]
    [JsonPropertyName("status")]
    public WechatChannelStatus Status { get; set; }
}
