namespace Volo.Abp.Settings;

public enum ValueType
{
    /// <summary>
    /// Do not join setting management (do not pass setting values when cooperating with the front end)
    /// </summary>
    NoSet = -1,
    /// <summary>
    /// characters
    /// </summary>
    String = 0,
    /// <summary>
    /// Number
    /// </summary>
    Number = 1,
    /// <summary>
    /// Boolean
    /// </summary>
    Boolean = 2,
    /// <summary>
    /// Date
    /// </summary>
    Date = 3,
    /// <summary>
    /// array
    /// </summary>
    Array = 4,
    /// <summary>
    /// Options
    /// </summary>
    Option = 5,
    /// <summary>
    /// Object
    /// </summary>
    Object = 10
}
