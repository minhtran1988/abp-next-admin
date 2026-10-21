using System.ComponentModel;

namespace LINGYUN.Abp.WeChat.Work.ExternalContact.Customers.Models;
/// <summary>
/// Customer source
/// </summary>
[Description("Customer Source")]
public enum FollowUserAddWay
{
    /// <summary>
    /// Unknown source
    /// </summary>
    [Description("Unknown Source")]
    None = 0,
    /// <summary>
    /// Scan the QR code
    /// </summary>
    [Description("Scan QR code")]
    ScanQrCode = 1,
    /// <summary>
    /// Search for mobile phone number
    /// </summary>
    [Description("Search mobile phone number")]
    SearchPhoneNumber = 2,
    /// <summary>
    /// Business card sharing
    /// </summary>
    [Description("Business Card Sharing")]
    SharedCard = 3,
    /// <summary>
    /// Group chat
    /// </summary>
    [Description("Group Chat")]
    GroupChat = 4,
    /// <summary>
    /// Mobile phone address book
    /// </summary>
    [Description("Mobile Address Book")]
    PhoneBook = 5,
    /// <summary>
    /// WeChat contact
    /// </summary>
    [Description("WeChat Contact")]
    WeChatContact = 6,
    /// <summary>
    /// Customer service personnel automatically added when installing third-party applications
    /// </summary>
    [Description("Customer service personnel automatically added when installing third-party applications")]
    InstallThirdPartyApp = 8,
    /// <summary>
    /// Search mailbox
    /// </summary>
    [Description("Search mailbox")]
    SearchEmail = 9,
    /// <summary>
    /// Add video number
    /// </summary>
    [Description("Video number added")]
    WechatChannel = 10,
    /// <summary>
    /// Add through schedule participants
    /// </summary>
    [Description("Add by schedule participant")]
    SchedulePart = 11,
    /// <summary>
    /// Added by meeting participants
    /// </summary>
    [Description("Add by meeting participant")]
    MeetPart = 12,
    /// <summary>
    /// Add the corporate WeChat corresponding to WeChat friends
    /// </summary>
    [Description("Add corporate WeChat corresponding to WeChat friends")]
    WeChatFriend = 13,
    /// <summary>
    /// Add through smart hardware dedicated customer service
    /// </summary>
    [Description("Add through smart hardware dedicated customer service")]
    SmartHardware = 14,
    /// <summary>
    /// Add through door-to-door service customer service
    /// </summary>
    [Description("Add through on-site customer service")]
    DoorService = 15,
    /// <summary>
    /// Add through customer acquisition link
    /// </summary>
    [Description("Add through customer acquisition link")]
    CustomerAcqLink = 16,
    /// <summary>
    /// Added through custom development
    /// </summary>
    [Description("Added through custom development")]
    CustomDevelopment = 17,
    /// <summary>
    /// Added by request reply
    /// </summary>
    [Description("Add via request reply")]
    DemandResponse = 18,
    /// <summary>
    /// Added through third-party pre-sales customer service
    /// </summary>
    [Description("Added through third-party pre-sales customer service")]
    ThirdPartyPreSales = 21,
    /// <summary>
    /// Add via possible business partners
    /// </summary>
    [Description("Add via possible business partners")]
    PotentialBusPart = 22,
    /// <summary>
    /// Add by accepting the friend request received from the WeChat account
    /// </summary>
    [Description("Add friend request received by accepting WeChat account")]
    WeChatFriendRequest = 24,
    /// <summary>
    /// Shared by internal members
    /// </summary>
    [Description("Internal member sharing")]
    SharedByInternalMembers = 201,
    /// <summary>
    /// Administrator/responsible person assignment
    /// </summary>
    [Description("Administrator/Responsible Person Assignment")]
    AllocationByAdmin = 202
}
