using JetBrains.Annotations;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.WeChat.Work.OA.Approvals.Models;
/// <summary>
/// Attachment control value
/// </summary>
public class FileControlValue : ControlValue
{
    /// <summary>
    /// Attachment list
    /// </summary>
    [NotNull]
    [JsonProperty("files")]
    [JsonPropertyName("files")]
    public List<FileValue> Files { get; set; } = default!;
    public FileControlValue()
    {

    }

    public FileControlValue(List<FileValue> files)
    {
        Files = files;
    }
}

public class FileValue
{
    /// <summary>
    /// File id, this id is the media_id returned by the temporary material upload interface. Note: After the bill of lading, it will be converted into a document content for long-term file storage; currently, an approval application form only supports the upload of 6 attachments globally, otherwise it will fail.
    /// </summary>
    [NotNull]
    [JsonProperty("file_id")]
    [JsonPropertyName("file_id")]
    public string FileId { get; set; } = default!;
    /// <summary>
    /// File name, type is string. If there is no string, you can fill in the blank string.
    /// </summary>
    [CanBeNull]
    [JsonProperty("file_name")]
    [JsonPropertyName("file_name")]
    public string? FileName { get; set; }
    /// <summary>
    /// File size, type is number, if not, you can fill in the blank string.
    /// </summary>
    [CanBeNull]
    [JsonProperty("file_size")]
    [JsonPropertyName("file_size")]
    public long? FileSize { get; set; }
    /// <summary>
    /// File type, the type is string. If there is no string, you can fill in the blank string.
    /// </summary>
    [CanBeNull]
    [JsonProperty("file_type")]
    [JsonPropertyName("file_type")]
    public string? FileType { get; set; }
    /// <summary>
    /// File address, type is string. If there is no string, you can fill in the blank string.
    /// </summary>
    [CanBeNull]
    [JsonProperty("file_url")]
    [JsonPropertyName("file_url")]
    public string? FileUrl { get; set; }
    public FileValue()
    {

    }

    public FileValue(string fileId)
    {
        FileId = fileId;
    }
}
