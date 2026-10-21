namespace LINGYUN.Abp.OpenApi;

public class AppDescriptor
{
    /// <summary>
    /// Application name
    /// </summary>
    public string AppName { get; set; } = default!;
    /// <summary>
    /// Application ID
    /// </summary>
    public string AppKey { get; set; } = default!;
    /// <summary>
    /// Application key
    /// </summary>
    public string AppSecret { get; set; } = default!;
    /// <summary>
    /// Application token
    /// </summary>
    public string? AppToken { get; set; }
    /// <summary>
    /// Signature validity time
    /// Unit: s
    /// </summary>
    public int? SignLifetime { get; set; }

    public AppDescriptor() { }
    public AppDescriptor(
        string appName,
        string appKey,
        string appSecret,
        string? appToken = null,
        int? signLifeTime = null)
    {
        AppName = appName;
        AppKey = appKey;
        AppSecret = appSecret;
        AppToken = appToken;
        SignLifetime = signLifeTime;
    }
}
