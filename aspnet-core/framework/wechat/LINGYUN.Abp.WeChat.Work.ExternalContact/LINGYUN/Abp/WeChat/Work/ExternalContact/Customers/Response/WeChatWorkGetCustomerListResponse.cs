using JetBrains.Annotations;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.WeChat.Work.ExternalContact.Customers.Response;
/// <summary>
/// Get the customer list response parameters
/// </summary>
/// <remarks>
/// For details, see: <see href="https://developer.work.weixin.qq.com/document/path/92113" />
/// </remarks>
public class WeChatWorkGetCustomerListResponse : WeChatWorkResponse
{
    /// <summary>
    /// List of userids of external contacts
    /// </summary>
    [NotNull]
    [JsonProperty("external_userid")]
    [JsonPropertyName("external_userid")]
    public List<string> ExternalUserId { get; set; } = new List<string>();
}
