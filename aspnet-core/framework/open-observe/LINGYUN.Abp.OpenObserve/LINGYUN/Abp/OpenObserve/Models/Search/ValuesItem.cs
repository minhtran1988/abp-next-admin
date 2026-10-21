using System.Text.Json.Serialization;

namespace LINGYUN.Abp.OpenObserve.Models.Search;

/// <summary>
/// Value item
/// </summary>
public class ValuesItem
{
    /// <summary>
    /// Get value
    /// </summary>
    [JsonPropertyName("zo_sql_key")]
    public string? Key { get; set; }

    /// <summary>
    /// Number of occurrences
    /// </summary>
    [JsonPropertyName("zo_sql_num")]
    public long Num { get; set; }
}
