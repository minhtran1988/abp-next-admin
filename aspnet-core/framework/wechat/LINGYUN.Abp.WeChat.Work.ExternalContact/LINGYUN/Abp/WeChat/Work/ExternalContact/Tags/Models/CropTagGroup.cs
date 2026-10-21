using JetBrains.Annotations;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.WeChat.Work.ExternalContact.Tags.Models;
public class CropTagGroup : TagGroup
{
    /// <summary>
    /// Whether the tag group has been deleted will only be returned when querying by specifying tag_id.
    /// </summary>
    [NotNull]
    [JsonProperty("deleted")]
    [JsonPropertyName("deleted")]
    public bool Deleted { get; set; }
    /// <summary>
    /// Tag list
    /// </summary>
    [NotNull]
    [JsonProperty("tag")]
    [JsonPropertyName("tag")]
    public CropTag[] Tag { get; set; } = default!;
}
