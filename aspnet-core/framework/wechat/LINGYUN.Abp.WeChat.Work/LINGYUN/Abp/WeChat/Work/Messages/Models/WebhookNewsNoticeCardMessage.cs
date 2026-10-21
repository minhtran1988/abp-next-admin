using JetBrains.Annotations;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Volo.Abp;

namespace LINGYUN.Abp.WeChat.Work.Messages.Models;
/// <summary>
/// Webhook graphic display template card message body
/// </summary>
public class WebhookNewsNoticeCardMessage : WebhookTemplateCardMessage
{
    /// <summary>
    /// Picture style
    /// </summary>
    [NotNull]
    [JsonProperty("aspect_ratio")]
    [JsonPropertyName("aspect_ratio")]
    public WebhookTemplateCardImage CardImage { get; set; }
    /// <summary>
    /// Left picture and right text style
    /// </summary>
    [CanBeNull]
    [JsonProperty("image_text_area")]
    [JsonPropertyName("image_text_area")]
    public WebhookTemplateCardImageTextArea? ImageTextArea { get; set; }
    /// <summary>
    /// Second-level vertical content of the card. This field can be an empty array, but if there is data, you need to confirm whether the corresponding field is required. The length of the list should not exceed 4
    /// </summary>
    [CanBeNull]
    [JsonProperty("vertical_content_list")]
    [JsonPropertyName("vertical_content_list")]
    public List<WebhookTemplateCardVerticalContent>? VerticalContents { get; set; }
    /// <summary>
    /// Create a Webhook graphic and text display template card message body
    /// </summary>
    /// <param name="cardImage">Picture style</param>
    /// <param name="action">Click jump event of the overall card</param>
    /// <param name="mainTitle">The main content of the template card</param>
    /// <param name="imageTextArea">Left image and right text style</param>
    /// <param name="source">Card source style information</param>
    /// <param name="quoteArea">Quotation style</param>
    /// <param name="horizontalContents">Second-level title + text list, the list length does not exceed 6</param>
    /// <param name="verticalContents">Card secondary vertical content, the list length does not exceed 4</param>
    /// <param name="jumps">List of jump guide styles, the length of the list should not exceed 3</param>
    /// <exception cref="ArgumentException"></exception>
    public WebhookNewsNoticeCardMessage(
        WebhookTemplateCardImage cardImage,
        WebhookTemplateCardAction action,
        WebhookTemplateCardMainTitle mainTitle,
        WebhookTemplateCardImageTextArea? imageTextArea = null,
        WebhookTemplateCardSource? source = null,
        WebhookTemplateCardQuoteArea? quoteArea = null, 
        List<WebhookTemplateCardHorizontalContent>? horizontalContents = null,
        List<WebhookTemplateCardVerticalContent>? verticalContents = null,
        List<WebhookTemplateCardJump>? jumps = null) 
        : base("news_notice", action, mainTitle, source, quoteArea, horizontalContents, jumps)
    {
        Check.NotNull(mainTitle, nameof(mainTitle));
        Check.NotNull(cardImage, nameof(cardImage));

        CardImage = cardImage;
        ImageTextArea = imageTextArea;
        VerticalContents = verticalContents;

        if (verticalContents != null && verticalContents.Count > 4)
        {
            throw new ArgumentException("The length of the secondary vertical content list on the card cannot exceed 4!");
        }
    }
}
