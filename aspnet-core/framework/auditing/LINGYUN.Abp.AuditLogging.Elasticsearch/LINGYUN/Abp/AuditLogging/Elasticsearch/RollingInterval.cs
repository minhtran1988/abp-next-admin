namespace LINGYUN.Abp.AuditLogging.Elasticsearch;
/// <summary>
/// Index scrolling interval
/// </summary>
public enum RollingInterval
{
    /// <summary>
    /// The index name does not scroll with time, and the index does not contain time range information
    /// </summary>
    Infinite,
    /// <summary>
    /// Rolling every minute, the index suffix increases by yyyyMMddHHmm
    /// </summary>
    Minute,
    /// <summary>
    /// Rolling every hour, the index suffix increases by yyyyMMddHH
    /// </summary>
    Hour,
    /// <summary>
    /// Rolling every day, the index suffix increases by yyyyMMdd
    /// </summary>
    Day,
    /// <summary>
    /// Monthly rolling, the index suffix increases by yyyyMM
    /// </summary>
    Month,
    /// <summary>
    /// Rolling every year, the index suffix increases by yyyy
    /// </summary>
    Year,
}
