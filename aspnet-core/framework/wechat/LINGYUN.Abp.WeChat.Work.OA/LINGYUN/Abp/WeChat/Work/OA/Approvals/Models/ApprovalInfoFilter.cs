using JetBrains.Annotations;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.WeChat.Work.OA.Approvals.Models;
/// <summary>
/// Filter conditions
/// </summary>
public class ApprovalInfoFilter
{
    /// <summary>
    /// Filter type
    /// </summary>
    /// <remarks>
    /// Includes the following values:<br />
    /// template_id - template type/template id;<br />
    /// creator - applicant;<br />
    /// department - the department of the person who approves the bill of lading;<br />
    /// sp_status - approval status;<br />
    /// record_type - approval document type attribute;<br />
    /// <br />
    /// Note:<br />
    /// 1. Only "Department" supports configuring multiple filter conditions at the same time. <br />
    /// 2. The relationship between filtering conditions of different types is "AND", and the relationship between filtering conditions of the same type is "OR"<br />
    /// 3. The record_type filtering type only supports newly submitted approval orders after 2021/05/31, and historical orders do not support form type attribute filtering<br />
    /// </remarks>
    [NotNull]
    [JsonProperty("key")]
    [JsonPropertyName("key")]
    public string Key { get; set; }
    /// <summary>
    /// Filter value
    /// </summary>
    /// <remarks>
    /// Corresponding value:<br />
    /// template_id-template id;<br />
    /// creator-applicant userid;<br />
    /// department-the department id;<br />
    /// sp_status-Approval order status (1-under approval; 2-passed; 3-rejected; 4-withdrawn; 6-withdrawn after passing; 7-deleted; 10-paid);<br />
    /// record_type - Approval document type attribute (1-leave; 2-punch in and make up; 3-business trip; 4-outing; 5-overtime; 6-shift; 7-conference room reservation; 8-refund approval; 9-red envelope reimbursement approval)<br />
    /// </remarks>
    [NotNull]
    [JsonProperty("value")]
    [JsonPropertyName("value")]
    public string Value { get; set; }
    public ApprovalInfoFilter(string key, string value)
    {
        Key = key;
        Value = value;
    }
}
