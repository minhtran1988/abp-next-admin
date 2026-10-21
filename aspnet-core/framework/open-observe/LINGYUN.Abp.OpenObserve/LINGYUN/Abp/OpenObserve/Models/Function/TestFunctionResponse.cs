using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.OpenObserve.Models.Function;

/// <summary>
/// Test function response
/// </summary>
public class TestFunctionResponse
{
    [JsonPropertyName("results")]
    public TestFunctionResult[] Results { get; set; } = [];
}
