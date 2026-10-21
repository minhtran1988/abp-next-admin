using Newtonsoft.Json;

namespace LINGYUN.Abp.Location.Tencent.Model;

/// <summary>
/// Administrative division center point coordinates
/// </summary>
public class Location
{
    /// <summary>
    /// Latitude
    /// </summary>
    [JsonProperty("lat")]
    public double Lat { get; set; }
    /// <summary>
    /// Longitude
    /// </summary>
    [JsonProperty("lng")]
    public double Lng { get; set; }
}
