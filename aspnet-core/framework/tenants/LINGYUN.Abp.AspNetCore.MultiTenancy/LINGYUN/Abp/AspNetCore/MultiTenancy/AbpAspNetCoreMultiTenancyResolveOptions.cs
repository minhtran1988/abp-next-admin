namespace LINGYUN.Abp.AspNetCore.MultiTenancy;

public class AbpAspNetCoreMultiTenancyResolveOptions
{
    /// <summary>
    /// Only resolve tenants in the domain name, default: true
    /// </summary>
    public bool OnlyResolveDomain {  get; set; }
    public AbpAspNetCoreMultiTenancyResolveOptions()
    {
        OnlyResolveDomain = true;
    }
}
