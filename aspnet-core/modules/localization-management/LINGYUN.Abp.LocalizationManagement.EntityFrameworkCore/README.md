# LINGYUN.Abp.LocalizationManagement.EntityFrameworkCore

The Entity Framework Core integrated implementation of the localization management module provides data access and persistence functions.

## Features

* Implement database mapping for localized management
* Support custom table prefix and Schema
* Provides EF Core implementation of warehousing interface
* Support database operations of language, resources, and text

## Module reference

```csharp
[DependsOn(typeof(AbpLocalizationManagementEntityFrameworkCoreModule))]
public class YouProjectModule : AbpModule
{
  // other
}
```

## Configuration items

```csharp
public override void ConfigureServices(ServiceConfigurationContext context)
{
    context.Services.AddAbpDbContext<YourDbContext>(options =>
    {
        options.AddRepository<Language, EfCoreLanguageRepository>();
        options.AddRepository<Resource, EfCoreResourceRepository>();
        options.AddRepository<Text, EfCoreTextRepository>();
    });

    Configure<AbpDbContextOptions>(options =>
    {
        options.UseMySQL(); // or other database
    });
}
```

Database table configuration options:
```csharp
public class LocalizationModelBuilderConfigurationOptions : AbpModelBuilderConfigurationOptions
{
    public LocalizationModelBuilderConfigurationOptions(
       string tablePrefix = "",
       string schema = null)
       : base(tablePrefix, schema)
    {
    }
}
```

## Database table

* Languages - Language table
* Resources - Resource table
* Texts - text table

## More information

* [English documentation](./README.EN.md)
