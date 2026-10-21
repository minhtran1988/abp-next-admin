using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace LINGYUN.Abp.Elasticsearch;
/// <summary>
/// Index mapping information
/// </summary>
public class IndexMappingInfo
{
    /// <summary>
    /// Index name
    /// </summary>
    public string IndexName { get; set; } = string.Empty;

    /// <summary>
    /// Document type
    /// </summary>
    public Type? DocumentType { get; set; }

    /// <summary>
    /// All field mapping (flattened)
    /// </summary>
    public Dictionary<string, FieldMappingInfo> Fields { get; set; } = new Dictionary<string, FieldMappingInfo>(StringComparer.CurrentCultureIgnoreCase);

    /// <summary>
    /// Field mapping indexed by CLR property path
    /// </summary>
    public Dictionary<string, FieldMappingInfo> ClrFields { get; set; } = new Dictionary<string, FieldMappingInfo>(StringComparer.CurrentCultureIgnoreCase);

    /// <summary>
    /// Nested field mapping
    /// </summary>
    public Dictionary<string, NestedMappingInfo> NestedFields { get; set; } = new Dictionary<string, NestedMappingInfo>(StringComparer.CurrentCultureIgnoreCase);

    /// <summary>
    /// Keyword field list
    /// </summary>
    public HashSet<string> KeywordFields { get; set; } = new();

    /// <summary>
    /// Text field list
    /// </summary>
    public HashSet<string> TextFields { get; set; } = new();
    /// <summary>
    /// Wildcard field list
    /// </summary>
    public HashSet<string> WildcardFields { get; set; } = new();

    /// <summary>
    /// Date field list
    /// </summary>
    public HashSet<string> DateFields { get; set; } = new();

    /// <summary>
    /// Numeric field list
    /// </summary>
    public HashSet<string> NumericFields { get; set; } = new();

    /// <summary>
    /// Boolean field list
    /// </summary>
    public HashSet<string> BooleanFields { get; set; } = new();

    /// <summary>
    /// Nested field path list
    /// </summary>
    public HashSet<string> NestedFieldPaths { get; set; } = new();

    /// <summary>
    /// Get field mapping information
    /// </summary>
    public FieldMappingInfo? GetField(string path)
    {
        return Fields.GetOrDefault(path);
    }

    /// <summary>
    /// Get field mapping information based on CLR attribute path
    /// </summary>
    public FieldMappingInfo? GetFieldByClrPath(string clrPath)
    {
        return ClrFields.GetOrDefault(clrPath);
    }

    /// <summary>
    /// Get field mapping information based on CLR attribute expression
    /// </summary>
    public FieldMappingInfo? GetFieldByExpression<TDocument>(Expression<Func<TDocument, object?>> expression)
    {
        var clrPath = GetPropertyPath(expression);
        return GetFieldByClrPath(clrPath);
    }

    /// <summary>
    /// Get the ES field path based on the CLR attribute expression
    /// </summary>
    public string? GetElasticsearchFieldPath<TDocument>(Expression<Func<TDocument, object?>> expression)
    {
        var clrPath = GetPropertyPath(expression);
        var field = GetFieldByClrPath(clrPath);
        return field?.Path;
    }

    /// <summary>
    /// Determine whether it is a Nested field
    /// </summary>
    public bool IsNested(string path)
    {
        return NestedFields.ContainsKey(path) || NestedFieldPaths.Contains(path);
    }

    /// <summary>
    /// Get Nested field information
    /// </summary>
    public NestedMappingInfo? GetNestedField(string path)
    {
        return NestedFields.GetOrDefault(path);
    }

    /// <summary>
    /// Get the exact matching path of the field (processing the keyword subfield of text)
    /// </summary>
    public string GetExactFieldPath(string path)
    {
        var field = GetField(path);
        if (field == null)
        {
            return path;
        }

        return field.GetKeywordPath();
    }

    /// <summary>
    /// Determine whether the field requires Nested query
    /// </summary>
    public bool ShouldUseNestedQuery(string path)
    {
        // Check if the path itself is Nested
        if (IsNested(path))
        {
            return true;
        }

        // Check if the parent of the path is Nested
        var parts = path.Split('.');
        for (var i = 0; i < parts.Length - 1; i++)
        {
            var parentPath = string.Join(".", parts.Take(i + 1));
            if (IsNested(parentPath))
            {
                return true;
            }
        }

        return false;
    }

    /// <summary>
    /// Get the Nested field path (if the field is inside Nested, return the nearest Nested parent path)
    /// </summary>
    public string? GetNestedParentPath(string path)
    {
        var parts = path.Split('.');
        for (var i = parts.Length - 1; i >= 0; i--)
        {
            var parentPath = string.Join(".", parts.Take(i + 1));
            if (IsNested(parentPath))
            {
                return parentPath;
            }
        }

        return null;
    }
    /// <summary>
    /// Convert CLR property paths to ES field paths
    /// </summary>
    public string? ConvertClrPathToElasticsearchPath(string clrPath)
    {
        var field = GetFieldByClrPath(clrPath);
        return field?.Path;
    }

    /// <summary>
    /// Convert ES field paths to CLR property paths
    /// </summary>
    public string? ConvertElasticsearchPathToClrPath(string esPath)
    {
        var field = GetField(esPath);
        return field?.ClrPath;
    }

    private static string GetPropertyPath<TDocument>(Expression<Func<TDocument, object?>> expression)
    {
        var parts = new List<string>();
        var currentExpression = expression.Body;

        while (currentExpression is MemberExpression memberExpression)
        {
            parts.Insert(0, memberExpression.Member.Name);
            currentExpression = memberExpression.Expression!;
        }

        // handle conversion expression
        if (currentExpression is UnaryExpression unaryExpression &&
            unaryExpression.NodeType == ExpressionType.Convert &&
            unaryExpression.Operand is MemberExpression convertMemberExpression)
        {
            parts.Insert(0, convertMemberExpression.Member.Name);
        }

        return string.Join(".", parts);
    }
}