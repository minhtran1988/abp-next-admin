using Newtonsoft.Json;

namespace LINGYUN.Abp.Location.Tencent.Model;

/// <summary>
/// Region information
/// </summary>
public class Area
{
    /// <summary>
    /// Unique identifier of location
    /// </summary>
    [JsonProperty("id")]
    public string Id { get; set; } = default!;
    /// <summary>
    /// name/title
    /// </summary>
    [JsonProperty("title")]
    public string? Title { get; set; }
    /// <summary>
    /// coordinates
    /// </summary>
    [JsonProperty("location")]
    public Location Location { get; set; } = new Location();
    /// <summary>
    /// The straight-line distance from this reference position to the input coordinates
    /// </summary>
    [JsonProperty("_distance")]
    public string? Distance { get; set; }
    /// <summary>
    /// The azimuth relationship between this reference position and the input coordinates,
    /// Such as: north, south, inner
    /// </summary>
    [JsonProperty("_dir_desc")]
    public string? DirDescription { get; set; }
}
