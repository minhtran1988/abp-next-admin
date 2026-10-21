using Newtonsoft.Json;

namespace LINGYUN.Abp.Location.Tencent.Model;

/// <summary>
/// Coordinate relative position reference
/// </summary>
public class AddressReference
{
    /// <summary>
    /// Business district
    /// </summary>
    [JsonProperty("business_area")]
    public Area BusinessArea { get; set; } = new Area();
    /// <summary>
    /// Well-known areas, such as business districts or areas that are generally considered to have high visibility
    /// </summary>
    [JsonProperty("famous_area")]
    public Area FamousArea { get; set; } = new Area();
    /// <summary>
    /// Township streets
    /// </summary>
    [JsonProperty("town")]
    public Area Town { get; set; } = new Area();
    /// <summary>
    /// First-level landmarks, highly identifiable and large-scale locations, communities, etc.
    /// </summary>
    [JsonProperty("landmark_l1")]
    public Area Landmark1 { get; set; } = new Area();
    /// <summary>
    /// Second-level landmarks are more accurate and smaller than first-level landmarks.
    /// </summary>
    [JsonProperty("landmark_l2")]
    public Area Landmark2 { get; set; } = new Area();
    /// <summary>
    /// street
    /// </summary>
    [JsonProperty("street")]
    public Area Street { get; set; } = new Area();
    /// <summary>
    /// House number
    /// </summary>
    [JsonProperty("street_number")]
    public Area StreetNumber { get; set; } = new Area();
    /// <summary>
    /// intersection
    /// </summary>
    [JsonProperty("crossroad")]
    public Area CrossRoad { get; set; } = new Area();
    /// <summary>
    /// Water system
    /// </summary>
    [JsonProperty("water")]
    public Area Water { get; set; } = new Area();
}
