using JetBrains.Annotations;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Volo.Abp;

namespace LINGYUN.Abp.WeChat.Work.Messages.Models;
/// <summary>
/// Webhook template card message body
/// </summary>
public abstract class WebhookTemplateCardMessage
{
    /// <summary>
    /// Template type of template card
    /// </summary>
    [CanBeNull]
    [JsonProperty("card_type")]
    [JsonPropertyName("card_type")]
    public string? CardType { get; set; }
    /// <summary>
    /// Card source style information, you don’t need to fill in the source style
    /// </summary>
    [CanBeNull]
    [JsonProperty("source")]
    [JsonPropertyName("source")]
    public WebhookTemplateCardSource? Source { get; set; }
    /// <summary>
    /// The main content of the template card, including first-level title and title auxiliary information
    /// </summary>
    [CanBeNull]
    [JsonProperty("main_title")]
    [JsonPropertyName("main_title")]
    public WebhookTemplateCardMainTitle? MainTitle { get; set; }
    /// <summary>
    /// Reference document style, it is recommended not to be shared with key data
    /// </summary>
    [CanBeNull]
    [JsonProperty("quote_area")]
    [JsonPropertyName("quote_area")]
    public WebhookTemplateCardQuoteArea? QuoteArea { get; set; }
    /// <summary>
    /// Second-level title + text list, this field can be an empty array, but if there is data, you need to confirm whether the corresponding field is required. The length of the list should not exceed 6
    /// </summary>
    [CanBeNull]
    [JsonProperty("horizontal_content_list")]
    [JsonPropertyName("horizontal_content_list")]
    public List<WebhookTemplateCardHorizontalContent>? HorizontalContents { get; set; }
    /// <summary>
    /// A list of jump guide styles. This field can be an empty array, but if there is data, you need to confirm whether the corresponding field is required. The length of the list should not exceed 3
    /// </summary>
    [CanBeNull]
    [JsonProperty("jump_list")]
    [JsonPropertyName("jump_list")]
    public List<WebhookTemplateCardJump>? Jumps { get; set; }
    /// <summary>
    /// Click jump event of the overall card. This field in the text_notice template card is required.
    /// </summary>
    [CanBeNull]
    [JsonProperty("card_action")]
    [JsonPropertyName("card_action")]
    public WebhookTemplateCardAction? Action { get; set; }
    /// <summary>
    /// Create a Webhook template card message body
    /// </summary>
    /// <param name="cardType">Template type of template card</param>
    /// <param name="action">Click jump event of the overall card</param>
    /// <param name="mainTitle">The main content of the template card</param>
    /// <param name="source">Card source style information</param>
    /// <param name="quoteArea">Quotation style</param>
    /// <param name="horizontalContents">Second-level title + text list, the list length does not exceed 6</param>
    /// <param name="jumps">List of jump guide styles, the length of the list should not exceed 3</param>
    /// <exception cref="ArgumentException"></exception>
    protected WebhookTemplateCardMessage(
        string cardType,
        WebhookTemplateCardAction action,
        WebhookTemplateCardMainTitle? mainTitle = null,
        WebhookTemplateCardSource? source = null,
        WebhookTemplateCardQuoteArea? quoteArea = null,
        List<WebhookTemplateCardHorizontalContent>? horizontalContents = null,
        List<WebhookTemplateCardJump>? jumps = null)
    {
        CardType = Check.NotNullOrWhiteSpace(cardType, nameof(cardType));
        Action = Check.NotNull(action, nameof(action));

        MainTitle = mainTitle;
        Source = source;
        QuoteArea = quoteArea;
        HorizontalContents = horizontalContents;
        Jumps = jumps;

        if (horizontalContents != null && horizontalContents.Count > 6)
        {
            throw new ArgumentException("The length of the secondary title and the text list should not exceed 6!");
        }
        if (jumps != null && jumps.Count > 3)
        {
            throw new ArgumentException("The length of the list in the style of jump guidance should not exceed 3!");
        }
    }
}
