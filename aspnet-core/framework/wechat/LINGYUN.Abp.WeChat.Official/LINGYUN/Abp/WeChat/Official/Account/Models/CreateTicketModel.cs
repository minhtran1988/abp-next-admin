using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.WeChat.Official.Account.Models;
public class CreateTicketModel : WeChatRequest
{
    /// <summary>
    /// The validity time of this QR code is in seconds. The maximum value cannot exceed 2592000 (that is, 30 days). If this field is not filled in, the default validity period is 60 seconds.
    /// </summary>
    [JsonProperty("expire_seconds")]
    [JsonPropertyName("expire_seconds")]
    public int? ExpireSeconds { get; set; }
    /// <summary>
    /// QR code type
    /// </summary>
    /// <remarks>
    ///     <list type="table">
    /// <item>QR_SCENE is a temporary integer parameter value</item>
    /// <item>QR_STR_SCENE is a temporary string parameter value</item>
    /// <item>QR_LIMIT_SCENE is a permanent integer parameter value</item>
    /// <item>QR_LIMIT_STR_SCENE is a permanent string parameter value</item>
    ///     </list>
    /// </remarks>
    [JsonProperty("action_name")]
    [JsonPropertyName("action_name")]
    public string ActionName { get; private set; } = default!;
    /// <summary>
    /// QR code details
    /// </summary>
    [JsonProperty("action_info")]
    [JsonPropertyName("action_info")]
    public Scene SceneInfo { get; private set; } = default!;
    private CreateTicketModel()
    {

    }
    /// <summary>
    /// Create a temporary QR code ticket through the scene value name
    /// </summary>
    /// <param name="sceneStr">Scene value name</param>
    /// <param name="expireSeconds">QR code validity time</param>
    /// <returns></returns>
    public static CreateTicketModel StringScene(
        string sceneStr,
        int expireSeconds = 60)
    {
        return new CreateTicketModel
        {
            ExpireSeconds = expireSeconds,
            ActionName = "QR_STR_SCENE",
            SceneInfo = new StringScene(sceneStr),
        };
    }
    /// <summary>
    /// Create a permanent QR code ticket through the scene value name
    /// </summary>
    /// <param name="sceneStr">Scene value name</param>
    /// <returns></returns>
    public static CreateTicketModel LimitStringScene(string sceneStr)
    {
        return new CreateTicketModel
        {
            ActionName = "QR_LIMIT_STR_SCENE",
            SceneInfo = new StringScene(sceneStr),
        };
    }
    /// <summary>
    /// Create a QR code ticket through the scene value identifier
    /// </summary>
    /// <param name="sceneId">Scene value identifier</param>
    /// <param name="expireSeconds">QR code validity time</param>
    /// <returns></returns>
    public static CreateTicketModel EnumScene(
        int sceneId,
        int expireSeconds = 60)
    {
        return new CreateTicketModel
        {
            ExpireSeconds = expireSeconds,
            ActionName = "QR_SCENE",
            SceneInfo = new EnumScene(sceneId),
        };
    }
    /// <summary>
    /// Create a permanent QR code ticket through the scene value identifier
    /// </summary>
    /// <param name="sceneId">Scene value identifier</param>
    /// <returns></returns>
    public static CreateTicketModel LimitEnumScene(int sceneId)
    {
        return new CreateTicketModel
        {
            ActionName = "QR_LIMIT_SCENE",
            SceneInfo = new EnumScene(sceneId),
        };
    }
}
