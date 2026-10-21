using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.WeChat.Official.Account.Models;
public class TicketModel
{
    /// <summary>
    /// The obtained QR code ticket can be used to exchange for the QR code within the valid time.
    /// </summary>
    [JsonProperty("ticket")]
    [JsonPropertyName("ticket")]
    public string Ticket { get; set; } = default!;
    /// <summary>
    /// The validity time of this QR code is in seconds. The maximum value does not exceed 2,592,000 (i.e. 30 days).
    /// </summary>
    [JsonProperty("expire_seconds")]
    [JsonPropertyName("expire_seconds")]
    public int ExpireSeconds { get; set; }
    /// <summary>
    /// The address after parsing the QR code image. Developers can generate the required QR code image based on this address.
    /// </summary>
    [JsonProperty("url")]
    [JsonPropertyName("url")]
    public string Url { get; set; } = default!;
}
