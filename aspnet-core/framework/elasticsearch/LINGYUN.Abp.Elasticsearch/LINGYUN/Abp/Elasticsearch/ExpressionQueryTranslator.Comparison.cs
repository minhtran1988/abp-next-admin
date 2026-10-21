using Elastic.Clients.Elasticsearch.QueryDsl;
using System;
using System.Linq.Expressions;

namespace LINGYUN.Abp.Elasticsearch;

public partial class ExpressionQueryTranslator
{
    /// <summary>
    /// Attempt to translate comparison expressions containing method calls
    /// </summary>
    private Query? TryTranslateMethodComparison(BinaryExpression node, string? prefix, IndexMappingInfo? mappingInfo)
    {
        // Process expressions such as (field.Method(value) == 0) or (field.Method(value) >= 0)
        if (node.Left is MethodCallExpression leftMethodCall)
        {
            return TranslateMethodComparison(leftMethodCall, node.Right, node.NodeType, prefix, mappingInfo);
        }

        // Process expressions such as (0 == field.Method(value)) or (0 <= field.Method(value))
        if (node.Right is MethodCallExpression rightMethodCall)
        {
            // Invert comparison operator
            var reversedNodeType = ReverseComparisonOperator(node.NodeType);
            return TranslateMethodComparison(rightMethodCall, node.Left, reversedNodeType, prefix, mappingInfo);
        }

        return null;
    }

    /// <summary>
    /// Translation method call comparison
    /// </summary>
    private Query? TranslateMethodComparison(
        MethodCallExpression methodCall,
        Expression constantExpression,
        ExpressionType comparisonType,
        string? prefix,
        IndexMappingInfo? mappingInfo)
    {
        // Limit string method calls
        if (methodCall.Method.DeclaringType == typeof(string))
        {
            return methodCall.Method.Name switch
            {
                nameof(string.CompareTo) => TranslateStringCompareToComparison(methodCall, constantExpression, comparisonType, prefix, mappingInfo),
                nameof(string.IndexOf) => TranslateStringIndexOfComparison(methodCall, constantExpression, comparisonType, prefix, mappingInfo),
                _ => null,
            };
        }
        return null;
    }

    /// <summary>
    /// Invert comparison operator
    /// </summary>
    private ExpressionType ReverseComparisonOperator(ExpressionType nodeType)
    {
        return nodeType switch
        {
            ExpressionType.GreaterThan => ExpressionType.LessThan,
            ExpressionType.GreaterThanOrEqual => ExpressionType.LessThanOrEqual,
            ExpressionType.LessThan => ExpressionType.GreaterThan,
            ExpressionType.LessThanOrEqual => ExpressionType.GreaterThanOrEqual,
            _ => nodeType
        };
    }
}
