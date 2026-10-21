using Elastic.Clients.Elasticsearch;
using Elastic.Clients.Elasticsearch.QueryDsl;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.DependencyInjection;

namespace LINGYUN.Abp.Elasticsearch;

/// <summary>
/// Expression Query Converter - Converts LINQ expressions to Elasticsearch Query
/// </summary>
public partial class ExpressionQueryTranslator : IExpressionQueryTranslator, ISingletonDependency
{
    private readonly IIndexMappingProvider _indexMappingProvider;

    public ExpressionQueryTranslator(
        IIndexMappingProvider indexMappingProvider)
    {
        _indexMappingProvider = indexMappingProvider;
    }

    /// <summary>
    /// Translation expression
    /// </summary>
    public async virtual Task<Query> TranslateAsync<TDocument>(string indexName, Expression<Func<TDocument, bool>> expression)
    {
        Check.NotNullOrWhiteSpace(indexName, nameof(indexName));
        Check.NotNull(expression, nameof(expression));

        var indexMapping = await _indexMappingProvider.GetMappingAsync<TDocument>(indexName);

        return TranslateNode(expression.Body, prefix: null, indexMapping);
    }

    #region node translation

    /// <summary>
    /// Translation expression node
    /// </summary>
    protected virtual Query TranslateNode(Expression node, string? prefix, IndexMappingInfo? mappingInfo)
    {
        return node switch
        {
            ConstantExpression { Value: bool value } =>
                value ? new MatchAllQuery() : new MatchNoneQuery(),

            UnaryExpression { NodeType: ExpressionType.Not } unary =>
                Negate(TranslateNode(unary.Operand, prefix, mappingInfo)),

            UnaryExpression { NodeType: ExpressionType.Convert or ExpressionType.ConvertChecked } unary =>
                TranslateNode(unary.Operand, prefix, mappingInfo),

            BinaryExpression binary => TranslateBinary(binary, prefix, mappingInfo),

            MethodCallExpression method => TranslateMethodCall(method, prefix, mappingInfo),

            MemberExpression member when IsHasValueAccess(member) =>
                new ExistsQuery { Field = ResolveField(member.Expression!, prefix, mappingInfo).Path },

            MemberExpression member when member.Type == typeof(bool) =>
                new TermQuery { Field = ResolveField(member, prefix, mappingInfo).Path, Value = true },

            MemberExpression member when IsNullableHasValue(member) =>
                new ExistsQuery { Field = ResolveField(member.Expression!, prefix, mappingInfo).Path },

            MemberExpression member when IsNullableValueAccess(member) =>
                TranslateNode(member.Expression!, prefix, mappingInfo),

            _ => throw new NotSupportedException($"Unsupported expression node {node.NodeType}: {node}"),
        };
    }
    #endregion

    #region binary expression translation

    /// <summary>
    /// Translate binary expressions
    /// </summary>
    protected virtual Query TranslateBinary(BinaryExpression node, string? prefix, IndexMappingInfo? mappingInfo)
    {
        return node.NodeType switch
        {
            ExpressionType.AndAlso or ExpressionType.And => TranslateAndAlso(node, prefix, mappingInfo),

            ExpressionType.OrElse or ExpressionType.Or => TranslateOrElse(node, prefix, mappingInfo),

            ExpressionType.Equal => TranslateComparison(node, prefix, mappingInfo),

            ExpressionType.NotEqual => TranslateComparison(node, prefix, mappingInfo),

            ExpressionType.GreaterThan or ExpressionType.GreaterThanOrEqual or
            ExpressionType.LessThan or ExpressionType.LessThanOrEqual => TranslateComparison(node, prefix, mappingInfo),

            _ => throw new NotSupportedException($"Unsupported binary operator {node.NodeType}: {node}"),
        };
    }


