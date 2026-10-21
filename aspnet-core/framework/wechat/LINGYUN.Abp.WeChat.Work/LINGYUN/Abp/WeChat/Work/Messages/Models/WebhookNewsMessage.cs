using JetBrains.Annotations;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Volo.Abp;

namespace LINGYUN.Abp.WeChat.Work.Messages.Models;
/// <summary>
/// Webhook graphic message body
/// </summary>
public class WebhookNewsMessage
{
    /// <summary>
    /// Graphic message, one graphic message supports 1 to 8 graphic messages
    /// </summary>
    [NotNull]
    [JsonProperty("articles")]
    [JsonPropertyName("articles")]
    public List<WebhookArticleMessage> Articles { get; set; }
    /// <summary>
    /// Create a Webhook graphic message body
    /// </summary>
    /// <param name="articles">Graphic and text message list</param>
    /// <exception cref="ArgumentException"></exception>
    public WebhookNewsMessage(List<WebhookArticleMessage> articles)
    {
        Articles = Check.NotNull(articles, nameof(articles));

        if (Articles.Count < 1 || Articles.Count > 8)
        {
            throw new ArgumentException("One image-text message supports 1 to 8 image-text messages!");
        }
    }
}

public class WebhookArticleMessage
{
    /// <summary>
    /// Title, no more than 128 bytes, it will be automatically truncated if it exceeds
    /// </summary>
    [NotNull]
    [StringLength(128)]
    [JsonProperty("title")]
    [JsonPropertyName("title")]
    public string Title { get; set; }
    /// <summary>
    /// Description, no more than 512 bytes, it will be automatically truncated if it exceeds
    /// </summary>
    [CanBeNull]
    [StringLength(512)]
    [JsonProperty("description")]
    [JsonPropertyName("description")]
    public string? Description { get; set; }
    /// <summary>
    /// The link that jumps after clicking.
    /// </summary>
    [NotNull]
    [JsonProperty("url")]
    [JsonPropertyName("url")]
    public string Url { get; set; }
    /// <summary>
    /// Image links for graphic messages support JPG and PNG formats. The best effect is 1068*455 for large images and 150*150 for small images.
    /// </summary>
    [CanBeNull]
    [JsonProperty("picurl")]
    [JsonPropertyName("picurl")]
    public string? PictureUrl { get; set; }
    /// <summary>
    /// Create a graphic message
    /// </summary>
    /// <param name="title">Title</param>
    /// <param name="url">The link that jumps after clicking</param>
    /// <param name="description">Description</param>
    /// <param name="pictureUrl">Picture link of graphic message</param>
    public WebhookArticleMessage(
        string title,
        string url,
        string description = "",
        string pictureUrl = "")
    {
        Title = Check.NotNullOrWhiteSpace(title, nameof(title), 128);
        Url = Check.NotNullOrWhiteSpace(url, nameof(url));
        Description = Check.Length(description, nameof(description), 512);
        PictureUrl = pictureUrl;
    }
}
