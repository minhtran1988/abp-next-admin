using JetBrains.Annotations;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.WeChat.Work.Messages.Models;
/// <summary>
/// Webhook text notification template card message body
/// </summary>
public class WebhookTextNoticeCardMessage : WebhookTemplateCardMessage
{
    /// <summary>
    /// Second-level ordinary text, it is recommended not to exceed 112 words. One of the first-level title main_title.title and the second-level ordinary text sub_title_text of the main content of the template card must be filled in
    /// </summary>
    [CanBeNull]
    [JsonProperty("sub_title_text")]
    [JsonPropertyName("sub_title_text")]
    public string? SubTitleText { get; set; }
    /// <summary>
    /// Key data style
    /// </summary>
    [CanBeNull]
    [JsonProperty("emphasis_content")]
    [JsonPropertyName("emphasis_content")]
    public WebhookTemplateCardEmphasisContent? EmphasisContent { get; set; }
    /// <summary>
    /// Create a Webhook text notification template card message body
    /// </summary>
    /// <param name="action">Click jump event of the overall card</param>
    /// <param name="mainTitle">The main content of the template card</param>
    /// <param name="subTitleText">Level two ordinary text</param>
    /// <param name="emphasisContent">Key data style</param>
    /// <param name="source">Card source style information</param>
    /// <param name="quoteArea">Quotation style</param>
    /// <param name="horizontalContents">Second-level title + text list, the list length does not exceed 6</param>
    /// <param name="jumps">List of jump guide styles, the length of the list should not exceed 3</param>
    /// <exception cref="ArgumentException"></exception>
    public WebhookTextNoticeCardMessage(
        WebhookTemplateCardAction action,
        WebhookTemplateCardMainTitle? mainTitle = null,
        string? subTitleText = null,
        WebhookTemplateCardEmphasisContent? emphasisContent = null,
        WebhookTemplateCardSource? source = null,
        WebhookTemplateCardQuoteArea? quoteArea = null,
        List<WebhookTemplateCardHorizontalContent>? horizontalContents = null,
        List<WebhookTemplateCardJump>? jumps = null) 
        : base("text_notice", action, mainTitle, source, quoteArea, horizontalContents, jumps)
    {
        MainTitle = mainTitle;
        SubTitleText = subTitleText;
        EmphasisContent = emphasisContent;
        Source = source;
        QuoteArea = quoteArea;
        HorizontalContents = horizontalContents;
        Jumps = jumps;

        if (mainTitle == null && subTitleText.IsNullOrWhiteSpace())
        {
            throw new ArgumentException("One of the primary title mainTitle and the secondary ordinary text subTitleText of the main content of the template card must be filled in!");
        }
    }
}
