namespace LINGYUN.Abp.OpenApi;

public static class AbpOpenApiConsts
{
    public const string SecurityChecking = "_AbpOpenApiSecurityChecking";

    public const string HEADER_APP_KEY = "X-API-APPKEY";
    public const string HEADER_SIGNATURE = "X-API-SIGN";
    public const string HEADER_NONCE = "X-API-NONCE";
    public const string HEADER_TIMESTAMP = "X-API-TIMESTAMP";

    public const string KeyPrefix = "AbpOpenApi";
    /// <summary>
    /// Invalid application identifier {AppKey}.
    /// </summary>
    public const string InvalidAccessWithAppKey = KeyPrefix + ":9100";
    /// <summary>
    /// Does not carry the application identifier (appKey).
    /// </summary>
    public const string InvalidAccessWithAppKeyNotFound = KeyPrefix + ":9101";

    /// <summary>
    /// Invalid signature sign.
    /// </summary>
    public const string InvalidAccessWithSign = KeyPrefix + ":9110";
    /// <summary>
    /// Does not carry signature.
    /// </summary>
    public const string InvalidAccessWithSignNotFound = KeyPrefix + ":9111";

    /// <summary>
    /// The request timed out or the session has expired.
    /// </summary>
    public const string InvalidAccessWithTimestamp = KeyPrefix + ":9210";
    /// <summary>
    /// Does not carry timestamp identifier.
    /// </summary>
    public const string InvalidAccessWithTimestampNotFound = KeyPrefix + ":9211";

    /// <summary>
    /// Repeat the initiated request.
    /// </summary>
    public const string InvalidAccessWithNonceRepeated = KeyPrefix + ":9220";
    /// <summary>
    /// Does not carry random numbers.
    /// </summary>
    public const string InvalidAccessWithNonceNotFound = KeyPrefix + ":9221";

    /// <summary>
    /// The client is not within the allowed range.
    /// </summary>
    public const string InvalidAccessWithClientId = KeyPrefix + ":9300";
    /// <summary>
    /// The client IP is not within the allowed range.
    /// </summary>
    public const string InvalidAccessWithIpAddress = KeyPrefix + ":9400";
}
