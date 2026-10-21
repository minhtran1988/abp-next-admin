using JetBrains.Annotations;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.WeChat.Work.Messages.Models;
/// <summary>
/// Graphic message carrier
/// </summary>
public class NewMessagePayload
{
    /// <summary>
    /// Graphic and text message list
    /// </summary>
    [NotNull]
    [JsonProperty("articles")]
    [JsonPropertyName("articles")]
    public List<NewMessage> Articles { get; set; }
    public NewMessagePayload(List<NewMessage> articles)
    {
        Articles = articles;
    }
}
/// <summary>
/// Graphic message
/// </summary>
public class NewMessage
{
    public NewMessage(
        string title,
        string description = "",
        string url = "",
        string pictureUrl = "",
        string appId = "",
        string pagePath = "")
    {
        Title = title;
        Description = description;
        Url = url;
        PictureUrl = pictureUrl;
        AppId = appId;
        PagePath = pagePath;
    }

    /// <summary>
    /// Title, no more than 128 bytes, will be automatically truncated if exceeded (supports id translation)
    /// </summary>
    [NotNull]
    [JsonProperty("title")]
    [JsonPropertyName("title")]
    public string Title { get; set; }
    /// <summary>
    /// Description, no more than 512 bytes, will be automatically truncated if exceeded (supports id translation)
    /// </summary>
    [CanBeNull]
    [JsonProperty("description")]
    [JsonPropertyName("description")]
    public string Description { get; set; }
    /// <summary>
    /// The link that jumps after clicking.
    /// The maximum length is 2048 bytes. Please ensure that the protocol header (http/https) is included. Mini program or URL must fill in one.
    /// </summary>
    [CanBeNull]
    [JsonProperty("url")]
    [JsonPropertyName("url")]
    public string Url { get; set; }
    /// <summary>
    /// The picture link of the graphic message can be up to 2048 bytes and supports JPG and PNG formats. The best effect is 1068*455 for large pictures and 150*150 for small pictures.
    /// </summary>
    [CanBeNull]
    [JsonProperty("picurl")]
    [JsonPropertyName("picurl")]
    public string PictureUrl { get; set; }
    /// <summary>
    /// The mini program appid must be a mini program associated with the current application. The appid and pagepath must be filled in at the same time. After filling in, the url field will be ignored.
    /// </summary>
    [CanBeNull]
    [JsonProperty("appid")]
    [JsonPropertyName("appid")]
    public string AppId { get; set; }
    /// <summary>
    /// The mini program page after clicking the message card has a maximum length of 128 bytes and is limited to pages within this mini program. appid and pagepath must be filled in at the same time, and the url field will be ignored after filling in.
    /// </summary>
    [CanBeNull]
    [JsonProperty("pagepath")]
    [JsonPropertyName("pagepath")]
    public string PagePath { get; set; }
}