    /// <summary>
    /// Build And query (flatten BoolQuery)
    /// </summary>
    private Query TranslateAndAlso(BinaryExpression node, string? prefix, IndexMappingInfo? mappingInfo)
    {
        var filters = new List<Query>();

        // Collect all Filters on the left
        CollectFilters(node.Left, prefix, mappingInfo, filters);

        // Collect the right Filter
        CollectFilters(node.Right, prefix, mappingInfo, filters);

        return new BoolQuery
        {
            Filter = filters.ToArray()
        };
    }

    /// <summary>
    /// Recursive collection Filter query
    /// </summary>
    private void CollectFilters(Expression node, string? prefix, IndexMappingInfo? mappingInfo, List<Query> filters)
    {
        if (node is BinaryExpression binary &&
            (binary.NodeType == ExpressionType.AndAlso || binary.NodeType == ExpressionType.And))
        {
            // Recursively collect left and right child nodes
            CollectFilters(binary.Left, prefix, mappingInfo, filters);
            CollectFilters(binary.Right, prefix, mappingInfo, filters);
        }
        else
        {
            // Non-And expressions, translated directly and added to the list
            var query = TranslateNode(node, prefix, mappingInfo);

            // If the translation result is a BoolQuery and there is a Filter, flatten it
            if (query.Bool != null && query.Bool.Filter != null && query.Bool.Filter.Count > 0)
            {
                foreach (var subQuery in query.Bool.Filter)
                {
                    filters.Add(subQuery);
                }
            }
            else if (query.Bool != null && query.Bool.Must != null && query.Bool.Must.Count > 0)
            {
                foreach (var subQuery in query.Bool.Must)
                {
                    filters.Add(subQuery);
                }
            }
            else
            {
                filters.Add(query);
            }
        }
    }

    /// <summary>
    /// Build Or query (flatten BoolQuery)
    /// </summary>
    private Query TranslateOrElse(BinaryExpression node, string? prefix, IndexMappingInfo? mappingInfo)
    {
        var shouldQueries = new List<Query>();

        CollectShouldQueries(node.Left, prefix, mappingInfo, shouldQueries);
        CollectShouldQueries(node.Right, prefix, mappingInfo, shouldQueries);

        return new BoolQuery
        {
            Should = shouldQueries.ToArray(),
            MinimumShouldMatch = 1
        };
    }

    /// <summary>
    /// Recursive collection Should query
    /// </summary>
    private void CollectShouldQueries(Expression node, string? prefix, IndexMappingInfo? mappingInfo, List<Query> shouldQueries)
    {
        if (node is BinaryExpression binary &&
            (binary.NodeType == ExpressionType.OrElse || binary.NodeType == ExpressionType.Or))
        {
            CollectShouldQueries(binary.Left, prefix, mappingInfo, shouldQueries);
            CollectShouldQueries(binary.Right, prefix, mappingInfo, shouldQueries);
        }
        else
        {
            var query = TranslateNode(node, prefix, mappingInfo);

            // If the translation result is a BoolQuery and has Should, flatten it
            if (query.Bool != null && query.Bool.Should != null && query.Bool.Should.Count > 0)
            {
                foreach (var subQuery in query.Bool.Should)
                {
                    shouldQueries.Add(subQuery);
                }
            }
            else
            {
                shouldQueries.Add(query);
            }
        }
    }

