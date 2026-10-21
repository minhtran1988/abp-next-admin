using System.Collections.Generic;

namespace LINGYUN.Abp.AspNetCore.Auditing;
public class AbpAspNetCoreAuditingHeaderOptions
{
    /// <summary>
    /// Whether to record HTTP request headers in the audit log, default: true
    /// </summary>
    public bool IsEnabled {  get; set; }
    /// <summary>
    /// Http request headers to be recorded
    /// </summary>
    public IList<string> HttpHeaders { get; }
    public AbpAspNetCoreAuditingHeaderOptions()
    {
        IsEnabled = true;
        HttpHeaders = new List<string>();
    }
}
