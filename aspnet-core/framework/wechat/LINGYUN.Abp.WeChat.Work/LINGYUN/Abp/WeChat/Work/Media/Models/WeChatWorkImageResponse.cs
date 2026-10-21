using Newtonsoft.Json;

namespace LINGYUN.Abp.WeChat.Work.Media.Models;
public class WeChatWorkImageResponse : WeChatWorkResponse
{
    /// <summary>
    /// The image URL obtained after uploading. Valid permanently
    /// </summary>
    [JsonProperty("url")]
    public string Url { get; set; } = default!;
}
