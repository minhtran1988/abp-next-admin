using System.Collections.Generic;

namespace LINGYUN.Abp.Location;

/// <summary>
/// Reverse address
/// </summary>
public class ReGeocodeLocation
{
    /// <summary>
    /// Detailed address
    /// </summary>
    public string? Address { get; set; }
    /// <summary>
    /// Formatted address description
    /// </summary>
    public string? FormattedAddress { get; set; }
    /// <summary>
    /// Country
    /// </summary>
    public string? Country { get; set; }
    /// <summary>
    /// Province
    /// </summary>
    public string? Province { get; set; }
    /// <summary>
    /// city
    /// </summary>
    public string? City { get; set; }
    /// <summary>
    /// Districts and counties
    /// </summary>
    public string? District { get; set; }
    /// <summary>
    /// street
    /// </summary>
    public string? Street { get; set; }
    /// <summary>
    /// adcode
    /// </summary>
    public string? AdCode { get; set; }
    /// <summary>
    /// Township
    /// </summary>
    public string? Town { get; set; }
    /// <summary>
    /// House number
    /// </summary>
    public string? Number { get; set; }
    /// <summary>
    /// Poi information list
    /// </summary>
    public IEnumerable<Poi> Pois { get; set; }
    /// <summary>
    /// Road information list
    /// </summary>
    public IEnumerable<Road> Roads { get; set; }
    /// <summary>
    /// Additional information
    /// </summary>
    public IDictionary<string, object> Additionals { get; }

    public ReGeocodeLocation()
    {
        Pois = new Poi[0];
        Roads = new Road[0];
        Additionals = new Dictionary<string, object>();
    }

    public void AddAdditional(string key, object value)
    {
        Additionals.Add(key, value);
    }
}
