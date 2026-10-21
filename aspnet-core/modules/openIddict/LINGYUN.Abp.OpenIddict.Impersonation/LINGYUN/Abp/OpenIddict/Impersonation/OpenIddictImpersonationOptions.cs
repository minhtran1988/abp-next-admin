namespace LINGYUN.Abp.OpenIddict.Impersonation;

public class OpenIddictImpersonationOptions
{
    /// <summary>
    /// Check permissions when impersonating user
    /// </summary>
    public string? ImpersonationPermission {  get; set; }
    /// <summary>
    /// Check permissions when simulating tenant
    /// </summary>
    public string? ImpersonationTenantPermission { get; set; }
    /// <summary>
    /// The default administrator username when the host simulates a tenant
    /// </summary>
    public string DefaultTenantAdminUserName {  get; set; }
    public OpenIddictImpersonationOptions()
    {
        DefaultTenantAdminUserName = "admin";
    }
}
