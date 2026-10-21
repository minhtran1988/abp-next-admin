using System;

namespace LINGYUN.Abp.Identity.Session;

[Serializable]
public class IdentitySessionCacheItem
{
    private const string CacheKeyFormat = "s:{0}";
    /// <summary>
    /// Log in to the device
    /// </summary>
    public string Device { get; set; } = default!;
    /// <summary>
    /// Device description
    /// </summary>
    public string? DeviceInfo { get; set; }
    /// <summary>
    /// UserId
    /// </summary>
    public Guid UserId { get; set; }
    /// <summary>
    /// SessionId
    /// </summary>
    public string SessionId { get; set; } = default!;
    /// <summary>
    /// ClientId
    /// </summary>
    public string? ClientId { get; set; }
    /// <summary>
    /// IP address
    /// </summary>
    public string? IpAddresses { get; set; }
    /// <summary>
    /// IP territory
    /// </summary>
    public string? IpRegion { get; set; }
    /// <summary>
    /// Login time
    /// </summary>
    public DateTime SignedIn { get; set; }
    /// <summary>
    /// Last access time
    /// </summary>
    public DateTime? LastAccessed { get; set; }
    /// <summary>
    /// Expiration time (ms)
    /// </summary>
    public double? ExpiraIn { get; set; }

    public IdentitySessionCacheItem()
    {
    }

    public IdentitySessionCacheItem(
        string device,
        string? deviceInfo,
        Guid userId,
        string sessionId,
        string? clientId,
        string? ipAddresses,
        DateTime signedIn,
        DateTime? lastAccessed = null,
        string? ipRegion = null,
        double? expiraIn = null)
    {
        Device = device;
        DeviceInfo = deviceInfo;
        UserId = userId;
        SessionId = sessionId;
        ClientId = clientId;
        IpAddresses = ipAddresses;
        IpRegion = ipRegion;
        SignedIn = signedIn;
        LastAccessed = lastAccessed;
        ExpiraIn = expiraIn;
    }

    public static string CalculateCacheKey(string sessionId)
    {
        return string.Format(CacheKeyFormat, sessionId);
    }
}