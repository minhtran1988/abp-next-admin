# LINGYUN.Abp.Sonatype.Nexus

[English](./README.EN.md) | Simplified Chinese

ABP framework integration module for Sonatype Nexus REST API.

## Function

* Support Nexus warehouse management
* Support component management
* Support asset management
*Support search function
* Support basic authentication

## Module dependencies

```csharp
[DependsOn(typeof(AbpSonatypeNexusModule))]
public class YouProjectModule : AbpModule
{
  // other
}
```

## Basic usage

### 1. Configure Nexus connection

Configure Nexus connection options in `appsettings.json`:

```json
{
  "SonatypeNexus": {
    "BaseUrl": "http://127.0.0.1:8081",
    "UserName": "sonatype",
    "Password": "sonatype"
  }
}
```

### 2. Use warehouse management

```csharp
public class MyService
{
    private readonly INexusRepositoryManager _repositoryManager;

    public MyService(INexusRepositoryManager repositoryManager)
    {
        _repositoryManager = repositoryManager;
    }

    public async Task<List<NexusRepository>> GetRepositoriesAsync()
    {
        var result = await _repositoryManager.ListAsync();
        return result.Items;
    }
}
```

### 3. Use component management

```csharp
public class MyService
{
    private readonly INexusComponentManager _componentManager;

    public MyService(INexusComponentManager componentManager)
    {
        _componentManager = componentManager;
    }

    public async Task UploadComponentAsync(string repository, string directory, byte[] fileBytes)
    {
        var asset = new Asset("filename.txt", fileBytes);
        var args = new NexusRawBlobUploadArgs(repository, directory, asset);
        await _componentManager.UploadAsync(args);
    }
}
```

### 4. Use resource management

```csharp
public class MyService
{
    private readonly INexusAssetManager _assetManager;

    public MyService(INexusAssetManager assetManager)
    {
        _assetManager = assetManager;
    }

    public async Task<List<NexusAsset>> GetAssetsAsync(string repository)
    {
        var result = await _assetManager.ListAsync(repository);
        return result.Items;
    }
}
```

## Configuration options

* BaseUrl - Nexus server address
* UserName - username
* Password - Password

## Best Practices

1. Keep certification information properly to avoid leakage
2. Properly plan warehouse structure and component classification
3. Regularly clean up unnecessary components and resources
4. Use appropriate error handling mechanisms

## Notes

1. Make sure the Nexus server is properly configured and accessible
2. When uploading large files, you need to pay attention to network conditions and timeout settings.
3. The deletion operation cannot be recovered, please operate with caution.
4. Some operations require administrator privileges
