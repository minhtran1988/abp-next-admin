namespace LINGYUN.Abp.AspNetCore.Session;

public class AbpAspNetCoreSessionOptions
{
    /// <summary>
    /// Whether to parse IP geographic information
    /// </summary>
    public bool IsParseIpLocation { get; set; }

    public AbpAspNetCoreSessionOptions()
    {
        IsParseIpLocation = false;
    }
}
