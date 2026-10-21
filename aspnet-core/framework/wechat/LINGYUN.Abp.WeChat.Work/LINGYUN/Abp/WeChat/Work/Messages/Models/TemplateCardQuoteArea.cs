using JetBrains.Annotations;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.WeChat.Work.Messages.Models;
/// <summary>
/// Citation style
/// </summary>
public class TemplateCardQuoteArea
{
    [Newtonsoft.Json.JsonConstructor]
    [System.Text.Json.Serialization.JsonConstructor]
    public TemplateCardQuoteArea(string url, string title = "", string text = "")
    {
        Type = 1;
        Url = url;
        Title = title;
        Text = text;
    }
    public TemplateCardQuoteArea(string appid, string pagePath, string title = "", string text = "")
    {
        Type = 2;
        AppId = appid;
        PagePath = pagePath;
        Title = title;
        Text = text;
    }
    /// <summary>
    /// Click event in the cited document style area, 0 or not filled in means there is no click event, 1 means jump url, 2 means jump applet
    /// </summary>
    [CanBeNull]
    [JsonProperty("type")]
    [JsonPropertyName("type")]
    public byte? Type { get; }
    /// <summary>
    /// Click to jump to the url, required when quote_area.type is 1
    /// </summary>
    [CanBeNull]
    [JsonProperty("url")]
    [JsonPropertyName("url")]
    public string? Url { get; set; }
    /// <summary>
    /// The appid of the applet that clicks to jump must be the applet associated with the current application. It is required when quote_area.type is 2.
    /// </summary>
    [CanBeNull]
    [JsonProperty("appid")]
    [JsonPropertyName("appid")]
    public string? AppId { get; set; }
    /// <summary>
    /// Click the pagepath of the mini program to jump. Optional when quote_area.type is 2.
    /// </summary>
    [CanBeNull]
    [JsonProperty("pagepath")]
    [JsonPropertyName("pagepath")]
    public string? PagePath { get; set; }
    /// <summary>
    /// Citation document style title
    /// </summary>
    [CanBeNull]
    [JsonProperty("title")]
    [JsonPropertyName("title")]
    public string? Title { get; set; }
    /// <summary>
    /// Citation copy in citation style
    /// </summary>
    [CanBeNull]
    [JsonProperty("quote_text")]
    [JsonPropertyName("quote_text")]
    public string? Text { get; set; }
}
