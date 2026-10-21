using LINGYUN.Abp.OpenObserve.Models;
using LINGYUN.Abp.OpenObserve.Models.Stream;
using LINGYUN.Abp.OpenObserve.Utils;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace LINGYUN.Abp.OpenObserve;

public partial class OpenObserveClient
{
    /// <summary>
    /// List data streams
    /// </summary>
    /// <remarks>
    /// GET /api/{organization}/streams
    /// Reference: https://openobserve.ai/docs/reference/api/stream/list/
    /// </remarks>
    /// <param name="fetchSchema">Whether to return the field definition of each data stream</param>
    /// <param name="type">Data flow type, default logs</param>
    public async virtual Task<StreamListResponse> GetStreamsAsync(
        string? organization = null,
        bool? fetchSchema = null,
        StreamType? type = null,
        CancellationToken cancellationToken = default)
    {
        var queryString = OpenObserveQueryString.Build(
            ("fetchSchema", fetchSchema),
            ("type", type?.AsString()));

        var requestUri = $"/api/{ResolveOrganization(organization)}/streams{queryString}";

        return (await GetAsync<StreamListResponse>(requestUri, cancellationToken: cancellationToken))!;
    }

    /// <summary>
    /// Get the data stream field definition
    /// </summary>
    /// <remarks>
    /// GET /api/{organization}/streams/{stream}/schema
    /// Reference: https://openobserve.ai/docs/reference/api/stream/schema/
    /// </remarks>
    public async virtual Task<StreamInfo> GetStreamSchemaAsync(
        string stream,
        string? organization = null,
        StreamType? type = null,
        CancellationToken cancellationToken = default)
    {
        var queryString = OpenObserveQueryString.Build(("type", type?.AsString()));

        var requestUri = $"/api/{ResolveOrganization(organization)}/streams/{Uri.EscapeDataString(stream)}/schema{queryString}";

        return (await GetAsync<StreamInfo>(requestUri, cancellationToken: cancellationToken))!;
    }

    /// <summary>
    /// Create data flow settings
    /// </summary>
    /// <remarks>
    /// POST /api/{org_id}/streams/{stream_name}/settings
    /// Reference: https://openobserve.ai/docs/reference/api/stream/setting/
    /// </remarks>
    /// <param name="partitionKeys">Partition fields</param>
    /// <param name="indexFields">Secondary index fields</param>
    /// <param name="fullTextSearchKeys">Full text search field</param>
    /// <param name="bloomFilterFields">Bloom filter fields</param>
    /// <param name="dataRetention">Data retention days, minimum 3 days</param>
    /// <param name="flattenLevel">Flatten level</param>
    /// <param name="definedSchemaFields">Reserved custom schema fields</param>
    /// <param name="maxQueryRange">Maximum time range for a single query (hours)</param>
    /// <param name="storeOriginalData">Whether to store original data</param>
    /// <param name="approxPartition">Whether to use equal time range partitioning</param>
    /// <param name="extendedRetentionDays">Extended retention period time range</param>
    /// <param name="indexOriginalData">Whether to establish a full-text index for the original log volume</param>
    /// <param name="indexAllValues">Whether to create exact matching indexes for all fields</param>
    /// <param name="storageType">Storage type: normal (default) / compliance</param>
    public async virtual Task<OpenObserveCodeMessageResponse> CreateStreamSettingsAsync(
        string stream,
        string[]? partitionKeys = null,
        string[]? indexFields = null,
        string[]? fullTextSearchKeys = null,
        string[]? bloomFilterFields = null,
        int? dataRetention = null,
        int? flattenLevel = null,
        string[]? definedSchemaFields = null,
        int? maxQueryRange = null,
        bool? storeOriginalData = null,
        bool? approxPartition = null,
        object[]? extendedRetentionDays = null,
        bool? indexOriginalData = null,
        bool? indexAllValues = null,
        string? storageType = null,
        string? organization = null,
        CancellationToken cancellationToken = default)
    {
        var requestUri = $"/api/{ResolveOrganization(organization)}/streams/{Uri.EscapeDataString(stream)}/settings";

        var request = new StreamSettingRequest
        {
            PartitionKeys = partitionKeys,
            IndexFields = indexFields,
            FullTextSearchKeys = fullTextSearchKeys,
            BloomFilterFields = bloomFilterFields,
            DataRetention = dataRetention,
            FlattenLevel = flattenLevel,
            DefinedSchemaFields = definedSchemaFields,
            MaxQueryRange = maxQueryRange,
            StoreOriginalData = storeOriginalData,
            ApproxPartition = approxPartition,
            ExtendedRetentionDays = extendedRetentionDays,
            IndexOriginalData = indexOriginalData,
            IndexAllValues = indexAllValues,
            StorageType = storageType,
        };

        return (await PostJsonAsync<StreamSettingRequest, OpenObserveCodeMessageResponse>(
            requestUri,
            request,
            cancellationToken: cancellationToken))!;
    }

