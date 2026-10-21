using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.Location.Baidu.Model;

/// <summary>
/// Area surface
/// </summary>
public class PoiRegion
{
    /// <summary>
    /// Belonging area name
    /// </summary>
    public string Name { get; set; } = default!;
    /// <summary>
    /// Belonging area surface type
    /// </summary>
    public string? Tag { get; set; }
    /// <summary>
    /// The relative position between the coordinates in the request and the area to which they belong
    /// </summary>
    [JsonProperty("direction_desc")]
    [JsonPropertyName("direction_desc")]
    public string? DirectionDesc { get; set; }
    /// <summary>
    /// poi unique identifier
    /// </summary>
    public string Uid { get; set; } = default!;
    /// <summary>
    /// Distance from coordinate point
    /// </summary>
    public string? Distance { get; set; }
}
