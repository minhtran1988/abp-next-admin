# LINGYUN.Abp.Authorization.OrganizationUnits

The organizational unit permission verification module provides permission verification functions based on organizational units.

## Features

* Support permission verification based on organizational units
* Provide organization unit permission value provider (OrganizationUnitPermissionValueProvider)
* Support multi-organizational unit permission verification
* Integrate ABP permission system
* Provide organization unit Claim type extension
* Support current user organizational unit query expansion

## Module reference

```csharp
[DependsOn(typeof(AbpAuthorizationOrganizationUnitsModule))]
public class YouProjectModule : AbpModule
{
  // other
}
```

## Basic usage

1. Configure permission provider
   ```csharp
   public override void ConfigureServices(ServiceConfigurationContext context)
   {
       Configure<AbpPermissionOptions>(options =>
       {
           options.ValueProviders.Add<OrganizationUnitPermissionValueProvider>();
       });
   }
   ```

2. Get the organizational unit of the current user
   ```csharp
   public class YourService
   {
       private readonly ICurrentUser _currentUser;

       public YourService(ICurrentUser currentUser)
       {
           _currentUser = currentUser;
       }

       public void YourMethod()
       {
           var organizationUnits = _currentUser.FindOrganizationUnits();
           //Use organizational units for business processing
       }
   }
   ```

3. Get the organizational unit from ClaimsPrincipal
   ```csharp
   public class YourService
   {
       public void YourMethod(ClaimsPrincipal principal)
       {
           var organizationUnits = principal.FindOrganizationUnits();
           //Use organizational units for business processing
       }
   }
   ```

## More resources

* [GitHub repository](https://github.com/colinin/abp-next-admin)
* [Sample Application](https://github.com/colinin/abp-next-admin/tree/master/aspnet-core/services/LINGYUN.Abp.MicroService.AllInOne)

[English](./README.EN.md)
