using JetBrains.Annotations;
using Newtonsoft.Json;
using System.Text.Json.Serialization;
using Volo.Abp;

namespace LINGYUN.Abp.WeChat.Work.Messages.Models;
/// <summary>
/// Left picture and right text style
/// </summary>
public class WebhookTemplateCardImageTextArea
{
    /// <summary>
    /// Click event in the left image and right text style area, 0 or not filled in means there is no click event, 1 means jump url, 2 means jump applet
    /// </summary>
    [CanBeNull]
    [JsonProperty("type")]
    [JsonPropertyName("type")]
    public int? Type { get; set; }
    /// <summary>
    /// Image url with left image and right text style
    /// </summary>
    [NotNull]
    [JsonProperty("image_url")]
    [JsonPropertyName("image_url")]
    public string ImageUrl { get; set; }
    /// <summary>
    /// Click to jump to the url, required when type is 1
    /// </summary>
    [NotNull]
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
    /// Title with left image and right text style
    /// </summary>
    [CanBeNull]
    [JsonProperty("title")]
    [JsonPropertyName("title")]
    public string? Title { get; set; }
    /// <summary>
    /// Description of the left image and right text style
    /// </summary>
    [CanBeNull]
    [JsonProperty("desc")]
    [JsonPropertyName("desc")]
    public string? Description { get; set; }
    [System.Text.Json.Serialization.JsonConstructor]
    public WebhookTemplateCardImageTextArea(
        string imageUrl,
        int? type = null, 
        string? url = null,
        string? appId = null,
        string? pagePath = null,
        string? title = null,
        string? description = null)
    {
        Type = type;
        ImageUrl = imageUrl;
        Url = url;
        AppId = appId;
        PagePath = pagePath;
        Title = title;
        Description = description;
    }
    /// <summary>
    /// Create a jump link with left image and right text style
    /// </summary>
    /// <param name="imageUrl">Image url with left image and right text style</param>
    /// <param name="url">Click to jump to the url</param>
    /// <param name="title">Title with left image and right text style</param>
    /// <param name="description">Description of the left image and right text style</param>
    /// <returns></returns>
    public static WebhookTemplateCardImageTextArea Link(
        string imageUrl,
        string url,
        string? title = null,
        string? description = null)
    {
        Check.NotNullOrWhiteSpace(imageUrl, nameof(imageUrl));
        Check.NotNullOrWhiteSpace(url, nameof(url));

        return new WebhookTemplateCardImageTextArea(imageUrl, 1, url: url, title: title, description: description);
    }
    /// <summary>
    /// Create a jump applet with left image and right text style
    /// </summary>
    /// <param name="imageUrl">Image url with left image and right text style</param>
    /// <param name="appId">Mini program AppId</param>
    /// <param name="pagePath">Mini program PagePath</param>
    /// <param name="title">Title with left image and right text style</param>
    /// <param name="description">Description of the left image and right text style</param>
    /// <returns></returns>
    public static WebhookTemplateCardImageTextArea MiniProgram(
        string imageUrl, 
        string appId, 
        string pagePath,
        string? title = null,
        string? description = null)
    {
        Check.NotNullOrWhiteSpace(imageUrl, nameof(imageUrl));
        Check.NotNullOrWhiteSpace(appId, nameof(appId));
        Check.NotNullOrWhiteSpace(pagePath, nameof(pagePath));

        return new WebhookTemplateCardImageTextArea(imageUrl, 2, appId: appId, pagePath: pagePath, title: title, description: description);
    }
}
