using LINGYUN.Abp.Location.Tencent.Localization;
using Newtonsoft.Json;
using Volo.Abp;
using Volo.Abp.Localization;

namespace LINGYUN.Abp.Location.Tencent.Response;

public abstract class TencentLocationResponse
{
    /// <summary>
    /// Status code, 0 means normal,
    /// 310 The request parameter information is incorrect.
    /// 311Key format error,
    /// If the 306 request has support information, please check the string.
    /// 110 The request source is not authorized
    /// </summary>
    [JsonProperty("status")]
    public int Status { get; set; }
    /// <summary>
    /// Status description
    /// </summary>
    [JsonProperty("message")]
    public string Message { get; set; } = default!;
    /// <summary>
    /// The unique identifier of this request
    /// </summary>
    [JsonProperty("request_id")]
    public string RequestId { get; set; } = default!;
    /// <summary>
    /// Whether the request is successful
    /// </summary>
    public bool IsSuccessed => Status.Equals(0);

    public ILocalizableString GetErrorMessage(bool throwToClient = false)
    {
        switch (Status)
        {
            case 0:
                return LocalizableString.Create<TencentLocationResource>("Message:RETURN_0");
            case 110:
                return LocalizableString.Create<TencentLocationResource>("Message:RETURN_110");
            case 306:
                return LocalizableString.Create<TencentLocationResource>("Message:RETURN_306");
            case 310:
                return LocalizableString.Create<TencentLocationResource>("Message:RETURN_310");
            case 311:
                return LocalizableString.Create<TencentLocationResource>("Message:RETURN_311");
            default:
                if (throwToClient)
                {
                    throw new LocationResolveException(Message);
                }
                throw new AbpException(Message);
        }
    }
}
