using Newtonsoft.Json;

namespace LINGYUN.Abp.Location.Tencent.Model;

/// <summary>
/// Address component, you can splice it yourself if the address does not meet the requirements.
/// </summary>
public class AddressComponent
{
    /// <summary>
    /// Country
    /// </summary>
    [JsonProperty("nation")]
    public string? Nation { get; set; }
    /// <summary>
    /// Province
    /// </summary>
    [JsonProperty("province")]
    public string? Province { get; set; }
    /// <summary>
    /// City
    /// </summary>
    [JsonProperty("city")]
    public string? City { get; set; }
    /// <summary>
    /// Area, may be an empty string
    /// </summary>
    [JsonProperty("district")]
    public string? District { get; set; }
    /// <summary>
    /// Street, may be an empty string
    /// </summary>
    [JsonProperty("street")]
    public string? Street { get; set; }
    /// <summary>
    /// House number, may be an empty string
    /// </summary>
    [JsonProperty("street_number")]
    public string? StreetNumber { get; set; }
}
