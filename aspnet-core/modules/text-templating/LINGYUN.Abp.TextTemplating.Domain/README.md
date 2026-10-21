# LINGYUN.Abp.TextTemplating.Domain

## Module description

The text template domain module provides core functions of text template definition and content management.

### Basic module

* Volo.Abp.TextTemplating
* Volo.Abp.Ddd.Domain

### Function definition

* Provide domain entities defined by text templates
  * TextTemplateDefinition - text template definition entity
  * TextTemplateContent - text template content entity
* Provides a warehousing interface defined by text templates
  * ITextTemplateDefinitionRepository - text template definition repository interface
  * ITextTemplateContentRepository - text template content storage interface
* Provide domain services for text template management
  * TextTemplateManager - text template manager
  * IStaticTemplateDefinitionStore - Static template definition storage
  * IDynamicTemplateDefinitionStore - dynamic template definition storage

### Domain Services

* TextTemplateManager
  * Manage the creation, update and deletion of text templates
  * Process the association between template definition and content
  * Supports management of static and dynamic template definitions

### Entity attributes

* TextTemplateDefinition
  * Name - Template name
  * DisplayName - display name
  * IsLayout - whether it is a layout template
  * Layout - layout name
  * IsInlineLocalized - whether to inline localize
  * DefaultCultureName - the default culture name
  * LocalizationResourceName - localization resource name
  * RenderEngine - rendering engine
  * IsStatic - whether it is a static template

### How to use

1. Add `AbpTextTemplatingDomainModule` dependency

```csharp
[DependsOn(typeof(AbpTextTemplatingDomainModule))]
public class YouProjectModule : AbpModule
{
}
```

2. Use the Text Template Manager

```csharp
public class YourService
{
    private readonly TextTemplateManager _templateManager;

    public YourService(TextTemplateManager templateManager)
    {
        _templateManager = templateManager;
    }

    public async Task ManageTemplateAsync()
    {
        //Create template definition
        var template = new TextTemplateDefinition(
            Guid.NewGuid(),
            "TemplateName",
            "Template Display Name",
            renderEngine: "Razor");

        await _templateManager.CreateAsync(template);
    }
}
```

[View in English](README.EN.md)
