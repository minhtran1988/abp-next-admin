using JetBrains.Annotations;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.WeChat.Work.Messages.Models;
/// <summary>
/// More action buttons in the upper right corner of the card
/// </summary>
public class TemplateCardActionMenu
{
    /// <summary>
    /// Card deputy interaction auxiliary text description
    /// </summary>
    [CanBeNull]
    [JsonProperty("desc")]
    [JsonPropertyName("desc")]
    public string? Description { get; set; }
    /// <summary>
    /// Operation list, the list length range is [1, 3]
    /// </summary>
    [NotNull]
    [JsonProperty("action_list")]
    [JsonPropertyName("action_list")]
    public List<TemplateCardAction> Actions { get; set;}

    [Newtonsoft.Json.JsonConstructor]
    [System.Text.Json.Serialization.JsonConstructor]
    public TemplateCardActionMenu(List<TemplateCardAction> actions, string description = "")
    {
        Actions = actions;
        Description = description;
    }
}
