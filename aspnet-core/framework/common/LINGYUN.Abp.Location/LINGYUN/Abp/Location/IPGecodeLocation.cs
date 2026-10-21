using System.Collections.Generic;

namespace LINGYUN.Abp.Location;

public class IPGecodeLocation
{
    /// <summary>
    /// IP address
    /// </summary>
    public string IpAddress { get; set; } = default!;
    /// <summary>
    /// Positioning coordinates
    /// </summary>
    public Location Location { get; set; } = new Location();
    /// <summary>
    /// Country
    /// </summary>
    public string? Country { get; set; }
    /// <summary>
    /// city
    /// </summary>
    public string? City { get; set; }
    /// <summary>
    /// Province
    /// </summary>
    public string? Province { get; set; }
    /// <summary>
    /// Districts and counties
    /// </summary>
    public string? District { get; set; }
    /// <summary>
    /// adcode
    /// </summary>
    public string? AdCode { get; set; }

    public IDictionary<string, object> Additionals { get; } = new Dictionary<string, object>();

    public void AddAdditional(string key, object value)
    {
        Additionals.Add(key, value);
    }
}
