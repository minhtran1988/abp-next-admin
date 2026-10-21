# LINGYUN.Platform.Domain.Shared

The shared domain layer of the platform management module defines the basic types, enumerations and constants required for platform management.

## Features

* Define platform management basic enumeration
* Define platform management constants
* Define platform to manage localized resources
* Define platform management domain shared interface

## Module reference

```csharp
[DependsOn(typeof(PlatformDomainSharedModule))]
public class YouProjectModule : AbpModule
{
  // other
}
```

## Basic usage

1. Reference module
2. Use defined enumerations and constants
3. Use localized resources

## More

For more information please refer to [Platform](../README.md)
