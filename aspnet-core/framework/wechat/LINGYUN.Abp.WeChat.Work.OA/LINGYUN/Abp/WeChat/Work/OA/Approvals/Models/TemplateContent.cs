using JetBrains.Annotations;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.WeChat.Work.OA.Approvals.Models;
/// <summary>
/// Template control definition
/// </summary>
public class TemplateContent
{
    /// <summary>
    /// Control array, multiple control types can be set in the template, and the order is the same as that displayed on the management side
    /// </summary>
    [NotNull]
    [JsonProperty("controls")]
    [JsonPropertyName("controls")]
    public List<Control> Controls { get; set; } = default!;
}