    /// <summary>
    /// Update data flow settings
    /// </summary>
    /// <remarks>
    /// PUT /api/{org_id}/streams/{stream_name}/settings
    /// Fields that support add/set/remove are passed using <see cref="UpdateStreamSettingValue{T}"/>
    /// Reference: https://openobserve.ai/docs/reference/api/stream/setting/
    /// </remarks>
    public async virtual Task<OpenObserveCodeMessageResponse> UpdateStreamSettingsAsync(
        string stream,
        UpdateStreamSettingValue<string>? partitionKeys = null,
        UpdateStreamSettingValue<string>? indexFields = null,
        UpdateStreamSettingValue<string>? fullTextSearchKeys = null,
        UpdateStreamSettingValue<string>? bloomFilterFields = null,
        UpdateStreamSettingValue<string>? definedSchemaFields = null,
        UpdateStreamSettingValue<object>? extendedRetentionDays = null,
        int? dataRetention = null,
        int? flattenLevel = null,
        int? maxQueryRange = null,
        bool? storeOriginalData = null,
        bool? approxPartition = null,
        bool? indexOriginalData = null,
        bool? indexAllValues = null,
        string? storageType = null,
        string? organization = null,
        CancellationToken cancellationToken = default)
    {
        var requestUri = $"/api/{ResolveOrganization(organization)}/streams/{Uri.EscapeDataString(stream)}/settings";

        var request = new UpdateStreamSettingRequest
        {
            PartitionKeys = partitionKeys,
            IndexFields = indexFields,
            FullTextSearchKeys = fullTextSearchKeys,
            BloomFilterFields = bloomFilterFields,
            DefinedSchemaFields = definedSchemaFields,
            ExtendedRetentionDays = extendedRetentionDays,
            DataRetention = dataRetention,
            FlattenLevel = flattenLevel,
            MaxQueryRange = maxQueryRange,
            StoreOriginalData = storeOriginalData,
            ApproxPartition = approxPartition,
            IndexOriginalData = indexOriginalData,
            IndexAllValues = indexAllValues,
            StorageType = storageType,
        };

        return (await PutJsonAsync<UpdateStreamSettingRequest, OpenObserveCodeMessageResponse>(
            requestUri,
            request,
            cancellationToken: cancellationToken))!;
    }

    /// <summary>
    /// Delete data stream
    /// </summary>
    /// <remarks>
    /// DELETE /api/{org_id}/streams/{stream_name}?type=logs&amp;delete_all=true
    /// Deletion is asynchronous (Compactor polls every 10 minutes) and is not recoverable
    /// Reference: https://openobserve.ai/docs/reference/api/stream/delete/
    /// </remarks>
    /// <param name="deleteAll">Whether to delete associated alarms and dashboards at the same time</param>
    public async virtual Task<OpenObserveCodeMessageResponse> DeleteStreamAsync(
        string stream,
        StreamType type = StreamType.Logs,
        bool deleteAll = false,
        string? organization = null,
        CancellationToken cancellationToken = default)
    {
        var queryString = OpenObserveQueryString.Build(
            ("type", type.AsString()),
            ("delete_all", deleteAll));

        var requestUri = $"/api/{ResolveOrganization(organization)}/streams/{Uri.EscapeDataString(stream)}{queryString}";

        return (await DeleteAsync<OpenObserveCodeMessageResponse>(requestUri, cancellationToken: cancellationToken))!;
    }

    /// <summary>
    /// Delete data by time range
    /// </summary>
    /// <remarks>
    /// DELETE /api/{org_id}/streams/{stream_name}/data_by_time_range?start=..&amp;end=..
    /// Logs are deleted by the hour and links are deleted by the day.
    /// </remarks>
    /// <param name="startTime">Start time (inclusive)</param>
    /// <param name="endTime">End time (inclusive)</param>
    public async virtual Task<StreamDeleteJobResponse> DeleteStreamDataByTimeRangeAsync(
        string stream,
        DateTime startTime,
        DateTime endTime,
        string? organization = null,
        CancellationToken cancellationToken = default)
    {
        var queryString = OpenObserveQueryString.Build(
            ("start", startTime.ToMicroseconds()),
            ("end", endTime.ToMicroseconds()));

        var requestUri = $"/api/{ResolveOrganization(organization)}/streams/{Uri.EscapeDataString(stream)}/data_by_time_range{queryString}";

        return (await DeleteAsync<StreamDeleteJobResponse>(requestUri, cancellationToken: cancellationToken))!;
    }

    /// <summary>
    /// Query the task status of deleting data by time range
    /// </summary>
    /// <remarks>
    /// GET /api/{org_id}/streams/{stream_name}/data_by_time_range/status/{id}
    /// </remarks>
    public async virtual Task<StreamDeleteJobStatusResponse> GetStreamDataByTimeRangeStatusAsync(
        string stream,
        string id,
        string? organization = null,
        CancellationToken cancellationToken = default)
    {
        var requestUri = $"/api/{ResolveOrganization(organization)}/streams/{Uri.EscapeDataString(stream)}/data_by_time_range/status/{Uri.EscapeDataString(id)}";

        return (await GetAsync<StreamDeleteJobStatusResponse>(requestUri, cancellationToken: cancellationToken))!;
    }

    /// <summary>
    /// Delete the data flow result cache
    /// </summary>
    /// <remarks>
    /// DELETE /api/{org_id}/streams/{stream_name}/cache/results?type=..&amp;ts=..
    /// Delete the cache before ts, keep the cache after (inclusive) ts
    /// </remarks>
    /// <param name="stream">Data stream name, delete the cache of all data streams when _all is passed in</param>
    /// <param name="timestamp">Time threshold</param>
    public async virtual Task<OpenObserveCodeMessageResponse> DeleteStreamCacheAsync(
        string stream,
        StreamType type,
        DateTime timestamp,
        string? organization = null,
        CancellationToken cancellationToken = default)
    {
        var queryString = OpenObserveQueryString.Build(
            ("type", type.AsString()),
            ("ts", timestamp.ToMicroseconds()));

        var requestUri = $"/api/{ResolveOrganization(organization)}/streams/{Uri.EscapeDataString(stream)}/cache/results{queryString}";

        return (await DeleteAsync<OpenObserveCodeMessageResponse>(requestUri, cancellationToken: cancellationToken))!;
    }
}
