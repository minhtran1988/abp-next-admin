using System.Text.Json.Serialization;

namespace LINGYUN.Abp.OpenObserve.Models.Search;

/// <summary>
/// Field value response
/// </summary>
/// <remarks>
/// GET /api/{organization}/{stream}/_values
/// Reference: https://openobserve.ai/docs/reference/api/search/
/// </remarks>
public class ValuesResponse
{
    [JsonPropertyName("took")]
    public long Took { get; set; }

    /// <summary>
    /// Time-consuming details
    /// </summary>
    [JsonPropertyName("took_detail")]
    public SearchTookDetail? TookDetail { get; set; }

    [JsonPropertyName("hits")]
    public ValuesHit[] Hits { get; set; } = [];

    [JsonPropertyName("total")]
    public long Total { get; set; }

    [JsonPropertyName("from")]
    public long From { get; set; }

    [JsonPropertyName("size")]
    public long Size { get; set; }

    [JsonPropertyName("scan_size")]
    public long ScanSize { get; set; }
}
