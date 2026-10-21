namespace LINGYUN.Abp.BlobStoring.Aliyun;

public static class AliyunBlobProviderConfigurationNames
{
    /// <summary>
    /// data center
    /// </summary>
    public const string Endpoint = "Aliyun:OSS:Endpoint";
    /// <summary>
    /// namespace
    /// </summary>
    public const string BucketName = "Aliyun:OSS:BucketName";
    /// <summary>
    /// Signature version (optional: v1, v4)
    /// </summary>
    public const string SignatureVersion = "Aliyun:OSS:SignatureVersion";
    /// <summary>
    /// Skip server certificate verification
    /// </summary>
    public const string InsecureSkipVerify = "Aliyun:OSS:InsecureSkipVerify";
    /// <summary>
    /// Whether to create the namespace if it does not exist
    /// </summary>
    public const string CreateBucketIfNotExists = "Aliyun:OSS:CreateBucketIfNotExists";
    /// <summary>
    /// Anti-hotlink list when creating namespace
    /// </summary>
    public const string CreateBucketReferer = "Aliyun:OSS:CreateBucketReferer";
    /// <summary>
    /// Acl when creating namespace
    /// </summary>
    public const string CreateBucketAcl = "Aliyun:OSS:CreateBucketAcl"; 
    /// <summary>
    /// Generate the expiration time of the pre-signed Uri (s)
    /// </summary>
    public const string PresignedGetExpirySeconds = "Aliyun:OSS:PresignedGetExpirySeconds";

    public const string UsePathStyle = "Aliyun:OSS:UsePathStyle";
    public const string UseCName = "Aliyun:OSS:UseCName";
    public const string UseDualStackEndpoint = "Aliyun:OSS:UseDualStackEndpoint";
    public const string UseAccelerateEndpoint = "Aliyun:OSS:UseAccelerateEndpoint";
    public const string UseInternalEndpoint = "Aliyun:OSS:UseInternalEndpoint";
    public const string DisableUploadCrc64Check = "Aliyun:OSS:DisableUploadCrc64Check";
    public const string DisableDownloadCrc64Check = "Aliyun:OSS:DisableDownloadCrc64Check";
}
