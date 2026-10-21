namespace LINGYUN.Abp.Sms.Aliyun;
public class SmsVerifyCodeMessage
{
    /// <summary>
    /// Scheme name, if left blank, it will be "default scheme". Maximum 20 characters.
    /// </summary>
    public string? SchemeName { get; set; }
    /// <summary>
    /// Number country code. The default is 86, and currently only supports domestic numbers in China.
    /// </summary>
    public string? CountryCode { get; set; }
    /// <summary>
    /// Uplink SMS extension code. Uplink text messages refer to text messages sent to communication service providers. They are used to customize certain services, complete inquiries, or handle certain services. Charges are required and are deducted according to the operator's ordinary SMS rates.
    /// </summary>
    public string? SmsUpExtendCode { get; set; }
    /// <summary>
    /// External serial number.
    /// </summary>
    public string? OutId { get; set; }
    /// <summary>
    /// The verification code length supports 4 to 8 digits, and the default is 4 digits.
    /// </summary>
    public long? CodeLength { get; set; }
    /// <summary>
    /// The validity period of the verification code, in seconds, the default is 300 seconds.
    /// </summary>
    public long? ValidTime { get; set; }
    /// <summary>
    /// Verification rules, when the verification code is sent repeatedly to the same number in the same scenario within the validity period, how to deal with the old verification code.
    /// </summary>
    /// <remarks>
    /// 1 - Overwrite processing (default), that is, the old verification code will become invalid. <br />
    /// 2 - Reserved, that is, multiple verification codes can be verified within the validity period.
    /// </remarks>
    public long? DuplicatePolicy { get; set; }
    /// <summary>
    /// Time interval, unit: seconds. That is, how often the verification code can be sent, which is used for frequency control. The default is 60 seconds.
    /// </summary>
    public long? Interval { get; set; }
    /// <summary>
    /// The type of verification code generated. When the parameter TemplateParam is passed in a placeholder, this parameter is required, and the system will generate a verification code according to the specified rules.
    /// </summary>
    /// <remarks>
    /// 1 - Pure number (default).
    /// 2 - Plain uppercase letters.
    /// 3 - Pure lowercase letters.
    /// 4 - Mix of small and large letters.
    /// 5 - Mixed numbers + uppercase letters.
    /// 6 - A mix of numbers + lowercase letters.
    /// 7 - Mixed numbers + uppercase and lowercase letters.
    /// </remarks>
    public long? CodeType { get; set; }
    /// <summary>
    /// Whether to return the verification code.
    /// </summary>
    public bool? ReturnVerifyCode { get; set; }
    /// <summary>
    /// Whether to automatically replace the signature and retry (enabled by default.
    /// </summary>
    public bool? AutoRetry { get; set; }
    /// <summary>
    /// The mobile phone number of the SMS recipient.
    /// </summary>
    public string PhoneNumber { get; }
    /// <summary>
    /// Signature name. The use of custom signatures is not currently supported. Please use the signatures provided by the system.
    /// </summary>
    public string? SignName { get; }
    /// <summary>
    /// SMS template CODE. When selecting a gift signature as the parameter SignName, the text message must be sent with a gift template. You can select a template suitable for your business scenario on the free template configuration page.
    /// </summary>
    public string? TemplateCode { get; }
    /// <summary>
    /// SMS template parameters.
    /// </summary>
    public SmsVerifyCodeMessageParam TemplateParam { get; }
    public SmsVerifyCodeMessage(
        string phoneNumber, 
        SmsVerifyCodeMessageParam templateParam,
        string? signName = null,
        string? templateCode = null)
    {
        PhoneNumber = phoneNumber;
        TemplateParam = templateParam;
        SignName = signName;
        TemplateCode = templateCode;
    }
}