    /// <summary>
    /// Translate comparison expressions
    /// </summary>
    protected virtual Query TranslateComparison(BinaryExpression node, string? prefix, IndexMappingInfo? mappingInfo)
    {
        var methodCallQuery = TryTranslateMethodComparison(node, prefix, mappingInfo);
        if (methodCallQuery != null)
        {
            return methodCallQuery;
        }

        var (fieldExpression, valueExpression) = ResolveOperands(node);
        var field = ResolveField(fieldExpression, prefix, mappingInfo);

        // handle null comparisons
        if (IsNullConstant(valueExpression))
        {
            return node.NodeType == ExpressionType.NotEqual
                ? new ExistsQuery { Field = field.Path }
                : new BoolQuery
                {
                    MustNot = new Query[] { new ExistsQuery { Field = field.Path } }
                };
        }

        var value = Evaluate(valueExpression);

        if (value == null)
        {
            throw new NotSupportedException("The null value is only supported for the == null / != null comparison.");
        }

        // Process collection contains
        if (field.Type.IsArray || (field.Type.IsGenericType && typeof(IEnumerable).IsAssignableFrom(field.Type)))
        {
            return BuildTermsQuery(field, value);
        }

        return node.NodeType switch
        {
            ExpressionType.Equal => BuildEquality(field, value),
            ExpressionType.NotEqual => BuildNotEqualQuery(field, value),
            ExpressionType.GreaterThan => BuildRange(field, greaterThan: value),
            ExpressionType.GreaterThanOrEqual => BuildRange(field, greaterThanOrEqualTo: value),
            ExpressionType.LessThan => BuildRange(field, lessThan: value),
            ExpressionType.LessThanOrEqual => BuildRange(field, lessThanOrEqualTo: value),
            _ => throw new NotSupportedException($"Unsupported comparison operator {node.NodeType}"),
        };
    }

    /// <summary>
    /// Parse the operand and determine the field and value
    /// </summary>
    private static (Expression Field, Expression Value) ResolveOperands(BinaryExpression node)
    {
        var leftIsField = IsFieldLike(node.Left);
        var rightIsField = IsFieldLike(node.Right);

        if (leftIsField == rightIsField)
        {
            throw new NotSupportedException($"The comparison expression must have one side as a field and the other side as a value: {node}");
        }

        return leftIsField ? (node.Left, node.Right) : (node.Right, node.Left);
    }

    /// <summary>
    /// Determine whether the expression is a field
    /// </summary>
    private static bool IsFieldLike(Expression expression)
    {
        var current = expression;
        while (current is UnaryExpression { NodeType: ExpressionType.Convert or ExpressionType.ConvertChecked } unary)
        {
            current = unary.Operand;
        }

        while (current is MemberExpression member)
        {
            current = member.Expression!;
        }

        return current is ParameterExpression;
    }

    /// <summary>
    /// Determine whether the expression is a null constant
    /// </summary>
    private static bool IsNullConstant(Expression expression)
    {
        while (expression is UnaryExpression { NodeType: ExpressionType.Convert or ExpressionType.ConvertChecked } unary)
        {
            expression = unary.Operand;
        }

        return expression is ConstantExpression { Value: null };
    }

    #endregion

    #region method call translation

