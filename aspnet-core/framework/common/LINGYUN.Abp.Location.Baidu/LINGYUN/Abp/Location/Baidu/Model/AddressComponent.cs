using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.Location.Baidu.Model;

public class AddressComponent
{
    /// <summary>
    /// Country
    /// </summary>
    public string? Country { get; set; }
    /// <summary>
    /// Country country code
    /// </summary>
    [JsonProperty("country_code")]
    [JsonPropertyName("country_code")]
    public int CountryCode { get; set; }
    /// <summary>
    /// Country English abbreviation (three digits)
    /// </summary>
    [JsonProperty("country_code_iso")]
    [JsonPropertyName("country_code_iso")]
    public string? CountryCodeIso { get; set; }
    /// <summary>
    /// Country English abbreviation (two digits)
    /// </summary>
    [JsonProperty("country_code_iso2")]
    [JsonPropertyName("country_code_iso2")]
    public string? CountryCodeIso2 { get; set; }
    /// <summary>
    /// Province name
    /// </summary>
    public string? Province { get; set; }
    /// <summary>
    /// City name
    /// </summary>
    public string? City { get; set; }
    /// <summary>
    /// The level of the city (only for reference in foreign countries.
    /// Foreign administrative divisions are different from those in China, and the corresponding level of a city is not necessarily "city".
    /// Country, province, city, district, and town correspond to levels 0-4 respectively. If city_level=3, the district level is the city level of the country)
    /// </summary>
    [JsonProperty("city_level")]
    [JsonPropertyName("city_level")]
    public int CityLevel { get; set; }
    /// <summary>
    /// District and county name
    /// </summary>
    public string? District { get; set; }
    /// <summary>
    /// Township name
    /// </summary>
    public string? Town { get; set; }
    /// <summary>
    /// Township id
    /// </summary>
    [JsonProperty("town_code")]
    [JsonPropertyName("town_code")]
    public string? TownCode { get; set; }
    /// <summary>
    /// Street name (street level in administrative division)
    /// </summary>
    public string? Street { get; set; }
    /// <summary>
    /// Street house number
    /// </summary>
    [JsonProperty("street_number")]
    [JsonPropertyName("street_number")]
    public string? StreetNumber { get; set; }
    /// <summary>
    /// Administrative division code
    /// </summary>
    public string? AdCode { get; set; }
    /// <summary>
    /// Relative to the direction of the current coordinate point, data is returned when there is a house number.
    /// </summary>
    public string? Direction { get; set; }
    /// <summary>
    /// The distance relative to the current coordinate point, returns data when there is a house number
    /// </summary>
    public string? Distance { get; set; }
}
