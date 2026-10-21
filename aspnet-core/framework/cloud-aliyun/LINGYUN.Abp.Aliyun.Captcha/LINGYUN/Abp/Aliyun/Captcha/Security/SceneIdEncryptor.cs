using System;
using System.Security.Cryptography;
using System.Text;
using Volo.Abp.DependencyInjection;

namespace LINGYUN.Abp.Aliyun.Captcha.Security;

public class SceneIdEncryptor : ISceneIdEncryptor, ISingletonDependency
{
    // IV is fixed to 16 bytes (AES block size)
    private const int IvLengthBytes = 16;

    /// <summary>
    /// Encrypt sceneId and generate EncryptedSceneId
    /// </summary>
    /// <param name="sceneId">Verification code business ID (formerly CaptchaAppid)</param>
    /// <param name="ekeyStr">ekey obtained by the console (as the key base, Base64 string)</param>
    /// <param name="expireTimeSec">Cryptotext expiration time, unit seconds, range 1~86400</param>
    /// <returns>Base64 encoded string: EncryptedSceneId (IV + encrypted data)</returns>
    public virtual string Encrypt(string sceneId, string ekeyStr, int expireTimeSec)
    {
        if (expireTimeSec <= 0 || expireTimeSec > 86400)
        {
            throw new ArgumentOutOfRangeException(
                nameof(expireTimeSec),
                "expireTimeSec must be between 1 and 86400 seconds.");
        }

        // Get the current timestamp (second level)
        var timestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds();

        // ==================== Step 1: Construct key Key (32 bytes) ====================
        var keyBytes = Convert.FromBase64String(ekeyStr);

        if (keyBytes.Length != 32)
        {
            throw new ArgumentException("ekey must decode to 32 bytes for AES-256.", nameof(ekeyStr));
        }

        // ==================== Step 2: Construct plaintext and perform AES-256-CBC encryption ====================
        // Plain text format: sceneId&timestamp&expireTime
        var plaintext = $"{sceneId}&{timestamp}&{expireTimeSec}";
        var plaintextBytes = Encoding.UTF8.GetBytes(plaintext);

        // Randomly generate 16-byte IV
        var iv = new byte[IvLengthBytes];
        RandomNumberGenerator.Fill(iv);

        byte[] encryptedBytes;

        using (var aes = Aes.Create())
        {
            aes.KeySize = 256;
            aes.BlockSize = 128;
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;
            aes.Key = keyBytes;
            aes.IV = iv;

            using var encryptor = aes.CreateEncryptor();
            encryptedBytes = encryptor.TransformFinalBlock(plaintextBytes, 0, plaintextBytes.Length);
        }

        // ==================== Step 3: Splice IV + ciphertext and perform Base64 encoding ====================
        var result = new byte[IvLengthBytes + encryptedBytes.Length];
        Buffer.BlockCopy(iv, 0, result, 0, IvLengthBytes);
        Buffer.BlockCopy(encryptedBytes, 0, result, IvLengthBytes, encryptedBytes.Length);

        return Convert.ToBase64String(result);
    }
}
