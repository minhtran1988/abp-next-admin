using JetBrains.Annotations;

namespace LINGYUN.Abp.Location.Amap
{
    /// <summary>
    /// Amap positive address resolution request parameters
    /// </summary>
    public class AmapPositiveHttpRequestParamter
    {
        /// <summary>
        /// Structured address
        /// Rules to follow: country, province, city, district, town, village, street, house number, housing estate, building, such as: No. 6, Futong East Street, Chaoyang District, Beijing.
        /// If you need to parse multiple addresses, please use "|" to separate them, and set the batch parameter to true. A maximum of 10 addresses are supported for "|" split requests.
        /// </summary>
        [NotNull]
        public string Address { get; set; }
        /// <summary>
        /// city
        /// Optional input content includes: Chinese of the specified city (such as Beijing), Chinese pinyin of the specified city (beijing), citycode (010), adcode (110000), county-level cities are not supported.
        /// When the specified city query content is empty, a nationwide address conversion search will be performed.
        /// </summary>
        [CanBeNull]
        public string City { get; set; }
        /// <summary>
        /// Batch query control
        /// When the batch parameter is set to true, batch query operation is performed, and a maximum of 10 addresses are supported for batch query.
        /// When the batch parameter is set to false, a single point query is performed. At this time, even if multiple addresses are passed in, only the parsing query result of the first address will be returned.
        /// </summary>
        [CanBeNull]
        public bool Batch { get; set; } = false;
        /// <summary>
        /// Digital signature
        /// </summary>
        [CanBeNull]

        public string Sig { get; set; }
        /// <summary>
        /// Return data format type
        /// Optional input content includes: JSON, XML. Set JSON. The returned result data will be composed of JSON structure;
        /// If XML is set, the returned result data will be composed of XML structure.
        /// </summary>
        [CanBeNull]
        public string Output { get; set; } = "JSON";
    }
}
