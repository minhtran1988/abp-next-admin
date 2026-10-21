using Newtonsoft.Json;

namespace LINGYUN.Abp.Location.Tencent.Model;

/// <summary>
/// Administrative division information
/// </summary>
public class AddressInfo
{
    [JsonProperty("adcode")]
    public string AdCode { get; set; } = default!;
    /// <summary>
    /// City code
    /// </summary>
    [JsonProperty("city_code")]
    public string? CityCode { get; set; }
    /// <summary>
    /// Administrative division code
    /// </summary>
    [JsonProperty("nation_code")]
    public string? NationCode { get; set; }
    /// <summary>
    /// Administrative division name
    /// </summary>
    [JsonProperty("name")]
    public string? Name { get; set; }
    /// <summary>
    /// Country
    /// </summary>
    [JsonProperty("nation")]
    public string? Nation { get; set; }
    /// <summary>
    /// Province/Municipality
    /// </summary>
    [JsonProperty("province")]
    public string? Province { get; set; }
    /// <summary>
    /// City/prefecture-level districts and administrative divisions at the same level
    /// </summary>
    [JsonProperty("city")]
    public string? City { get; set; }
    /// <summary>
    /// District/county-level city and administrative divisions at the same level
    /// </summary>
    [JsonProperty("district")]
    public string? District { get; set; }
    /// <summary>
    /// Administrative division center point coordinates
    /// </summary>
    [JsonProperty("location")]
    public Location Location { get; set; } = new Location();
}
