using JetBrains.Annotations;
using LINGYUN.Abp.WeChat.Work.ExternalContact.Customers.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.WeChat.Work.ExternalContact.Customers.Response;
/// <summary>
/// Get customer details response parameters in batches
/// </summary>
/// <remarks>
/// For details, see: <see href="https://developer.work.weixin.qq.com/document/path/92994" />
/// </remarks>
public class WeChatWorkBulkGetCustomerResponse : WeChatWorkResponse
{
    /// <summary>
    /// List of userids of external contacts
    /// </summary>
    [NotNull]
    [JsonProperty("external_contact_list")]
    [JsonPropertyName("external_contact_list")]
    public List<ExternalContactList> ExternalUserId { get; set; } = new List<ExternalContactList>();
    /// <summary>
    /// Paging cursor, fill it in on the next request to obtain subsequent paging records, if there is no more data, return empty
    /// </summary>
    [CanBeNull]
    [JsonProperty("next_cursor")]
    [JsonPropertyName("next_cursor")]
    public string? NextCursor { get; set; }
    /// <summary>
    /// If all userids in the request do not have valid interoperability permissions, the interface will directly report error 701008. If some userids do not have valid interoperability permission, the interface returns success
    /// </summary>
    [CanBeNull]
    [JsonProperty("fail_info")]
    [JsonPropertyName("fail_info")]
    public WeChatWorkBulkGetCustomerFailInfo? FailInfo { get; set; }
}

public class WeChatWorkBulkGetCustomerFailInfo
{
    /// <summary>
    /// Unpermitted userid list
    /// </summary>
    [NotNull]
    [JsonProperty("next_cursor")]
    [JsonPropertyName("next_cursor")]
    public List<string> UnlicensedUseridList { get; set; } = new List<string>();
}