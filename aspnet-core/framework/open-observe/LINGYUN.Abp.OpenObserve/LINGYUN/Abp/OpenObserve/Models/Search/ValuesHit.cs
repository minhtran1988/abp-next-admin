using System.Text.Json.Serialization;

namespace LINGYUN.Abp.OpenObserve.Models.Search;

/// <summary>
/// The value of a single field
/// </summary>
public class ValuesHit
{
    /// <summary>
    /// Field name
    /// </summary>
    [JsonPropertyName("field")]
    public string? Field { get; set; }

    /// <summary>
    /// Value list
    /// </summary>
    [JsonPropertyName("values")]
    public ValuesItem[] Values { get; set; } = [];
}
