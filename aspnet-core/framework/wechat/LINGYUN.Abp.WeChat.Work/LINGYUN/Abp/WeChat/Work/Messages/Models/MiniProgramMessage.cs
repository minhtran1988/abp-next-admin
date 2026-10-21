using JetBrains.Annotations;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.WeChat.Work.Messages.Models;
/// <summary>
/// Mini program notification message
/// </summary>
public class MiniProgramMessage
{
    /// <summary>
    /// The applet appid must be the applet associated with the current application
    /// </summary>
    [NotNull]
    [JsonProperty("appid")]
    [JsonPropertyName("appid")]
    public string AppId { get; set; } = default!;
    /// <summary>
    /// Message title, length limit 4-12 Chinese characters (supports id translation)
    /// </summary>
    [NotNull]
    [JsonProperty("title")]
    [JsonPropertyName("title")]
    public string Title { get; set; } = default!;
    /// <summary>
    /// The mini program page after clicking the message card has a maximum length of 1024 bytes and is limited to pages within this mini program. If this field is not filled in, the message will not jump after being clicked.
    /// </summary>
    [CanBeNull]
    [JsonProperty("page")]
    [JsonPropertyName("page")]
    public string? Page { get; set; }
    /// <summary>
    /// Message description, length limit 4-12 Chinese characters (supports id translation)
    /// </summary>
    [CanBeNull]
    [JsonProperty("description")]
    [JsonPropertyName("description")]
    public string? Description { get; set; }
    /// <summary>
    /// Whether to enlarge the first content_item
    /// </summary>
    [CanBeNull]
    [JsonProperty("emphasis_first_item")]
    [JsonPropertyName("emphasis_first_item")]
    public bool? EmphasisFirstItem { get; set; }
    /// <summary>
    /// Message content key-value pair, up to 10 items allowed
    /// </summary>
    [CanBeNull]
    [JsonProperty("content_item")]
    [JsonPropertyName("content_item")]
    public List<MiniProgramContent>? ContentItems { get; set; }

    [Newtonsoft.Json.JsonConstructor]
    [System.Text.Json.Serialization.JsonConstructor]
    public MiniProgramMessage(
        string appId,
        string title,
        string? page = null, 
        string? description = null, 
        bool? emphasisFirstItem = null)
    {
        AppId = appId;
        Page = page;
        Title = title;
        Description = description;
        EmphasisFirstItem = emphasisFirstItem;
    }
}

public class MiniProgramContent
{
    /// <summary>
    /// Length within 10 Chinese characters
    /// </summary>
    [NotNull]
    [JsonProperty("key")]
    [JsonPropertyName("key")]
    public string Key { get; set; } = default!;
    /// <summary>
    /// Length within 30 Chinese characters (supports id translation)
    /// </summary>
    [NotNull]
    [JsonProperty("value")]
    [JsonPropertyName("value")]
    public string Value { get; set; } = default!;

    [Newtonsoft.Json.JsonConstructor]
    [System.Text.Json.Serialization.JsonConstructor]
    public MiniProgramContent(string key, string value)
    {
        Key = key;
        Value = value;
    }
}
