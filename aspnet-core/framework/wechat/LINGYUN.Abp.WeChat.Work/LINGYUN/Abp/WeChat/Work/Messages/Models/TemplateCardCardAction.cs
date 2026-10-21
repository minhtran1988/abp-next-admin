using JetBrains.Annotations;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.WeChat.Work.Messages.Models;
/// <summary>
/// Click jump event of the overall card
/// </summary>
public class TemplateCardCardAction
{
    [Newtonsoft.Json.JsonConstructor]
    [System.Text.Json.Serialization.JsonConstructor]
    public TemplateCardCardAction(string url)
    {
        Type = 1;
        Url = url;
    }

    public TemplateCardCardAction(string appid, string pagePath)
    {
        Type = 2;
        AppId = appid;
        PagePath = pagePath;
    }
    /// <summary>
    /// Jump link type, 0 or not filled in means it is not a link, 1 means jump url, 2 means jump applet
    /// </summary>
    [CanBeNull]
    [JsonProperty("type")]
    [JsonPropertyName("type")]
    public byte Type { get; set; }
    /// <summary>
    /// The url of the jump link, required when card_action.type is 1
    /// </summary>
    [CanBeNull]
    [JsonProperty("url")]
    [JsonPropertyName("url")]
    public string? Url { get; set; }
    /// <summary>
    /// The appid of the mini program for the jump link must be the mini program associated with the current application. Required when card_action.type is 2
    /// </summary>
    [CanBeNull]
    [JsonProperty("appid")]
    [JsonPropertyName("appid")]
    public string? AppId { get; set; }
    /// <summary>
    /// The pagepath of the mini program that jumps to the link. Optional when card_action.type is 2.
    /// </summary>
    [CanBeNull]
    [JsonProperty("pagepath")]
    [JsonPropertyName("pagepath")]
    public string? PagePath { get; set; }
}
