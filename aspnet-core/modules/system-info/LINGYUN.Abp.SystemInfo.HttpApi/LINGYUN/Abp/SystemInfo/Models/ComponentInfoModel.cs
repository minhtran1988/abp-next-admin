using System.Collections.Generic;

namespace LINGYUN.Abp.SystemInfo.Models;
/// <summary>
/// Component
/// </summary>
public class ComponentInfoModel
{
    /// <summary>
    /// Component name
    /// </summary>
    public string Name { get; set; }
    /// <summary>
    /// Collection of component names
    /// </summary>
    public ComponentKeyModel[] Keys { get; set; }
    /// <summary>
    /// Component state collection
    /// </summary>
    public Dictionary<string, object?> Details { get; set; }
    public ComponentInfoModel(string name, ComponentKeyModel[] keys, Dictionary<string, object?> details)
    {
        Name = name;
        Keys = keys;
        Details = details;
    }
}
