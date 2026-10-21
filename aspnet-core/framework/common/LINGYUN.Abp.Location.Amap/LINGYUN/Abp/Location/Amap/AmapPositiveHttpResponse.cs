using Newtonsoft.Json;

namespace LINGYUN.Abp.Location.Amap
{
    /// <summary>
    /// Positive address resolution result
    /// </summary>
    public class AmapPositiveHttpResponse : AmapHttpResponse
    {
        /// <summary>
        /// Return the number of results
        /// </summary>
        /// <remarks>
        /// Return the number of results.
        /// </remarks>
        public int Count { get; set; }

        /// <summary>
        /// Geocoding information list
        /// </summary>
        [JsonProperty("geocodes")]
        public AmapGeocode[] Geocodes { get; set; }

        public AmapPositiveHttpResponse()
        {
            Geocodes = new AmapGeocode[0];
        }
    }
}
