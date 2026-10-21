using Newtonsoft.Json.Linq;
using System;
using System.Text.Json;

namespace LINGYUN.Abp.WeChat.Work.Contacts.Members.Models;
internal static class MemberAttributeDeserializeFactory
{
    /// <summary>
    /// Create properties based on property types (System.Text.Json)
    /// </summary>
    public static MemberAttribute CreateExternalAttribute(AttributeType type, JsonElement configElement)
    {
        return type switch
        {
            AttributeType.Text => JsonSerializer.Deserialize<MemberTextAttribute>(configElement.GetRawText())!,
            AttributeType.Web => JsonSerializer.Deserialize<MemberWebAttribute>(configElement.GetRawText())!,
            _ => throw new NotSupportedException($"Attribute type {type} is not supported for the time being"),
        };
    }

    /// <summary>
    /// Create properties based on property types (Newtonsoft.Json)
    /// </summary>
    public static MemberAttribute CreateExternalAttribute(AttributeType type, JToken configToken)
    {
        return type switch
        {
            AttributeType.Text => configToken.ToObject<MemberTextAttribute>()!,
            AttributeType.Web => configToken.ToObject<MemberWebAttribute>()!,
            _ => throw new NotSupportedException($"Attribute type {type} is not supported for the time being"),
        };
    }
}
