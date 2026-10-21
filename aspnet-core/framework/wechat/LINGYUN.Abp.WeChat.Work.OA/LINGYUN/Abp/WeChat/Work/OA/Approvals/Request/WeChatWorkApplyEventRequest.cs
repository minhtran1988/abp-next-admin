using JetBrains.Annotations;
using LINGYUN.Abp.WeChat.Work.OA.Approvals.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.WeChat.Work.OA.Approvals.Request;
/// <summary>
/// Submit approval application request parameters
/// </summary>
/// <remarks>
/// For details, see: <see href="https://developer.work.weixin.qq.com/document/path/91853"/>
/// </remarks>
public class WeChatWorkApplyEventRequest : WeChatWorkRequest
{
    /// <summary>
    /// Process
    /// </summary>
    /// <remarks>
    /// Required when use_template_approver = 0
    /// </remarks>
    [CanBeNull]
    [JsonProperty("process")]
    [JsonPropertyName("process")]
    public ApprovalApplyProcess? Process { get; set; }
    /// <summary>
    /// Summary information, used to display summary information on the approval notification card and approval list, up to 3 lines
    /// </summary>
    [CanBeNull]
    [JsonProperty("summary_list")]
    [JsonPropertyName("summary_list")]
    public List<ApprovalSummary>? Summaries { get; set; }
    /// <summary>
    /// Approval application data can define the values of each control in the approval application. The required items must have values, and the optional items can be empty. The data structure is the same as the parameter "apply_data" with the same name in the return value of the "Get Approval Application Details" interface.
    /// </summary>
    [NotNull]
    [JsonProperty("apply_data")]
    [JsonPropertyName("apply_data")]
    public ApprovalApplyData ApplyData { get; set; }
    /// <summary>
    /// Template id. It can be obtained in "Get Approval Application Details", "Approval Status Change Callback Notification", or in the template editing page link of the approval template. Submitting the [Punch-in and Replacement of Cards] [Shift Adjustment] template approval forms through the interface is currently not supported.
    /// </summary>
    [NotNull]
    [JsonProperty("template_id")]
    [JsonPropertyName("template_id")]
    public string TemplateId { get; set; }
    /// <summary>
    /// Applicant userid, this approval application will be submitted as this employee, the applicant must be within the visible range of the application
    /// </summary>
    [NotNull]
    [JsonProperty("creator_userid")]
    [JsonPropertyName("creator_userid")]
    public string CreatorUserId { get; set; }
    /// <summary>
    /// Approver mode
    /// </summary>
    /// <remarks>
    /// 0-Specify the approver and carbon copy person through the interface (the process parameter is required at this time); <br />
    /// 1-Use this template to set up the approval process in the management background (you need to ensure that there is no "applicant self-selected" node in the approval process), which supports conditional approval. Default is 0
    /// </remarks>
    [NotNull]
    [JsonProperty("use_template_approver")]
    [JsonPropertyName("use_template_approver")]
    public byte UseTemplateApprover 
    {
        get {
            if (Process == null)
            {
                return 1;
            }
            return 0;
        }
    }
    /// <summary>
    /// The id of the bill of lading department of the bill of lader. If not filled in, the default is the main department.
    /// </summary>
    [CanBeNull]
    [JsonProperty("choose_department")]
    [JsonPropertyName("choose_department")]
    public byte? ChooseDepartment { get; set; }
    public WeChatWorkApplyEventRequest(
        string templateId,
        string creatorUserid,
        ApprovalApplyData applyData,
        ApprovalApplyProcess? process = null,
        byte? chooseDepartment = null)
    {
        TemplateId = templateId;
        ApplyData = applyData;
        CreatorUserId = creatorUserid;
        ChooseDepartment = chooseDepartment;
        Process = process;
    }
}
