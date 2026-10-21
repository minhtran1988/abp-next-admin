using JetBrains.Annotations;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.WeChat.Work.ExternalContact.MsgAudits.Request;
/// <summary>
/// Get session content archive to open member list request parameters
/// </summary>
/// <remarks>
/// For details, see: <see href="https://developer.work.weixin.qq.com/document/path/91614" />
/// </remarks>
public class WeChatWorkGetPermitUserListRequest : WeChatWorkRequest
{
    /// <summary>
    /// Pull the list of open members of the corresponding version. 1 represents the office version; 2 represents the service version; 3 represents the enterprise version. Optional, if not filled in, the full member list will be returned.
    /// </summary>
    [CanBeNull]
    [JsonProperty("cursor")]
    [JsonPropertyName("cursor")]
    public int? Type { get; }
    private WeChatWorkGetPermitUserListRequest(int? type = null)
    {
        Type = type; 
    }
    /// <summary>
    /// Default
    /// </summary>
    /// <returns></returns>
    public static WeChatWorkGetPermitUserListRequest Default()
    {
        return new WeChatWorkGetPermitUserListRequest();
    }
    /// <summary>
    /// Office version
    /// </summary>
    /// <returns></returns>
    public static WeChatWorkGetPermitUserListRequest Office()
    {
        return new WeChatWorkGetPermitUserListRequest(1);
    }
    /// <summary>
    /// Service version
    /// </summary>
    /// <returns></returns>
    public static WeChatWorkGetPermitUserListRequest Service()
    {
        return new WeChatWorkGetPermitUserListRequest(2);
    }
    /// <summary>
    /// Enterprise Edition
    /// </summary>
    /// <returns></returns>
    public static WeChatWorkGetPermitUserListRequest Enterprise()
    {
        return new WeChatWorkGetPermitUserListRequest(3);
    }
}
