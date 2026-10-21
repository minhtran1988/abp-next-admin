using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.WeChat.Official.Account.Models;
public class EnumScene : Scene
{
    /// <summary>
    /// Scene value ID, which is a 32-bit non-0 integer when using a temporary QR code. The maximum value when using a permanent QR code is 100000 (currently the parameter only supports 1--100000)
    /// </summary>
    [JsonProperty("scene_id")]
    [JsonPropertyName("scene_id")]
    public int SceneId { get; }
    public EnumScene(int sceneId)
    {
        SceneId = sceneId;
    }

    public override string GetKey()
    {
        return SceneId.ToString();
    }
}
