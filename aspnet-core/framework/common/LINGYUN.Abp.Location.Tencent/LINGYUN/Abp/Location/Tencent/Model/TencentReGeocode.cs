using Newtonsoft.Json;

namespace LINGYUN.Abp.Location.Tencent.Model;

/// <summary>
/// Reverse geocoding result
/// </summary>
public class TencentReGeocode
{
    /// <summary>
    /// Address description
    /// </summary>
    [JsonProperty("address")]
    public string? Address { get; set; }
    /// <summary>
    /// Location description
    /// </summary>
    [JsonProperty("formatted_addresses")]
    public FormattedAddress FormattedAddress { get; set; } = new FormattedAddress();
    /// <summary>
    /// Address component, you can splice it yourself if the address does not meet the requirements.
    /// </summary>
    [JsonProperty("address_component")]
    public AddressComponent AddressComponent { get; set; } = new AddressComponent();
    /// <summary>
    /// Administrative division information
    /// </summary>
    [JsonProperty("ad_info")]
    public AddressInfo AddressInfo { get; set; } = new AddressInfo();
    /// <summary>
    /// Coordinate relative position reference
    /// </summary>
    [JsonProperty("address_reference")]
    public AddressReference AddressReference { get; set; } = new AddressReference();
    /// <summary>
    /// The total number of surrounding POIs queried
    /// </summary>
    [JsonProperty("poi_count")]
    public int PoiCount { get; set; }
    /// <summary>
    /// POI array, each sub-item in the object is a POI object
    /// </summary>
    [JsonProperty("pois")]
    public Poi[] Pois { get; set; } = new Poi[0];
}
