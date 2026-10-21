using AlibabaCloud.OSS.V2.Models;
using System.Collections.Generic;
using Volo.Abp.BlobStoring;

namespace LINGYUN.Abp.BlobStoring.Aliyun;

public class AliyunBlobProviderConfiguration
{
    /// <summary>
    /// Default signature version
    /// </summary>
    public const string DefaultSignatureVersion = "v1"; //TODO: Alibaba Cloud OSS SDKv2 SignerV4.ResourcePath is defective, v1 signature is temporarily used
    /// <summary>
    /// Skip server certificate verification by default
    /// </summary>
    public const bool DefaultInsecureSkipVerify = false;
    /// <summary>
    /// Whether to create the default namespace if it does not exist
    /// </summary>
    public const bool DefaultCreateBucketIfNotExists = false;
    /// <summary>
    /// Default pre-signed link expiration time
    /// </summary>
    public const int DefaultPresignedGetExpirySeconds = 600;
    /// <summary>
    /// namespace
    /// </summary>
    public string BucketName
    {
        get => _containerConfiguration.GetConfiguration<string>(AliyunBlobProviderConfigurationNames.BucketName);
        set => _containerConfiguration.SetConfiguration(AliyunBlobProviderConfigurationNames.BucketName, value);
    }
    /// <summary>
    /// Signature version (optional: v1, v4)
    /// Default: v1
    /// </summary>
    public string? SignatureVersion {
        get => _containerConfiguration.GetConfigurationOrDefault(AliyunBlobProviderConfigurationNames.SignatureVersion, DefaultSignatureVersion);
        set => _containerConfiguration.SetConfiguration(AliyunBlobProviderConfigurationNames.SignatureVersion, value);
    }
    /// <summary>
    /// Skip server certificate verification
    /// </summary>
    public bool InsecureSkipVerify {
        get => _containerConfiguration.GetConfigurationOrDefault(AliyunBlobProviderConfigurationNames.InsecureSkipVerify, DefaultInsecureSkipVerify);
        set => _containerConfiguration.SetConfiguration(AliyunBlobProviderConfigurationNames.InsecureSkipVerify, value);
    }
    /// <summary>
    /// Whether to create the namespace if it does not exist
    /// </summary>
    public bool CreateBucketIfNotExists
    {
        get => _containerConfiguration.GetConfigurationOrDefault(AliyunBlobProviderConfigurationNames.CreateBucketIfNotExists, DefaultCreateBucketIfNotExists);
        set => _containerConfiguration.SetConfiguration(AliyunBlobProviderConfigurationNames.CreateBucketIfNotExists, value);
    }
    /// <summary>
    /// Acl when creating namespace
    /// </summary>
    public BucketAclType? CreateBucketAcl {
        get => _containerConfiguration.GetConfigurationOrDefault<BucketAclType?>(AliyunBlobProviderConfigurationNames.CreateBucketAcl);
        set => _containerConfiguration.SetConfiguration(AliyunBlobProviderConfigurationNames.CreateBucketAcl, value);
    }
    /// <summary>
    /// Anti-hotlink list when creating namespace
    /// </summary>
    public List<string>? CreateBucketReferer
    {
        get => _containerConfiguration.GetConfigurationOrDefault(AliyunBlobProviderConfigurationNames.CreateBucketReferer, new List<string>());
        set
        {
            if (value == null)
            {
                _containerConfiguration.SetConfiguration(AliyunBlobProviderConfigurationNames.CreateBucketReferer, new List<string>());
            }
            else
            {
                _containerConfiguration.SetConfiguration(AliyunBlobProviderConfigurationNames.CreateBucketReferer, value);
            }
        }
    }


    public int PresignedGetExpirySeconds {
        get => _containerConfiguration.GetConfigurationOrDefault(AliyunBlobProviderConfigurationNames.PresignedGetExpirySeconds, DefaultPresignedGetExpirySeconds);
        set => _containerConfiguration.SetConfiguration(AliyunBlobProviderConfigurationNames.PresignedGetExpirySeconds, value);
    }

    public string? Endpoint {
        get => _containerConfiguration.GetConfigurationOrDefault<string>(AliyunBlobProviderConfigurationNames.Endpoint, null);
        set => _containerConfiguration.SetConfiguration(AliyunBlobProviderConfigurationNames.Endpoint, value);
    }
    public bool? UsePathStyle {
        get => _containerConfiguration.GetConfigurationOrDefault<bool?>(AliyunBlobProviderConfigurationNames.UsePathStyle, null);
        set => _containerConfiguration.SetConfiguration(AliyunBlobProviderConfigurationNames.UsePathStyle, value);
    }
    public bool? UseCName {
        get => _containerConfiguration.GetConfigurationOrDefault<bool?>(AliyunBlobProviderConfigurationNames.UseCName, null);
        set => _containerConfiguration.SetConfiguration(AliyunBlobProviderConfigurationNames.UseCName, value);
    }
    public bool? UseDualStackEndpoint {
        get => _containerConfiguration.GetConfigurationOrDefault<bool?>(AliyunBlobProviderConfigurationNames.UseDualStackEndpoint, null);
        set => _containerConfiguration.SetConfiguration(AliyunBlobProviderConfigurationNames.UseDualStackEndpoint, value);
    }
    public bool? UseAccelerateEndpoint {
        get => _containerConfiguration.GetConfigurationOrDefault<bool?>(AliyunBlobProviderConfigurationNames.UseAccelerateEndpoint, null);
        set => _containerConfiguration.SetConfiguration(AliyunBlobProviderConfigurationNames.UseAccelerateEndpoint, value);
    }
    public bool? UseInternalEndpoint {
        get => _containerConfiguration.GetConfigurationOrDefault<bool?>(AliyunBlobProviderConfigurationNames.UseInternalEndpoint, null);
        set => _containerConfiguration.SetConfiguration(AliyunBlobProviderConfigurationNames.UseInternalEndpoint, value);
    }
    public bool? DisableUploadCrc64Check {
        get => _containerConfiguration.GetConfigurationOrDefault<bool?>(AliyunBlobProviderConfigurationNames.DisableUploadCrc64Check, null);
        set => _containerConfiguration.SetConfiguration(AliyunBlobProviderConfigurationNames.DisableUploadCrc64Check, value);
    }
    public bool? DisableDownloadCrc64Check {
        get => _containerConfiguration.GetConfigurationOrDefault<bool?>(AliyunBlobProviderConfigurationNames.DisableDownloadCrc64Check, null);
        set => _containerConfiguration.SetConfiguration(AliyunBlobProviderConfigurationNames.DisableDownloadCrc64Check, value);
    }

    private readonly BlobContainerConfiguration _containerConfiguration;

    public AliyunBlobProviderConfiguration(BlobContainerConfiguration containerConfiguration)
    {
        _containerConfiguration = containerConfiguration;
    }
}
