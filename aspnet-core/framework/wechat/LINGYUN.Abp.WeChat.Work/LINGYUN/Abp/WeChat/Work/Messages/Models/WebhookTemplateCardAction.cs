using JetBrains.Annotations;
using Newtonsoft.Json;
using System.Text.Json.Serialization;
using Volo.Abp;

namespace LINGYUN.Abp.WeChat.Work.Messages.Models;
/// <summary>
/// Card click jump event
/// </summary>
public class WebhookTemplateCardAction
{
    /// <summary>
    /// Card jump type, 1 means jump to url, 2 means open mini program. The value range of this field in the text_notice template card is [1,2]
    /// </summary>
    [NotNull]
    [JsonProperty("type")]
    [JsonPropertyName("type")]
    public int Type { get; set; }
    /// <summary>
    /// The url of the jump link, required when type is 1
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
    [System.Text.Json.Serialization.JsonConstructor]
    public WebhookTemplateCardAction(
        int type,
        string? url = null,
        string? appId = null,
        string? pagePath = null)
    {
        Type = type;
        Url = url;
        AppId = appId;
        PagePath = pagePath;
    }
    /// <summary>
    /// Create a jump link card event
    /// </summary>
    /// <param name="url">URL of jump link</param>
    /// <returns></returns>
    public static WebhookTemplateCardAction Link(string url)
    {
        Check.NotNullOrWhiteSpace(url, nameof(url));

        return new WebhookTemplateCardAction(1, url);
    }
    /// <summary>
    /// Create a jump applet card event
    /// </summary>
    /// <param name="appId">appid of the mini program</param>
    /// <param name="pagePath">pagePath of the applet</param>
    /// <returns></returns>
    public static WebhookTemplateCardAction MiniProgram(string appId, string pagePath)
    {
        Check.NotNullOrWhiteSpace(appId, nameof(appId));
        Check.NotNullOrWhiteSpace(pagePath, nameof(pagePath));

        return new WebhookTemplateCardAction(2, appId: appId, pagePath: pagePath);
    }
}
