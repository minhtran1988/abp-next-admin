using Newtonsoft.Json.Linq;
using System.Text.Json;

namespace LINGYUN.Abp.WeChat.Work.OA.Approvals.Models;
internal static class ControlConfigFactory
{
    /// <summary>
    /// Create configuration (System.Text.Json) based on control type
    /// </summary>
    public static ControlConfig? CreateConfig(string controlType, JsonElement configElement)
    {
        return controlType switch
        {
            "Attendance" => JsonSerializer.Deserialize<AttendanceControlConfig>(configElement.GetRawText()),
            "Contact" => JsonSerializer.Deserialize<ContactControlConfig>(configElement.GetRawText()),
            "Date" => JsonSerializer.Deserialize<DateControlConfig>(configElement.GetRawText()),
            "DateRange" => JsonSerializer.Deserialize<DateRangeControlConfig>(configElement.GetRawText()),
            "File" => JsonSerializer.Deserialize<FileControlConfig>(configElement.GetRawText()),
            "Location" => JsonSerializer.Deserialize<LocationControlConfig>(configElement.GetRawText()),
            "RelatedApproval" => JsonSerializer.Deserialize<RelatedApprovalControlConfig>(configElement.GetRawText()),
            "Selector" => JsonSerializer.Deserialize<SelectorControlConfig>(configElement.GetRawText()),
            "Table" => JsonSerializer.Deserialize<TableControlConfig>(configElement.GetRawText()),
            "Tips" => JsonSerializer.Deserialize<TipsControlConfig>(configElement.GetRawText()),
            // Add other control types...
            _ => null
        };
    }

    /// <summary>
    /// Create configuration (Newtonsoft.Json) based on control type
    /// </summary>
    public static ControlConfig? CreateConfig(string controlType, JToken configToken)
    {
        return controlType switch
        {
            "Attendance" => configToken.ToObject<AttendanceControlConfig>(),
            "Contact" => configToken.ToObject<ContactControlConfig>(),
            "Date" => configToken.ToObject<DateControlConfig>(),
            "DateRange" => configToken.ToObject<DateRangeControlConfig>(),
            "File" => configToken.ToObject<FileControlConfig>(),
            "Location" => configToken.ToObject<LocationControlConfig>(),
            "RelatedApproval" => configToken.ToObject<RelatedApprovalControlConfig>(),
            "Selector" => configToken.ToObject<SelectorControlConfig>(),
            "Table" => configToken.ToObject<TableControlConfig>(),
            "Tips" => configToken.ToObject<TipsControlConfig>(),
            // Add other control types...
            _ => null
        };
    }
}
