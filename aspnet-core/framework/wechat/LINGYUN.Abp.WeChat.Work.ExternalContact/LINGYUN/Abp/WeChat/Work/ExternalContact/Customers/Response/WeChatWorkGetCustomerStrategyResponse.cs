using JetBrains.Annotations;
using LINGYUN.Abp.WeChat.Work.ExternalContact.Customers.Models;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.WeChat.Work.ExternalContact.Customers.Response;
/// <summary>
/// Get the rule group details response parameters
/// </summary>
/// <remarks>
/// For details, see: <see href="https://developer.work.weixin.qq.com/document/path/94883" />
/// </remarks>
public class WeChatWorkGetCustomerStrategyResponse : WeChatWorkResponse
{
    /// <summary>
    /// Rule group details
    /// </summary>
    [NotNull]
    [JsonProperty("strategy")]
    [JsonPropertyName("strategy")]
    public CustomerStrategyInfo Strategy { get; set; } = default!;
}
