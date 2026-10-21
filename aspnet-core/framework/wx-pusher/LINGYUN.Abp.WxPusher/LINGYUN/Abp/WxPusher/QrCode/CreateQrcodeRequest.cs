using JetBrains.Annotations;
using Newtonsoft.Json;
using System;
using Volo.Abp;

namespace LINGYUN.Abp.WxPusher.QrCode;

[Serializable]
public class CreateQrcodeRequest
{
    /// <summary>
    /// Application flag
    /// </summary>
    [JsonProperty("appToken")]
    public string AppToken { get; }
    /// <summary>
    /// Parameters carried by the QR code, up to 64 bits
    /// </summary>
    [JsonProperty("extra")]
    public string Extra { get; }
    /// <summary>
    /// The validity time of the QR code, in s, is up to 30 days.
    /// </summary>
    [JsonProperty("validTime")]
    public int ValidTime { get; }

    public CreateQrcodeRequest(
        [NotNull] string appToken,
        [NotNull] string extra, 
        int validTime = 1800)
    {
        Check.NotNullOrWhiteSpace(appToken, nameof(appToken));
        Check.NotNullOrWhiteSpace(extra, nameof(extra), 64);

        AppToken = appToken;
        Extra = extra;
        ValidTime = validTime;
    }
}
