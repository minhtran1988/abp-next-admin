# LINGYUN.Abp.BlobStoring.Tencent

Tencent Cloud Object Storage (COS) module integrates Tencent Cloud object storage service into the ABP BlobStoring system.

## Features

* Support Tencent Cloud object storage service
* Support multi-tenant configuration
* Support automatic creation of buckets (Bucket)
* Support bucket anti-leeching configuration
* Support multi-region configuration
* Support file size limit
* Supports storage isolation by tenant

## Configuration item description

### Basic configuration

```json
{
  "Settings": {
    "Abp.TencentCloud": {
      "SecretId": "Your Tencent Cloud SecretId", // Get it from the Tencent Cloud console
      "SecretKey": "Your Tencent Cloud SecretKey", // Get it from the Tencent Cloud console
      "DurationSecond": "600" // Session duration, in seconds
    }
  }
}
```

### Object storage configuration

```json
{
  "Tencent": {
    "OSS": {
      "AppId": "", // Tencent Cloud AppId
      "Region": "", // The region where the bucket is located
      "BucketName": "", // Bucket name
      "CreateBucketIfNotExists": false, // Whether to create the bucket if it does not exist
      "CreateBucketReferer": [] // Anti-leeching settings when creating a bucket
    }
  }
}
```

### Bucket naming convention

* Only lowercase English letters and numbers are supported, namely [a-z, 0-9], dash "-" and their combinations
* cannot start or end with a dash (-)
* The maximum allowed characters of the bucket name are affected by the number of characters in the region abbreviation and APPID. The total number of characters in the complete requested domain name is up to 60 characters.
* For more specifications, please refer to [Tencent Cloud Storage Bucket Naming Specification](https://cloud.tencent.com/document/product/436/13312)

### Object naming convention

* It is not allowed to start with a forward slash / or a backslash \\
* The object key does not support the character up (↑), character down (↓), character right (→), and character left (←) in the ASCII control characters
* For more specifications, please refer to [Tencent Cloud Object Naming Specification](https://cloud.tencent.com/document/product/436/13324)

## Basic usage

1. Add module dependencies
```csharp
[DependsOn(typeof(AbpBlobStoringTencentCloudModule))]
public class YourModule : AbpModule
{
    // ...
}
```

2. Configure Tencent Cloud Object Storage
```csharp
Configure<AbpBlobStoringOptions>(options =>
{
    options.Containers.Configure<YourContainer>(container =>
    {
        container.UseTencentCloud(tencent =>
        {
            tencent.AppId = "Your Tencent Cloud AppId";
            tencent.Region = "ap-guangzhou";
            tencent.BucketName = "your-bucket-name";
            tencent.CreateBucketIfNotExists = true;
            tencent.CreateBucketReferer = new List<string>
            {
                "*.example.com",
                "example.com"
            };
        });
    });
});
```

3. Use BLOB storage
```csharp
public class YourService
{
    private readonly IBlobContainer<YourContainer> _blobContainer;

    public YourService(IBlobContainer<YourContainer> blobContainer)
    {
        _blobContainer = blobContainer;
    }

    public async Task SaveBlobAsync(string name, Stream stream)
    {
        await _blobContainer.SaveAsync(name, stream);
    }

    public async Task<Stream> GetBlobAsync(string name)
    {
        return await _blobContainer.GetAsync(name);
    }
}
```

## Advanced features

### Multi-tenant support

The module supports isolated storage by tenant, and the storage path format is as follows:
* Host: `host/{blobName}`
* Tenants: `tenants/{tenantId}/{blobName}`

### Feature Management

The module provides the following feature switches:

* TencentBlobStoring - Controls the enabling/disabling of Tencent Cloud object storage service
* TencentBlobStoringMaximumStreamSize - Controls the maximum size limit of uploaded files (MB)

## More documentation

* [Tencent Cloud Object Storage](https://cloud.tencent.com/document/product/436)
* [Tencent Cloud COS Console](https://console.cloud.tencent.com/cos)
* [ABP BlobStoring system](https://docs.abp.io/en/abp/latest/Blob-Storing)

[English](./README.EN.md)
