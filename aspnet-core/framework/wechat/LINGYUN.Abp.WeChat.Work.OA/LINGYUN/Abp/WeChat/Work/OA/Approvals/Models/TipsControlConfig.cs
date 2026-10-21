using JetBrains.Annotations;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.WeChat.Work.OA.Approvals.Models;
/// <summary>
/// Description text control configuration
/// </summary>
public class TipsControlConfig : ControlConfig
{
    /// <summary>
    /// Description text control
    /// </summary>
    [NotNull]
    [JsonProperty("tips")]
    [JsonPropertyName("tips")]
    public TipsConfig Tips { get; set; } = default!;
    public TipsControlConfig()
    {

    }

    public TipsControlConfig(TipsConfig tips)
    {
        Tips = tips;
    }
}

public class TipsConfig
{
    /// <summary>
    /// Description array, the elements are rich text descriptions in different languages
    /// </summary>
    [NotNull]
    [JsonProperty("tips_content")]
    [JsonPropertyName("tips_content")]
    public List<TipsContent> TipsContents { get; set; } = default!;
    public TipsConfig()
    {

    }

    public TipsConfig(List<TipsContent> tipsContents)
    {
        TipsContents = tipsContents;
    }
}

public class TipsContent
{
    /// <summary>
    /// Rich text description in a certain language
    /// </summary>
    [NotNull]
    [JsonProperty("text")]
    [JsonPropertyName("text")]
    public TipsContentText Text { get; set; } = default!;
    /// <summary>
    /// Multi-language name
    /// </summary>
    [NotNull]
    [JsonProperty("lang")]
    [JsonPropertyName("lang")]
    public string Lang { get; set; } = default!;
    public TipsContent()
    {

    }

    public TipsContent(TipsContentText text, string lang = "zh_CN")
    {
        Text = text;
        Lang = lang;
    }
}

public class TipsContentText
{
    /// <summary>
    /// Description text segmentation
    /// </summary>
    [NotNull]
    [JsonProperty("sub_text")]
    [JsonPropertyName("sub_text")]
    public List<TipsContentSubText> SubText { get; set; } = default!;
    public TipsContentText()
    {

    }

    public TipsContentText(List<TipsContentSubText> subText)
    {
        SubText = subText;
    }
}

public class TipsContentSubText
{
    /// <summary>
    /// Text type 1: plain text 2: link, each description text only supports one link.
    /// </summary>
    [NotNull]
    [JsonProperty("type")]
    [JsonPropertyName("type")]
    public byte Type { get; set; }
    /// <summary>
    /// Content
    /// </summary>
    [NotNull]
    [JsonProperty("content")]
    [JsonPropertyName("content")]
    public SubTextContent Content { get; set; } = default!;
    public TipsContentSubText()
    {

    }

    private TipsContentSubText(byte type, SubTextContent content)
    {
        Type = type;
        Content = content;
    }

    public static TipsContentSubText Text(TipsContentPlainText content)
    {
        return new TipsContentSubText(1, content);
    }

    public static TipsContentSubText Link(TipsContentLinkText content)
    {
        return new TipsContentSubText(2, content);
    }
}

public abstract class SubTextContent
{
}

public class TipsContentPlainText : SubTextContent
{
    /// <summary>
    /// Plain text text
    /// </summary>
    [NotNull]
    [JsonProperty("content")]
    [JsonPropertyName("content")]
    public string Content { get; set; } = default!;
}

public class TipsContentLinkText : SubTextContent
{
    /// <summary>
    /// Link title
    /// </summary>
    [NotNull]
    [JsonProperty("title")]
    [JsonPropertyName("title")]
    public string Title { get; set; } = default!;
    /// <summary>
    /// Link url, cannot exceed 600 characters
    /// </summary>
    [NotNull]
    [StringLength(600)]
    [JsonProperty("url")]
    [JsonPropertyName("url")]
    public string Url { get; set; } = default!;
}