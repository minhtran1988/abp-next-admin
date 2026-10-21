using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.WeChat.Work.Security.Models;
public class WeChatServerDomainModel
{
    /// <summary>
    /// Domain name list
    /// </summary>
    [JsonProperty("domain_list")]
    [JsonPropertyName("domain_list")]
    public List<WeChatDomainModel> Domains { get; }
    /// <summary>
    /// IP list
    /// </summary>
    [JsonProperty("ip_list")]
    [JsonPropertyName("ip_list")]
    public List<WeChatIpModel> Ips { get; }
    public WeChatServerDomainModel(List<WeChatDomainModel> domains, List<WeChatIpModel> ips)
    {
        Domains = domains;
        Ips = ips;
    }
}
