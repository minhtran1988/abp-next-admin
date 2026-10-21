# LINGYUN.Abp.IdentityServer.EntityFrameworkCore

The IdentityServer EntityFrameworkCore module provides the Entity Framework Core implementation of IdentityServer4.

## Features

* Warehousing implementation
  * API resource storage - `EfCoreApiResourceRepository`
    * Get the list of API resource names
    * API resource warehousing base class inherited from the ABP framework

  * Identity resource storage - `EfCoreIdentityResourceRepository`
    * Identity resource storage base class inherited from ABP framework

  * Persistent grant repository - `EfCorePersistentGrantRepository`
    * Persistent authorization repository base class inherited from ABP framework

* Database context
  * Use `IIdentityServerDbContext` of ABP framework
  * Support multi-tenant data isolation

## Module reference

```csharp
[DependsOn(
    typeof(AbpIdentityServerEntityFrameworkCoreModule)
)]
public class YourModule : AbpModule
{
    // ...
}
```

## Dependent modules

* `LINGYUN.Abp.IdentityServer.AbpIdentityServerDomainModule` - IdentityServer domain module
* `Volo.Abp.IdentityServer.EntityFrameworkCore.AbpIdentityServerEntityFrameworkCoreModule` - ABP IdentityServer EntityFrameworkCore module

## Configuration usage

### Configure database context

```csharp
public class YourDbContext : AbpDbContext<YourDbContext>, IIdentityServerDbContext
{
    public DbSet<ApiResource> ApiResources { get; set; }
    public DbSet<ApiScope> ApiScopes { get; set; }
    public DbSet<IdentityResource> IdentityResources { get; set; }
    public DbSet<Client> Clients { get; set; }
    public DbSet<PersistedGrant> PersistedGrants { get; set; }
    public DbSet<DeviceFlowCodes> DeviceFlowCodes { get; set; }

    public YourDbContext(DbContextOptions<YourDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ConfigureIdentityServer();
    }
}
```

Related documents:
* [IdentityServer4 Documentation](https://identityserver4.readthedocs.io/)
* [ABP EntityFrameworkCore Documentation](https://docs.abp.io/en/abp/latest/Entity-Framework-Core)

[View English document](README.EN.md)
