using Newtonsoft.Json;
using System;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.WeChat.Work;
/// <summary>
/// Enterprise WeChat request response
/// </summary>
[Serializable]
public class WeChatWorkResponse
{
    /// <summary>
    /// Error code
    /// </summary>
    [JsonProperty("errcode")]
    [JsonPropertyName("errcode")]
    public int ErrorCode { get; set; }
    /// <summary>
    /// Error message
    /// </summary>
    [JsonProperty("errmsg")]
    [JsonPropertyName("errmsg")]
    public string? ErrorMessage { get; set; }

    public bool IsSuccessed => ErrorCode == 0;

    public void ThrowIfNotSuccess()
    {
        if (ErrorCode != 0)
        {
            throw new AbpWeChatWorkException($"WeChatWork:{ErrorCode}", $"Wechat work request error:{ErrorMessage}");
        }
    }
}
