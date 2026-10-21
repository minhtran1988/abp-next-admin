using JetBrains.Annotations;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.WeChat.Work.ExternalContact.MsgAudits.Models;
/// <summary>
/// Group chat consent status
/// </summary>
public class RoomAgreeInfo
{
    /// <summary>
    /// externalopenid of external member
    /// </summary>
    [NotNull]
    [JsonProperty("exteranalopenid")]
    [JsonPropertyName("exteranalopenid")]
    public string ExteranalOpenId { get; set; } = default!;
    /// <summary>
    /// Agree: "Agree", disagree: "Disagree"
    /// </summary>
    [NotNull]
    [JsonProperty("agree_status")]
    [JsonPropertyName("agree_status")]
    public string AgreeStatus { get; set; } = default!;
    /// <summary>
    /// The specific time of agreeing to the status change, UTC time
    /// </summary>
    [CanBeNull]
    [JsonProperty("status_change_time")]
    [JsonPropertyName("status_change_time")]
    public long? StatusChangeTime { get; set; }
}
