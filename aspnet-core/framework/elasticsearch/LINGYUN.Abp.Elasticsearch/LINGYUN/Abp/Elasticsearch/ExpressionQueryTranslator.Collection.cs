using Elastic.Clients.Elasticsearch.QueryDsl;
using System;
using System.Collections;
using System.Linq;
using System.Linq.Expressions;

namespace LINGYUN.Abp.Elasticsearch;

public partial class ExpressionQueryTranslator
{
    /// <summary>
    /// Translation collection Contains
    /// </summary>
    private Query TranslateCollectionContains(MethodCallExpression node, string? prefix, IndexMappingInfo? mappingInfo)
    {
        // list.Contains(value) or hashSet.Contains(value)
        // There are two situations:
        // 1. External collection.Contains(field) - For example: new[]{"a","b"}.Contains(x.xxx)
        // 2. Field.Contains(value) - For example: x.Y.Contains("B")

        Expression collectionExpr = node.Object!;
        Expression valueExpr = node.Arguments[0];

        // Determine which is the field and which is the value
        var collectionIsField = IsFieldLike(collectionExpr);
        var valueIsField = IsFieldLike(valueExpr);

        if (collectionIsField && !valueIsField)
        {
            // Case 2: Field.Contains(value)
            // This is usually used for collection fields, such as x.Tags.Contains("B")
            var field = ResolveField(collectionExpr, prefix, mappingInfo);
            var value = Evaluate(valueExpr);

            if (value == null)
            {
                return new MatchNoneQuery();
            }

            // For collection fields, use Terms query or Term query
            // If the field is an array or collection type, the Contains of a single value is actually a Term query
            return BuildEquality(field, value);
        }
        else if (!collectionIsField && valueIsField)
        {
            // Case 1: External collection.Contains(field)
            var field = ResolveField(valueExpr, prefix, mappingInfo);

            // Try to get the value of the collection
            var collectionValue = Evaluate(collectionExpr);

            if (collectionValue is IEnumerable enumerable && collectionValue is not string)
            {
                var values = enumerable.Cast<object>().Select(NormalizeValue).ToList();
                if (values.Count == 0)
                {
                    return new MatchNoneQuery();
                }
                if (values.Count == 1)
                {
                    // If the collection has only one value, use TermQuery
                    return BuildEquality(field, values[0]);
                }
                // Use TermsQuery for multiple values
                return new TermsQuery { Field = field.Path, Terms = new TermsQueryField(values) };
            }

            // If the collection value cannot be obtained, use the default processing
            var defaultValue = Evaluate(valueExpr);
            return BuildEquality(field, defaultValue!);
        }
        else if (collectionIsField && valueIsField)
        {
            // Both are fields, this situation is rare
            throw new NotSupportedException($"Unsupported Contains with two field expressions: {node}");
        }
        else
        {
            // Neither of them are fields, try to evaluate them directly
            var collectionValue = Evaluate(collectionExpr);
            var value = Evaluate(valueExpr);

            if (collectionValue is IEnumerable enumerable && collectionValue is not string)
            {
                if (enumerable.Cast<object>().Contains(value))
                {
                    return new MatchAllQuery();
                }
                else
                {
                    return new MatchNoneQuery();
                }
            }

            throw new NotSupportedException($"Unsupported Contains expression: {node}");
        }
    }
}