    /// <summary>
    /// Negation query
    /// </summary>
    private static Query Negate(Query query)
    {
        // Check if it's a BoolQuery (via Bool property)
        if (query.Bool != null)
        {
            var boolQuery = query.Bool;
            var newBool = new BoolQuery();

            // MustNot -> Must
            if (boolQuery.MustNot != null && boolQuery.MustNot.Count > 0)
            {
                newBool.Must = boolQuery.MustNot.ToArray();
                return newBool;
            }

            // Must -> MustNot
            if (boolQuery.Must != null && boolQuery.Must.Count > 0)
            {
                newBool.MustNot = boolQuery.Must.ToArray();
                return newBool;
            }

            // Filter -> MustNot
            if (boolQuery.Filter != null && boolQuery.Filter.Count > 0)
            {
                newBool.MustNot = boolQuery.Filter.ToArray();
                return newBool;
            }

            // Should -> MustNot
            if (boolQuery.Should != null && boolQuery.Should.Count > 0)
            {
                newBool.MustNot = boolQuery.Should.ToArray();
                return newBool;
            }

            // empty BoolQuery -> MatchAll
            return new MatchAllQuery();
        }

        // For ExistsQuery, the negation becomes MustNot + Exists
        if (query.Exists != null)
        {
            return new BoolQuery
            {
                MustNot = new Query[] { query.Exists }
            };
        }

        // For MatchAllQuery, it becomes MatchNoneQuery after negation
        if (query.MatchAll != null)
        {
            return new MatchNoneQuery();
        }

        // For MatchNoneQuery, it becomes MatchAllQuery after negation
        if (query.MatchNone != null)
        {
            return new MatchAllQuery();
        }

        // For TermQuery, it becomes MustNot + Term after negation
        if (query.Term != null)
        {
            if (query.Term.Value.IsBool && query.Term.Value.TryGetBool(out var boolValue))
            {
                return new TermQuery
                {
                    Field = query.Term.Field,
                    Value = FieldValue.FromValue(!boolValue)
                };
            }
            return new BoolQuery
            {
                MustNot = new Query[] { query.Term }
            };
        }

        // For WildcardQuery, the inversion becomes MustNot + Wildcard
        if (query.Wildcard != null)
        {
            return new BoolQuery
            {
                MustNot = new Query[] { query.Wildcard }
            };
        }

        // For MatchQuery, the inversion becomes MustNot + Match
        if (query.Match != null)
        {
            return new BoolQuery
            {
                MustNot = new Query[] { query.Match }
            };
        }

        // For MatchPhraseQuery, the negation becomes MustNot + MatchPhrase
        if (query.MatchPhrase != null)
        {
            return new BoolQuery
            {
                MustNot = new Query[] { query.MatchPhrase }
            };
        }

        // For MatchPhrasePrefixQuery, the inversion becomes MustNot + MatchPhrasePrefix
        if (query.MatchPhrasePrefix != null)
        {
            return new BoolQuery
            {
                MustNot = new Query[] { query.MatchPhrasePrefix }
            };
        }

        // for NumberRangeQuery
        if (query.Range != null && query.Range is NumberRangeQuery numberRange)
        {
            return new BoolQuery
            {
                MustNot = new Query[] { numberRange }
            };
        }

        // for DateRangeQuery
        if (query.Range != null && query.Range is DateRangeQuery dateRange)
        {
            return new BoolQuery
            {
                MustNot = new Query[] { dateRange }
            };
        }

        // for TermsQuery
        if (query.Terms != null)
        {
            return new BoolQuery
            {
                MustNot = new Query[] { query.Terms }
            };
        }

        // for NestedQuery
        if (query.Nested != null)
        {
            return new BoolQuery
            {
                MustNot = new Query[] { query.Nested }
            };
        }

        // for ScriptQuery
        if (query.Script != null)
        {
            return new BoolQuery
            {
                MustNot = new Query[] { query.Script }
            };
        }

        // Default: For unknown types, use MustNot wrapper
        return new BoolQuery
        {
            MustNot = new Query[] { query }
        };
    }

    /// <summary>
    /// Translation method call expression
    /// </summary>
    protected virtual Query TranslateMethodCall(MethodCallExpression node, string? prefix, IndexMappingInfo? mappingInfo)
    {
        // Enumerable.Any / Enumerable.Contains / Enumerable.All
        if (node.Method.DeclaringType == typeof(Enumerable))
        {
            return TranslateEnumerableMethod(node, prefix, mappingInfo);
        }

        // string method
        if (node.Method.DeclaringType == typeof(string))
        {
            return TranslateStringMethod(node, prefix, mappingInfo);
        }

        // Enum.HasFlag
        if (node.Method.Name == nameof(Enum.HasFlag))
        {
            return TranslateEnumHasFlag(node, prefix, mappingInfo);
        }

        // List/Collection.Contains (instance method)
        if (node.Method.Name == nameof(ICollection<>.Contains) && node.Object != null)
        {
            return TranslateCollectionContains(node, prefix, mappingInfo);
        }

        throw new NotSupportedException(
            $"Unsupported method invocation {node.Method.DeclaringType?.Name}.{node.Method.Name}");
    }

    #endregion

    #region Query Building

