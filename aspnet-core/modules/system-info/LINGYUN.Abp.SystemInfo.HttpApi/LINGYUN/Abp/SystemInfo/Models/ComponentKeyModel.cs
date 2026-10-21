namespace LINGYUN.Abp.SystemInfo.Models;
/// <summary>
/// Component name
/// </summary>
public class ComponentKeyModel
{
    /// <summary>
    /// Component name
    /// </summary>
    public string Name { get; set; }
    /// <summary>
    /// Component display name
    /// </summary>
    public string DisplayName { get; set; }
    public ComponentKeyModel(string name, string displayName)
    {
        Name = name;
        DisplayName = displayName;
    }
}
