using JetBrains.Annotations;
using LINGYUN.Abp.WeChat.Work.OA.Approvals.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Volo.Abp;

namespace LINGYUN.Abp.WeChat.Work.OA.Approvals.Request;
/// <summary>
/// Get approval order number request parameters in batches
/// </summary>
/// <remarks>
/// For details, see: <see href="https://developer.work.weixin.qq.com/document/path/91816"/>
/// </remarks>
public class WeChatWorkGetApprovalInfoRequest : WeChatWorkRequest
{
    /// <summary>
    /// Approval form submission time range, start time, UNix timestamp
    /// </summary>
    [NotNull]
    [JsonProperty("starttime")]
    [JsonPropertyName("starttime")]
    public long StartTime { get; set; }
    /// <summary>
    /// Approval form submission time range, end time, Unix timestamp
    /// </summary>
    [NotNull]
    [JsonProperty("endtime")]
    [JsonPropertyName("endtime")]
    public long EndTime { get; set; }
    /// <summary>
    /// Paging query cursor, the default is an empty string, and the returned new_next_cursor is subsequently used for paging pull.
    /// </summary>
    [CanBeNull]
    [JsonProperty("new_cursor")]
    [JsonPropertyName("new_cursor")]
    public string? NewCursor { get; set; } = "";
    /// <summary>
    /// The number of approval orders pulled in one request. The default value is 100 and the upper limit is 100.
    /// If the accesstoken is a self-built application, only forms submitted by applicants within the visible range of the application are allowed to be obtained. The number of returned sp_no_list may be inconsistent with the size. The developer needs to use next_cursor to determine whether the form records have been fetched.
    /// </summary>
    [NotNull]
    [JsonProperty("size")]
    [JsonPropertyName("size")]
    public int Size { get; set; }
    /// <summary>
    /// Filtering conditions can set constraints on batch-pulled approval applications and support setting multiple conditions.
    /// </summary>
    [CanBeNull]
    [JsonProperty("filters")]
    [JsonPropertyName("filters")]
    public List<ApprovalInfoFilter>? Filters { get; set; }
    public WeChatWorkGetApprovalInfoRequest(
        long sartTime,
        long endTime,
        int size = 100,
        List<ApprovalInfoFilter>? filters = null)
    {
        StartTime = sartTime;
        EndTime = endTime;
        Size = Check.Range(size, nameof(size), 1, 100);

        Filters = filters;
    }
}
