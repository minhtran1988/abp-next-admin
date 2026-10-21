namespace LINGYUN.Abp.OpenObserve.Models;

public static class OpenObserveConstants
{
    /// <summary>
    /// Search interface default timeout (seconds)
    /// </summary>
    public const int ZO_QUERY_TIMEOUT = 30;
    /// <summary>
    /// Default endpoint
    /// </summary>
    public const string DefaultEndpoint = "http://localhost:5080";
    /// <summary>
    /// Default request timeout
    /// </summary>
    public const int DefaultTimeoutSeconds = 30;
    /// <summary>
    /// Default organization name
    /// </summary>
    public const string DefaultOrganization = "default";
    /// <summary>
    /// Metadata organization name
    /// </summary>
    /// <remarks>
    /// Interfaces such as cluster information are only open to the _meta organization
    /// </remarks>
    public const string MetaOrganization = "_meta";
    /// <summary>
    /// JSON content type
    /// </summary>
    public const string JsonContentType = "application/json";
    /// <summary>
    /// NDJSON content type
    /// </summary>
    /// <remarks>
    /// For _bulk and _multi interfaces (one JSON object per line)
    /// </remarks>
    public const string NdJsonContentType = "application/x-ndjson";

    /// <summary>
    /// Data stream type
    /// </summary>
    public static class StreamTypes
    {
        public const string Logs = "logs";
        public const string Metrics = "metrics";
        public const string Traces = "traces";
    }

    /// <summary>
    /// Actions supported by _bulk
    /// </summary>
    /// <remarks>
    /// delete is not supported
    /// </remarks>
    public static class BulkActions
    {
        public const string Index = "index";
        public const string Create = "create";
        public const string Update = "update";
    }

    /// <summary>
    /// Default folder
    /// </summary>
    public const string DefaultFolder = "default";
}
