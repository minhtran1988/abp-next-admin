# LINGYUN.Abp.Identity.EntityFrameworkCore

The identity authentication EntityFrameworkCore module provides identity authentication-related data access implementation.

## Features

* Extend the Volo.Abp.Identity.EntityFrameworkCore.AbpIdentityEntityFrameworkCoreModule module
* Provide EF Core warehousing implementation related to identity authentication
* Support database mapping of user avatar URL extended attributes
* Provide EF Core warehousing implementation for organizational units
* Provides EF Core warehousing implementation for session management

## Module reference

```csharp
[DependsOn(
    typeof(AbpIdentityDomainModule),
    typeof(Volo.Abp.Identity.EntityFrameworkCore.AbpIdentityEntityFrameworkCoreModule))]
public class YouProjectModule : AbpModule
{
  // other
}
```

## Warehousing implementation

* EfCoreIdentityUserRepository - User repository implementation
* EfCoreIdentityRoleRepository - role storage implementation
* EfCoreIdentitySessionRepository - session storage implementation
* EfCoreOrganizationUnitRepository - organizational unit warehousing implementation

## Basic usage

1. Configure DbContext
```csharp
public override void ConfigureServices(ServiceConfigurationContext context)
{
    context.Services.AddAbpDbContext<IdentityDbContext>(options =>
    {
        options.AddRepository<IdentityRole, EfCoreIdentityRoleRepository>();
        options.AddRepository<IdentityUser, EfCoreIdentityUserRepository>();
        options.AddRepository<IdentitySession, EfCoreIdentitySessionRepository>();
        options.AddRepository<OrganizationUnit, EfCoreOrganizationUnitRepository>();
    });
}
```

2. Use warehousing
```csharp
public class YourService
{
    private readonly IRepository<IdentityUser, Guid> _userRepository;
    private readonly IRepository<IdentityRole, Guid> _roleRepository;

    public YourService(
        IRepository<IdentityUser, Guid> userRepository,
        IRepository<IdentityRole, Guid> roleRepository)
    {
        _userRepository = userRepository;
        _roleRepository = roleRepository;
    }

    public async Task<IdentityUser> GetUserAsync(Guid id)
    {
        return await _userRepository.GetAsync(id);
    }

    public async Task<IdentityRole> GetRoleAsync(Guid id)
    {
        return await _roleRepository.GetAsync(id);
    }
}
```

## Database migration

1. Add migration
```bash
dotnet ef migrations add Added_Identity_Tables
```

2. Update database
```bash
dotnet ef database update
```

## More information

* [ABP Identity Authentication Document](https://docs.abp.io/en/abp/latest/Identity)
* [EF Core Documentation](https://docs.microsoft.com/en-us/ef/core/)
