# LINGYUN.Abp.BlobStoring.Nexus

[English](./README.EN.md) | Simplified Chinese

Blob storage provider based on Sonatype Nexus.

## Function

* Supports storing files to Nexus repository
* Supports basic operations such as file upload, download, and deletion
* Support file path normalization
* Supports file duplicate checking and overwriting options
*Support multi-tenancy

## Module dependencies

```csharp
[DependsOn(typeof(AbpBlobStoringNexusModule))]
public class YouProjectModule : AbpModule
{
  // other
}
```

## Basic usage

### 1. Configure Nexus storage

Configure Nexus storage options in `appsettings.json`:

```json
{
  "BlobStoring": {
    "Nexus": {
      "Repository": "your-repository-name"
    }
  }
}
```

### 2. Configure container

```csharp
Configure<AbpBlobStoringOptions>(options =>
{
    options.Containers.ConfigureDefault(container =>
    {
        container.UseNexus(nexus =>
        {
            nexus.Repository = "your-repository-name";
        });
    });
});
```

### 3. Use BLOB storage

```csharp
public class MyService
{
    private readonly IBlobContainer _blobContainer;

    public MyService(IBlobContainer blobContainer)
    {
        _blobContainer = blobContainer;
    }

    public async Task SaveBlobAsync(byte[] bytes)
    {
        await _blobContainer.SaveAsync("my-blob-name", bytes);
    }

    public async Task<byte[]> GetBlobAsync()
    {
        return await _blobContainer.GetAllBytesAsync("my-blob-name");
    }
}
```

## Configuration options

* Repository - Nexus warehouse name

## Best Practices

1. Properly plan file paths and naming to facilitate management and maintenance
2. Configure file coverage options according to actual needs
3. Properly use multi-tenant functions to ensure data isolation

## Notes

1. Make sure the Nexus repository is properly configured and accessible
2. The file path will be automatically normalized, replacing backslashes with forward slashes.
3. By default, overwriting existing files is not allowed, and the OverrideExisting option needs to be explicitly set.
4. File operations may be affected by network conditions. It is recommended to add appropriate error handling mechanisms.
