using JetBrains.Annotations;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.WeChat.Work.Messages.Models;
/// <summary>
/// Second-level title + text
/// </summary>
public class TemplateCardHorizontalContent
{
    public static TemplateCardHorizontalContent None(string keyName, string value = "")
    {
        return new TemplateCardHorizontalContent(
            keyName, 0, value);
    }

    public static TemplateCardHorizontalContent FromUrl(string keyName, string url, string value = "")
    {
        return new TemplateCardHorizontalContent(
            keyName, 1, value, url);
    }

    public static TemplateCardHorizontalContent FromMedia(string keyName, string mediaId, string value = "")
    {
        return new TemplateCardHorizontalContent(
            keyName, 2, value, mediaId: mediaId);
    }

    public static TemplateCardHorizontalContent FromUser(string keyName, string userId, string value = "")
    {
        return new TemplateCardHorizontalContent(
            keyName, 3, value, userId: userId);
    }

    [Newtonsoft.Json.JsonConstructor]
    [System.Text.Json.Serialization.JsonConstructor]
    public TemplateCardHorizontalContent(
        string keyName,
        byte type = 0, 
        string value = "", 
        string url = "", 
        string mediaId = "", 
        string userId = "")
    {
        Type = type;
        KeyName = keyName;
        Value = value;
        Url = url;
        MediaId = mediaId;
        UserId = userId;
    }

    /// <summary>
    /// Link type, 0 or not filled in means it is not a link, 1 means jump url, 2 means download attachment, 3 means click to jump to member details.
    /// </summary>
    [CanBeNull]
    [JsonProperty("type")]
    [JsonPropertyName("type")]
    public byte Type { get; }
    /// <summary>
    /// Second-level title, it is recommended not to exceed 5 words
    /// </summary>
    [NotNull]
    [JsonProperty("keyname")]
    [JsonPropertyName("keyname")]
    public string KeyName { get; set; } = default!;
    /// <summary>
    /// Secondary text, if horizontal_content_list.type is 2, this field represents the file name (including file type), it is recommended not to exceed 30 words, (supports id translation)
    /// </summary>
    [CanBeNull]
    [JsonProperty("value")]
    [JsonPropertyName("value")]
    public string Value { get; set; }
    /// <summary>
    /// URL for link jump, required when horizontal_content_list.type is 1
    /// </summary>
    [CanBeNull]
    [JsonProperty("url")]
    [JsonPropertyName("url")]
    public string Url { get; set; }
    /// <summary>
    /// Required when the media_id of the attachment and horizontal_content_list.type is 2
    /// </summary>
    [CanBeNull]
    [JsonProperty("media_id")]
    [JsonPropertyName("media_id")]
    public string MediaId { get; set; }
    /// <summary>
    /// Userid of member details, required when horizontal_content_list.type is 3
    /// </summary>
    [CanBeNull]
    [JsonProperty("userid")]
    [JsonPropertyName("userid")]
    public string UserId { get; set; }
}
