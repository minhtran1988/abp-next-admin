using JetBrains.Annotations;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.WeChat.Work.OA.Approvals.Models;
/// <summary>
/// Position control value
/// </summary>
public class LocationControlValue : ControlValue
{
    /// <summary>
    /// position
    /// </summary>
    [NotNull]
    [JsonProperty("location")]
    [JsonPropertyName("location")]
    public LocationValue Location { get; set; } = default!;
    public LocationControlValue()
    {

    }

    public LocationControlValue(LocationValue location)
    {
        Location = location;
    }
}

public class LocationValue
{
    /// <summary>
    /// Latitude, accurate to 6 decimal places
    /// </summary>
    [NotNull]
    [JsonProperty("latitude")]
    [JsonPropertyName("latitude")]
    public decimal Latitude { get; set; }
    /// <summary>
    /// Longitude, accurate to 6 decimal places
    /// </summary>
    [NotNull]
    [JsonProperty("longitude")]
    [JsonPropertyName("longitude")]
    public decimal Longitude { get; set; }
    /// <summary>
    /// Location title
    /// </summary>
    [NotNull]
    [JsonProperty("title")]
    [JsonPropertyName("title")]
    public string Title { get; set; } = default!;
    /// <summary>
    /// Location details address
    /// </summary>
    [NotNull]
    [JsonProperty("address")]
    [JsonPropertyName("address")]
    public string Address { get; set; } = default!;
    /// <summary>
    /// Select the time of location
    /// </summary>
    [NotNull]
    [JsonProperty("time")]
    [JsonPropertyName("time")]
    public long Time { get; set; }
    public LocationValue()
    {

    }

    public LocationValue(decimal latitude, decimal longitude, string title, string address, long time)
    {
        Latitude = latitude;
        Longitude = longitude;
        Title = title;
        Address = address;
        Time = time;
    }
}