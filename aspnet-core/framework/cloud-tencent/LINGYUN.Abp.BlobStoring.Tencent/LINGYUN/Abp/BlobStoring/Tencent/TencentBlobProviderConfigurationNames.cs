namespace LINGYUN.Abp.BlobStoring.Tencent;

public static class TencentBlobProviderConfigurationNames
{
    /// <summary>
    /// AppId
    /// </summary>
    public const string AppId = "Tencent:OSS:AppId";
    /// <summary>
    /// area
    /// </summary>
    public const string Region = "Tencent:OSS:Region";
    /// <summary>
    /// namespace
    /// </summary>
    public const string BucketName = "Tencent:OSS:BucketName";
    /// <summary>
    /// Whether to create the namespace if it does not exist
    /// </summary>
    public const string CreateBucketIfNotExists = "Tencent:OSS:CreateBucketIfNotExists";
    /// <summary>
    /// Anti-hotlink list when creating namespace
    /// </summary>
    public const string CreateBucketReferer = "Tencent:OSS:CreateBucketReferer";
    /// <summary>
    /// Generate the expiration time of the pre-signed Uri (s)
    /// </summary>
    public const string PresignedGetExpirySeconds = "Tencent:OSS:PresignedGetExpirySeconds";
}
