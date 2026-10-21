using Newtonsoft.Json;

namespace LINGYUN.Abp.PushPlus.Topic;

public class PushPlusTopicQrCode
{
    /// <summary>
    /// Group QR code image path
    /// </summary>
    [JsonProperty("qrCodeImgUrl")]
    public string QrCodeImgUrl { get; set; } = default!;
    /// <summary>
    /// QR code type;
    /// 0-Temporary QR code,
    /// 1-Permanent QR code
    /// </summary>
    [JsonProperty("forever")]
    public PushPlusTopicQrCodeType Forever { get; set; }
}
