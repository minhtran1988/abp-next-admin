using Newtonsoft.Json;
using System;

namespace LINGYUN.Abp.PushPlus.Message;

public class SendPushPlusMessageResult
{
    /// <summary>
    /// Message delivery status
    /// 0 - not delivered,
    /// 1-Sending,
    /// 2-Sent,
    /// 3-Sending failed
    /// </summary>
    [JsonProperty("status")] 
    public PushPlusMessageStatus Status { get; set; }
    /// <summary>
    /// Reason for sending failure
    /// </summary>
    [JsonProperty("errorMessage")]
    public string? ErrorMessage { get; set; }
    /// <summary>
    /// Update time
    /// </summary>
    [JsonProperty("updateTime")]
    public DateTime UpdateTime { get; set; }
}
