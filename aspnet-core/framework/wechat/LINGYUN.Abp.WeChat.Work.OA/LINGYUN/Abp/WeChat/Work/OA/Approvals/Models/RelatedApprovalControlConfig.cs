using JetBrains.Annotations;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.WeChat.Work.OA.Approvals.Models;
/// <summary>
/// Associated approval order control configuration
/// </summary>
public class RelatedApprovalControlConfig : ControlConfig
{
    /// <summary>
    /// Associated approval order control
    /// </summary>
    [NotNull]
    [JsonProperty("related_approval")]
    [JsonPropertyName("related_approval")]
    public RelatedApprovalConfig RelatedApproval { get; set; } = default!;
    public RelatedApprovalControlConfig()
    {

    }

    public RelatedApprovalControlConfig(RelatedApprovalConfig relatedApproval)
    {
        RelatedApproval = relatedApproval;
    }
}

public class RelatedApprovalConfig
{
    /// <summary>
    /// The template_id associated with the approval form. If not filled in, it means that all templates can be associated. The template_id can be obtained through the Get Approval Template interface.
    /// </summary>
    [NotNull]
    [JsonProperty("template_id")]
    [JsonPropertyName("template_id")]
    public string TemplateId { get; set; } = default!;
    public RelatedApprovalConfig()
    {

    }

    public RelatedApprovalConfig(string templateId)
    {
        TemplateId = templateId;
    }
}
