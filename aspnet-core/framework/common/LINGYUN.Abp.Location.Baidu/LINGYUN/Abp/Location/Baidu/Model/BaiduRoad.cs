namespace LINGYUN.Abp.Location.Baidu.Model;

/// <summary>
/// road
/// </summary>
public class BaiduRoad 
{
    public string Name { get; set; } = default!;
    /// <summary>
    /// The approximate distance between the incoming coordinate point and the road
    /// </summary>
    public string? Distance { get; set; }
}
