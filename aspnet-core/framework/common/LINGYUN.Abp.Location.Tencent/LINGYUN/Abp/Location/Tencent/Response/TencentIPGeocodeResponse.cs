using LINGYUN.Abp.Location.Tencent.Model;
using Newtonsoft.Json;

namespace LINGYUN.Abp.Location.Tencent.Response;

public class TencentIPGeocodeResponse : TencentLocationResponse
{
    /// <summary>
    /// IP positioning results
    /// </summary>
    [JsonProperty("result")]
    public TencentIPGeocode Result { get; set; } = new TencentIPGeocode();
}
