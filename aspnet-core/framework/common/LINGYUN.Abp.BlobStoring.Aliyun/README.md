# LINGYUN.Abp.BlobStoring.Aliyun

Alibaba Cloud OSS implementation of the ABP framework object storage provider **IBlobProvider**.

## Features

* Implement the IBlobProvider interface of ABP and use Alibaba Cloud OSS service
* Support multi-tenant Blob storage
* Automatically create Bucket
* Configurable Bucket access control
*Support STS Token access
* Customizable Blob naming strategy

## Module reference

First define the **appsettings.json** file:

```json
{
  "Aliyun": {
    "OSS": {
      "BucketName": "BucketName you defined",
      "Endpoint": "http://oss-cn-shanghai.aliyuncs.com",
      "CreateBucketIfNotExists": true
    }
  }
}
```

Then reference the module in the project:

```csharp
[DependsOn(typeof(AbpBlobStoringAliyunModule))]
public class YouProjectModule : AbpModule
{
  // other
}
```

## Configuration instructions

### OSS configuration

* **BucketName**
  * Description: OSS storage space name
  *Type: optional
  *Default value: container name

* **Endpoint**
  * Description: Access point of OSS service
  * Type: required
  * Example: http://oss-cn-shanghai.aliyuncs.com

* **CreateBucketIfNotExists**
  * Description: Whether to automatically create the Bucket if it does not exist
  *Type: optional
  *Default value: false

* **CreateBucketReferer**
  * Description: Bucket access whitelist
  *Type: optional

### Blob naming rules

* Container (Bucket) name rules:
  * The length must be between 3-63 characters
  * Can only contain lowercase letters, numbers and dashes
  * Must start with a letter or number
  * Cannot start or end with a dash

* Blob name rules:
  * Tenants: `tenants/{tenantId}/{blobName}`
  * Host: `host/{blobName}`

## Performance optimization

* Use distributed cache to store STS Token
* Support data redundancy configuration
* Configurable Bucket access control to improve security

## Related modules

* [LINGYUN.Abp.Aliyun](../../cloud-aliyun/LINGYUN.Abp.Aliyun/README.md) - Provides basic integration with Alibaba Cloud

[Click to view English documentation](README.EN.md)