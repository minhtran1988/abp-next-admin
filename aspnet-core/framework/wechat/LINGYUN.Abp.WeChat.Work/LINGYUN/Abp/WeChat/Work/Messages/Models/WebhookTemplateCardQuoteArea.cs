using JetBrains.Annotations;
using Newtonsoft.Json;
using System.Text.Json.Serialization;
using Volo.Abp;

namespace LINGYUN.Abp.WeChat.Work.Messages.Models;
/// <summary>
/// Cited documents
/// </summary>
public class WebhookTemplateCardQuoteArea
{
    /// <summary>
    /// Click event in the cited document style area, 0 or not filled in means there is no click event, 1 means jump url, 2 means jump applet
    /// </summary>
    [CanBeNull]
    [JsonProperty("type")]
    [JsonPropertyName("type")]
    public int? Type { get; set; }
    /// <summary>
    /// Click to jump to the url, required when type is 1
    /// </summary>
    [CanBeNull]
    [JsonProperty("url")]
    [JsonPropertyName("url")]
    public string? Url { get; set; }
    /// <summary>
    /// The appid of the mini program that you click to jump to. Required when type is 2.
    /// </summary>
    [CanBeNull]
    [JsonProperty("appid")]
    [JsonPropertyName("appid")]
    public string? AppId { get; set; }
    /// <summary>
    /// Click to jump to the pagepath of the applet. Optional when type is 2.
    /// </summary>
    [CanBeNull]
    [JsonProperty("pagepath")]
    [JsonPropertyName("pagepath")]
    public string? PagePath { get; set; }
    /// <summary>
    /// Citation document style title
    /// </summary>
    [CanBeNull]
    [JsonProperty("title")]
    [JsonPropertyName("title")]
    public string? Title { get; set; }
    /// <summary>
    /// Citation copy in citation style
    /// </summary>
    [CanBeNull]
    [JsonProperty("quote_text")]
    [JsonPropertyName("quote_text")]
    public string? QuoteText { get; set; }
    [System.Text.Json.Serialization.JsonConstructor]
    public WebhookTemplateCardQuoteArea(
        string title,
        int? type = null,
        string? url = null,
        string? appId = null,
        string? pagePath = null,
        string? quoteText = null)
    {
        Title = title;
        QuoteText = quoteText;
        Type = type;
        Url = url;
        AppId = appId;
        PagePath = pagePath;
    }
    /// <summary>
    /// Create a default citation
    /// </summary>
    /// <param name="title">Citation document style title</param>
    /// <param name="quoteText">Quotation copy in citation style</param>
    /// <returns></returns>
    public static WebhookTemplateCardQuoteArea Default(string title, string? quoteText = null)
    {
        Check.NotNullOrWhiteSpace(title, nameof(title));

        return new WebhookTemplateCardQuoteArea(title, quoteText: quoteText);
    }
    /// <summary>
    /// Create a citation for a jump link
    /// </summary>
    /// <param name="title">Citation document style title</param>
    /// <param name="url">Click to jump to the url</param>
    /// <param name="quoteText">Quotation copy in citation style</param>
    /// <returns></returns>
    public static WebhookTemplateCardQuoteArea Link(string title, string url, string? quoteText = null)
    {
        Check.NotNullOrWhiteSpace(title, nameof(title));
        Check.NotNullOrWhiteSpace(url, nameof(url));

        return new WebhookTemplateCardQuoteArea(title, 1, url, quoteText: quoteText);
    }
    /// <summary>
    /// Create a reference document for the jump applet
    /// </summary>
    /// <param name="title">Citation document style title</param>
    /// <param name="appId">The appid of the mini program that jumps to the link</param>
    /// <param name="pagePath">The pagepath of the mini program for the jump link</param>
    /// <param name="quoteText">Quotation copy in citation style</param>
    /// <returns></returns>
    public static WebhookTemplateCardQuoteArea MiniProgram(string title, string appId, string pagePath, string? quoteText = null)
    {
        Check.NotNullOrWhiteSpace(title, nameof(title));
        Check.NotNullOrWhiteSpace(appId, nameof(appId));
        Check.NotNullOrWhiteSpace(pagePath, nameof(pagePath));

        return new WebhookTemplateCardQuoteArea(title, 2, appId: appId, pagePath: pagePath, quoteText: quoteText);
    }
}
