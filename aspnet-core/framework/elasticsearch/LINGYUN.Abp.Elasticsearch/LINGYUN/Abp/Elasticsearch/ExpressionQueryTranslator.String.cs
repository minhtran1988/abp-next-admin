using Elastic.Clients.Elasticsearch.QueryDsl;
using System;
using System.Linq.Expressions;

namespace LINGYUN.Abp.Elasticsearch;

public partial class ExpressionQueryTranslator
{
    /// <summary>
    /// Translate string method
    /// </summary>
    private Query TranslateStringMethod(MethodCallExpression node, string? prefix, IndexMappingInfo? mappingInfo)
    {
        return node.Method.Name switch
        {
            // Instance method
            nameof(string.Contains) => TranslateStringContains(node, prefix, mappingInfo),
            nameof(string.StartsWith) => TranslateStringStartsWith(node, prefix, mappingInfo),
            nameof(string.EndsWith) => TranslateStringEndsWith(node, prefix, mappingInfo),
            nameof(string.Equals) => TranslateStringEquals(node, prefix, mappingInfo),
            nameof(string.CompareTo) => TranslateStringCompareTo(node, prefix, mappingInfo),
            nameof(string.IndexOf) => TranslateStringIndexOf(node, prefix, mappingInfo),

            // static method
            nameof(string.IsNullOrEmpty) => TranslateStringIsNullOrEmpty(node, prefix, mappingInfo),
            nameof(string.IsNullOrWhiteSpace) => TranslateStringIsNullOrWhiteSpace(node, prefix, mappingInfo),

            // Other methods are not supported
            _ => throw new NotSupportedException($"Unsupported string method {node.Method.Name}"),
        };
    }

    /// <summary>
    /// Translate Contains
    /// </summary>
    private Query TranslateStringContains(MethodCallExpression node, string? prefix, IndexMappingInfo? mappingInfo)
    {
        var (field, value) = GetStringMethodOperands(node, prefix, mappingInfo);
        var fieldMapping = mappingInfo?.GetField(field.Path);
        return TranslateStringContains(field, fieldMapping, value);
    }

    /// <summary>
    /// Translation StartsWith
    /// </summary>
    private Query TranslateStringStartsWith(MethodCallExpression node, string? prefix, IndexMappingInfo? mappingInfo)
    {
        var (field, value) = GetStringMethodOperands(node, prefix, mappingInfo);
        var fieldMapping = mappingInfo?.GetField(field.Path);
        return TranslateStartsWith(field, fieldMapping, value);
    }

    /// <summary>
    /// Translation EndsWith
    /// </summary>
    private Query TranslateStringEndsWith(MethodCallExpression node, string? prefix, IndexMappingInfo? mappingInfo)
    {
        var (field, value) = GetStringMethodOperands(node, prefix, mappingInfo);
        var fieldMapping = mappingInfo?.GetField(field.Path);
        return TranslateEndsWith(field, fieldMapping, value);
    }

    /// <summary>
    /// Translation StartsWith
    /// </summary>
    private Query TranslateStartsWith(FieldInfo field, FieldMappingInfo? fieldMapping, string value)
    {
        var pattern = EscapeWildcard(value) + "*";

        // Wildcard type
        if (field.IsWildcard || fieldMapping?.IsWildcard == true)
        {
            return new WildcardQuery { Field = field.Path, Value = pattern };
        }

        // Keyword type or Text has keyword subfield
        if (field.IsKeyword || fieldMapping?.IsKeyword == true ||
            (fieldMapping?.IsText == true && fieldMapping?.Properties?.ContainsKey("keyword") == true))
        {
            var fieldPath = fieldMapping?.IsText == true && fieldMapping?.Properties?.ContainsKey("keyword") == true
                ? $"{field.Path}.keyword"
                : field.Path;
            return new WildcardQuery { Field = fieldPath, Value = pattern };
        }

        // Text type has no keyword subfield
        if (field.IsText || fieldMapping?.IsText == true)
        {
            return new MatchPhrasePrefixQuery
            {
                Field = field.Path,
                Query = value
            };
        }

        return new WildcardQuery { Field = field.Path, Value = pattern };
    }

