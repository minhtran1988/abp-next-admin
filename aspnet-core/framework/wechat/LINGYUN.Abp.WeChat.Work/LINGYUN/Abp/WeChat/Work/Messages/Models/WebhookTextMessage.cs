using JetBrains.Annotations;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Volo.Abp;

namespace LINGYUN.Abp.WeChat.Work.Messages.Models;
/// <summary>
/// Webhook text message body
/// </summary>
public class WebhookTextMessage
{
    /// <summary>
    /// Erase text content, up to 2048 bytes, must be UTF8 encoding
    /// </summary>
    [NotNull]
    [StringLength(2048)]
    [JsonProperty("content")]
    [JsonPropertyName("content")]
    public string Content { get; set; }
    /// <summary>
    /// A list of userids to remind designated members in the group (@a certain member). @all means to remind everyone. If the developer cannot obtain the userid, mentioned_mobile_list can be used
    /// </summary>
    [NotNull]
    [JsonProperty("mentioned_list")]
    [JsonPropertyName("mentioned_list")]
    public List<string> MentionedList { get; set; }
    /// <summary>
    /// List of mobile phone numbers to remind the group members corresponding to the mobile phone number (@a certain member), @all means to remind everyone
    /// </summary>
    [NotNull]
    [StringLength(2048)]
    [JsonProperty("mentioned_mobile_list")]
    [JsonPropertyName("mentioned_mobile_list")]
    public List<string> MentionedMobileList { get; set; }
    /// <summary>
    /// Create a Webhook text message body
    /// </summary>
    /// <param name="content">Message content</param>
    public WebhookTextMessage(string content)
    {
        Content = Check.NotNullOrWhiteSpace(content, nameof(content), 2048);

        MentionedList = new List<string>();
        MentionedMobileList = new List<string>();
    }
}
