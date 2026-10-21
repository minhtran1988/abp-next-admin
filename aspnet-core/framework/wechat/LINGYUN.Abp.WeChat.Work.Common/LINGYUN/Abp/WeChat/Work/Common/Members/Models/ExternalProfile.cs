using JetBrains.Annotations;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.WeChat.Work.Common.Members.Models;
/// <summary>
/// Member external information
/// </summary>
[System.Text.Json.Serialization.JsonConverter(typeof(ExternalProfileSystemTextJsonConverter))]
[Newtonsoft.Json.JsonConverter(typeof(ExternalProfileNewtonsoftJsonConverter))]
public class ExternalProfile
{
    /// <summary>
    /// The company's external abbreviation must be selected from the certified company abbreviations. You can check the company abbreviation certification status on the "My Company" page.
    /// </summary>
    [NotNull]
    [JsonProperty("external_corp_name")]
    [JsonPropertyName("external_corp_name")]
    public string ExternalCorpName {  get; set; } = default!;
    /// <summary>
    /// Video number attribute. You must choose from the video accounts bound to Enterprise WeChat by your enterprise. You can view the bound video accounts on the "My Enterprise" page.
    /// The third party can only obtain the address book application; for members who are not created by the third party, the third party address book application cannot obtain it either.
    /// </summary>
    [NotNull]
    [JsonProperty("wechat_channels")]
    [JsonPropertyName("wechat_channels")]
    public List<WechatChannel> WechatChannels { get; set; } = default!;
    /// <summary>
    /// Property list
    /// </summary>
    [NotNull]
    [JsonProperty("external_attr")]
    [JsonPropertyName("external_attr")]
    public List<ExternalAttribute> ExternalAttributes { get; set; } = default!;
}
