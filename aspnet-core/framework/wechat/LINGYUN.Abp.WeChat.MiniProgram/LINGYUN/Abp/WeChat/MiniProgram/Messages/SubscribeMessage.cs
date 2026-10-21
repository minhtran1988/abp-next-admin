using Newtonsoft.Json;
using System.Collections.Generic;

namespace LINGYUN.Abp.WeChat.MiniProgram.Messages;

public class SubscribeMessage
{
    /// <summary>
    /// The openid of the recipient (user)
    /// </summary>
    [JsonProperty("touser")]
    public string ToUser { get; set; } = default!;
    /// <summary>
    /// The subscription template id to be distributed
    /// </summary>
    [JsonProperty("template_id")]
    public string TemplateId { get; set; } = default!;
    /// <summary>
    /// The jump page after clicking the template card is limited to pages within this mini program.
    /// Supports parameters, (example index?foo=bar).
    /// If this field is not filled in, the template will not jump.
    /// </summary>
    [JsonProperty("page")]
    public string? Page { get; set; }
    /// <summary>
    /// Jump applet type:
    /// developer is the development version; trial is the experience version; formal is the official version;
    /// The default is the official version
    /// </summary>
    [JsonProperty("miniprogram_state")]
    public string? MiniProgramState { get; set; }
    /// <summary>
    /// Enter the mini program to view the language type of "
    /// Supports zh_CN (Simplified Chinese), en_US (English), zh_HK (Traditional Chinese), zh_TW (Traditional Chinese),
    /// The default is zh_CN
    /// </summary>
    [JsonProperty("lang")]
    public string? Lang { get; set; } = "zh_CN";
    /// <summary>
    /// Template content,
    /// The format is { "key1": { "value": any }, "key2": { "value": any } }
    /// </summary>
    [JsonProperty("data")]
    public Dictionary<string, MessageData> Data { get; set; } = new Dictionary<string, MessageData>();

    public SubscribeMessage() { }
    public SubscribeMessage(
        string openId,
        string templateId,
        string? redirectPage = "",
        string? state = "formal",
        string? miniLang = "zh_CN")
    {
        ToUser = openId;
        TemplateId = templateId;
        Page = redirectPage;
        MiniProgramState = state;
        Lang = miniLang;
    }

    public SubscribeMessage WriteData(string prefix, string key, object value)
    {
        // Only intercept data that matches the mark
        if (key.StartsWith(prefix))
        {
            key = key.Replace(prefix, "");
            if (!Data.ContainsKey(key))
            {
                Data.Add(key, new MessageData(value));
            }
        }
        return this;
    }

    public SubscribeMessage WriteData(string prefix, IDictionary<string, object?>? setData)
    {
        if (setData != null)
        {
            foreach (var kv in setData)
            {
                WriteData(prefix, kv.Key, kv.Value ?? "");
            }
        } 
        return this;
    }

    public SubscribeMessage WriteData(IDictionary<string, object?>? setData)
    {
        if (setData != null)
        {
            foreach (var kv in setData)
            {
                if (!Data.ContainsKey(kv.Key))
                {
                    Data.Add(kv.Key, new MessageData(kv.Value ?? ""));
                }
            }
        }
        return this;
    }
}

public class MessageData
{
    [JsonProperty("value")]
    public object Value { get; }

    public MessageData(object value)
    {
        Value = value;
    }
}
