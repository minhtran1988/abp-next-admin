namespace LINGYUN.Abp.Identity;
/// <summary>
/// Repeat login strategy
/// </summary>
public enum ConcurrentLoginStrategy
{
    /// <summary>
    /// undefined
    /// </summary>
    None,
    /// <summary>
    /// Limit the number of logins from the same device
    /// </summary>
    LogoutFromSameTypeDevicesLimit,
    /// <summary>
    /// Other identical devices exit
    /// </summary>
    LogoutFromSameTypeDevices,
    /// <summary>
    /// All other devices exit
    /// </summary>
    LogoutFromAllDevices
}