    /// <summary>
    /// Translation EndsWith
    /// </summary>
    private Query TranslateEndsWith(FieldInfo field, FieldMappingInfo? fieldMapping, string value)
    {
        var pattern = "*" + EscapeWildcard(value);

        // Wildcard type
        if (field.IsWildcard || fieldMapping?.IsWildcard == true)
        {
            return new WildcardQuery { Field = field.Path, Value = pattern };
        }

        // Keyword type or Text has keyword subfield
        if (field.IsKeyword || fieldMapping?.IsKeyword == true ||
            (fieldMapping?.IsText == true && fieldMapping?.Properties?.ContainsKey("keyword") == true))
        {
            var fieldPath = fieldMapping?.IsText == true && fieldMapping?.Properties?.ContainsKey("keyword") == true
                ? $"{field.Path}.keyword"
                : field.Path;
            return new WildcardQuery { Field = fieldPath, Value = pattern };
        }

        // Text type has no keyword subfield
        if (field.IsText || fieldMapping?.IsText == true)
        {
            return new MatchPhraseQuery
            {
                Field = field.Path,
                Query = value
            };
        }

        return new WildcardQuery { Field = field.Path, Value = pattern };
    }

    /// <summary>
    /// Translate string.Equals
    /// </summary>
    private Query TranslateStringEquals(MethodCallExpression node, string? prefix, IndexMappingInfo? mappingInfo)
    {
        var (field, value) = GetStringMethodOperands(node, prefix, mappingInfo);

        if (string.IsNullOrEmpty(value))
        {
            return new BoolQuery { MustNot = new Query[] { new ExistsQuery { Field = field.Path } } };
        }

        return BuildEquality(field, value);
    }

    /// <summary>
    /// Translate string.CompareTo
    /// </summary>
    private Query TranslateStringCompareTo(MethodCallExpression node, string? prefix, IndexMappingInfo? mappingInfo)
    {
        var (field, value) = GetStringMethodOperands(node, prefix, mappingInfo);

        // CompareTo is usually used for comparison, here it is simplified to equality comparison
        // If you need more complex comparison logic, you can extend it here
        return BuildEquality(field, value);
    }

    /// <summary>
    /// Translate string.IndexOf
    /// </summary>
    private Query TranslateStringIndexOf(MethodCallExpression node, string? prefix, IndexMappingInfo? mappingInfo)
    {
        var (field, value) = GetStringMethodOperands(node, prefix, mappingInfo);
        var fieldMapping = mappingInfo?.GetField(field.Path);

        // IndexOf >= 0 is equivalent to Contains
        return TranslateStringContains(field, fieldMapping, value);
    }

    /// <summary>
    /// Translate Contains
    /// </summary>
    private Query TranslateStringContains(FieldInfo field, FieldMappingInfo? fieldMapping, string value)
    {
        // 1. Wildcard type - use wildcard query directly (optimal)
        if (field.IsWildcard || fieldMapping?.IsWildcard == true)
        {
            return new WildcardQuery
            {
                Field = field.Path,
                Value = "*" + EscapeWildcard(value) + "*"
            };
        }

        // 2. Keyword type - use wildcard query
        if (field.IsKeyword || fieldMapping?.IsKeyword == true)
        {
            return new WildcardQuery
            {
                Field = field.Path,
                Value = "*" + EscapeWildcard(value) + "*"
            };
        }

        // 3. Text type
        if (field.IsText || fieldMapping?.IsText == true)
        {
            // 3.1 If there is a keyword subfield, use .keyword for wildcard query
            if (fieldMapping?.Properties?.ContainsKey("keyword") == true)
            {
                return new WildcardQuery
                {
                    Field = $"{field.Path}.keyword",
                    Value = "*" + EscapeWildcard(value) + "*"
                };
            }

            // 3.2 There is no keyword subfield, use MatchPhrase for full-text search
            // Note: This is not exact Contains, but a phrase match after word segmentation
            return new MatchPhraseQuery
            {
                Field = field.Path,
                Query = value
            };
        }

        // 4. Default - try to use wildcards
        return new WildcardQuery
        {
            Field = field.Path,
            Value = "*" + EscapeWildcard(value) + "*"
        };
    }

    /// <summary>
    /// Translation string.IsNullOrEmpty
    /// </summary>
    private Query TranslateStringIsNullOrEmpty(MethodCallExpression node, string? prefix, IndexMappingInfo? mappingInfo)
    {
        var field = ResolveField(node.Arguments[0], prefix, mappingInfo);

        // field == null || field == ""
        return new BoolQuery
        {
            Should = new Query[]
            {
                // null or does not exist
                new BoolQuery
                {
                    MustNot = new Query[] { new ExistsQuery { Field = field.Path } }
                },
                // empty string
                new TermQuery
                {
                    Field = field.Path,
                    Value = string.Empty
                }
            },
            MinimumShouldMatch = 1
        };
    }

