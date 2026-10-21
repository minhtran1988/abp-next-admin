namespace LINGYUN.Abp.Location.Amap
{
    public class AmapInverseHttpRequestParamter
    {
        /// <summary>
        /// Longitude and latitude coordinates
        /// 
        /// Rules for incoming content: longitude first, latitude last, longitude and latitude separated by ",", no more than 6 digits after the decimal point for longitude and latitude.
        /// If you need to parse multiple longitudes and latitudes, please use "|" to separate them, and set the batch parameter to true. Up to 20 pairs of coordinate points can be passed in.
        /// Each pair of point coordinates is separated by "|".
        /// </summary>
        public Location[] Locations { get; set; } = new Location[0];
        /// <summary>
        /// Returns the nearby POI type
        /// The following content requires the extensions parameter to be all to take effect.
        /// After coordinate parsing, reverse geocoding can not only return the address description, but also return the POI content near the longitude and latitude that meets the limited requirements (the POI content will be returned only when the extensions field value is all).
        /// Setting the POI type parameters is equivalent to defining the requirements for the above operations.
        /// 
        /// The parameter only supports passing in POI TYPECODE. Multiple POI TYPECODE can be passed in, separated by "|".
        /// This parameter does not take effect when the batch value is true. Get POI TYPECODE
        /// </summary>
        public string PoiType { get; set; }
        /// <summary>
        /// Search radius
        /// The value range of radius is 0~3000, and the default is 1000. Unit: meter
        /// </summary>
        public short Radius { get; set; } = 1000;
        /// <summary>
        /// Return result control
        /// The default value of the extensions parameter is base, which returns the basic address information;
        /// When the extensions parameter value is all, basic address information, nearby POI content, road information and road intersection information will be returned.
        /// </summary>
        public string Extensions { get; set; } = "base";
        /// <summary>
        /// Batch query control
        /// When the batch parameter is set to true, batch query operations are performed, and a maximum of 20 latitude and longitude points are supported for batch address query operations.
        /// When the batch parameter is set to false, a single point query is performed. At this time, even if multiple longitudes and latitudes are passed in, only the address resolution query result of the first longitude and latitude will be returned.
        /// </summary>
        public bool Batch { get; set; } = false;
        /// <summary>
        /// Road level
        /// 
        /// The following content requires the extensions parameter to be all to take effect.
        /// Optional values: 0, 1
        /// When roadlevel=0, display all roads
        /// When roadlevel = 1, filter non-trunk roads and only output trunk road data
        /// </summary>
        public sbyte? RoadLevel { get; set; }
        /// <summary>
        /// Digital signature
        /// </summary>
        public string Sig { get; set; }
        /// <summary>
        /// Return data format type
        /// Optional input content includes: JSON, XML. Set JSON. The returned result data will be composed of JSON structure;
        /// If XML is set, the returned result data will be composed of XML structure.
        /// </summary>
        public string Output { get; set; } = "JSON";
        /// <summary>
        /// Whether to optimize the POI return order
        /// The following content requires the extensions parameter to be all to take effect.
        /// The setting of the homeorcorp parameter can affect the sorting strategy of recalling POI content. Currently, three optional parameters are provided:
        /// 
        /// 0: No interference with the recall sorting strategy.
        /// 
        /// 1: Comprehensive big data analysis returns home-related POI content first, that is, optimizing the POI order of the pois field in the returned results.
        /// 
        /// 2: Comprehensive big data analysis returns company-related POI content first, that is, optimizing the POI order of the pois field in the returned results.
        /// </summary>
        public sbyte HomeOrCorp { get; set; } = 0;
    }
}
