using Newtonsoft.Json;
using System.Collections.Generic;

namespace LINGYUN.Abp.Location.Amap
{
    public class AmapRegeocode
    {
        /// <summary>
        /// Structured address information
        /// Province + city + district and county + town + village + street + house number
        /// </summary>
        [JsonProperty("formatted_address")]
        public string Address { get; set; }
        /// <summary>
        /// List of address elements
        /// </summary>
        public AddressComponent AddressComponent { get; set; }
        /// <summary>
        /// Road information list
        /// Returned when the request parameter extensions is all
        /// </summary>
        public Road[] Roads { get; set; }
        /// <summary>
        /// Road intersection list
        /// Returned when the request parameter extensions is all
        /// </summary>
        public Roadinter[] Roadinters { get; set; }
        /// <summary>
        /// poi information list
        /// Returned when the request parameter extensions is all
        /// </summary>
        public Poi[] Pois { get; set; }
        /// <summary>
        /// aoi information list
        /// Returned when the request parameter extensions is all
        /// </summary>
        public Aoi[] Aois { get; set; }
        public AmapRegeocode()
        {
            AddressComponent = new AddressComponent();
            Roads = new Road[0];
            Roadinters = new Roadinter[0];
            Pois = new Poi[0];
            Aois = new Aoi[0];
        }
    }

    public class AddressComponent
    {
        /// <summary>
        /// Country
        /// </summary>
        public string Country { get; set; }
        /// <summary>
        /// The name of the province where the coordinate point is located
        /// For example: Beijing City
        /// </summary>
        public string Province { get; set; }

        /// <summary>
        /// The name of the city where the coordinate point is located
        /// Please note: When the city is a county directly under the central government of the province, the return value is empty, and when the city is the four municipalities directly under the central government: Beijing, Shanghai, Tianjin, and Chongqing, this field returns empty
        /// </summary>
        public string[] City { get; set; }

        /// <summary>
        /// City code
        /// For example: 010
        /// </summary>
        public string CityCode { get; set; }

        /// <summary>
        /// The area where the coordinate point is located
        /// For example: Haidian District
        /// </summary>
        public string District { get; set; }

        /// <summary>
        /// Administrative area code
        /// For example: 110108
        /// </summary>
        public string AdCode { get; set; }

        /// <summary>
        /// The town/street where the coordinate point is located (this street is a community street, not road information)
        /// For example: Yanyuan Street
        /// </summary>
        public string[] TownShip { get; set; }

        /// <summary>
        /// Township street code
        /// For example: 110101001000
        /// </summary>
        public string TownCode { get; set; }
        /// <summary>
        /// Community information list
        /// </summary>
        public NeighBorHood NeighBorHood { get; set; }
        /// <summary>
        /// Building information list
        /// </summary>
        public Building Building { get; set; }
        /// <summary>
        /// House number information list
        /// </summary>
        public StreetNumber StreetNumber { get; set; }
        /// <summary>
        /// The sea area information to which it belongs
        /// For example: Bohai
        /// </summary>
        public string SeaArea { get; set; }
        /// <summary>
        /// List of business districts to which the latitude and longitude belongs
        /// </summary>
        public List<BusinessArea> BusinessAreas { get; set; }
        public AddressComponent()
        {
            NeighBorHood = new NeighBorHood();
            Building = new Building();
            StreetNumber = new StreetNumber();
            BusinessAreas = new List<BusinessArea>();
        }
    }

    /// <summary>
    /// Community information
    /// </summary>
    public class NeighBorHood
    {
        /// <summary>
        /// Community name
        /// For example: Peking University
        /// </summary>
        public string[] Name { get; set; } = new string[0];

        /// <summary>
        /// POI type
        /// For example: science, education and cultural services; schools; colleges and universities
        /// </summary>
        public string[] Type { get; set; } = new string[0];
    }
    /// <summary>
    /// Building information
    /// </summary>
    public class Building
    {
        /// <summary>
        /// Building name
        /// For example: Wanda Plaza
        /// </summary>
        public string[] Name { get; set; } = new string[0];

        /// <summary>
        /// Type
        /// For example: science, education and cultural services; schools; colleges and universities
        /// </summary>
        public string[] Type { get; set; } = new string[0];
    }
    /// <summary>
    /// Street information
    /// </summary>
    public class StreetNumber
    {
        /// <summary>
        /// street name
        /// For example: Zhongguancun North 2nd Street
        /// </summary>
        public string Street { get; set; }

