using Newtonsoft.Json;

namespace LINGYUN.Abp.Location.Tencent.Model;

/// <summary>
/// Location description
/// </summary>
public class FormattedAddress
{
    /// <summary>
    /// The description method optimized by Tencent Maps is more humane.
    /// </summary>
    [JsonProperty("recommend")]
    public string? ReCommend { get; set; }
    /// <summary>
    /// Approximate location, can be used to roughly describe the location
    /// </summary>
    [JsonProperty("rough")]
    public string? Rough { get; set; }
}
