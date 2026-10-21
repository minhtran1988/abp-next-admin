using JetBrains.Annotations;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.WeChat.Work.OA.Approvals.Models;
/// <summary>
/// Radio/multi-select control value
/// </summary>
public class SelectorControlValue : ControlValue
{
    /// <summary>
    /// Single selection/multiple selection content
    /// </summary>
    [NotNull]
    [JsonProperty("selector")]
    [JsonPropertyName("selector")]
    public SelectorValue Selector { get; set; } = default!;
    public SelectorControlValue()
    {

    }

    public SelectorControlValue(SelectorValue selector)
    {
        Selector = selector;
    }
}

public class SelectorValue
{
    /// <summary>
    /// Selection method: single-radio selection; multi-multiple selection
    /// </summary>
    [NotNull]
    [JsonProperty("type")]
    [JsonPropertyName("type")]
    public string Type { get; set; } = default!;
    /// <summary>
    /// Multi-select option, the selection control of multi-select attribute allows multiple input
    /// </summary>
    [NotNull]
    [JsonProperty("options")]
    [JsonPropertyName("options")]
    public List<SelectorValueOption> Options { get; set; } = default!;
    public SelectorValue()
    {

    }

    private SelectorValue(string type, List<SelectorValueOption> options)
    {
        Type = type;
        Options = options;
    }
    /// <summary>
    /// Create a radio option
    /// </summary>
    /// <param name="option"></param>
    /// <returns></returns>
    public static SelectorValue Single(SelectorValueOption option)
    {
        return new SelectorValue("single", new List<SelectorValueOption> { option });
    }

    /// <summary>
    /// Create a multi-select option
    /// </summary>
    /// <param name="options"></param>
    /// <returns></returns>
    public static SelectorValue Multiple(List<SelectorValueOption> options)
    {
        return new SelectorValue("multi", options);
    }
}

public class SelectorValueOption
{
    /// <summary>
    /// Option key can be obtained through the "Get Approval Template Details" interface
    /// </summary>
    [NotNull]
    [JsonProperty("key")]
    [JsonPropertyName("key")]
    public string Key { get; set; } = default!;
    /// <summary>
    /// Option value, if multiple languages are configured, it will include Chinese and English option values.
    /// </summary>
    [NotNull]
    [JsonProperty("value")]
    [JsonPropertyName("value")]
    public List<SelectorValueOptionValue> Value { get; set; } = default!;
    public SelectorValueOption()
    {

    }

    public SelectorValueOption(string key, List<SelectorValueOptionValue> value)
    {
        Key = key;
        Value = value;
    }
}

public class SelectorValueOptionValue
{
    /// <summary>
    /// option value
    /// </summary>
    [NotNull]
    [StringLength(40)]
    [JsonProperty("text")]
    [JsonPropertyName("text")]
    public string Text { get; set; } = default!;
    /// <summary>
    /// Multi-language name
    /// </summary>
    [NotNull]
    [JsonProperty("lang")]
    [JsonPropertyName("lang")]
    public string Lang { get; set; } = default!;
    public SelectorValueOptionValue()
    {

    }

    public SelectorValueOptionValue(string text, string lang = "zh_CN")
    {
        Text = text;
        Lang = lang;
    }
}