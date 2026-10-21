# LINGYUN.Abp.TextTemplating.EntityFrameworkCore

## Module description

Text template EntityFrameworkCore module provides data access implementation of text templates.

### Basic module

* LINGYUN.Abp.TextTemplating.Domain
* Volo.Abp.EntityFrameworkCore

### Function definition

* Implement the warehousing interface of text templates
  * EfCoreTextTemplateDefinitionRepository - template definition repository implementation
  * EfCoreTextTemplateContentRepository - Template content storage implementation
* Provide database context and configuration
  * ITextTemplatingDbContext - text template database context interface
  * TextTemplatingDbContext - text template database context
  * TextTemplatingDbContextModelCreatingExtensions - database model configuration extensions

### Database table

* AbpTextTemplateDefinitions - template definition table
  *Id - primary key
  * Name - Template name
  * DisplayName - display name
  * IsLayout - whether it is a layout template
  * Layout - layout name
  * IsInlineLocalized - whether to inline localize
  * DefaultCultureName - the default culture name
  * LocalizationResourceName - localization resource name
  * RenderEngine - rendering engine
  * IsStatic - whether it is a static template
* AbpTextTemplateContents - template content table
  *Id - primary key
  * Name - Template name
  * CultureName - culture name
  * Content - template content

### How to use

1. Add `AbpTextTemplatingEntityFrameworkCoreModule` dependency

```csharp
[DependsOn(typeof(AbpTextTemplatingEntityFrameworkCoreModule))]
public class YouProjectModule : AbpModule
{
}
```

2. Configure database context

```csharp
public class YourDbContext : AbpDbContext<YourDbContext>, ITextTemplatingDbContext
{
    public DbSet<TextTemplateDefinition> TextTemplateDefinitions { get; set; }
    public DbSet<TextTemplateContent> TextTemplateContents { get; set; }

    public YourDbContext(DbContextOptions<YourDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ConfigureTextTemplating();
    }
}
```

[View in English](README.EN.md)
