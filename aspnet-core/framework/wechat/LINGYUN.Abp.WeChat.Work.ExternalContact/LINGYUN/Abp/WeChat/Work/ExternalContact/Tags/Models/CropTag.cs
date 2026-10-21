using JetBrains.Annotations;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.WeChat.Work.ExternalContact.Tags.Models;
public class CropTag : Tag
{
    /// <summary>
    /// Whether the tag has been deleted will only be returned when querying by specifying tag_id/group_id
    /// </summary>
    [NotNull]
    [JsonProperty("deleted")]
    [JsonPropertyName("deleted")]
    public bool Deleted { get; set; }
}
