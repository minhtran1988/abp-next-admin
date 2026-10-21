namespace LINGYUN.Abp.AuditLogging.OpenObserve;

/// <summary>
/// Statistical query results
/// </summary>
/// <remarks>
/// Corresponding to the hit structure of <c>select count(*) as total from ...</c>
/// </remarks>
public class OpenObserveCount
{
    public long Total { get; set; }
}
