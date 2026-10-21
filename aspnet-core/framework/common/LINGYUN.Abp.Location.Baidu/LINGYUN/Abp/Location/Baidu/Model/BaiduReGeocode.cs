using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.Location.Baidu.Model;

public class BaiduReGeocode
{
    /// <summary>
    /// Longitude and latitude coordinates
    /// </summary>
    [JsonProperty("location")]
    [JsonPropertyName("location")]
    public BaiduLocation Location { get; set; } = default!;
    /// <summary>
    /// Structured address information
    /// </summary>
    [JsonProperty("formatted_address")]
    [JsonPropertyName("formatted_address")]
    public string? FormattedAddress { get; set; }
    /// <summary>
    /// The business district information where the coordinates are located, such as "Renmin University, Zhongguancun, Suzhou Street".
    /// Return at most 3.
    /// </summary>
    public string? Business { get; set; }
    /// <summary>
    /// List of address elements
    /// </summary>
    public AddressComponent? AddressComponent { get; set; }
    /// <summary>
    /// Surrounding poi array
    /// </summary>
    public List<BaiduPoi> Pois { get; set; }
    /// <summary>
    /// Array of surrounding roads
    /// </summary>
    public List<BaiduRoad> Roads { get; set; }
    /// <summary>
    /// Surrounding area face array
    /// </summary>
    public List<PoiRegion> PoiRegions { get; set; }
    /// <summary>
    /// The current position is combined with the semantic result description of POI
    /// </summary>
    [JsonProperty("sematic_description")]
    [JsonPropertyName("sematic_description")]
    public string? SematicDescription { get; set; }
    public BaiduReGeocode()
    {
        AddressComponent = new AddressComponent();
        Pois = new List<BaiduPoi>();
        Roads = new List<BaiduRoad>();
        PoiRegions = new List<PoiRegion>();
    }
}
