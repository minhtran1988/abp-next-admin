using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Collections;
// using System.Web;
using System.Security.Cryptography;
//-40001: Signature verification error
//-40002: xml parsing failed
//-40003: SHA encryption failed to generate signature
//-40004: AESKey illegal
//-40005: corpid verification error
//-40006: AES encryption failed
//-40007: AES decryption failed
//-40008: The buffer obtained after decryption is illegal
//-40009: base64 encryption exception
//-40010: base64 decryption exception
namespace LINGYUN.Abp.WeChat.Common.Utils;
#nullable disable
public class WXBizMsgCrypt
{
    string m_sToken;
    string m_sEncodingAESKey;
    string m_sReceiveId;
    enum WXBizMsgCryptErrorCode
    {
        WXBizMsgCrypt_OK = 0,
        WXBizMsgCrypt_ValidateSignature_Error = -40001,
        WXBizMsgCrypt_ParseXml_Error = -40002,
        WXBizMsgCrypt_ComputeSignature_Error = -40003,
        WXBizMsgCrypt_IllegalAesKey = -40004,
        WXBizMsgCrypt_ValidateCorpid_Error = -40005,
        WXBizMsgCrypt_EncryptAES_Error = -40006,
        WXBizMsgCrypt_DecryptAES_Error = -40007,
        WXBizMsgCrypt_IllegalBuffer = -40008,
        WXBizMsgCrypt_EncodeBase64_Error = -40009,
        WXBizMsgCrypt_DecodeBase64_Error = -40010
    };

    // Constructor
    // @param sToken: Enterprise WeChat backend, Token set by the developer
    // @param sEncodingAESKey: Enterprise WeChat backend, EncodingAESKey set by the developer
    // @param sReceiveId: Different scenarios have different meanings. Please refer to the documentation for details.
    public WXBizMsgCrypt(string sToken, string sEncodingAESKey, string sReceiveId)
    {
        m_sToken = sToken;
        m_sReceiveId = sReceiveId;
        m_sEncodingAESKey = sEncodingAESKey;
    }

    // Verify URL
    // @param sMsgSignature: signature string, corresponding to the msg_signature of the URL parameter
    // @param sTimeStamp: timestamp, corresponding to the timestamp of the URL parameter
    // @param sNonce: random string, corresponding to the nonce of the URL parameter
    // @param sEchoStr: random string, corresponding to the echostr of the URL parameter
    // @param sReplyEchoStr: echostr after decryption, valid when return returns 0
    // @return: 0 on success, return the corresponding error code on failure
    public int VerifyURL(string sMsgSignature, string sTimeStamp, string sNonce, string sEchoStr, ref string sReplyEchoStr)
    {
        var ret = 0;
        if (m_sEncodingAESKey.Length != 43)
        {
            return (int)WXBizMsgCryptErrorCode.WXBizMsgCrypt_IllegalAesKey;
        }
        ret = VerifySignature(m_sToken, sTimeStamp, sNonce, sEchoStr, sMsgSignature);
        if (0 != ret)
        {
            return ret;
        }
        sReplyEchoStr = "";
        var cpid = "";
        try
        {
            sReplyEchoStr = Cryptography.AES_decrypt(sEchoStr, m_sEncodingAESKey, ref cpid); //m_sReceiveId);
        }
        catch (Exception)
        {
            sReplyEchoStr = "";
            return (int)WXBizMsgCryptErrorCode.WXBizMsgCrypt_DecryptAES_Error;
        }
        if (cpid != m_sReceiveId)
        {
            sReplyEchoStr = "";
            return (int)WXBizMsgCryptErrorCode.WXBizMsgCrypt_ValidateCorpid_Error;
        }
        return 0;
    }

    // Verify the authenticity of the message and obtain the decrypted plaintext
    // @param sMsgSignature: signature string, corresponding to the msg_signature of the URL parameter
    // @param sTimeStamp: timestamp, corresponding to the timestamp of the URL parameter
    // @param sNonce: random string, corresponding to the nonce of the URL parameter
    // @param sPostData: ciphertext, corresponding to the data of the POST request
    // @param sMsg: The decrypted original text, valid when return returns 0
    // @return: 0 on success, return the corresponding error code on failure
    public int DecryptMsg(string sMsgSignature, string sTimeStamp, string sNonce, string sPostData, ref string sMsg)
    {
        if (m_sEncodingAESKey.Length != 43)
        {
            return (int)WXBizMsgCryptErrorCode.WXBizMsgCrypt_IllegalAesKey;
        }
        var doc = new XmlDocument();
        XmlNode root;
        string sEncryptMsg;
        try
        {
            doc.LoadXml(sPostData);
            root = doc.FirstChild;
            sEncryptMsg = root["Encrypt"].InnerText;
        }
        catch (Exception)
        {
            return (int)WXBizMsgCryptErrorCode.WXBizMsgCrypt_ParseXml_Error;
        }
        // verify signature
        var ret = 0;
        ret = VerifySignature(m_sToken, sTimeStamp, sNonce, sEncryptMsg, sMsgSignature);
        if (ret != 0)
            return ret;
        // decrypt
        var cpid = "";
        try
        {
            sMsg = Cryptography.AES_decrypt(sEncryptMsg, m_sEncodingAESKey, ref cpid);
        }
        catch (FormatException)
        {
            sMsg = "";
            return (int)WXBizMsgCryptErrorCode.WXBizMsgCrypt_DecodeBase64_Error;
        }
        catch (Exception)
        {
            sMsg = "";
            return (int)WXBizMsgCryptErrorCode.WXBizMsgCrypt_DecryptAES_Error;
        }
        if (cpid != m_sReceiveId)
            return (int)WXBizMsgCryptErrorCode.WXBizMsgCrypt_ValidateCorpid_Error;
        return 0;
    }

