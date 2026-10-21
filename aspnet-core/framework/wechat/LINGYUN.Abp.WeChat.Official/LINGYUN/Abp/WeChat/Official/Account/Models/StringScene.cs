using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.WeChat.Official.Account.Models;
public class StringScene : Scene
{
    /// <summary>
    /// Scene value ID (ID in string form), string type, length limit is 1 to 64
    /// </summary>
    [JsonProperty("scene_str")]
    [JsonPropertyName("scene_str")]
    public string SceneStr { get; }
    public StringScene(string sceneStr)
    {
        SceneStr = sceneStr;
    }

    public override string GetKey()
    {
        return SceneStr;
    }
}
