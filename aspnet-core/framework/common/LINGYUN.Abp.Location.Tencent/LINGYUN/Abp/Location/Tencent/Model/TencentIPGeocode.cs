using Newtonsoft.Json;

namespace LINGYUN.Abp.Location.Tencent.Model;

/// <summary>
/// IP positioning results
/// </summary>
public class TencentIPGeocode
{
    /// <summary>
    /// IP address used for positioning
    /// </summary>
    [JsonProperty("ip")]
    public string IpAddress { get; set; } = default!;
    /// <summary>
    /// Positioning coordinates
    /// </summary>
    [JsonProperty("location")]
    public Location Location { get; set; } = new Location();
    /// <summary>
    /// Locate administrative division information
    /// </summary>
    [JsonProperty("ad_info")]
    public AddressInfo AddressInfo { get; set; } = new AddressInfo();
}
