using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.WeChat.Work.Security.Models;
public class WeChatDomainModel
{
    /// <summary>
    /// Domain name
    /// </summary>
    [JsonProperty("domain")]
    [JsonPropertyName("domain")]
    public string Domain { get; set; } = default!;
    /// <summary>
    /// Generic domain name
    /// </summary>
    [JsonProperty("universal_domian")]
    [JsonPropertyName("universal_domian")]
    public string? UniversalDomian { get; set; }
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
