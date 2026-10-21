using JetBrains.Annotations;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.WeChat.Work.OA.Approvals.Models;
/// <summary>
/// Select control configuration
/// </summary>
public class SelectorControlConfig : ControlConfig
{
    /// <summary>
    /// Select the content of the control, that is, the option content selected by the applicant in this control
    /// </summary>
    [NotNull]
    [JsonProperty("selector")]
    [JsonPropertyName("selector")]
    public SelectorConfig Selector { get; set; } = default!;
    public SelectorControlConfig()
    {

    }

    public SelectorControlConfig(SelectorConfig selector)
    {
        Selector = selector;
    }
}

public class SelectorConfig
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
    [CanBeNull]
    [JsonProperty("options")]
    [JsonPropertyName("options")]
    public List<SelectorOption>? Options {  get; set; }
    /// <summary>
    /// Associated controls
    /// </summary>
    [CanBeNull]
    [JsonProperty("op_relations")]
    [JsonPropertyName("op_relations")]
    public List<SelectorOptionRelation>? OptionRelations { get; set; }
    /// <summary>
    /// Associated external options
    /// </summary>
    [CanBeNull]
    [JsonProperty("external_option")]
    [JsonPropertyName("external_option")]
    public SelectorOptionExternal? ExternalOption { get; set; }
    public SelectorConfig()
    {

    }

    private SelectorConfig(
        string type,
        List<SelectorOption> options,
        List<SelectorOptionRelation>? optionRelations = null,
        SelectorOptionExternal? optionExternal = null)
    {
        Type = type;
        Options = options;
        OptionRelations = optionRelations;
        ExternalOption = optionExternal;
    }

    public static SelectorConfig Single(
        List<SelectorOption> options,
        List<SelectorOptionRelation>? optionRelations = null, 
        SelectorOptionExternal? optionExternal = null)
    {
        return new SelectorConfig("single", options, optionRelations, optionExternal);
    }

    public static SelectorConfig Multiple(
        List<SelectorOption> options,
        List<SelectorOptionRelation>? optionRelations = null,
        SelectorOptionExternal? optionExternal = null)
    {
        return new SelectorConfig("multi", options, optionRelations, optionExternal);
    }
}

public class SelectorOption
{
    /// <summary>
    /// option key
    /// </summary>
    [NotNull]
    [JsonProperty("key")]
    [JsonPropertyName("key")]
    public string Key { get; set; } = default!;
    /// <summary>
    /// Option description
    /// </summary>
    [NotNull]
    [JsonProperty("value")]
    [JsonPropertyName("value")]
    public List<SelectorOptionValue> Value { get; set; } = default!;
    public SelectorOption()
    {

    }

    public SelectorOption(string key, List<SelectorOptionValue> value)
    {
        Key = key;
        Value = value;
    }
}

public class SelectorOptionValue
{
    /// <summary>
    /// Name
    /// </summary>
    [NotNull]
    [JsonProperty("text")]
    [JsonPropertyName("text")]
    public string Text { get; set; } = default!;
    /// <summary>
    /// Display language
    /// </summary>
    [NotNull]
    [JsonProperty("lang")]
    [JsonPropertyName("lang")]
    public string Lang { get; set; } = default!;
    public SelectorOptionValue()
    {

    }

    public SelectorOptionValue(string text, string lang = "zh_CN")
    {
        Text = text;
        Lang = lang;
    }
}

public class SelectorOptionRelation
{
    /// <summary>
    /// option key
    /// </summary>
    [NotNull]
    [JsonProperty("key")]
    [JsonPropertyName("key")]
    public string Key { get; set; } = default!;
    /// <summary>
    /// List of associated controls
    /// </summary>
    [NotNull]
    [JsonProperty("relation_list")]
    [JsonPropertyName("relation_list")]
    public List<SelectorRelation> Relations { get; set; } = default!;
    public SelectorOptionRelation()
    {

    }

    public SelectorOptionRelation(string key, List<SelectorRelation> relations)
    {
        Key = key;
        Relations = relations;
    }
}

public class SelectorRelation
{
    /// <summary>
    /// ControlId
    /// </summary>
    [NotNull]
    [JsonProperty("related_control_id")]
    [JsonPropertyName("related_control_id")]
    public string ControlId { get; set; } = default!;
    /// <summary>
    /// Operation method
    /// </summary>
    /// <remarks>
    /// 1 - Display the corresponding control
    /// </remarks>
    [NotNull]
    [JsonProperty("action")]
    [JsonPropertyName("action")]
    public int Action { get; set; } = default!;
    public SelectorRelation()
    {

    }

    public SelectorRelation(string controlId, int action = 1)
    {
        ControlId = controlId;
        Action = action;
    }
}

public class SelectorOptionExternal
{
    /// <summary>
    /// Associated external options
    /// </summary>
    [NotNull]
    [JsonProperty("use_external_option")]
    [JsonPropertyName("use_external_option")]
    public bool UseExternalOption { get; set; }
    /// <summary>
    /// External options page address
    /// </summary>
    [NotNull]
    [JsonProperty("external_url")]
    [JsonPropertyName("external_url")]
    public string ExternalUrl { get; set; } = default!;
    public SelectorOptionExternal()
    {

    }

    public SelectorOptionExternal(string externalUrl)
    {
        ExternalUrl = externalUrl;
        UseExternalOption = true;
    }
}