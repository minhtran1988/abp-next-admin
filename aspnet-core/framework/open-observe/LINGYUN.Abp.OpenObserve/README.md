# LINGYUN.Abp.OpenObserve

Simplified Chinese | [English](./README.EN.md)

OpenObserve HTTP API client package, covering all interfaces in the official API reference document

Reference documentation: https://openobserve.ai/docs/reference/api/

## Module reference

```csharp
[DependsOn(typeof(AbpOpenObserveModule))]
public class YouProjectModule : AbpModule
{
  // other
}
```

## Configuration items

| Configuration items | Description | Default value |
| --- | --- | --- |
| `Endpoint` | Service address | `http://localhost:5080` |
| `Organization` | The default organization name, used when the calling interface does not explicitly specify an organization | `default` |
| `UserName` | Username (together with Password to construct the Basic authentication header) | - |
| `Password` | Password | - |
| `AccessToken` | Complete authorization header, such as `Basic xxx` or `Bearer xxx` (service account token), takes precedence over UserName/Password | - |
| `TimeoutSeconds` | Request timeout (seconds) | `600` |

> All interfaces require authorization headers, and the official recommendation is `Authorization: Basic base64(email:password)`.
> Note: **Ingestion token can only be used for ingestion interfaces**, and cannot be used for APIs such as `_meta` / `_search` / `/mcp`, otherwise `401 Unauthorized` will be returned.
> `TimeoutSeconds` defaults to 600 seconds, consistent with the `ZO_QUERY_TIMEOUT` of the search interface, to avoid long queries being interrupted by HttpClient.

## appsettings.json

```json
{
  "OpenObserve": {
    "Endpoint": "http://localhost:5080",
    "Organization": "default",
    "UserName": "admin@abp.io",
    "Password": "your-password",
    "TimeoutSeconds": 600
  }
}
```

## Interface list

### Stream

| Method | HTTP | Path |
| --- | --- | --- |
| `GetStreamsAsync` | GET | `/api/{organization}/streams` |
| `GetStreamSchemaAsync` | GET | `/api/{organization}/streams/{stream}/schema` |
| `CreateStreamSettingsAsync` | POST | `/api/{org_id}/streams/{stream_name}/settings` |
| `UpdateStreamSettingsAsync` | PUT | `/api/{org_id}/streams/{stream_name}/settings` |
| `DeleteStreamAsync` | DELETE | `/api/{org_id}/streams/{stream_name}` |
| `DeleteStreamDataByTimeRangeAsync` | DELETE | `/api/{org_id}/streams/{stream_name}/data_by_time_range` |
| `GetStreamDataByTimeRangeStatusAsync` | GET | `/api/{org_id}/streams/{stream_name}/data_by_time_range/status/{id}` |
| `DeleteStreamCacheAsync` | DELETE | `/api/{org_id}/streams/{stream_name}/cache/results` |

### Ingestion

| Method | HTTP | Path | Description |
| --- | --- | --- | --- |
| `JsonRequestAsync` | POST | `/api/{organization}/{stream}/_json` | JSON array |
| `JsonRecordAsync` | POST | `/api/{organization}/{stream}/_json` | Single record (internally packaged as an array) |
| `JsonBulkAsync` | POST | `/api/{organization}/_bulk` | NDJSON, action row + record row, compatible with Elasticsearch _bulk |
| `JsonMultiAsync` | POST | `/api/{organization}/{stream}/_multi` | NDJSON, one record per line |

> `_bulk` supports `index` / `create` / `update` three actions, but does not support `delete`.
> `_timestamp` (microseconds, or RFC 3339/RFC 2822 string) in the record can override the default ingestion time; if not passed, the current time is used.
> `status[].error` in the ingestion response will give the failure reason (such as `flatten value must be an object`), which can be read through `JsonStatus.Error`.

### Search

| Method | HTTP | Path |
| --- | --- | --- |
| `SearchAsync` | POST | `/api/{organization}/_search` |
| `AroundAsync` | GET | `/api/{organization}/{stream}/_around` |
| `GetValuesAsync` | GET | `/api/{organization}/{stream}/_values` |

> The time range unit is **microseconds**, `start_time` / `end_time` must be provided, otherwise it will be scanned in full.
> Search error codes (20001-20013) and `hint` / `suggestions` are carried by `OpenObserveSearchException`.

