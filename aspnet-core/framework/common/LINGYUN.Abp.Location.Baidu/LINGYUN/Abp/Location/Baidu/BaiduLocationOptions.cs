using System;

namespace LINGYUN.Abp.Location.Baidu;

public class BaiduLocationOptions
{
    /// <summary>
    /// The key for the user to apply for registration
    /// </summary>
    public string AccessKey { get; set; } = default!;
    /// <summary>
    /// AccessSecret for user registration
    /// </summary>
    public string AccessSecret { get; set; } = default!;
    /// <summary>
    /// Type of coordinates. Currently supported coordinate types include:
    /// bd09ll (Baidu latitude and longitude coordinates)
    /// bd09mc (Baidu metric coordinates)
    /// gcj02ll (Longitude and latitude coordinates of the National Survey Bureau, China only)
    /// wgs84ll (GPS latitude and longitude)
    /// </summary>
    public string CoordType { get; set; } = "bd09ll";
    /// <summary>
    /// Optional parameters, after adding, return the longitude and latitude coordinates of the National Bureau of Surveying or Baidu metric system
    /// gcj02ll (National Survey Bureau coordinates)
    /// bd09mc (Baidu Mercator coordinates)
    /// </summary>
    public string ReturnCoordType { get; set; } = "bd09ll";
    /// <summary>
    /// Calculate sn
    /// </summary>
    public bool CaculateAKSN => !AccessSecret.IsNullOrWhiteSpace();
    /// <summary>
    /// extensions_poi=0, do not recall pois data.
    /// extensions_poi=1, return pois data,
    /// By default, POI within 1000 meters will be displayed.
    /// </summary>
    public string ExtensionsPoi { get; set; } = "1";
    /// <summary>
    /// When the value is true, recall the three nearest road data around the coordinates.
    /// Different from the street parameter in the administrative division (the street parameter is the street in the administrative division and does not correspond to ordinary roads)
    /// </summary>
    public bool ExtensionsRoad { get; set; } = false;
    /// <summary>
    /// When the value is true, the administrative division returns township-level data (only domestic township data is recalled).
    /// Not accessed by default.
    /// </summary>
    public bool ExtensionsTown { get; set; } = false;
    /// <summary>
    /// Specify the recalled New Deal zoning language type.
    /// el gu en vi ca it iw sv eu ar cs gl id es en-GB ru sr nl pt tr tl lv en-AU lt th ro 
    /// fil ta fr bg hr bn de hu fa hi pt-BR fi da ja te pt-PT ml ko kn sk zh-CN pl uk sl mr 
    /// local
    /// </summary>
    public string Language { get; set; } = "zh-CN";
    /// <summary>
    /// The output format is json or xml
    /// </summary>
    public string Output { get; set; } = "json";
    /// <summary>
    /// Display errors to the client
    /// </summary>
    public bool VisableErrorToClient { get; set; } = false;
}
