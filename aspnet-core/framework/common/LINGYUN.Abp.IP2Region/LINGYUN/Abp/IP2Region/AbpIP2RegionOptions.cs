using IP2Region.Net.XDB;

namespace LINGYUN.Abp.IP2Region;

/// <summary>
/// IP2Region options
/// </summary>
public class AbpIP2RegionOptions
{
    /// <summary>
    /// IPv4 database file
    /// </summary>
    /// <remarks>
    /// Supports physical file paths and virtual file system paths (the built-in ip2region_v4.xdb resource is used by default):
    /// * When passing in the physical file path, priority is given to loading by physical file, which facilitates replacement/update of offline libraries;
    /// * Otherwise read built-in resources from the virtual file system.
    /// </remarks>
    public string IPv4DatabaseFile { get; set; } = AbpIP2RegionModule.DefaultIPv4DatabaseFile;

    /// <summary>
    /// IPv6 database file
    /// </summary>
    /// <remarks>
    /// Consistent with <see cref="IPv4DatabaseFile"/> (physical files take precedence);
    /// The IPv6 offline library is optional. When the file does not exist, the IPv6 query returns null.
    /// </remarks>
    public string IPv6DatabaseFile { get; set; } = AbpIP2RegionModule.DefaultIPv6DatabaseFile;

    /// <summary>
    /// Caching strategy
    /// </summary>
    /// <remarks>
    /// Default <see cref="CachePolicy.Content"/>: Load all offline libraries into memory before querying, which is a **concurrency security** strategy
    /// (IPv4 library is about 10MB, IPv6 library is about 35MB).
    /// 
    /// <see cref="CachePolicy.File"/> and <see cref="CachePolicy.VectorIndex"/> are implemented based on "shared data flow + Seek",
    /// And <c>ISearcher</c> is registered as a singleton, and multi-threaded concurrent queries will interrupt each other's data flow positioning.
    /// Only use when serial query can be guaranteed (or thread safety can be guaranteed by yourself).
    /// </remarks>
    public CachePolicy CachePolicy { get; set; } = CachePolicy.Content;
}
