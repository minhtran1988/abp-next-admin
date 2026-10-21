# LINGYUN.Abp.DataProtection.EntityFrameworkCore

Data permission EF implementation module

## Interface description

* DisableDataProtectedAttribute: Automatically implement DataFilter.Disable<IDataProtected>() through the interceptor, the data filter will be disabled in the current scope

## Notes

* When using the warehousing interface, try to avoid using *await GetDbSetAsync()* directly. Instead, use *await GetQueryableAsync()*. Due to the **DbSet** design mode, it cannot be processed at the moment.
* Your warehouse interface should inherit from **EfCoreDataProtectionRepository**, and your *DbContext* should inherit from **AbpDataProtectionDbContext**

## Configuration usage

```csharp

[DependsOn(
    typeof(AbpDataProtectionEntityFrameworkCoreModule)
    )]
public class YouProjectModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<DataProtectionManagementOptions>(options =>
        {
            // Persistence of protected entity list
            options.AddEntities(typeof(YouResource), new Type[]
            {
                typeof(YouProtectionObject),
            });

            //The following format
            // options.AddEntities(typeof(IdentityResource), new Type[]
            // {
            //     typeof(IdentityUser),
            //     typeof(IdentityRole),
            //     typeof(OrganizationUnit),
            // });
        });
    }
}

public class YouDbContext : AbpDataProtectionDbContext<YouDbContext>
{
    public DbSet<YouProtectionObject> ProtectionObjects { get; set; }
    public YouDbContext(
        DbContextOptions<YouDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<YouProtectionObject>(b =>
        {
            // ...
        });
    }
}


public class EfCoreYouProtectionObjectRepository :
    EfCoreDataProtectionRepository<YouDbContext, YouProtectionObject, int>,
    IYouProtectionObjectRepository
{
    protected IDataFilter DataFilter { get; }
    public EfCoreYouProtectionObjectRepository(
        [NotNull] IDbContextProvider<YouDbContext> dbContextProvider, 
        [NotNull] IDataAuthorizationService dataAuthorizationService, 
        [NotNull] IEntityTypeFilterBuilder entityTypeFilterBuilder,
        IDataFilter dataFilter) 
        : base(dbContextProvider, dataAuthorizationService, entityTypeFilterBuilder)
    {
        DataFilter = dataFilter;
    }

    // Get the protected data list
    public async virtual Task<List<YouProtectionObject>> GetProtectedListAsync()
    {
        return await (await GetQueryableAsync())
            .ToListAsync();
    }

    // Mark DisableDataProtected to get the entire data list and automatically process it through the interceptor DataFilter.Disable<IDataProtected>()
    [DisableDataProtected]
    public async virtual Task<List<YouProtectionObject>> GetUnProtectedListAsync()
    {
        return await (await GetQueryableAsync())
            .ToListAsync();
    }

    // Disable IDataProtected filter to get all data list (can be used anywhere)
    public async virtual Task<List<YouProtectionObject>> GetUnProtectedByFilterListAsync()
    {
        using (DataFilter.Disable<IDataProtected>())
        {
            return await (await GetQueryableAsync())
                .ToListAsync();
        }
    }
}
```

