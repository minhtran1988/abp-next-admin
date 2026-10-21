using JetBrains.Annotations;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.WeChat.Work.Messages.Models;
/// <summary>
/// Jump guide style
/// </summary>
public class TemplateCardJump
{
    [Newtonsoft.Json.JsonConstructor]
    [System.Text.Json.Serialization.JsonConstructor]
    public TemplateCardJump(string title, string url)
    {
        Type = 1;
        Url = url;
        Title = title;
    }
    public TemplateCardJump(string title, string appid, string pagePath)
    {
        Type = 2;
        AppId = appid;
        PagePath = pagePath;
        Title = title;
    }
    /// <summary>
    /// Jump link type, 0 or not filled in means it is not a link, 1 means jump url, 2 means jump applet
    /// </summary>
    [CanBeNull]
    [JsonProperty("type")]
    [JsonPropertyName("type")]
    public byte Type { get; }
    /// <summary>
    /// It is recommended that the copy content of the jump link style should not exceed 18 words.
    /// </summary>
    [NotNull]
    [JsonProperty("title")]
    [JsonPropertyName("title")]
    public string Title { get; set; }
    /// <summary>
    /// The url of the jump link, required when jump_list.type is 1
    /// </summary>
    [CanBeNull]
    [JsonProperty("url")]
    [JsonPropertyName("url")]
    public string? Url { get; set; }
    /// <summary>
    /// The appid of the applet of the jump link must be the applet associated with the current application. It is required when jump_list.type is 2.
    /// </summary>
    [CanBeNull]
    [JsonProperty("appid")]
    [JsonPropertyName("appid")]
    public string? AppId { get; set; }
    /// <summary>
    /// The pagepath of the mini program for the jump link, optional when jump_list.type is 2
    /// </summary>
    [CanBeNull]
    [JsonProperty("pagepath")]
    [JsonPropertyName("pagepath")]
    public string? PagePath { get; set; }
}
