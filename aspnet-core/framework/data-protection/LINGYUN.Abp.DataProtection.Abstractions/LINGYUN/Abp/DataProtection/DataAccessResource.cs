using System;
using System.Collections.Generic;

namespace LINGYUN.Abp.DataProtection;

[Serializable]
public class DataAccessResource
{
    /// <summary>
    /// Authorization subject
    /// </summary>
    public string SubjectName { get; set; } = default!;

    /// <summary>
    /// Permission subject identification
    /// </summary>
    public string SubjectId { get; set; } = default!;

    /// <summary>
    /// Entity type full name
    /// </summary>
    public string EntityTypeFullName { get; set; } = default!;

    /// <summary>
    /// Data permission operations
    /// </summary>
    public DataAccessOperation Operation { get; set; }

    /// <summary>
    /// Get or set data filtering rules
    /// </summary>
    public DataAccessFilterGroup? FilterGroup { get; set; }

    /// <summary>
    /// List of properties that are allowed to operate
    /// </summary>
    public List<string> AccessedProperties { get; set; }

    public DataAccessResource()
    {
        AccessedProperties = new List<string>();
    }

    public DataAccessResource(
        string subjectName,
        string subjectId,
        string entityTypeFullName,
        DataAccessOperation operation,
        DataAccessFilterGroup? filterGroup = null)
    {
        SubjectName = subjectName;
        SubjectId = subjectId;
        EntityTypeFullName = entityTypeFullName;
        Operation = operation;
        FilterGroup = filterGroup;

        AccessedProperties = new List<string>();
    }
}
