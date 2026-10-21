using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.OpenObserve.Models.Search;

/// <summary>
/// Search time-consuming details
/// </summary>
public class SearchTookDetail
{
    /// <summary>
    /// Total time spent (milliseconds)
    /// </summary>
    [JsonPropertyName("total")]
    public long Total { get; set; }

    /// <summary>
    /// Cache time (milliseconds)
    /// </summary>
    [JsonPropertyName("cache_took")]
    public long CacheTook { get; set; }

    /// <summary>
    /// File list takes time (milliseconds)
    /// </summary>
    [JsonPropertyName("file_list_took")]
    public long FileListTook { get; set; }

    /// <summary>
    /// Queuing waiting time (milliseconds)
    /// </summary>
    [JsonPropertyName("wait_in_queue")]
    public long WaitInQueue { get; set; }

    /// <summary>
    /// Indexing time (milliseconds)
    /// </summary>
    [JsonPropertyName("idx_took")]
    public long IdxTook { get; set; }

    /// <summary>
    /// Query time taken (milliseconds)
    /// </summary>
    [JsonPropertyName("search_took")]
    public long SearchTook { get; set; }
}
