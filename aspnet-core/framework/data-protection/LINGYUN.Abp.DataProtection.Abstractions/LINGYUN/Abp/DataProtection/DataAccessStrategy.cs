namespace LINGYUN.Abp.DataProtection;
/// <summary>
/// Data access strategy
/// </summary>
public enum DataAccessStrategy
{
    /// <summary>
    /// All data can be accessed
    /// </summary>
    All,
    /// <summary>
    /// Custom rules
    /// </summary>
    Custom,
    /// <summary>
    /// Only current user
    /// </summary>
    CurrentUser,
    /// <summary>
    /// Only current user role
    /// </summary>
    CurrentRoles,
    /// <summary>
    /// Only the current user organization
    /// </summary>
    CurrentOrganizationUnits,
    /// <summary>
    /// Only the current user organization and subordinate organizations
    /// </summary>
    CurrentAndSubOrganizationUnits,
}
