using Newtonsoft.Json;

namespace LINGYUN.Abp.Location.Tencent.Model;

public class TencentGeocode
{
    /// <summary>
    /// The parsed coordinates
    /// </summary>
    [JsonProperty("location")]
    public Location Location { get; set; } = new Location();
    /// <summary>
    /// Parsed address component
    /// </summary>
    [JsonProperty("address_components")]
    public AddressComponent AddressComponent { get; set; } = new AddressComponent();
    /// <summary>
    /// Administrative division information
    /// </summary>
    [JsonProperty("ad_info")]
    public GeocodeAddressInfo AddressInfo { get; set; } = new GeocodeAddressInfo();
    /// <summary>
    /// Confidence reference: value range 1 <low confidence> - 10 <high confidence>
    /// </summary>
    [JsonProperty("reliability")]
    public int Reliability { get; set; }
    /// <summary>
    /// The parsing accuracy level is divided into 11 levels. Generally >=9 can be used (locating to the point, the accuracy is higher). It can also be adjusted according to actual business needs.
    /// </summary>
    [JsonProperty("level")]
    public int Level { get; set; }
}
/// <summary>
/// Administrative division information
/// </summary>
public class GeocodeAddressInfo
{
    /// <summary>
    /// Administrative division code
    /// </summary>
    [JsonProperty("adcode")]
    public string AdCode { get; set; } = default!;
}
