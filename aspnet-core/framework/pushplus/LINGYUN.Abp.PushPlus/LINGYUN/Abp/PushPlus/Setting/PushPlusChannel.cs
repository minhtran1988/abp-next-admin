using Newtonsoft.Json;

namespace LINGYUN.Abp.PushPlus.Setting;
/// <summary>
/// Sending channel
/// </summary>
public class PushPlusChannel
{
    /// <summary>
    /// Default channel encoding
    /// </summary>
    [JsonProperty("defaultChannel")]
    public string? DefaultChannel { get; set; }
    /// <summary>
    /// Default channel name
    /// </summary>
    [JsonProperty("defaultChannelTxt")]
    public string? DefaultChannelName { get; set; }
    /// <summary>
    /// Channel parameters
    /// </summary>
    [JsonProperty("defaultWebhook")]
    public string? DefaultWebhook { get; set; }
    /// <summary>
    /// Sending restrictions; 0-no restrictions, 1-forbid sending through all channels, 2-limit WeChat channels, 3-limit email channels
    /// </summary>
    [JsonProperty("sendLimit")]
    public PushPlusChannelRecevieLimit SendLimit { get; set; }
    /// <summary>
    /// Reception limit; 0-receive all, 1-do not receive messages
    /// </summary>
    [JsonProperty("recevieLimit")]
    public PushPlusChannelRecevieLimit RecevieLimit { get; set; }

}
