using JetBrains.Annotations;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.WeChat.Work.ExternalContact.Customers.Models;
/// <summary>
/// Enterprise members who have added external contacts
/// </summary>
public class FollowUser
{
    /// <summary>
    /// The userid of the enterprise member who added this external contact
    /// </summary>
    [NotNull]
    [JsonProperty("userid")]
    [JsonPropertyName("userid")]
    public string UserId { get; set; } = default!;
    /// <summary>
    /// Initiate the added userid<br />
    /// If the member is added actively, it is the userid of the member;<br />
    /// If the customer actively adds it, it is the customer’s external contact userid;<br />
    /// If it is shared by an internal member/assigned by an administrator, it is the corresponding member/administrator userid.
    /// </summary>
    [NotNull]
    [JsonProperty("oper_userid")]
    [JsonPropertyName("oper_userid")]
    public string OperUserId { get; set; } = default!;
    /// <summary>
    /// Enterprise-defined state parameters are used to distinguish which "Contact Me" or customer acquisition link the customer was added through.
    /// </summary>
    [CanBeNull]
    [JsonProperty("state")]
    [JsonPropertyName("state")]
    public string? State { get; set; }
    /// <summary>
    /// The member's remarks about this external contact
    /// </summary>
    [CanBeNull]
    [JsonProperty("remark")]
    [JsonPropertyName("remark")]
    public string? Remark { get; set; }
    /// <summary>
    /// The member's description of this external contact
    /// </summary>
    [CanBeNull]
    [JsonProperty("description")]
    [JsonPropertyName("description")]
    public string? Description { get; set; }
    /// <summary>
    /// The time the member added this external contact
    /// </summary>
    [NotNull]
    [JsonProperty("createtime")]
    [JsonPropertyName("createtime")]
    public long Createtime { get; set; }
    /// <summary>
    /// List of labels tagged by external contacts
    /// </summary>
    [CanBeNull]
    [JsonProperty("tags")]
    [JsonPropertyName("tags")]
    public List<FollowUserTag>? Tags { get; set; }
    /// <summary>
    /// The company name that the member noted for this WeChat customer (only WeChat customers have this field)
    /// </summary>
    [CanBeNull]
    [JsonProperty("remark_corp_name")]
    [JsonPropertyName("remark_corp_name")]
    public string? RemarkCorpName { get; set; }
    /// <summary>
    /// The mobile phone number that the member noted for this customer can only be obtained by authorizing the administrator to develop self-built applications.
    /// </summary>
    [CanBeNull]
    [JsonProperty("remark_mobiles")]
    [JsonPropertyName("remark_mobiles")]
    public List<string>? RemarkMobiles { get; set; }
    /// <summary>
    /// The member adds the source of this customer
    /// </summary>
    [NotNull]
    [JsonProperty("add_way")]
    [JsonPropertyName("add_way")]
    public FollowUserAddWay AddWay { get; set; }
    /// <summary>
    /// When the source add_way of this member adds this customer is 10, the corresponding video number information
    /// </summary>
    [CanBeNull]
    [JsonProperty("wechat_channels")]
    [JsonPropertyName("wechat_channels")]
    public FollowUserWechatChannel? WechatChannel { get; set; }
}
