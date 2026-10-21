namespace LINGYUN.Abp.WeChat.MiniProgram;

public class AbpWeChatMiniProgramOptions
{
    /// <summary>
    /// Mini program AppId
    /// </summary>
    public string AppId { get; set; } = default!;
    /// <summary>
    /// Mini Program AppSecret
    /// </summary>
    public string AppSecret { get; set; } = default!;
    /// <summary>
    /// Mini program message decryption token
    /// </summary>
    public string Token { get; set; } = default!;
    /// <summary>
    /// Mini program message decryption AESKey
    /// </summary>
    public string EncodingAESKey { get; set; } = default!;
}
