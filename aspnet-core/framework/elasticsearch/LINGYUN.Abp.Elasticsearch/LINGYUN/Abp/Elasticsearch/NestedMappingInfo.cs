using System.Collections.Generic;

namespace LINGYUN.Abp.Elasticsearch;

/// <summary>
/// Mapping information of Nested fields
/// </summary>
public class NestedMappingInfo
{
    /// <summary>
    /// The full path to the Nested field
    /// </summary>
    public string Path { get; set; } = string.Empty;

    /// <summary>
    /// Nested field name (last paragraph)
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Property mapping inside Nested
    /// </summary>
    public Dictionary<string, FieldMappingInfo> Properties { get; set; } = new();

    /// <summary>
    /// Whether to include the specified subfield
    /// </summary>
    public bool ContainsProperty(string propertyName)
    {
        return Properties.ContainsKey(propertyName);
    }

    /// <summary>
    /// Get subfield information
    /// </summary>
    public FieldMappingInfo? GetProperty(string propertyName)
    {
        return Properties.GetOrDefault(propertyName);
    }

    /// <summary>
    /// Get the full path of the nested internal field
    /// </summary>
    public string GetFullPath(string propertyName)
    {
        return $"{Path}.{propertyName}";
    }

    /// <summary>
    /// Get mapping information of nested internal fields (recursively)
    /// </summary>
    public FieldMappingInfo? GetNestedField(string fullPath)
    {
        // Remove the current nested path prefix
        if (!fullPath.StartsWith(Path + "."))
        {
            return null;
        }

        var remainingPath = fullPath.Substring(Path.Length + 1);
        var parts = remainingPath.Split('.');

        FieldMappingInfo? current = null;
        Dictionary<string, FieldMappingInfo>? currentProperties = Properties;

        for (var i = 0; i < parts.Length; i++)
        {
            var part = parts[i];

            if (currentProperties == null)
            {
                return null;
            }

            if (!currentProperties.TryGetValue(part, out current))
            {
                return null;
            }

            // If there is another next level and the current field is object or nested type
            if (i < parts.Length - 1)
            {
                currentProperties = current.Properties;
            }
        }

        return current;
    }
}
