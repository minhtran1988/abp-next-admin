using JetBrains.Annotations;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.WeChat.Work.OA.Approvals.Models;
/// <summary>
/// Department control value
/// </summary>
public class DepartmentControlValue : ContactControlValue
{
    /// <summary>
    /// The content of the selected department, that is, the department selected by the applicant in this control, there may be multiple in multi-select mode
    /// </summary>
    [NotNull]
    [JsonProperty("departments")]
    [JsonPropertyName("departments")]
    public List<DepartmentValue> Departments { get; set; } = default!;
    public DepartmentControlValue()
    {

    }

    private DepartmentControlValue(List<DepartmentValue> departments)
    {
        Departments = departments;
    }

    public static DepartmentControlValue Single(DepartmentValue department)
    {
        return new DepartmentControlValue(new List<DepartmentValue> { department });
    }

    public static DepartmentControlValue Multiple(List<DepartmentValue> departments)
    {
        return new DepartmentControlValue(departments);
    }
}

public class DepartmentValue
{
    /// <summary>
    /// Selected department id
    /// </summary>
    [NotNull]
    [JsonProperty("openapi_id")]
    [JsonPropertyName("openapi_id")]
    public string DepartmentId { get; set; } = default!;
    /// <summary>
    /// Selected department name
    /// </summary>
    [NotNull]
    [JsonProperty("name")]
    [JsonPropertyName("name")]
    public string Name { get; set; } = default!;
    public DepartmentValue()
    {

    }

    public DepartmentValue(string departmentId, string name)
    {
        DepartmentId = departmentId;
        Name = name;
    }
}
