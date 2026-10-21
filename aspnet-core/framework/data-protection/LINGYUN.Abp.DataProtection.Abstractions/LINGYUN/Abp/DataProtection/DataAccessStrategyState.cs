using System;

namespace LINGYUN.Abp.DataProtection;

[Serializable]
public class DataAccessStrategyState
{
    /// <summary>
    /// Authorization subject
    /// </summary>
    public string SubjectName { get; set; } = default!;

    /// <summary>
    /// Permission subject identification
    /// </summary>
    public string[] SubjectKeys { get; set; } = default!;

    /// <summary>
    /// Permission policy
    /// </summary>
    public DataAccessStrategy Strategy { get; set; }
    public DataAccessStrategyState()
    {

    }
    public DataAccessStrategyState(
        string subjectName,
        string[] subjectKeys,
        DataAccessStrategy strategy)
    {
        SubjectName = subjectName;
        SubjectKeys = subjectKeys;
        Strategy = strategy;
    }
}
