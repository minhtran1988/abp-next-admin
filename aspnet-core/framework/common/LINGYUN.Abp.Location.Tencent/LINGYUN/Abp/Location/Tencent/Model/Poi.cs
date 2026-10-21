using Newtonsoft.Json;

namespace LINGYUN.Abp.Location.Tencent.Model;

/// <summary>
/// POI object
/// </summary>
public class Poi
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
    public string Title { get; set; } = default!;
    /// <summary>
    /// address
    /// </summary>
    [JsonProperty("address")]
    public string Address { get; set; } = default!;
    /// <summary>
    /// POI classification
    /// </summary>
    [JsonProperty("category")]
    public string CateGory { get; set; } = default!;
    /// <summary>
    /// coordinates
    /// </summary>
    [JsonProperty("location")]
    public Location Location { get; set; } = new Location();
    /// <summary>
    /// The straight-line distance from the POI to the coordinates passed in by reverse address analysis
    /// </summary>
    [JsonProperty("_distance")]
    public double Distance { get; set; }
    /// <summary>
    /// Administrative division information
    /// </summary>
    [JsonProperty("ad_info")]
    public AddressInfo AddressInfo { get; set; } = new AddressInfo();
}
