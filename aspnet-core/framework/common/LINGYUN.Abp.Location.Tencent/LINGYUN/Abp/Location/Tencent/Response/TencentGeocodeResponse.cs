using LINGYUN.Abp.Location.Tencent.Model;
using Newtonsoft.Json;

namespace LINGYUN.Abp.Location.Tencent.Response;

public class TencentGeocodeResponse : TencentLocationResponse
{
    /// <summary>
    /// Address resolution results
    /// </summary>
    [JsonProperty("result")]
    public TencentGeocode Result { get; set; } = new TencentGeocode();
}
