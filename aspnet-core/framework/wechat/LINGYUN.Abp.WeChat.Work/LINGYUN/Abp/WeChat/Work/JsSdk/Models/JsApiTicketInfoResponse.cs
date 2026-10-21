using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.WeChat.Work.JsSdk.Models;

public class JsApiTicketInfoResponse : WeChatWorkResponse
{
    /// <summary>
    /// jsapi_ticket required to generate signature, up to 512 bytes
    /// </summary>
    [JsonProperty("ticket")]
    [JsonPropertyName("ticket")]
    public string Ticket { get; set; } = default!;
    /// <summary>
    /// Validity time of the voucher (seconds)
    /// </summary>
    [JsonProperty("expires_in")]
    [JsonPropertyName("expires_in")]
    [System.Text.Json.Serialization.JsonConverter(typeof(NumberToStringConverter))]
    public int ExpiresIn { get; set; }

    public JsApiTicketInfo ToJsApiTicket()
    {
        ThrowIfNotSuccess();
        return new JsApiTicketInfo(Ticket, ExpiresIn);
    }
}
