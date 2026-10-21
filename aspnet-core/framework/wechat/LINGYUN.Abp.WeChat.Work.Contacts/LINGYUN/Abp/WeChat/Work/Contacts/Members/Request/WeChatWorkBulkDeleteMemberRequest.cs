using JetBrains.Annotations;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.WeChat.Work.Contacts.Members.Request;
/// <summary>
/// Batch delete member request parameters
/// </summary>
/// <remarks>
/// For details, see: <see href="https://developer.work.weixin.qq.com/document/path/90199" />
/// </remarks>
public class WeChatWorkBulkDeleteMemberRequest : WeChatWorkRequest
{
    /// <summary>
    /// Member UserID list. Corresponding to the account on the management side. Supports up to 200. If there is an invalid UserID, an error will be returned directly.
    /// </summary>
    [NotNull]
    [JsonProperty("useridlist")]
    [JsonPropertyName("useridlist")]
    public List<string> UserIdList {  get; }
    public WeChatWorkBulkDeleteMemberRequest() :this([])
    {
    }

    public WeChatWorkBulkDeleteMemberRequest(List<string> userIdList)
    {
        UserIdList = userIdList;
    }

    protected override void Validate()
    {
        if (UserIdList.Count > 100)
        {
            throw new ArgumentException("The maximum number of member ids that can be deleted simultaneously is only 200!");
        }
    }
}
