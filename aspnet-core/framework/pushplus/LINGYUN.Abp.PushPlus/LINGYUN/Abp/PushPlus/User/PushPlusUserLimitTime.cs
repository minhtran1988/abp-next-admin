using Newtonsoft.Json;

namespace LINGYUN.Abp.PushPlus.User;

public class PushPlusUserLimitTime
{
    /// <summary>
    /// Send restriction status;
    /// 1-unlimited,
    /// 2-Short term limit,
    /// 3-Permanent restrictions
    /// </summary>
    [JsonProperty("sendLimit")]
    public PushPlusUserSendLimit SendLimit { get; set; }
    /// <summary>
    /// Unblocking time
    /// </summary>
    [JsonProperty("userLimitTime")]
    public string? UserLimitTime { get; set; }
}
