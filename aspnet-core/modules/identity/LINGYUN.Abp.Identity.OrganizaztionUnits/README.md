# LINGYUN.Abp.Identity.OrganizaztionUnits

The identity authentication organizational unit module provides the integration function of the identity authentication system and organizational units.

## Features

* Extend AbpIdentityDomainModule module
* Integrate AbpAuthorizationOrganizationUnitsModule module
* Support dynamic addition of organizational unit declaration
* Provide identity authentication functions related to organizational units

## Module reference

```csharp
[DependsOn(
    typeof(AbpIdentityDomainModule),
    typeof(AbpAuthorizationOrganizationUnitsModule))]
public class YouProjectModule : AbpModule
{
  // other
}
```

## Configuration items

### AbpClaimsPrincipalFactoryOptions

```json
{
  "Abp": {
    "Security": {
      "Claims": {
        "DynamicClaims": {
          "OrganizationUnit": true // Enable dynamic declaration of organizational units
        }
      }
    }
  }
}
```

## Declaration type

* `AbpOrganizationUnitClaimTypes.OrganizationUnit` - Organization unit claim type
  * Used to identify the organizational unit to which the user belongs
  * Automatically added to claims upon user authentication

## Basic usage

1. Configure organizational unit declaration
```csharp
public override void ConfigureServices(ServiceConfigurationContext context)
{
    Configure<AbpClaimsPrincipalFactoryOptions>(options =>
    {
        options.DynamicClaims.Add(AbpOrganizationUnitClaimTypes.OrganizationUnit);
    });
}
```

2. Obtain the user's organizational unit statement
```csharp
public class YourService
{
    private readonly ICurrentUser _currentUser;

    public YourService(ICurrentUser currentUser)
    {
        _currentUser = currentUser;
    }

    public string[] GetUserOrganizationUnits()
    {
        return _currentUser.FindClaims(AbpOrganizationUnitClaimTypes.OrganizationUnit)
            .Select(c => c.Value)
            .ToArray();
    }
}
```

## More information

* [ABP Identity Authentication Document](https://docs.abp.io/en/abp/latest/Identity)
* [ABP Organizational Unit Documentation](https://docs.abp.io/en/abp/latest/Organization-Units)
