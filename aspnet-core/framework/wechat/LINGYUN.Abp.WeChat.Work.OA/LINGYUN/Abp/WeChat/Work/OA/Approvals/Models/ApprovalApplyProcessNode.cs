using JetBrains.Annotations;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.WeChat.Work.OA.Approvals.Models;
/// <summary>
/// Application process node
/// </summary>
public class ApprovalApplyProcessNode
{
    /// <summary>
    /// Node type 1: Approver 2: Copy person 3: Handler
    /// </summary>
    [NotNull]
    [JsonProperty("type")]
    [JsonPropertyName("type")]
    public ApprovalProcessNodeType Type { get; set; }
    /// <summary>
    /// Multi-person approval method 1-countersign; 2-or sign 3-approval in sequence
    /// </summary>
    /// <remarks>
    /// Required when type is 1 or 3
    /// </remarks>
    [NotNull]
    [JsonProperty("apv_rel")]
    [JsonPropertyName("apv_rel")]
    public byte? ApvRel { get; set; }
    /// <summary>
    /// user id
    /// </summary>
    [NotNull]
    [JsonProperty("userid")]
    [JsonPropertyName("userid")]
    public string UserId { get; set; } = default!;
    public ApprovalApplyProcessNode()
    {

    }

    private ApprovalApplyProcessNode(ApprovalProcessNodeType type, string userId, byte? apvRel = null)
    {
        Type = type;
        UserId = userId;
        ApvRel = apvRel;
    }
    /// <summary>
    /// Approver node
    /// </summary>
    /// <param name="userId">Approver Id</param>
    /// <param name="apvRel">Multi-person approval method 1-countersign; 2-or sign 3-approval in sequence</param>
    /// <returns></returns>
    public static ApprovalApplyProcessNode Approver(string userId, byte apvRel)
    {
        return new ApprovalApplyProcessNode(ApprovalProcessNodeType.Approver, userId, apvRel);
    }
    /// <summary>
    /// Manager node
    /// </summary>
    /// <param name="userId">Handler Id</param>
    /// <param name="apvRel">Multi-person approval method 1-countersign; 2-or sign 3-approval in sequence</param>
    /// <returns></returns>
    public static ApprovalApplyProcessNode Handler(string userId, byte apvRel)
    {
        return new ApprovalApplyProcessNode(ApprovalProcessNodeType.Handler, userId, apvRel);
    }
    /// <summary>
    /// Cc node
    /// </summary>
    /// <param name="userId">CCI Id</param>
    /// <returns></returns>
    public static ApprovalApplyProcessNode To(string userId)
    {
        return new ApprovalApplyProcessNode(ApprovalProcessNodeType.CC, userId);
    }
}
