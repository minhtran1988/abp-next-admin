using Newtonsoft.Json;
using System;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.WeChat.Official.Services.Models;
/// <summary>
/// Send text message
/// </summary>
[Serializable]
public class TextMessageModel : MessageModel
{
    /// <summary>
    /// Receive message user
    /// </summary>
    [JsonProperty("touser")]
    [JsonPropertyName("touser")]
    public string ToUser { get; }
    /// <summary>
    /// Message content
    /// </summary>
    [JsonProperty("text")]
    [JsonPropertyName("text")]
    public TextMessage Text { get; }
    public TextMessageModel(string toUser, TextMessage text)
        : base("text")
    {
        ToUser = toUser;
        Text = text;
    }
}

public class TextMessage
{
    /// <summary>
    /// Content text
    /// </summary>
    [JsonProperty("content")]
    [JsonPropertyName("content")]
    public string Content { get; set; }
    public TextMessage(string content)
    {
        Content = content;
    }
}
