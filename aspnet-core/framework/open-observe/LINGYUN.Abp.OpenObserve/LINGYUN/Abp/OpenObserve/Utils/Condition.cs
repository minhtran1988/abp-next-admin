namespace LINGYUN.Abp.OpenObserve.Utils;

/// <summary>
/// Query conditions
/// </summary>
/// <param name="Field">Field name (expression member name)</param>
/// <param name="Op">Comparison operator</param>
/// <param name="Value">Conditional value</param>
public record Condition(string Field, ComparisonOp Op, object? Value);