### Function

| Method | HTTP | Path |
| --- | --- | --- |
| `CreateFunctionAsync` | POST | `/api/{organization}/functions` |
| `UpdateFunctionAsync` | PUT | `/api/{organization}/functions/{name}` |
| `DeleteFunctionAsync` | DELETE | `/api/{organization}/functions/{name}` |
| `GetFunctionsAsync` | GET | `/api/{organization}/functions` |
| `TestFunctionAsync` | POST | `/api/{org_id}/functions/test` |

> The function language is determined by the request body `transType` (`FunctionTransType.Vrl` / `JavaScript`), there is no `type` query parameter.
> `TestFunctionAsync` does not appear in the API documentation, coming from the implementation source code.

### User

| Method | HTTP | Path |
| --- | --- | --- |
| `CreateUserAsync` | POST | `/api/{organization}/users` |
| `UpdateUserAsync` | PUT | `/api/{organization}/users/{user_email}` |
| `AddUserToOrganizationAsync` | POST | `/api/{organization}/users/{user_email}` |
| `RemoveUserFromOrganizationAsync` | DELETE | `/api/{organization}/users/{user_email}` |
| `GetUsersAsync` | GET | `/api/{organization}/users` |

### Metrics and Cluster

| Method | HTTP | Path |
| --- | --- | --- |
| `GetMetricsAsync` | GET | `/metrics` (Prometheus text format, requires `ZO_PROMETHEUS_ENABLE=true`) |
| `GetClusterInfoAsync` | GET | `/api/{org_id}/cluster/info` |

### Dashboard

| Method | HTTP | Path |
| --- | --- | --- |
| `AddDashboardPanelAsync` | POST | `/api/{org_id}/dashboards/{dashboard_id}/panels` |
| `UpdateDashboardPanelAsync` | PUT | `/api/{org_id}/dashboards/{dashboard_id}/panels/{panel_id}` |
| `DeleteDashboardPanelAsync` | DELETE | `/api/{org_id}/dashboards/{dashboard_id}/panels/{panel_id}` |

> Panel operation only supports v8 dashboard, and the `hash` parameter is required (optimistic concurrency control), and 409 will be returned if there is no match.

### Report

| Method | HTTP | Path |
| --- | --- | --- |
| `GetReportsAsync` | GET | `/api/v2/{org_id}/reports` |
| `CreateReportAsync` | POST | `/api/v2/{org_id}/reports` |
| `GetReportAsync` | GET | `/api/v2/{org_id}/reports/{report_id}` |
| `UpdateReportAsync` | PUT | `/api/v2/{org_id}/reports/{report_id}` |
| `DeleteReportAsync` | DELETE | `/api/v2/{org_id}/reports/{report_id}` |
| `BulkDeleteReportsAsync` | DELETE | `/api/v2/{org_id}/reports/bulk` |
| `MoveReportsAsync` | PATCH | `/api/v2/{org_id}/reports/move` |
| `EnableReportAsync` | PATCH | `/api/v2/{org_id}/reports/{report_id}/enable` |
| `TriggerReportAsync` | PUT | `/api/v2/{org_id}/reports/{report_id}/trigger` |

> The report interface uses the `/api/v2` prefix, which is different from other interfaces.

## Calling style

* **Just pass the business parameters**, no need to assemble the `*Request` model yourself (the request body is assembled internally by the client), for example:

```csharp
var client = ServiceProvider.GetRequiredService<OpenObserveClient>();

// Create new user
await client.CreateUserAsync("user@abp.io", "complex#pass", firstName: "ming", role: "admin");

//Write a piece of audit data
await client.JsonRecordAsync("default", "audit-log", new { action = "Login", user = "admin" });

// search
var result = await client.SearchAsync<JsonNode>(
    new SearchQuery("SELECT * FROM audit-log", DateTime.UtcNow.AddHours(-1), DateTime.UtcNow, size: 20));
```

