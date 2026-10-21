# LINGYUN.Abp.Identity.Domain.Shared

The shared module in the identity authentication field provides basic definitions related to identity authentication.

## Features

* Provide localized resources related to identity authentication
* Provide virtual file system configuration related to identity authentication
* Extend the Volo.Abp.Identity.AbpIdentityDomainSharedModule module

## Module reference

```csharp
[DependsOn(typeof(AbpIdentityDomainSharedModule))]
public class YouProjectModule : AbpModule
{
  // other
}
```

## Configuration items

No additional configuration is required.

## Basic usage

1. Reference module
```csharp
[DependsOn(typeof(AbpIdentityDomainSharedModule))]
public class YouProjectModule : AbpModule
{
}
```

2. Use localized resources
```csharp
public class YourClass
{
    private readonly IStringLocalizer<IdentityResource> _localizer;

    public YourClass(IStringLocalizer<IdentityResource> localizer)
    {
        _localizer = localizer;
    }

    public string GetLocalizedString(string key)
    {
        return _localizer[key];
    }
}
```

## More information

* [ABP Identity Authentication Document](https://docs.abp.io/en/abp/latest/Identity)
