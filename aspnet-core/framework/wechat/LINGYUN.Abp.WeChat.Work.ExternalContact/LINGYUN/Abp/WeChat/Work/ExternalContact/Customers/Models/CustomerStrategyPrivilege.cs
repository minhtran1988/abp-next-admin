using JetBrains.Annotations;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.WeChat.Work.ExternalContact.Customers.Models;
/// <summary>
/// Rule group permissions
/// </summary>
public class CustomerStrategyPrivilege
{
    /// <summary>
    /// View customer list, basic permissions, cannot be canceled
    /// </summary>
    [NotNull]
    [JsonProperty("view_customer_list")]
    [JsonPropertyName("view_customer_list")]
    public bool ViewCustomerList { get; set; }
    /// <summary>
    /// View customer statistics, basic permissions, cannot be canceled
    /// </summary>
    [NotNull]
    [JsonProperty("view_customer_data")]
    [JsonPropertyName("view_customer_data")]
    public bool ViewCustomerData { get; set; }
    /// <summary>
    /// View the group chat list, basic permissions, cannot be canceled
    /// </summary>
    [NotNull]
    [JsonProperty("view_room_list")]
    [JsonPropertyName("view_room_list")]
    public bool ViewRoomList { get; set; }
    /// <summary>
    /// Can use Contact Me, basic permissions, cannot be canceled
    /// </summary>
    [NotNull]
    [JsonProperty("contact_me")]
    [JsonPropertyName("contact_me")]
    public bool ContactMe { get; set; }
    /// <summary>
    /// Can join group chat, basic permissions, cannot be canceled
    /// </summary>
    [NotNull]
    [JsonProperty("join_room")]
    [JsonPropertyName("join_room")]
    public bool JoinRoom { get; set; }
    /// <summary>
    /// Allow sharing customers to other members, default is true
    /// </summary>
    [NotNull]
    [JsonProperty("share_customer")]
    [JsonPropertyName("share_customer")]
    public bool ShareCustomer { get; set; }
    /// <summary>
    /// Allows assignment of resigned member customers, default is true
    /// </summary>
    [NotNull]
    [JsonProperty("oper_resign_customer")]
    [JsonPropertyName("oper_resign_customer")]
    public bool OperResignCustomer { get; set; }
    /// <summary>
    /// Allows assignment of resigned member customer groups, the default is true
    /// </summary>
    [NotNull]
    [JsonProperty("oper_resign_group")]
    [JsonPropertyName("oper_resign_group")]
    public bool OperResignGroup { get; set; }
    /// <summary>
    /// Allow sending messages to corporate customers, default is true
    /// </summary>
    [NotNull]
    [JsonProperty("send_customer_msg")]
    [JsonPropertyName("send_customer_msg")]
    public bool SendCustomerMsg { get; set; }
    /// <summary>
    /// Allows configuration of welcome message, default is true
    /// </summary>
    [NotNull]
    [JsonProperty("edit_welcome_msg")]
    [JsonPropertyName("edit_welcome_msg")]
    public bool EditWelcomeMsg { get; set; }
    /// <summary>
    /// Allows viewing member contact customer statistics, the default is true
    /// </summary>
    [NotNull]
    [JsonProperty("view_behavior_data")]
    [JsonPropertyName("view_behavior_data")]
    public bool ViewBehaviorData { get; set; }
    /// <summary>
    /// Allow viewing group chat data statistics, the default is true
    /// </summary>
    [NotNull]
    [JsonProperty("view_room_data")]
    [JsonPropertyName("view_room_data")]
    public bool ViewRoomData { get; set; }
    /// <summary>
    /// Allows messages to be sent to the enterprise's customer base, the default is true
    /// </summary>
    [NotNull]
    [JsonProperty("send_group_msg")]
    [JsonPropertyName("send_group_msg")]
    public bool SendGroupMsg { get; set; }
    /// <summary>
    /// Allow deduplication of enterprise customer groups, the default is true
    /// </summary>
    [NotNull]
    [JsonProperty("room_deduplication")]
    [JsonPropertyName("room_deduplication")]
    public bool RoomDeduplication { get; set; }
    /// <summary>
    /// Configure quick reply, the default is true
    /// </summary>
    [NotNull]
    [JsonProperty("rapid_reply")]
    [JsonPropertyName("rapid_reply")]
    public bool RapidReply { get; set; }
    /// <summary>
    /// Transfer customers of current members, the default is true
    /// </summary>
    [NotNull]
    [JsonProperty("onjob_customer_transfer")]
    [JsonPropertyName("onjob_customer_transfer")]
    public bool OnjobCustomerTransfer { get; set; }
    /// <summary>
    /// Edit the anti-harassment rules for corporate members, the default is true
    /// </summary>
    [NotNull]
    [JsonProperty("edit_anti_spam_rule")]
    [JsonPropertyName("edit_anti_spam_rule")]
    public bool EditAntiSpamRule { get; set; }
    /// <summary>
    /// Export customer list, default is true
    /// </summary>
    [NotNull]
    [JsonProperty("export_customer_list")]
    [JsonPropertyName("export_customer_list")]
    public bool ExportCustomerList { get; set; }
    /// <summary>
    /// Export member customer statistics, the default is true
    /// </summary>
    [NotNull]
    [JsonProperty("export_customer_data")]
    [JsonPropertyName("export_customer_data")]
    public bool ExportCustomerData { get; set; }
    /// <summary>
    /// Export customer group list, default is true
    /// </summary>
    [NotNull]
    [JsonProperty("export_customer_group_list")]
    [JsonPropertyName("export_customer_group_list")]
    public bool ExportCustomerGroupList { get; set; }
    /// <summary>
    /// Configure enterprise customer tags, the default is true
    /// </summary>
    [NotNull]
    [JsonProperty("manage_customer_tag")]
    [JsonPropertyName("manage_customer_tag")]
    public bool ManageCustomerTag { get; set; }

    public static CustomerStrategyPrivilege Default()
    {
        return new CustomerStrategyPrivilege
        {
            ViewCustomerList = true,
            ViewCustomerData = true,
            ViewRoomList = true,
            ContactMe = true,
            JoinRoom = true,
            ShareCustomer = true,
            EditWelcomeMsg = true,
            ViewBehaviorData = true,
            ViewRoomData = true,
            SendGroupMsg = true,
            RoomDeduplication = true,
            RapidReply = true,
            OnjobCustomerTransfer = true,
            EditAntiSpamRule = true,
            ExportCustomerList = true,
            ExportCustomerData = true,
            ExportCustomerGroupList = true,
            ManageCustomerTag = true,
        };
    }
}