        /// <summary>
        /// House number
        /// For example: No. 3
        /// </summary>
        public string Number { get; set; }

        /// <summary>
        /// Coordinate point
        /// Longitude and latitude coordinate points: longitude, latitude
        /// </summary>
        public string Location { get; set; }

        /// <summary>
        /// direction
        /// The street orientation of the coordinate point
        /// </summary>
        public string Direction { get; set; }

        /// <summary>
        /// The distance from the house address to the requested coordinates
        /// Unit: meters
        /// </summary>
        public string Distance { get; set; }
    }

    /// <summary>
    /// Business district information
    /// </summary>
    public class BusinessArea
    {
        /// <summary>
        /// Longitude and latitude of the center point of the business district
        /// Longitude and latitude coordinate points: longitude, latitude
        /// </summary>
        public string Location { get; set; }

        /// <summary>
        /// Business district name
        /// For example: Summer Palace
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The adcode of the area where the business district is located
        /// For example: Chaoyang District/Haidian District
        /// </summary>
        public string Id { get; set; }
    }

    /// <summary>
    /// Road information
    /// </summary>
    public class Road
    {
        /// <summary>
        /// RoadId
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// Road name
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Distance from the road to the requested coordinates
        /// Unit: meters
        /// </summary>
        public string Distance { get; set; }
        /// <summary>
        /// Orientation
        /// Enter the relative orientation of the point and this road
        /// </summary>
        public string Direction { get; set; }
        /// <summary>
        /// Coordinate point
        /// Longitude and latitude coordinate points: longitude, latitude
        /// </summary>
        public string Location { get; set; }
    }

    /// <summary>
    /// road intersection
    /// </summary>
    public class Roadinter
    {
        /// <summary>
        /// The distance from the intersection to the requested coordinates
        /// Unit: meters
        /// </summary>
        public string Distance { get; set; }
        /// <summary>
        /// Orientation
        /// Enter the orientation of the point relative to the intersection
        /// </summary>
        public string Direction { get; set; }
        /// <summary>
        /// Intersection latitude and longitude
        /// </summary>
        public string Location { get; set; }
        /// <summary>
        /// The first road id
        /// </summary>
        [JsonProperty("first_id")]
        public string FirstId { get; set; }
        /// <summary>
        /// First road name
        /// </summary>
        [JsonProperty("first_name")]
        public string FirstName { get; set; }
        /// <summary>
        /// The second road id
        /// </summary>
        [JsonProperty("second_id")]
        public string SecondId { get; set; }
        /// <summary>
        /// Second road name
        /// </summary>
        [JsonProperty("second_name")]
        public string SecondName { get; set; }
    }

    /// <summary>
    /// poi information
    /// </summary>
    public class Poi
    {
        /// <summary>
        /// Poi information
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// poi point name
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// poi type
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// Phone
        /// </summary>
        public string Tel { get; set; }
        /// <summary>
        /// The distance from the center point of the POI to the requested coordinates
        /// Unit: meters
        /// </summary>
        public string Distance { get; set; }
        /// <summary>
        /// direction
        /// is the orientation of the input point relative to the building
        /// </summary>
        public string Direction { get; set; }
        /// <summary>
        /// poi address information
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// Coordinate point
        /// </summary>
        public string Location { get; set; }
        /// <summary>
        /// The name of the business district where poi is located
        /// </summary>
        public string BusinessArea { get; set; }
    }
    /// <summary>
    /// aoi information
    /// </summary>
    public class Aoi
    {
        /// <summary>
        /// The id of the aoi to which it belongs
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// The name of the aoi to which it belongs
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// The region code of the aoi to which it belongs
        /// </summary>
        public string AdCode { get; set; }
        /// <summary>
        /// The coordinates of the center point of the aoi it belongs to
        /// </summary>
        public string Location { get; set; }
        /// <summary>
        /// The area of the aoi point it belongs to
        /// Unit: square meters
        /// </summary>
        public string Area { get; set; }
        /// <summary>
        /// Whether the input longitude and latitude are in the aoi plane
        /// 0, means within the aoi. The remaining integers represent the distance from the AOI.
        /// </summary>
        public string Distance { get; set; }
    }
}
