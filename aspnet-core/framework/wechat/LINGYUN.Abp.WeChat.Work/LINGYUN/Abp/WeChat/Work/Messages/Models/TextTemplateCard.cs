using JetBrains.Annotations;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.WeChat.Work.Messages.Models;
/// <summary>
/// Text template card message
/// </summary>
public class TextTemplateCard : TemplateCard
{
    [Newtonsoft.Json.JsonConstructor]
    [System.Text.Json.Serialization.JsonConstructor]
    public TextTemplateCard(TemplateCardCardAction cardAction, string taskId = "")
        : base("template_card", taskId)
    {
        CardAction = cardAction;
    }

    /// <summary>
    /// Card source style information, you don’t need to fill in the source style
    /// </summary>
    [CanBeNull]
    [JsonProperty("source")]
    [JsonPropertyName("source")]
    public TemplateCardSource? Source { get; set; }
    /// <summary>
    /// More action buttons in the upper right corner of the card
    /// </summary>
    [CanBeNull]
    [JsonProperty("action_menu")]
    [JsonPropertyName("action_menu")]
    public TemplateCardActionMenu? ActionMenu { get; set; }
    /// <summary>
    /// Level 1 title
    /// </summary>
    [CanBeNull]
    [JsonProperty("main_title")]
    [JsonPropertyName("main_title")]
    public TemplateCardMainTitle? MainTitle { get; set; }
    /// <summary>
    /// Second-level ordinary text, it is recommended not to exceed 160 words (supports id translation)
    /// </summary>
    [CanBeNull]
    [JsonProperty("sub_title_text")]
    [JsonPropertyName("sub_title_text")]
    public string? SubTitle { get; set; }
    /// <summary>
    /// Citation style
    /// </summary>
    [CanBeNull]
    [JsonProperty("quote_area")]
    [JsonPropertyName("quote_area")]
    public TemplateCardQuoteArea? QuoteArea { get; set; }
    /// <summary>
    /// Key data style
    /// </summary>
    [CanBeNull]
    [JsonProperty("emphasis_content")]
    [JsonPropertyName("emphasis_content")]
    public TemplateCardEmphasisContent? EmphasisContent { get; set; }
    /// <summary>
    /// Second-level title + text list, this field can be an empty array, but if there is data, you need to confirm whether the corresponding field is required. The length of the list should not exceed 6
    /// </summary>
    [CanBeNull]
    [JsonProperty("horizontal_content_list")]
    [JsonPropertyName("horizontal_content_list")]
    public List<TemplateCardHorizontalContent>? HorizontalContents { get; set; }
    /// <summary>
    /// A list of jump guide styles. This field can be an empty array, but if there is data, you need to confirm whether the corresponding field is required. The length of the list should not exceed 3
    /// </summary>
    [CanBeNull]
    [JsonProperty("jump_list")]
    [JsonPropertyName("jump_list")]
    public List<TemplateCardJump>? Jumps { get; set; }
    /// <summary>
    /// Click jump event of the overall card, text_notice must fill in this field
    /// </summary>
    [NotNull]
    [JsonProperty("TemplateCardCardAction")]
    [JsonPropertyName("TemplateCardCardAction")]
    public TemplateCardCardAction CardAction { get; set; } = default!;
}
