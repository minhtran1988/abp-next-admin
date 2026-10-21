# LINGYUN.Abp.MultiTenancy.Editions

The multi-tenant version management module provides basic functional support for tenant versions (Editions).

## Features

- Version management
  - Support assigning different versions to tenants
  - Version information includes ID and display name
  -Supports storage and retrieval of version information
- Global function switch
  - Support version control through global function switch
  - Flexible configuration of enabling/disabling version functions
- Identity authentication integration
  - Automatically add version information to user Claims
  - Supports obtaining the version information of the current tenant in the application
- Scalability
  - Provide IEditionStore interface for customized version storage
  - Support custom version configuration provider

## Module reference

```csharp
[DependsOn(typeof(AbpMultiTenancyEditionsModule))]
public class YouProjectModule : AbpModule
{
  // other
}
```

## Configuration instructions

### 1. Global function configuration

```csharp
GlobalFeatureManager.Instance.Modules.Editions(editions =>
{
    // Configure version-related global functions here
});
```

### 2. Implement version storage

```csharp
public class YourEditionStore : IEditionStore
{
    public async Task<EditionInfo> FindByTenantAsync(Guid tenantId)
    {
        // Implement the logic to obtain tenant version information from storage
        return new EditionInfo(
            id: Guid.NewGuid(),
            displayName: "Standard Edition"
        );
    }
}
```

### 3. Get version information

```csharp
public class YourService
{
    private readonly IEditionConfigurationProvider _editionProvider;

    public YourService(IEditionConfigurationProvider editionProvider)
    {
        _editionProvider = editionProvider;
    }

    public async Task<EditionConfiguration> GetEditionAsync(Guid? tenantId)
    {
        return await _editionProvider.GetAsync(tenantId);
    }
}
```

## Use of version information in Claims

When the version feature is enabled, the module automatically adds version information to the user's Claims:

```csharp
public class YourController
{
    public async Task<IActionResult> GetEditionInfo()
    {
        var editionId = User.FindFirstValue(AbpClaimTypes.EditionId);
        // Use version ID to perform related operations
    }
}
```

## Notes

1. Before using the version function, you need to ensure that the global function switch is enabled.
2. The implementation of version storage needs to consider performance and concurrent access issues.
3. Changes in version information may affect tenants’ functional access rights.
4. The version information in Claims will be automatically updated when the user authenticates.

## More documentation

- [English Documentation](README.EN.md)
