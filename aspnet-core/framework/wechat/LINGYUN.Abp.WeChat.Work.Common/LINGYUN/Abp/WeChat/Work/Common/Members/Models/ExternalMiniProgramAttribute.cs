using JetBrains.Annotations;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.WeChat.Work.Common.Members.Models;
/// <summary>
/// Mini program type attributes
/// </summary>
public class ExternalMiniProgramAttribute : ExternalAttribute
{
    /// <summary>
    /// Mini program
    /// </summary>
    [NotNull]
    [JsonProperty("miniprogram")]
    [JsonPropertyName("miniprogram")]
    public ExternalMiniProgramModel MiniProgram { get; set; } = default!;
}

public class ExternalMiniProgramModel
{
    /// <summary>
    /// The applet appid must be an applet authorized to be installed in this enterprise, otherwise it will be ignored.
    /// </summary>
    [NotNull]
    [JsonProperty("appid")]
    [JsonPropertyName("appid")]
    public string AppId { get; set; } = default!;
    /// <summary>
    /// The display title of the mini program, the length is limited to 12 UTF8 characters
    /// </summary>
    [NotNull]
    [JsonProperty("title")]
    [JsonPropertyName("title")]
    public string Title { get; set; } = default!;
    /// <summary>
    /// The page path of the mini program
    /// </summary>
    [NotNull]
    [JsonProperty("pagepath")]
    [JsonPropertyName("pagepath")]
    public string PagePath { get; set; } = default!;
}
