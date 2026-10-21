namespace LINGYUN.Linq.Dynamic.Queryable;
public enum DynamicComparison
{
    /// <summary>
    /// equal to
    /// </summary>
    Equal = 0,
    /// <summary>
    /// Not equal to
    /// </summary>
    NotEqual = 1,
    /// <summary>
    /// Less than
    /// </summary>
    LessThan = 2,
    /// <summary>
    /// Less than or equal to
    /// </summary>
    LessThanOrEqual = 3,
    /// <summary>
    /// Greater than
    /// </summary>
    GreaterThan = 4,
    /// <summary>
    /// Greater than or equal to
    /// </summary>
    GreaterThanOrEqual = 5,
    /// <summary>
    /// Left contains
    /// </summary>
    StartsWith = 6,
    /// <summary>
    /// Left does not contain
    /// </summary>
    NotStartsWith = 7,
    /// <summary>
    /// right contains
    /// </summary>
    EndsWith = 8,
    /// <summary>
    /// The right does not contain
    /// </summary>
    NotEndsWith = 9,
    /// <summary>
    /// Contains
    /// </summary>
    Contains = 10,
    /// <summary>
    /// Does not contain
    /// </summary>
    NotContains = 11,
    /// <summary>
    /// null value
    /// </summary>
    Null = 12,
    /// <summary>
    /// Not empty
    /// </summary>
    NotNull = 13
}

