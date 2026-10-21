using JetBrains.Annotations;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.WeChat.Work.OA.Approvals.Models;
/// <summary>
/// Position control configuration
/// </summary>
public class LocationControlConfig : ControlConfig
{
    /// <summary>
    /// Position control
    /// </summary>
    [NotNull]
    [JsonProperty("location")]
    [JsonPropertyName("location")]
    public LocationConfig Location { get; set; } = default!;
    public LocationControlConfig()
    {

    }

    public LocationControlConfig(LocationConfig location)
    {
        Location = location;
    }
}

public class LocationConfig
{
    /// <summary>
    /// Distance, currently supports 100, 200, 300
    /// </summary>
    [NotNull]
    [JsonProperty("distance")]
    [JsonPropertyName("distance")]
    public int Distance { get; set; }
    public LocationConfig()
    {

    }

    public LocationConfig(int distance)
    {
        Distance = distance;
    }
}
