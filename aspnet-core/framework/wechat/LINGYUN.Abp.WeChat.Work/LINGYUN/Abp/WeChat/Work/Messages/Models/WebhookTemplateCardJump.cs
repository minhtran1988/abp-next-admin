using JetBrains.Annotations;
using Newtonsoft.Json;
using System.Text.Json.Serialization;
using Volo.Abp;

namespace LINGYUN.Abp.WeChat.Work.Messages.Models;
/// <summary>
/// Jump guide style
/// </summary>
public class WebhookTemplateCardJump
{
    /// <summary>
    /// Jump link type, 0 or not filled in means it is not a link, 1 means jump url, 2 means jump applet
    /// </summary>
    [CanBeNull]
    [JsonProperty("type")]
    [JsonPropertyName("type")]
    public int? Type { get; set; }
    /// <summary>
    /// It is recommended that the copy content of the jump link style should not exceed 13 words.
    /// </summary>
    [NotNull]
    [JsonProperty("title")]
    [JsonPropertyName("title")]
    public string Title { get; set; }
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
    public WebhookTemplateCardJump(
        string title,
        int? type = null,
        string? url = null,
        string? appId = null,
        string? pagePath = null)
    {
        Type = type;
        Title = title;
        Url = url;
        AppId = appId;
        PagePath = pagePath;
    }
    /// <summary>
    /// Create a default guide style
    /// </summary>
    /// <param name="title">Copy content of jump link style</param>
    /// <returns></returns>
    public static WebhookTemplateCardJump Default(string title)
    {
        Check.NotNullOrWhiteSpace(title, nameof(title));

        return new WebhookTemplateCardJump(title);
    }
    /// <summary>
    /// Create a guide style for jump links
    /// </summary>
    /// <param name="title">Copy content of jump link style</param>
    /// <param name="url">URL of jump link</param>
    /// <returns></returns>
    public static WebhookTemplateCardJump Link(string title, string url)
    {
        Check.NotNullOrWhiteSpace(title, nameof(title));
        Check.NotNullOrWhiteSpace(url, nameof(url));

        return new WebhookTemplateCardJump(title, 1, url);
    }
    /// <summary>
    /// Create a guide style for jumping applet
    /// </summary>
    /// <param name="title">Copy content of jump link style</param>
    /// <param name="appId">The appid of the mini program that jumps to the link</param>
    /// <param name="pagePath">The pagepath of the mini program for the jump link</param>
    /// <returns></returns>
    public static WebhookTemplateCardJump MiniProgram(string title, string appId, string pagePath)
    {
        Check.NotNullOrWhiteSpace(title, nameof(title));
        Check.NotNullOrWhiteSpace(appId, nameof(appId));
        Check.NotNullOrWhiteSpace(pagePath, nameof(pagePath));

        return new WebhookTemplateCardJump(title, 2, appId: appId, pagePath: pagePath);
    }
}
