using Newtonsoft.Json;
using System;

namespace LINGYUN.Abp.PushPlus.Message;

public class PushPlusMessage
{
    /// <summary>
    /// Message sending channel;
    /// wechat-WeChat public account,
    /// mail-mail,
    /// cp-Enterprise WeChat application,
    /// webhook - third-party webhook
    /// </summary>
    [JsonProperty("channel")]
    public string Channel { get; set; } = default!;
    /// <summary>
    /// Message type;
    /// 1-One-to-one message,
    /// 2-One-to-many message
    /// </summary>
    [JsonProperty("messageType")]
    public PushPlusMessageType MessageType { get; set; }
    /// <summary>
    /// Message short chain code; can be used to query message sending results
    /// </summary>
    [JsonProperty("shortCode")]
    public string ShortCode { get; set; } = default!;
    /// <summary>
    /// Message title
    /// </summary>
    [JsonProperty("title")]
    public string Title { get; set; } = default!;
    /// <summary>
    /// Group name, only one-to-many messages have value
    /// </summary>
    [JsonProperty("topicName")]
    public string? TopicName { get; set; }
    /// <summary>
    /// Update date
    /// </summary>
    [JsonProperty("updateTime")]
    public DateTime UpdateTime { get; set; }
}
