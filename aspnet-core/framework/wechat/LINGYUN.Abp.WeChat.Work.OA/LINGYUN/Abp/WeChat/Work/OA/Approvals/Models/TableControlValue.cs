using JetBrains.Annotations;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.WeChat.Work.OA.Approvals.Models;
/// <summary>
/// Detailed control value
/// </summary>
public class TableControlValue : ControlValue
{
    /// <summary>
    /// Detail content, a detail control may contain multiple sub-details
    /// </summary>
    [NotNull]
    [JsonProperty("children")]
    [JsonPropertyName("children")]
    public List<TableValue> Children { get; set; } = default!;
    public TableControlValue()
    {

    }

    public TableControlValue(List<TableValue> children)
    {
        Children = children;
    }
}

public class TableValue
{
    /// <summary>
    /// Sub-detail list, fill in the values of all sub-controls of the sub-detail here. The data structure of sub-controls is the same as that of general controls.
    /// </summary>
    /// <remarks>
    /// Note: It cannot be an empty array. It must contain at least one sub-detail. The sub-detail must include all sub-controls set in the template. If the sub-detail is empty, the values of all sub-controls need to be set to empty.
    /// </remarks>
    [NotNull]
    [JsonProperty("list")]
    [JsonPropertyName("list")]
    public List<ControlData> List { get; set; } = default!;
    public TableValue()
    {

    }

    public TableValue(List<ControlData> list)
    {
        List = list;
    }
}