    /// <summary>
    /// Construct an equality query
    /// </summary>
    protected virtual Query BuildEquality(FieldInfo field, object value)
    {
        var fieldName = field.Path;

        // 1. Wildcard field - use Term query
        if (field.IsWildcard)
        {
            return new TermQuery
            {
                Field = fieldName,
                Value = value.ToString()!
            };
        }

        // 2. Keyword field or string
        if (field.IsKeyword || field.Type == typeof(string))
        {
            // If the enumeration type is stored as a string, it needs to be converted.
            var stringValue = field.Type.IsEnum
                ? NormalizeEnumName(field.Type, value)
                : value.ToString()!;
            return new TermQuery { Field = fieldName, Value = stringValue };
        }

        // 3. Date field
        if (field.IsDate || field.Type == typeof(DateTime) || field.Type == typeof(DateTime?))
        {
            var date = (DateTime)value;
            return new DateRangeQuery
            {
                Field = fieldName,
                Gte = date,
                Lte = date,
                Format = field.Format ?? "yyyy-MM-dd HH:mm:ss||yyyy-MM-dd||epoch_millis"
            };
        }

        // 4. Numeric field
        if (field.IsNumeric || IsNumericType(field.Type))
        {
            return new TermQuery { Field = fieldName, Value = NormalizeValue(value) };
        }

        // 5. Boolean field
        if (field.IsBoolean || field.Type == typeof(bool) || field.Type == typeof(bool?))
        {
            return new TermQuery { Field = fieldName, Value = (bool)value };
        }

        // 6. Enumeration (value as numeric value)
        if (field.Type.IsEnum)
        {
            return new TermQuery { Field = fieldName, Value = Convert.ToInt64(value) };
        }

        // 7. If the value is an enumeration but the field type is not an enumeration
        if (value.GetType().IsEnum)
        {
            return new TermQuery { Field = fieldName, Value = Convert.ToInt64(value) };
        }

        // 8. Text field - using MatchQuery
        if (field.IsText)
        {
            return new MatchQuery
            {
                Field = fieldName,
                Query = value.ToString()!
            };
        }

        // 9. Default
        return new TermQuery { Field = fieldName, Value = NormalizeValue(value) };
    }

    /// <summary>
    /// Build NotEqual query
    /// </summary>
    private Query BuildNotEqualQuery(FieldInfo field, object value)
    {
        // For Boolean values, directly negate the value (more concise)
        if (value is bool boolValue)
        {
            return new TermQuery { Field = field.Path, Value = !boolValue };
        }

        // For other types, use the must_not wrapper
        return new BoolQuery
        {
            MustNot = new Query[] { BuildEquality(field, value) }
        };
    }

    /// <summary>
    /// Build Terms query (for collection inclusion)
    /// </summary>
    protected virtual Query BuildTermsQuery(FieldInfo field, object value)
    {
        var fieldName = field.Path;

        if (value is IEnumerable enumerable && value is not string)
        {
            var values = enumerable.Cast<object>().Select(NormalizeValue).ToList();
            if (values.Count == 0)
            {
                return new MatchNoneQuery();
            }
            if (values.Count == 1)
            {
                return BuildEquality(field, values[0]);
            }
            return new TermsQuery { Field = fieldName, Terms = new TermsQueryField(values) };
        }

        return BuildEquality(field, value);
    }

    /// <summary>
    /// Build range query
    /// </summary>
    protected virtual Query BuildRange(
        FieldInfo field,
        object? greaterThan = null,
        object? greaterThanOrEqualTo = null,
        object? lessThan = null,
        object? lessThanOrEqualTo = null)
    {
        var fieldName = field.Path;

        // date range
        if (field.IsDate || field.Type == typeof(DateTime) || field.Type == typeof(DateTime?))
        {
            var range = new DateRangeQuery
            {
                Field = fieldName,
                Format = field.Format ?? "yyyy-MM-dd HH:mm:ss||yyyy-MM-dd||epoch_millis"
            };

            if (greaterThan != null)
            {
                range.Gt = DateMath.FromString(NormalizeDate(greaterThan));
            }
            if (greaterThanOrEqualTo != null)
            {
                range.Gte = DateMath.FromString(NormalizeDate(greaterThanOrEqualTo));
            }
            if (lessThan != null)
            {
                range.Lt = DateMath.FromString(NormalizeDate(lessThan));
            }
            if (lessThanOrEqualTo != null)
            {
                range.Lte = DateMath.FromString(NormalizeDate(lessThanOrEqualTo));
            }

            return range;
        }

