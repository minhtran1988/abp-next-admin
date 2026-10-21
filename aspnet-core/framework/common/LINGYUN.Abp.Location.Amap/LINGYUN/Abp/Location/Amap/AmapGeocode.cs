using Newtonsoft.Json;

namespace LINGYUN.Abp.Location.Amap
{
    /// <summary>
    /// The return parameter format of Amap is really weird
    /// Deprecated
    /// </summary>
    public class AmapGeocode
    {
        /// <summary>
        /// Structured address information
        /// Province + city + district and county + town + village + street + house number
        /// </summary>
        [JsonProperty("formatted_address")]
        public string Address { get; set; }

        /// <summary>
        /// Country
        /// Domestic addresses return to China by default
        /// </summary>
        [JsonProperty("country")]
        public string Country { get; set; }

        /// <summary>
        /// The name of the province where the address is located
        /// For example: Beijing. It should be noted here that China’s four major municipalities also count as provincial units.
        /// </summary>
        [JsonProperty("province")]
        public string Province { get; set; }

        /// <summary>
        /// The city name where the address is located
        /// For example: Beijing City
        /// </summary>
        [JsonProperty("city")]
        public string City { get; set; }

        /// <summary>
        /// City code
        /// For example: 010
        /// </summary>
        [JsonProperty("citycode")]
        public string CityCode { get; set; }

        /// <summary>
        /// The area where the address is located
        /// For example: Chaoyang District
        /// </summary>
        [JsonProperty("district")]
        public string District { get; set; }

        /// <summary>
        /// The town/street where the coordinate point is located (this street is a community street, not road information)
        /// For example: Yanyuan Street
        /// </summary>
        public string[] TownShip { get; set; } = new string[0];

        /// <summary>
        /// Community information list
        /// </summary>
        public NeighBorHood NeighBorHood { get; set; } = new NeighBorHood();

        /// <summary>
        /// Building information list
        /// </summary>
        public Building Building { get; set; } = new Building();

        /// <summary>
        /// street
        /// For example: Futong East Street
        /// </summary>
        [JsonProperty("street")]
        public string[] Street { get; set; } = new string[0];

        /// <summary>
        /// House number
        /// For example: No. 6
        /// </summary>
        [JsonProperty("number")]
        public string[] Number { get; set; } = new string[0];

        /// <summary>
        /// Region code
        /// For example: 110101
        /// </summary>
        [JsonProperty("adcode")]
        public string Adcode { get; set; }

        /// <summary>
        /// Coordinate point
        /// Longitude, latitude
        /// </summary>
        [JsonProperty("location")]
        public string Location { get; set; }

        /// <summary>
        /// Match level
        /// For example: Chaoyang District
        /// </summary>
        [JsonProperty("level")]
        public string Level { get; set; }
    }
}
