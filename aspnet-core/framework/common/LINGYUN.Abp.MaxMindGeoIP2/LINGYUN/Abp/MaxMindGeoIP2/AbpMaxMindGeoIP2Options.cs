using MaxMind.Db;
using System;
using System.IO;

namespace LINGYUN.Abp.MaxMindGeoIP2;

/// <summary>
/// MaxMind GeoIP2 options
/// </summary>
public class AbpMaxMindGeoIP2Options
{
    /// <summary>
    /// Physical path of GeoIP2 database file (mmdb)
    /// </summary>
    /// <remarks>
    /// Needs to be provided by the user:
    /// * This module ** no longer has built-in ** GeoLite2 offline library (GeoLite2 is subject to the MaxMind license and may not be distributed with the package),
    /// Please download it from MaxMind and accept its license agreement (https://www.maxmind.com/en/geolite2/eula);
    /// * Point to the decompressed <c>GeoLite2-City.mmdb</c> through the configuration section <c>MaxMindGeoIP2:DatabaseFile</c> (or here);
    /// * Use <see cref="EmptyGeoIP2DatabaseReader"/> when the file does not exist, the parsing result will be empty and a warning log will be recorded.
    /// </remarks>
    public string DatabaseFile { get; set; } = string.Empty;

    /// <summary>
    /// Access mode of physical database files
    /// </summary>
    /// <remarks>
    /// Default <see cref="FileAccessMode.MemoryMapped"/>(memory mapped), the query is singleton and thread-safe
    /// </remarks>
    public FileAccessMode FileAccessMode { get; set; } = FileAccessMode.MemoryMapped;

    /// <summary>
    /// Whether the database file exists
    /// </summary>
    public virtual bool DatabaseFileExists()
    {
        return !DatabaseFile.IsNullOrWhiteSpace() && File.Exists(DatabaseFile);
    }
}
