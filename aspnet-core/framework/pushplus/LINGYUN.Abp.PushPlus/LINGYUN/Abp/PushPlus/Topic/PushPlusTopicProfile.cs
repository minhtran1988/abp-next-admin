using Newtonsoft.Json;

namespace LINGYUN.Abp.PushPlus.Topic;
/// <summary>
/// Group details
/// </summary>
public class PushPlusTopicProfile : PushPlusTopic
{
    /// <summary>
    /// Permanent QR code image address
    /// </summary>
    [JsonProperty("qrCodeImgUrl")]
    public string? QrCodeImgUrl { get; set; }
    /// <summary>
    /// Contact information
    /// </summary>
    [JsonProperty("contact")]
    public string? Contact { get; set; }
    /// <summary>
    /// Group introduction
    /// </summary>
    [JsonProperty("introduction")]
    public string? Introduction { get; set; }
    /// <summary>
    /// Reply content after joining
    /// </summary>
    [JsonProperty("receiptMessage")]
    public string? ReceiptMessage { get; set; }
    /// <summary>
    /// Total number of group subscribers
    /// </summary>
    [JsonProperty("topicUserCount")]
    public string? TopicUserCount { get; set; }
}
