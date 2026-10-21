namespace LINGYUN.Abp.WeChat.Official;

public class AbpWeChatOfficialOptions
{
    /// <summary>
    /// Whether to sandbox test
    /// </summary>
    /// <remarks>
    /// Tips: When using the test number, the message is transmitted in clear text and needs to be enabled during debugging.
    /// </remarks>
    public bool IsSandBox { get; set; }
    /// <summary>
    /// Official account server message Url
    /// </summary>
    public string Url { get; set; } = default!;
    /// <summary>
    /// Official account AppId
    /// </summary>
    public string AppId { get; set; } = default!;
    /// <summary>
    /// Official account AppSecret
    /// </summary>
    public string AppSecret { get; set; } = default!;
    /// <summary>
    /// Official account message decryption token
    /// </summary>
    public string Token { get; set; } = default!;
    /// <summary>
    /// Official account message decryption AESKey
    /// </summary>
    public string EncodingAESKey { get; set; } = default!;
}
