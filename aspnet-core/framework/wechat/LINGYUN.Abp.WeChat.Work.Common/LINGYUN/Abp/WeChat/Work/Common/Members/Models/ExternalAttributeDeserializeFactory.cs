using Newtonsoft.Json.Linq;
using System;
using System.Text.Json;

namespace LINGYUN.Abp.WeChat.Work.Common.Members.Models;
internal static class ExternalAttributeDeserializeFactory
{
    /// <summary>
    /// Create properties based on property types (System.Text.Json)
    /// </summary>
    public static ExternalAttribute CreateExternalAttribute(ExternalAttributeType type, JsonElement configElement)
    {
        return type switch
        {
            ExternalAttributeType.Text => JsonSerializer.Deserialize<ExternalTextAttribute>(configElement.GetRawText())!,
            ExternalAttributeType.Web => JsonSerializer.Deserialize<ExternalWebAttribute>(configElement.GetRawText())!,
            ExternalAttributeType.MiniProgram => JsonSerializer.Deserialize<ExternalMiniProgramAttribute>(configElement.GetRawText())!,
            _ => throw new NotSupportedException($"Attribute type {type} is not supported for the time being"),
        };
    }

    /// <summary>
    /// Create properties based on property types (Newtonsoft.Json)
    /// </summary>
    public static ExternalAttribute CreateExternalAttribute(ExternalAttributeType type, JToken configToken)
    {
        return type switch
        {
            ExternalAttributeType.Text => configToken.ToObject<ExternalTextAttribute>()!,
            ExternalAttributeType.Web => configToken.ToObject<ExternalWebAttribute>()!,
            ExternalAttributeType.MiniProgram => configToken.ToObject<ExternalMiniProgramAttribute>()!,
            _ => throw new NotSupportedException($"Attribute type {type} is not supported for the time being"),
        };
    }
}
