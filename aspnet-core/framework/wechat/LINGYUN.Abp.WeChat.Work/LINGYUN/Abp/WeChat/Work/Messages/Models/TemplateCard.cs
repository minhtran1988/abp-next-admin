using JetBrains.Annotations;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.WeChat.Work.Messages.Models;

public abstract class TemplateCard
{
    /// <summary>
    /// Template card type
    /// </summary>
    [NotNull]
    [JsonProperty("card_type")]
    [JsonPropertyName("card_type")]
    public string Type { get; }
    /// <summary>
    /// Task id. The same application task id cannot be repeated. It can only consist of numbers, letters and "_-@", with a maximum length of 128 bytes.
    /// </summary>
    [CanBeNull]
    [JsonProperty("task_id")]
    [JsonPropertyName("task_id")]
    public string TaskId { get; set; }
    protected TemplateCard(string type, string taskId = "")
    {
        Type = type;
        TaskId = taskId;
    }
}
