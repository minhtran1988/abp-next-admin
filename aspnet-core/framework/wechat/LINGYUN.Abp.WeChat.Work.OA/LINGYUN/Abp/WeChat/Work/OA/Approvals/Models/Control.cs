using JetBrains.Annotations;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.WeChat.Work.OA.Approvals.Models;
/// <summary>
/// Template control
/// </summary>
[Newtonsoft.Json.JsonConverter(typeof(ControlNewtonsoftJsonConverter))]
[System.Text.Json.Serialization.JsonConverter(typeof(ControlSystemTextJsonConverter))]
public class Control
{
    /// <summary>
    /// Control properties
    /// </summary>
    [NotNull]
    [JsonProperty("property")]
    [JsonPropertyName("property")]
    public ControlInfo Property { get; set; } = default!;
    /// <summary>
    /// Control configuration
    /// </summary>
    [CanBeNull]
    [JsonProperty("config")]
    [JsonPropertyName("config")]
    public ControlConfig? Config { get; set; }
}
