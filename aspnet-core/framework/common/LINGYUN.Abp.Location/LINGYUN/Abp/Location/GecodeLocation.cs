using System.Collections.Generic;

namespace LINGYUN.Abp.Location;

/// <summary>
/// Positive address
/// </summary>
public class GecodeLocation : Location
{
    /// <summary>
    /// Absolute accuracy
    /// </summary>
    public int Confidence { get; set; }
    /// <summary>
    /// Level of understanding
    /// Score range 0-100
    /// The greater the score, the better the service understands the address.
    /// </summary>
    public int Pomprehension { get; set; }
    /// <summary>
    /// Address type that can be accurately understood
    /// </summary>
    public string? Level { get; set; }
    /// <summary>
    /// Additional information
    /// </summary>
    public IDictionary<string, object> Additionals { get; }

    public GecodeLocation()
    {
        Additionals = new Dictionary<string, object>();
    }

    public void AddAdditional(string key, object value)
    {
        Additionals.Add(key, value);
    }
}
