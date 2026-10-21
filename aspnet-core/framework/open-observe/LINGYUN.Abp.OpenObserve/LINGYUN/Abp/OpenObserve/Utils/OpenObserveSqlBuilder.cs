using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace LINGYUN.Abp.OpenObserve.Utils;

/// <summary>
/// OpenObserve SQL condition builder
/// </summary>
/// <remarks>
/// Convert the <see cref="Condition"/> list into SQL WHERE conditions, and the field names can pass
/// <see cref="MapField"/> Mapping (expression member name -> data flow column name)
/// </remarks>
public class OpenObserveSqlBuilder
{
    protected Dictionary<string, string> FieldMappings { get; }

    public OpenObserveSqlBuilder()
    {
        FieldMappings = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
    }

    /// <summary>
    /// Add field mapping
    /// </summary>
    public virtual OpenObserveSqlBuilder MapField(string field, string column)
    {
        FieldMappings[field] = column;

        return this;
    }

    /// <summary>
    /// Get the mapped column name
    /// </summary>
    public virtual string GetMappedField(string field)
    {
        return FieldMappings.TryGetValue(field, out var column) ? column : field;
    }

    /// <summary>
    /// Construct SQL WHERE condition
    /// </summary>
    /// <param name="conditions">Query conditions</param>
    /// <param name="skipFields">Fields that need to be ignored (usually handled by the time range of the query)</param>
    public virtual string Build(IEnumerable<Condition> conditions, params string[] skipFields)
    {
        var parts = new List<string>();

        foreach (var condition in conditions)
        {
            if (IsSkip(condition.Field, skipFields))
            {
                continue;
            }

            parts.Add(BuildCondition(condition));
        }

        return parts.Count == 0 ? "1 = 1" : string.Join(" AND ", parts);
    }

    /// <summary>
    /// Build a single condition
    /// </summary>
    protected virtual string BuildCondition(Condition condition)
    {
        var field = GetMappedField(condition.Field);
        var value = FormatValue(condition.Field, condition.Value);

        return condition.Op switch
        {
            ComparisonOp.Equal => $"{field} = {value}",
            ComparisonOp.NotEqual => $"{field} != {value}",
            ComparisonOp.GreaterThan => $"{field} > {value}",
            ComparisonOp.GreaterThanOrEqual => $"{field} >= {value}",
            ComparisonOp.LessThan => $"{field} < {value}",
            ComparisonOp.LessThanOrEqual => $"{field} <= {value}",
            ComparisonOp.Contains => $"{field} LIKE '%{Escape(condition.Value?.ToString() ?? string.Empty)}%'",
            ComparisonOp.StartsWith => $"{field} LIKE '{Escape(condition.Value?.ToString() ?? string.Empty)}%'",
            ComparisonOp.EndsWith => $"{field} LIKE '%{Escape(condition.Value?.ToString() ?? string.Empty)}'",
            ComparisonOp.IsNull => $"{field} IS NULL",
            ComparisonOp.IsNotNull => $"{field} IS NOT NULL",
            _ => throw new NotSupportedException($"Unsupported comparison operator: {condition.Op}")
        };
    }

    /// <summary>
    /// Format condition value
    /// </summary>
    protected virtual string FormatValue(string field, object? value)
    {
        return value switch
        {
            null => "NULL",
            string s => $"'{Escape(s)}'",
            bool b => b ? "true" : "false",
            DateTime d => FormatDateTime(d),
            DateTimeOffset d => $"'{d:yyyy-MM-ddTHH:mm:ss.fffffffzzz}'",
            Guid g => $"'{g}'",
            Enum e => $"'{Escape(e.ToString())}'",
            _ => Convert.ToString(value, CultureInfo.InvariantCulture) ?? "NULL"
        };
    }

    /// <summary>
    /// Format time value
    /// </summary>
    /// <remarks>
    /// OpenObserve serializes time into ISO 8601 (with time zone offset), and compares in the same format to hit
    /// </remarks>
    public virtual string FormatDateTime(DateTime value)
    {
        return $"'{value:yyyy-MM-ddTHH:mm:ss.fffffffzzz}'";
    }

    /// <summary>
    /// Escape string value
    /// </summary>
    public virtual string Escape(string input)
    {
        return input.Replace("'", "''");
    }

    protected virtual bool IsSkip(string field, string[] skipFields)
    {
        foreach (var skip in skipFields)
        {
            if (string.Equals(field, skip, StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }
        }

        return false;
    }

    /// <summary>
    /// Append conditions to SQL
    /// </summary>
    public virtual void AppendCondition(StringBuilder sqlBuilder, string column, string sqlValue, bool isLike = false)
    {
        if (isLike)
        {
            sqlBuilder.AppendFormat(" and {0} like '%{1}%'", column, Escape(sqlValue));
        }
        else
        {
            sqlBuilder.AppendFormat(" and {0} = '{1}'", column, Escape(sqlValue));
        }
    }
}
