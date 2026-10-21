using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.WeChat.Work.Security.Models;
public class WeChatIpModel
{
    /// <summary>
    /// ip address
    /// </summary>
    [JsonProperty("ip")]
    [JsonPropertyName("ip")]
    public string Ip { get; set; } = default!;
    /// <summary>
    /// Protocol such as TCP UDP
    /// </summary>
    [JsonProperty("protocol")]
    [JsonPropertyName("protocol")]
    public string Protocol { get; set; } = default!;
    /// <summary>
    /// Port number list
    /// </summary>
    [JsonProperty("port")]
    [JsonPropertyName("port")]
    public List<int> Port { get; set; } = new List<int>();
    /// <summary>
    /// Is it necessary, 0-no 1-yes, if the necessary domain name or IP is blocked, it will cause the function of Enterprise WeChat to be abnormal.
    /// </summary>
    [JsonProperty("is_necessary")]
    [JsonPropertyName("is_necessary")]
    public int IsNecessary { get; set; } = default!;
    /// <summary>
    /// Description information of functions involved in IP
    /// </summary>
    [JsonProperty("description")]
    [JsonPropertyName("description")]
    public string? Description { get; set; }
}