    // Encrypt and package the enterprise account's reply to the user's message
    // @param sReplyMsg: The enterprise account’s message to be replied to by the user, a string in xml format
    // @param sTimeStamp: timestamp, you can generate it yourself, or you can use the timestamp of the URL parameter
    // @param sNonce: random string, you can generate it yourself, or you can use the nonce of the URL parameter
    // @param sEncryptMsg: The encrypted ciphertext can be directly replied to the user, including msg_signature, timestamp, nonce, encrypted xml format string,
    // Valid when return returns 0
    // return: 0 on success, return the corresponding error code on failure
    public int EncryptMsg(string sReplyMsg, string sTimeStamp, string sNonce, ref string sEncryptMsg)
    {
        if (m_sEncodingAESKey.Length != 43)
        {
            return (int)WXBizMsgCryptErrorCode.WXBizMsgCrypt_IllegalAesKey;
        }
        var raw = "";
        try
        {
            raw = Cryptography.AES_encrypt(sReplyMsg, m_sEncodingAESKey, m_sReceiveId);
        }
        catch (Exception)
        {
            return (int)WXBizMsgCryptErrorCode.WXBizMsgCrypt_EncryptAES_Error;
        }
        var MsgSigature = "";
        var ret = 0;
        ret = GenarateSinature(m_sToken, sTimeStamp, sNonce, raw, ref MsgSigature);
        if (0 != ret)
            return ret;
        sEncryptMsg = "";

        var EncryptLabelHead = "<Encrypt><![CDATA[";
        var EncryptLabelTail = "]]></Encrypt>";
        var MsgSigLabelHead = "<MsgSignature><![CDATA[";
        var MsgSigLabelTail = "]]></MsgSignature>";
        var TimeStampLabelHead = "<TimeStamp><![CDATA[";
        var TimeStampLabelTail = "]]></TimeStamp>";
        var NonceLabelHead = "<Nonce><![CDATA[";
        var NonceLabelTail = "]]></Nonce>";
        sEncryptMsg = sEncryptMsg + "<xml>" + EncryptLabelHead + raw + EncryptLabelTail;
        sEncryptMsg = sEncryptMsg + MsgSigLabelHead + MsgSigature + MsgSigLabelTail;
        sEncryptMsg = sEncryptMsg + TimeStampLabelHead + sTimeStamp + TimeStampLabelTail;
        sEncryptMsg = sEncryptMsg + NonceLabelHead + sNonce + NonceLabelTail;
        sEncryptMsg += "</xml>";
        return 0;
    }

    public class DictionarySort : IComparer
    {
        public int Compare(object oLeft, object oRight)
        {
            var sLeft = oLeft as string;
            var sRight = oRight as string;
            var iLeftLength = sLeft.Length;
            var iRightLength = sRight.Length;
            var index = 0;
            while (index < iLeftLength && index < iRightLength)
            {
                if (sLeft[index] < sRight[index])
                    return -1;
                else if (sLeft[index] > sRight[index])
                    return 1;
                else
                    index++;
            }
            return iLeftLength - iRightLength;

        }
    }
    // Verify Signature
    private static int VerifySignature(string sToken, string sTimeStamp, string sNonce, string sMsgEncrypt, string sSigture)
    {
        var hash = "";
        var ret = 0;
        ret = GenarateSinature(sToken, sTimeStamp, sNonce, sMsgEncrypt, ref hash);
        if (ret != 0)
            return ret;
        if (hash == sSigture)
            return 0;
        else
        {
            return (int)WXBizMsgCryptErrorCode.WXBizMsgCrypt_ValidateSignature_Error;
        }
    }

    public static int GenarateSinature(string sToken, string sTimeStamp, string sNonce, string sMsgEncrypt, ref string sMsgSignature)
    {
        var AL = new ArrayList();
        AL.Add(sToken);
        AL.Add(sTimeStamp);
        AL.Add(sNonce);
        AL.Add(sMsgEncrypt);
        AL.Sort(new DictionarySort());
        var raw = "";
        for (var i = 0; i < AL.Count; ++i)
        {
            raw += AL[i];
        }

        SHA1 sha;
        ASCIIEncoding enc;
        var hash = "";
        try
        {
#if NET462
            sha = new SHA1CryptoServiceProvider();
#else
            sha = SHA1.Create();
#endif
            enc = new ASCIIEncoding();
            byte[] dataToHash = enc.GetBytes(raw);
            byte[] dataHashed = sha.ComputeHash(dataToHash);
            hash = BitConverter.ToString(dataHashed).Replace("-", "");
            hash = hash.ToLower();
        }
        catch (Exception)
        {
            return (int)WXBizMsgCryptErrorCode.WXBizMsgCrypt_ComputeSignature_Error;
        }
        sMsgSignature = hash;
        return 0;
    }
}
#nullable enable