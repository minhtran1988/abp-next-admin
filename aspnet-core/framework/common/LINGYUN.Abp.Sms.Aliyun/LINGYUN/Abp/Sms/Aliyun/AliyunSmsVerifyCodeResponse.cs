namespace LINGYUN.Abp.Sms.Aliyun;
public class AliyunSmsVerifyCodeResponse
{
    /// <summary>
    /// Request status code, OK means the request is successful
    /// </summary>
    public string Code { get; set; } = default!;
    /// <summary>
    /// Description of status code
    /// </summary>
    public string Message { get; set; } = default!;
    /// <summary>
    /// Whether the request was successful
    /// </summary>
    public bool Success { get; set; }
    /// <summary>
    /// Request result data
    /// </summary>
    public AliyunSmsVerifyCodeModel Model { get; set; } = default!;
}

public class AliyunSmsVerifyCodeModel
{
    /// <summary>
    /// Request ID
    /// </summary>
    public string RequestId { get; set; } = default!;
    /// <summary>
    /// Business ID
    /// </summary>
    public string? BizId { get; set; }
    /// <summary>
    /// External serial number
    /// </summary>
    public string? OutId { get; set; }
    /// <summary>
    /// Verification code, carried only when the verification code is generated using Alibaba Cloud SMS Verification Service
    /// </summary>
    public string? VerifyCode { get; set; }
}
