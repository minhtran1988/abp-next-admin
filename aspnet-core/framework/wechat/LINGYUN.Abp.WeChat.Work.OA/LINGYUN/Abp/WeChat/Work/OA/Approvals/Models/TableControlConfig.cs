using JetBrains.Annotations;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.WeChat.Work.OA.Approvals.Models;
/// <summary>
/// Detailed control configuration
/// </summary>
public class TableControlConfig : ControlConfig
{
    /// <summary>
    /// Details control
    /// </summary>
    [NotNull]
    [JsonProperty("table")]
    [JsonPropertyName("table")]
    public TableConfig Table { get; set; } = default!;
    public TableControlConfig()
    {

    }

    public TableControlConfig(TableConfig table)
    {
        Table = table;
    }
}

public class TableConfig
{
    /// <summary>
    /// Printing format; 0 - merge into one line for printing, 1 - split into multiple lines for printing
    /// </summary>
    [NotNull]
    [JsonProperty("print_format")]
    [JsonPropertyName("print_format")]
    public byte PrintFormat { get; set; }
    /// <summary>
    /// Detail content, a detail control may contain multiple sub-controls, each sub-control has the same properties as the control
    /// </summary>
    [NotNull]
    [JsonProperty("children")]
    [JsonPropertyName("children")]
    public List<Control> Children { get; set; } = default!;
    public TableConfig()
    {

    }

    public TableConfig(byte printFormat, List<Control> children)
    {
        PrintFormat = printFormat;
        Children = children;
    }
}