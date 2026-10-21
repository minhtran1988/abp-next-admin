using JetBrains.Annotations;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.WeChat.Work.OA.Approvals.Models;
/// <summary>
/// Attachment control configuration
/// </summary>
public class FileControlConfig : ControlConfig
{
    /// <summary>
    /// Attachment control
    /// </summary>
    [NotNull]
    [JsonProperty("file")]
    [JsonPropertyName("file")]
    public FileConfig File { get; set; } = default!;
    public FileControlConfig()
    {

    }

    public FileControlConfig(FileConfig file)
    {
        File = file;
    }
}

public class FileConfig
{
    /// <summary>
    /// Whether only taking photos is allowed, 1--yes, 0--no
    /// </summary>
    [NotNull]
    [JsonProperty("is_only_photo")]
    [JsonPropertyName("is_only_photo")]
    public byte IsOnlyPhoto { get; set; }
    public FileConfig()
    {

    }

    public FileConfig(byte isOnlyPhoto)
    {
        IsOnlyPhoto = isOnlyPhoto;
    }
}
