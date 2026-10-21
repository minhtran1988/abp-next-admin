using JetBrains.Annotations;
using Newtonsoft.Json;
using System.Text.Json.Serialization;
using Volo.Abp;

namespace LINGYUN.Abp.WeChat.Work.Messages.Models;
/// <summary>
/// Second-level title + text
/// </summary>
public class WebhookTemplateCardHorizontalContent
{
    /// <summary>
    /// Types supported by the secondary title information content of the template card, 1 is url, 2 is file attachment, 3 represents click to jump to member details
    /// </summary>
    [CanBeNull]
    [JsonProperty("type")]
    [JsonPropertyName("type")]
    public int? Type { get; set; }
    /// <summary>
    /// Second-level title, it is recommended not to exceed 5 words
    /// </summary>
    [NotNull]
    [JsonProperty("keyname")]
    [JsonPropertyName("keyname")]
    public string KeyName { get; set; }
    /// <summary>
    /// Second-level text, if type is 2, this field represents the file name (including file type), it is recommended not to exceed 26 characters
    /// </summary>
    [CanBeNull]
    [JsonProperty("value")]
    [JsonPropertyName("value")]
    public string? Value { get; set; }
    /// <summary>
    /// URL for link jump, required when type is 1
    /// </summary>
    [CanBeNull]
    [JsonProperty("url")]
    [JsonPropertyName("url")]
    public string? Url { get; set; }
    /// <summary>
    /// The media_id of the attachment, required when type is 2
    /// </summary>
    [CanBeNull]
    [JsonProperty("media_id")]
    [JsonPropertyName("media_id")]
    public string? MediaId { get; set; }
    /// <summary>
    /// Userid of member details, required when type is 3
    /// </summary>
    [CanBeNull]
    [JsonProperty("userid")]
    [JsonPropertyName("userid")]
    public string? UserId { get; set; }
    [System.Text.Json.Serialization.JsonConstructor]
    public WebhookTemplateCardHorizontalContent(
        string keyName,
        int? type = null,
        string? value = null,
        string? url = null,
        string? mediaId = null,
        string? userId = null)
    {
        Type = type;
        KeyName = keyName;
        Value = value;
        Url = url;
        MediaId = mediaId;
        UserId = userId;
    }
    /// <summary>
    /// Create a default secondary title + text
    /// </summary>
    /// <param name="keyName">Second-level title</param>
    /// <param name="value">Second-level text</param>
    /// <returns></returns>
    public static WebhookTemplateCardHorizontalContent Default(string keyName, string? value = null)
    {
        Check.NotNullOrWhiteSpace(keyName, nameof(keyName));

        return new WebhookTemplateCardHorizontalContent(keyName, value: value);
    }
    /// <summary>
    /// Create a secondary title + text for a jump link
    /// </summary>
    /// <param name="keyName">Second-level title</param>
    /// <param name="url">URL for link jump</param>
    /// <param name="value">Second-level text</param>
    /// <returns></returns>
    public static WebhookTemplateCardHorizontalContent Link(string keyName, string url, string? value = null)
    {
        Check.NotNullOrWhiteSpace(keyName, nameof(keyName));
        Check.NotNullOrWhiteSpace(url, nameof(url));

        return new WebhookTemplateCardHorizontalContent(keyName, 1, value: value, url: url);
    }
    /// <summary>
    /// Create a secondary title + text that references the file
    /// </summary>
    /// <param name="keyName">Second-level title</param>
    /// <param name="fileName">File name</param>
    /// <param name="mediaId">mediaId of the attachment</param>
    /// <returns></returns>
    public static WebhookTemplateCardHorizontalContent File(string keyName, string fileName, string mediaId)
    {
        Check.NotNullOrWhiteSpace(keyName, nameof(keyName));
        Check.NotNullOrWhiteSpace(fileName, nameof(fileName));
        Check.NotNullOrWhiteSpace(mediaId, nameof(mediaId));

        return new WebhookTemplateCardHorizontalContent(keyName, 2, value: fileName, mediaId: mediaId);
    }
    /// <summary>
    /// Create a secondary title + text for member details
    /// </summary>
    /// <param name="keyName">Second-level title</param>
    /// <param name="userId">Member's userid</param>
    /// <returns></returns>
    public static WebhookTemplateCardHorizontalContent UserInfo(string keyName, string userId)
    {
        Check.NotNullOrWhiteSpace(keyName, nameof(keyName));
        Check.NotNullOrWhiteSpace(userId, nameof(userId));

        return new WebhookTemplateCardHorizontalContent(keyName, 3, userId: userId);
    }
}
