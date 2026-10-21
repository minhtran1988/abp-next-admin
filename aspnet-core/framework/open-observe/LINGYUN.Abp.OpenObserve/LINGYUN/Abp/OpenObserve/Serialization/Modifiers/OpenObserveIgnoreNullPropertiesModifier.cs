using System;
using System.Text.Json.Serialization.Metadata;

namespace LINGYUN.Abp.OpenObserve.Serialization.Modifiers;

/// <summary>
/// Ignore serialization decorators for data model null fields
/// </summary>
/// <remarks>
public static class OpenObserveIgnoreNullPropertiesModifier
{
    /// <summary>
    /// The namespace of this library’s data model
    /// </summary>
    public const string ModelsNamespace = "LINGYUN.Abp.OpenObserve.Models";

    public static void Modify(JsonTypeInfo jsonTypeInfo)
    {
        if (jsonTypeInfo.Type.Namespace == null ||
            !jsonTypeInfo.Type.Namespace.StartsWith(ModelsNamespace, StringComparison.Ordinal))
        {
            return;
        }

        foreach (var property in jsonTypeInfo.Properties)
        {
            property.ShouldSerialize = (_, value) => value != null;
        }
    }
}
