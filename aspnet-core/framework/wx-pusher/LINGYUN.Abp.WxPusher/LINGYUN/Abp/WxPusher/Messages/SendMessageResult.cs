using Newtonsoft.Json;
using System;

namespace LINGYUN.Abp.WxPusher.Messages;

[Serializable]
public class SendMessageResult
{
    /// <summary>
    /// Status code
    /// </summary>
    [JsonProperty("code")]
    public int Code { get; set; } = default!;
    /// <summary>
    /// Message ID
    /// </summary>
    [JsonProperty("messageId")]
    [Obsolete("Obsolete, please do not use it again, this field will be deleted later, see: https://wxpusher.dingliqc.com/docs/#/?id=%e5%8f%91%e9%80%81%e6%b6%88%e6%81%af")]
    public long? MessageId { get; set; }
    /// <summary>
    /// Message content identifier
    /// </summary>
    /// <remarks>
    /// Call the interface once to generate one. You can call the delete message interface through this id to delete the message.
    /// All users sent this time share the content of this message.
    /// </remarks>
    [JsonProperty("messageContentId")]
    public long ContentId { get; set; }
    /// <summary>
    /// Message sending identifier
    /// </summary>
    /// <remarks>
    /// Generate one for each uid user or topicId. You can use this ID to query the sending status of a certain user.
    /// </remarks>
    [JsonProperty("sendRecordId")]
    public long RecordId { get; set; }
    /// <summary>
    /// Status
    /// </summary>
    [JsonProperty("status")]
    public string Status { get; set; } = default!;
    /// <summary>
    /// User ID
    /// </summary>
    [JsonProperty("uid")]
    public string Uid { get; set; } = default!;
    /// <summary>
    /// Group ID
    /// </summary>
    [JsonProperty("topicId")]
    public long? TopicId { get; set; }
    /// <summary>
    /// Whether the call is successful
    /// </summary>
    public bool IsSuccessed => Code == 1000;
}
