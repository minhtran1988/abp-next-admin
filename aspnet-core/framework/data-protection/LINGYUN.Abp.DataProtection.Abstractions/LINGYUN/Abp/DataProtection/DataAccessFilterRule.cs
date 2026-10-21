using System;

namespace LINGYUN.Abp.DataProtection;

[Serializable]
public class DataAccessFilterRule
{
    /// <summary>
    /// Field name
    /// </summary>
    public string Field { get; set; } = default!;
    /// <summary>
    /// Field value
    /// </summary>
    public object? Value { get; set; }
    /// <summary>
    /// Type full name
    /// </summary>
    public string TypeFullName { get; set; } = default!;
    /// <summary>
    /// Js type
    /// </summary>
    public string JavaScriptType { get; set; } = default!;
    /// <summary>
    /// Operation type
    /// </summary>
    public DataAccessFilterOperate Operate { get; set; }
    /// <summary>
    /// Left condition
    /// </summary>
    public bool IsLeft { get; set; }

    public DataAccessFilterRule()
    {

    }

    public DataAccessFilterRule(
        string field,
        object value,
        string typeFullName,
        string javaScriptType,
        DataAccessFilterOperate operate = DataAccessFilterOperate.Equal, 
        bool isLeft = false)
    {
        Field = field;
        Value = value;
        TypeFullName = typeFullName;
        JavaScriptType = javaScriptType;
        Operate = operate;
        IsLeft = isLeft;
    }
}