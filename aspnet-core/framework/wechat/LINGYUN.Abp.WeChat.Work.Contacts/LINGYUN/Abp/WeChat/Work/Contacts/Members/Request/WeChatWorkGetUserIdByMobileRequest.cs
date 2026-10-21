using JetBrains.Annotations;
using Newtonsoft.Json;
using System.Text.Json.Serialization;
using Volo.Abp;

namespace LINGYUN.Abp.WeChat.Work.Contacts.Members.Request;
/// <summary>
/// Mobile phone number gets userid request parameter
/// </summary>
/// <remarks>
/// For details, see: <see href="https://developer.work.weixin.qq.com/document/path/95402" />
/// </remarks>
public class WeChatWorkGetUserIdByMobileRequest : WeChatWorkRequest
{
    /// <summary>
    /// The user’s mobile phone number in the enterprise WeChat address book. Length is 5~32 bytes
    /// </summary>
    [NotNull]
    [JsonProperty("mobile")]
    [JsonPropertyName("mobile")]
    public string Mobile { get; }
    public WeChatWorkGetUserIdByMobileRequest(string mobile)
    {
        Mobile = Check.NotNullOrWhiteSpace(mobile, nameof(mobile), 32, 5);
    }
}
