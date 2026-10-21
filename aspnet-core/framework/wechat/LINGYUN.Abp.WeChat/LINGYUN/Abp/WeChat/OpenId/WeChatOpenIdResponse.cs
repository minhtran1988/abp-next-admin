using LINGYUN.Abp.WeChat.Common;
using Newtonsoft.Json;
using System;

namespace LINGYUN.Abp.WeChat.OpenId;

/// <summary>
/// WeChat OpenId return object
/// </summary>
public class WeChatOpenIdResponse
{
    /// <summary>
    /// Error code
    /// </summary>
    [JsonProperty("errcode")]
    public string ErrorCode { get; set; } = default!;
    /// <summary>
    /// Session key
    /// </summary>
    [JsonProperty("session_key")]
    public string SessionKey { get; set; } = default!;
    /// <summary>
    /// User unique identifier
    /// </summary>
    [JsonProperty("openid")]
    public string OpenId { get; set; } = default!;
    /// <summary>
    /// The user's unique identifier on the open platform will be returned if the UnionID issuance conditions are met.
    /// </summary>
    [JsonProperty("unionid")]
    public string? UnionId { get; set; }
    /// <summary>
    /// Error message
    /// </summary>
    public string ErrorMessage
    {
        get
        {
            return ErrorCode switch
            {
                "-1" => "The system is busy, developers please try again later",
                "0" => string.Empty,
                "40029" => "code is invalid",
                "45011" => "Frequency limit, 100 times per user per minute",
                _ => $"Undefined exception code: {ErrorCode}, please try again",
            };
            ;
        }
    }
    /// <summary>
    /// If WeChat authentication is successful, there is no errorcode or the errorcode is 0
    /// </summary>
    public bool IsError => !ErrorCode.IsNullOrWhiteSpace() && !"0".Equals(ErrorCode);

    public WeChatOpenId ToWeChatOpenId()
    {
        if(IsError)
        {
            throw new AbpWeChatException(ErrorCode, ErrorMessage);
        }
        return new WeChatOpenId(OpenId, SessionKey, UnionId);
    }
}
