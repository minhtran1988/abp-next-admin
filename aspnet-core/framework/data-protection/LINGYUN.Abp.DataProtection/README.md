# LINGYUN.Abp.DataProtection

The data permission implementation module provides the core implementation of data permissions.

## Function

* Data permission interceptor - automatically intercept methods marked with data permission attributes
* Data permission verification service - Provides data permission verification function
* Data permission resource storage - Provides memory storage implementation of data permission resources

## Configuration items

```csharp
public class AbpDataProtectionOptions
{
    /// <summary>
    /// Whether to enable data permissions
    ///Default: true
    /// </summary>
    public bool IsEnabled { get; set; }

    /// <summary>
    /// Data permission subject provider list
    /// </summary>
    public IList<IDataAccessSubjectContributor> SubjectContributors { get; }

    /// <summary>
    /// Data permission keyword provider dictionary
    /// </summary>
    public IDictionary<string, IDataAccessKeywordContributor> KeywordContributors { get; }

    /// <summary>
    /// Data permission operation provider dictionary
    /// </summary>
    public IDictionary<DataAccessFilterOperate, IDataAccessOperateContributor> OperateContributors { get; }

    /// <summary>
    /// Ignore the audit attribute list
    /// Default includes: Id, LastModifierId, LastModificationTime, CreatorId, CreationTime,
    /// IsDeleted, DeleterId, DeletionTime, TenantId, EntityVersion, 
    /// ConcurrencyStamp, ExtraProperties
    /// </summary>
    public IList<string> IgnoreAuditedProperties { get; }
}
```

## Usage

1. Configuration module

```csharp
[DependsOn(typeof(AbpDataProtectionModule))]
public class YourModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpDataProtectionOptions>(options =>
        {
            //Configure data permission options
            options.IsEnabled = true;
            
            //Add custom principal provider
            options.SubjectContributors.Add(new YourSubjectContributor());
            
            //Add custom keyword provider
            options.KeywordContributors.Add("your-keyword", new YourKeywordContributor());
            
            //Add custom action provider
            options.OperateContributors.Add(DataAccessFilterOperate.Equal, new YourOperateContributor());
            
            //Add ignored audit properties
            options.IgnoreAuditedProperties.Add("YourProperty");
        });
    }
}
```

2. Use the data permissions feature

```csharp
// Class-level data permission control
[DataProtected]
public class YourService
{
    //Method level data permission control
    [DataProtected]
    public virtual Task<YourEntity> GetAsync(Guid id)
    {
        // ...
    }

    // Disable data permission control
    [DisableDataProtected]
    public virtual Task<YourEntity> GetWithoutProtectionAsync(Guid id)
    {
        // ...
    }
}
```

## Related links

* [English document](./README.EN.md)