        // Number range
        var numberRange = new NumberRangeQuery { Field = fieldName };

        if (greaterThan != null)
        {
            numberRange.Gt = ToNumber(greaterThan);
        }
        if (greaterThanOrEqualTo != null)
        {
            numberRange.Gte = ToNumber(greaterThanOrEqualTo);
        }
        if (lessThan != null)
        {
            numberRange.Lt = ToNumber(lessThan);
        }
        if (lessThanOrEqualTo != null)
        {
            numberRange.Lte = ToNumber(lessThanOrEqualTo);
        }

        return numberRange;
    }


    #endregion

    #region helper method

    /// <summary>
    /// Unpack the lambda expression
    /// </summary>
    private static LambdaExpression UnwrapLambda(Expression expression)
    {
        while (expression is UnaryExpression { NodeType: ExpressionType.Quote } unary)
        {
            expression = unary.Operand;
        }
        return (LambdaExpression)expression;
    }

    /// <summary>
    /// Evaluate expression
    /// </summary>
    private static object? Evaluate(Expression expression)
    {
        // If it is a constant expression, return the value directly
        if (expression is ConstantExpression constant)
        {
            return constant.Value;
        }

        // If it is a parameter expression, it cannot be evaluated
        if (expression is ParameterExpression)
        {
            throw new InvalidOperationException($"Cannot evaluate parameter expression: {expression}");
        }

        // Handle member access (capture external variables)
        if (expression is MemberExpression memberExpr)
        {
            // Check if it is a field or attribute access
            if (memberExpr.Member is System.Reflection.FieldInfo fieldInfo)
            {
                // If it is a static field
                if (fieldInfo.IsStatic)
                {
                    return fieldInfo.GetValue(null);
                }

                // If it is an instance field, the instance needs to be evaluated first
                if (memberExpr.Expression != null)
                {
                    var obj = Evaluate(memberExpr.Expression);
                    if (obj != null)
                    {
                        return fieldInfo.GetValue(obj);
                    }
                }

                // If it cannot be evaluated, try to compile the entire expression
                try
                {
                    return Expression.Lambda(memberExpr).Compile().DynamicInvoke();
                }
                catch
                {
                    throw new InvalidOperationException($"Cannot evaluate member expression: {memberExpr}");
                }
            }

            if (memberExpr.Member is PropertyInfo propertyInfo)
            {
                // If it is a static property
                var getMethod = propertyInfo.GetGetMethod();
                if (getMethod != null && getMethod.IsStatic)
                {
                    return propertyInfo.GetValue(null);
                }

                // If it is an instance attribute, the instance needs to be evaluated first
                if (memberExpr.Expression != null)
                {
                    var obj = Evaluate(memberExpr.Expression);
                    if (obj != null)
                    {
                        return propertyInfo.GetValue(obj);
                    }
                }

                // If it cannot be evaluated, try to compile the entire expression
                try
                {
                    return Expression.Lambda(memberExpr).Compile().DynamicInvoke();
                }
                catch
                {
                    throw new InvalidOperationException($"Cannot evaluate member expression: {memberExpr}");
                }
            }
        }

        // handle array index
        if (expression is IndexExpression indexExpr)
        {
            var obj = Evaluate(indexExpr.Object!);
            var indices = indexExpr.Arguments.Select(Evaluate).ToArray();
            if (obj is Array array && indices.Length == 1 && indices[0] is int index)
            {
                return array.GetValue(index);
            }
        }

        // Handle method calls
        if (expression is MethodCallExpression methodCall)
        {
            try
            {
                // Try to compile and execute
                var lambda = Expression.Lambda(methodCall);
                return lambda.Compile().DynamicInvoke();
            }
            catch (InvalidOperationException)
            {
                // If it contains parameter references, it cannot be compiled
                throw;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Cannot evaluate method call: {methodCall}", ex);
            }
        }

