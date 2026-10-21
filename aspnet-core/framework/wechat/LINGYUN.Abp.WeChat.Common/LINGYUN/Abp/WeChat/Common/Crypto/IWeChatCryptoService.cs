using LINGYUN.Abp.WeChat.Common.Crypto.Models;

namespace LINGYUN.Abp.WeChat.Common.Crypto;

public interface IWeChatCryptoService
{
    /// <summary>
    /// Verification
    /// </summary>
    /// <param name="data"></param>
    /// <param name="sReplyEchoStr"></param>
    /// <returns></returns>
    string Validation(WeChatCryptoEchoData data);
    /// <summary>
    /// Decrypt
    /// </summary>
    /// <param name="data"></param>
    /// <param name="sMsg"></param>
    /// <returns></returns>
    string Decrypt(WeChatCryptoDecryptData data);
    /// <summary>
    /// Encryption
    /// </summary>
    /// <param name="data"></param>
    /// <param name="sEncryptMsg"></param>
    /// <returns></returns>
    string Encrypt(WeChatCryptoEncryptData data);
}
