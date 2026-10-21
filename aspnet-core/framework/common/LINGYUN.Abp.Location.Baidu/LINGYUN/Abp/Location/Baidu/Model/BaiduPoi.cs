using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.Location.Baidu.Model;

public class BaiduPoi
{
    /// <summary>
    /// Address information
    /// </summary>
    [JsonProperty("addr")]
    [JsonPropertyName("addr")]
    public string Address { get; set; } = default!;
    /// <summary>
    /// Name
    /// </summary>
    public string Name { get; set; } = default!;
    /// <summary>
    /// mark
    /// </summary>
    public string? Tag { get; set; }
    /// <summary>
    /// and the direction of the current coordinate point
    /// </summary>
    public string? Direction { get; set; }
    /// <summary>
    /// Distance from coordinate point
    /// </summary>
    public string? Distance { get; set; }
    /// <summary>
    /// poi coordinates {x,y}
    /// </summary>
    public Point Point { get; set; } = new Point();

    /// <summary>
    /// Coordinate type
    /// </summary>
    public string? PoiType { get; set; }
    /// <summary>
    /// Phone
    /// </summary>
    [JsonProperty("tel")]
    [JsonPropertyName("tel")]
    public string? TelPhone { get; set; }
    /// <summary>
    /// poi unique identifier
    /// </summary>
    public string Uid { get; set; } = default!;
    /// <summary>
    /// Zip code
    /// </summary>
    [JsonProperty("zip")]
    [JsonPropertyName("zip")]
    public string? Post { get; set; }
    /// <summary>
    /// The main point poi corresponding to poi (for example, the main point of Haidilao is Shangdi Hualian, this field is the poi information of Shangdi Hualian.
    /// If none, this field is empty), and the subfields included are the same as the pois basic recall field.
    /// </summary>
    [JsonProperty("parent_poi")]
    [JsonPropertyName("parent_poi")]
    public BaiduPoi? ParentPoi { get; set; }
}