    /// <summary>
    /// Translation string.IsNullOrWhiteSpace
    /// </summary>
    private Query TranslateStringIsNullOrWhiteSpace(MethodCallExpression node, string? prefix, IndexMappingInfo? mappingInfo)
    {
        var field = ResolveField(node.Arguments[0], prefix, mappingInfo);

        // field == null || field == "" || Regex.IsMatch(field, @"^\s*$")
        return new BoolQuery
        {
            Should = new Query[]
            {
                // null or does not exist
                new BoolQuery
                {
                    MustNot = new Query[] { new ExistsQuery { Field = field.Path } }
                },
                // empty string
                new TermQuery
                {
                    Field = field.Path,
                    Value = string.Empty
                },
                // Use regular expressions to match strings with only whitespace characters
                new RegexpQuery
                {
                    Field = field.Path,
                    Value = @"^\s*$"
                }
            },
            MinimumShouldMatch = 1
        };
    }

    /// <summary>
    /// Translation string.CompareTo comparison
    /// </summary>
    private Query? TranslateStringCompareToComparison(
        MethodCallExpression methodCall,
        Expression constantExpression,
        ExpressionType comparisonType,
        string? prefix,
        IndexMappingInfo? mappingInfo)
    {
        var (field, compareValue) = GetStringMethodOperands(methodCall, prefix, mappingInfo);
        var compareResult = Convert.ToInt32(Evaluate(constantExpression));

        switch (comparisonType)
        {
            case ExpressionType.Equal when compareResult == 0:
                // CompareTo == 0 means equal
                return BuildEquality(field, compareValue);

            case ExpressionType.NotEqual when compareResult == 0:
                // CompareTo != 0 means not equal
                return BuildNotEqualQuery(field, compareValue);

            case ExpressionType.GreaterThan when compareResult == 0:
                // CompareTo > 0 means the current field value is greater than the comparison value
                return BuildRange(field, greaterThan: compareValue);

            case ExpressionType.GreaterThanOrEqual when compareResult == 0:
                // CompareTo >= 0 means the current field value is greater than or equal to the comparison value
                return BuildRange(field, greaterThanOrEqualTo: compareValue);

            case ExpressionType.LessThan when compareResult == 0:
                // CompareTo < 0 means the current field value is less than the comparison value
                return BuildRange(field, lessThan: compareValue);

            case ExpressionType.LessThanOrEqual when compareResult == 0:
                // CompareTo <= 0 means the current field value is less than or equal to the comparison value
                return BuildRange(field, lessThanOrEqualTo: compareValue);

            default:
                return null;
        }
    }

    /// <summary>
    /// Translation string.IndexOf comparison
    /// </summary>
    private Query? TranslateStringIndexOfComparison(
        MethodCallExpression methodCall,
        Expression constantExpression,
        ExpressionType comparisonType,
        string? prefix,
        IndexMappingInfo? mappingInfo)
    {
        // Get field and search values
        var (field, searchValue) = GetStringMethodOperands(methodCall, prefix, mappingInfo);
        var indexResult = Convert.ToInt32(Evaluate(constantExpression));
        var fieldMapping = mappingInfo?.GetField(field.Path);

        switch (comparisonType)
        {
            case ExpressionType.GreaterThanOrEqual when indexResult >= 0:
            case ExpressionType.GreaterThan when indexResult > -1:
            case ExpressionType.NotEqual when indexResult == -1:
                // IndexOf >= 0, IndexOf > -1, IndexOf != -1 means inclusion
                return TranslateStringContains(field, fieldMapping, searchValue);

            case ExpressionType.LessThan when indexResult <= 0:
            case ExpressionType.Equal when indexResult == -1:
                // IndexOf == -1, IndexOf < 0 means not included
                var containsQuery = TranslateStringContains(field, fieldMapping, searchValue);
                return new BoolQuery
                {
                    MustNot = new Query[] { containsQuery }
                };

            default:
                return null;
        }
    }

    /// <summary>
    /// Translate string instance method (get fields and values)
    /// </summary>
    private (FieldInfo Field, string Value) GetStringMethodOperands(MethodCallExpression node, string? prefix, IndexMappingInfo? mappingInfo)
    {
        Expression fieldExpression;
        Expression valueExpression;

        if (node.Object != null)
        {
            // Instance method: obj.Method(value)
            fieldExpression = node.Object;
            valueExpression = node.Arguments[0];
        }
        else
        {
            // Static method: string.Method(field, value)
            fieldExpression = node.Arguments[0];
            valueExpression = node.Arguments.Count > 1 ? node.Arguments[1] : node.Arguments[0];
        }

        var field = ResolveField(fieldExpression, prefix, mappingInfo);
        var value = Evaluate(valueExpression)?.ToString() ?? string.Empty;

        return (field, value);
    }
}
