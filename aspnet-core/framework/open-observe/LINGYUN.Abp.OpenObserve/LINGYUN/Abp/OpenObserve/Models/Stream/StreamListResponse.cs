using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.OpenObserve.Models.Stream;

/// <summary>
/// Data stream list response
/// </summary>
public class StreamListResponse
{
    [JsonPropertyName("list")]
    public StreamInfo[] List { get; set; } = [];
}
