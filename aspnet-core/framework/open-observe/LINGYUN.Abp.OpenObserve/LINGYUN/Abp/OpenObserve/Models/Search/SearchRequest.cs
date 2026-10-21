using System.Text.Json.Serialization;

namespace LINGYUN.Abp.OpenObserve.Models.Search;

/// <summary>
/// Search request
/// </summary>
public class SearchRequest
{
    /// <summary>
    /// Query conditions
    /// </summary>
    [JsonPropertyName("query")]
    public SearchQuery Query { get; }

    /// <summary>
    /// Search source
    /// </summary>
    /// <remarks>
    /// Supports ui / dashboards / reports / alerts, etc., the value in the request body takes precedence over the query string
    /// </remarks>
    [JsonPropertyName("search_type")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? SearchTypeString { get; set; }

    /// <summary>
    /// Timeout (seconds)
    /// </summary>
    /// <remarks>
    /// The default value is determined by the server ZO_QUERY_TIMEOUT=600
    /// </remarks>
    [JsonPropertyName("timeout")]
    public int? Timeout { get; }

    /// <summary>
    /// agent/MCP client options
    /// </summary>
    [JsonPropertyName("agent_options")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public SearchAgentOptions? AgentOptions { get; }

    /// <summary>
    /// SQL encoding method
    /// </summary>
    /// <remarks>
    /// It is empty or base64. When it is base64, query.sql needs URL Base64 encoding, and the server decodes it and sets it empty.
    /// </remarks>
    [JsonPropertyName("encoding")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Encoding { get; set; }

    /// <summary>
    /// Query area
    /// </summary>
    /// <remarks>
    /// Query all areas by default, local means only the local area
    /// </remarks>
    [JsonPropertyName("regions")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string[]? Regions { get; set; }

    /// <summary>
    /// Query cluster
    /// </summary>
    /// <remarks>
    /// local means only the local cluster
    /// </remarks>
    [JsonPropertyName("clusters")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string[]? Clusters { get; set; }

    /// <summary>
    /// Whether to use cache
    /// </summary>
    /// <remarks>
    /// use_cache in the query string will overwrite this value
    /// </remarks>
    [JsonPropertyName("use_cache")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? UseCache { get; set; }

    /// <summary>
    /// Whether to clear cache
    /// </summary>
    /// <remarks>
    /// clear_cache in the query string will overwrite this value
    /// </remarks>
    [JsonPropertyName("clear_cache")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? ClearCache { get; set; }

    public SearchRequest(
        SearchQuery query,
        SearchType? searchType = null,
        int? timeout = OpenObserveConstants.ZO_QUERY_TIMEOUT,
        SearchAgentOptions? agentOptions = null)
    {
        Query = query;
        Timeout = timeout;
        AgentOptions = agentOptions;
        SearchTypeString = searchType?.AsString();
    }
}