* All serialization/deserialization is completed through **`IOpenObserveSerializer`**, and the default implementation is `DefaultOpenObserveSerializer` based on ABP's `IJsonSerializer`:
  * Follow the `AbpSystemTextJsonSerializerOptions` of the host configuration (converter, date format, encoder, etc.); ABP sets the encoder to `UnsafeRelaxedJsonEscaping` by default, and Chinese and quotation marks will not be escaped as `\uXXXX`;
  * The field name is based on the `JsonPropertyName` on the model (note that the `camelCase` default value of `IJsonSerializer.Serialize` only affects properties without annotated attributes);
  * OpenObserve's unique "ignore null fields during serialization" is implemented through ABP's **type-level serialization decorator**
    (`AbpSystemTextJsonSerializerModifiersOptions.Modifiers`: `List<Action<JsonTypeInfo>>` → `OpenObserveIgnoreNullPropertiesModifier`),
    Only applies to `LINGYUN.Abp.OpenObserve.Models` namespace: `null` fields will not be generated when optional parameters are not passed (such as user password fields, the document requires "do not pass values if they are not modified");
  * Note: `IJsonSerializer` interface (ABP 10.x) only has `Serialize(object, camelCase, indented)` / `Deserialize<T>(string, camelCase)` /
    `Deserialize(Type, string, camelCase)`, **Does not accept customization `JsonSerializerOptions`**; The above modifiers should be used when you need to customize serialization by type, for example:

```csharp
Configure<AbpSystemTextJsonSerializerModifiersOptions>(options =>
{
    options.Modifiers.Add(jsonTypeInfo =>
    {
        if (jsonTypeInfo.Type == typeof(MyType))
        {
            foreach (var property in jsonTypeInfo.Properties)
            {
                property.ShouldSerialize = (_, value) => value != null;
            }
        }
    });
});
```

* If you need to completely replace the serialization method, implement `IOpenObserveSerializer` and add `[Dependency(ReplaceServices = true)]` to override the default implementation.
* The organization name parameter can be omitted, and the default value is `AbpOpenObserveOptions.Organization`; the cluster information interface uses the `_meta` organization by default (server limit).
* The data model is organized into **one file per type**, and the directory structure is consistent with `Models/Search`.

## Unit testing

Test project: `aspnet-core/tests/LINGYUN.Abp.OpenObserve.Tests` (joined `LINGYUN.MicroService.All` solution)

```bash
dotnet test aspnet-core/tests/LINGYUN.Abp.OpenObserve.Tests/LINGYUN.Abp.OpenObserve.Tests.csproj
```

The test intercepts requests through `HttpMessageHandler`, verifies the **HTTP method/path/query parameter/request body** of each interface, as well as response deserialization and exception type, without generating real network requests.

##Exception handling

| Exception | Description |
| --- | --- |
| `OpenObserveException` | Business exception base class, carrying `Code` / `StatusCode` / `Error` / `ResponseBody` |
| `OpenObserveRequestException` | The response body cannot be parsed into an OpenObserve standard error structure |
| `OpenObserveSearchException` | Search interface business error (including error code, `hint`, `suggestions`) |
| `OpenObserveIngestionException` | Ingestion interface business error (including data flow name) |

## Differences between documentation and implementation

In case of inconsistencies between the documentation and implementation discovered during the implementation process, the code shall be based on the actual available behavior:

1. **Cluster**: The document is `GET /api/{org_id}/cluster_info`, and the implementation is `GET /api/{org_id}/cluster/info`. The latter is used by default and can be adjusted by overriding `CreateClusterInfoRequestUri`.
2. **Metrics**: The document text is `GET /metrics`, but the example is written as `PUT`, and only GET is actually registered.
3. **Function list response**: The document field table uses `stream_name` / `order` / `num_args` / `trans_type`, the implementation returns camelCase(`numArgs` / `transType` / `streams[]`), the model is subject to the implementation and uses extended attributes to retain undeclared fields.
4. **Function `order` field**: Document example contains, the current implementation does not accept it (only `streams[].order` takes effect).
5. **Dashboard**: The document only describes the panel-level interface, and the addition, deletion, modification, check/move/folder of the dashboard itself is not given in the document (for official instructions, see the OpenAPI/Swagger built into the instance).
6. Content-Type of **Ingestion**: The document is not declared, `_json` uses `application/json`, `_bulk` / `_multi` uses `application/x-ndjson`.
7. **Search `sort_by`**: It is not a field of `_search` (only supported by multi-stream search), please use SQL `ORDER BY` for sorting.
8. **`use_cache` / `clear_cache`**: The value in the query string will overwrite the request body, the default is `use_cache=true`.