        // Process NewArrayExpression
        if (expression is NewArrayExpression newArray)
        {
            var values = newArray.Expressions.Select(Evaluate).ToArray();
            return values;
        }

        // Try to compile and execute by default

        return Expression.Lambda(expression).Compile().DynamicInvoke();
    }

    /// <summary>
    /// Escape wildcard characters
    /// </summary>
    private static string EscapeWildcard(string value)
    {
        return value.Replace("\\", "\\\\").Replace("*", "\\*").Replace("?", "\\?");
    }

    /// <summary>
    /// Normalized value
    /// </summary>
    private static FieldValue NormalizeValue(object value)
    {
        return value switch
        {
            string s => s,
            bool b => b,
            int i => i,
            long l => l,
            double d => d,
            float f => f,
            decimal dec => Convert.ToDouble(dec),
            Guid g => g.ToString(),
            Enum e => Convert.ToInt64(e),
            DateTime dt => NormalizeDate(dt),
            DateTimeOffset dto => NormalizeDate(dto),
            _ => Convert.ToDouble(value)
        };
    }

    /// <summary>
    /// Normalized date value
    /// </summary>
    private static string NormalizeDate(object value)
    {
        if (value is DateTime dateTime)
        {
            // Format to a format supported by Elasticsearch (without time zone)
            return dateTime.ToString("yyyy-MM-dd HH:mm:ss");
        }

        if (value is DateTimeOffset dateTimeOffset)
        {
            return dateTimeOffset.ToString("yyyy-MM-dd HH:mm:ss");
        }

        return value.ToString()!;
    }
    /// <summary>
    /// Convert to Number
    /// </summary>
    private static Number? ToNumber(object? value)
    {
        return value == null
            ? null
            : value switch
        {
            Number n => n,
            sbyte v => (Number)v,
            byte v => (Number)v,
            short v => (Number)v,
            ushort v => (Number)v,
            int v => (Number)v,
            uint v => (Number)v,
            long v => (Number)v,
            ulong v => (Number)v,
            float v => (Number)v,
            double v => (Number)v,
            decimal v => (Number)(double)v,
            _ => (Number)Convert.ToDouble(value)
        };
    }

    /// <summary>
    /// Normalized enumeration name
    /// </summary>
    private static string NormalizeEnumName(Type enumType, object value)
    {
        var enumValue = value is Enum e
            ? e
            : (Enum)Enum.ToObject(enumType, value);
        return enumValue.ToString();
    }

    /// <summary>
    /// Determine whether it is a numeric type
    /// </summary>
    private static bool IsNumericType(Type type)
    {
        var underlyingType = Nullable.GetUnderlyingType(type) ?? type;
        return underlyingType == typeof(byte) || underlyingType == typeof(sbyte) ||
               underlyingType == typeof(short) || underlyingType == typeof(ushort) ||
               underlyingType == typeof(int) || underlyingType == typeof(uint) ||
               underlyingType == typeof(long) || underlyingType == typeof(ulong) ||
               underlyingType == typeof(float) || underlyingType == typeof(double) ||
               underlyingType == typeof(decimal);
    }

    /// <summary>
    /// Determine whether it is Nullable.HasValue access
    /// </summary>
    private static bool IsNullableHasValue(MemberExpression member)
    {
        return member.Member.Name == nameof(Nullable<>.HasValue)
               && member.Expression is MemberExpression
               && member.Member.DeclaringType?.IsGenericType == true
               && member.Member.DeclaringType.GetGenericTypeDefinition() == typeof(Nullable<>);
    }

    /// <summary>
    /// Determine whether it is Nullable.Value access
    /// </summary>
    private static bool IsNullableValueAccess(MemberExpression member)
    {
        return member.Member.Name == nameof(Nullable<>.Value)
               && member.Expression is MemberExpression
               && member.Member.DeclaringType?.IsGenericType == true
               && member.Member.DeclaringType.GetGenericTypeDefinition() == typeof(Nullable<>);
    }

    private static bool IsHasValueAccess(MemberExpression member)
    {
        return IsNullableHasValue(member);
    }

    #endregion
}