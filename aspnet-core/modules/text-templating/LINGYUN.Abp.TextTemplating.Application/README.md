# LINGYUN.Abp.TextTemplating.Application

## Module description

The text template application service module implements the management and operation functions of text templates.

### Basic module

* LINGYUN.Abp.TextTemplating.Application.Contracts
* Volo.Abp.TextTemplating
* Volo.Abp.Ddd.Application

### Function definition

* Provide text template definition management services
  * TextTemplateDefinitionAppService - template definition management service
  * TextTemplateContentAppService - Template content management service
* Implement the following application service interfaces
  * ITextTemplateDefinitionAppService
  * ITextTemplateContentAppService

### Application Services

* TextTemplateDefinitionAppService
  * GetAsync - Get template definition
  * GetListAsync - Get the template definition list
  * CreateAsync - Create template definition
  * UpdateAsync - Update template definition
  * DeleteAsync - delete template definition
* TextTemplateContentAppService
  * GetAsync - Get template content
  * UpdateAsync - Update template content
  * DeleteAsync - delete template content
  * RestoreAsync - restore template content

### Permissions

* AbpTextTemplating.TextTemplateDefinitions
  * Create - Create a template definition
  * Update - Update template definition
  * Delete - delete template definition
* AbpTextTemplating.TextTemplateContents
  * Update - update template content
  * Delete - delete template content

### How to use

1. Add `AbpTextTemplatingApplicationModule` dependency

```csharp
[DependsOn(typeof(AbpTextTemplatingApplicationModule))]
public class YouProjectModule : AbpModule
{
}
```

2. Inject and use the template service

```csharp
public class YourService
{
    private readonly ITextTemplateDefinitionAppService _templateDefinitionAppService;

    public YourService(ITextTemplateDefinitionAppService templateDefinitionAppService)
    {
        _templateDefinitionAppService = templateDefinitionAppService;
    }

    public async Task ManageTemplateAsync()
    {
        //Create template definition
        var template = await _templateDefinitionAppService.CreateAsync(
            new TextTemplateDefinitionCreateDto
            {
                Name = "TemplateName",
                DisplayName = "Template Display Name",
                RenderEngine = "Razor"
            });
    }
}
```

[View in English](README.EN.md)
