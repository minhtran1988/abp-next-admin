# LINGYUN.Abp.TextTemplating.Application.Contracts

## Module description

The text template application service contract module provides interface definitions and DTOs related to text template management.

### Basic module

* Volo.Abp.TextTemplating
* Volo.Abp.Ddd.Application.Contracts

### Function definition

* Provide application service interface for text template management
  * ITextTemplateDefinitionAppService - Template definition management service interface
  * ITextTemplateContentAppService - Template content management service interface
* Provide DTO definitions related to text templates
  * TextTemplateDefinitionDto - template definition DTO
  * TextTemplateContentDto - template content DTO
  * TextTemplateDefinitionCreateDto - Create a template definition DTO
  * TextTemplateDefinitionUpdateDto - update template definition DTO
  * TextTemplateContentUpdateDto - Update template content DTO
* Provide permission definitions related to text template management

### Permission definition

* AbpTextTemplating.TextTemplateDefinitions
  * Create - Create a template definition
  * Update - Update template definition
  * Delete - delete template definition
* AbpTextTemplating.TextTemplateContents
  * Update - update template content
  * Delete - delete template content

### Application service interface

* ITextTemplateDefinitionAppService
  * GetAsync - Get template definition
  * GetListAsync - Get the template definition list
  * CreateAsync - Create template definition
  * UpdateAsync - Update template definition
  * DeleteAsync - delete template definition
* ITextTemplateContentAppService
  * GetAsync - Get template content
  * UpdateAsync - Update template content
  * DeleteAsync - delete template content
  * RestoreAsync - restore template content

### How to use

1. Add `AbpTextTemplatingApplicationContractsModule` dependency

```csharp
[DependsOn(typeof(AbpTextTemplatingApplicationContractsModule))]
public class YouProjectModule : AbpModule
{
}
```

2. Inject and use the template service interface

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
        // Get the template definition list
        var templates = await _templateDefinitionAppService.GetListAsync(
            new TextTemplateDefinitionGetListInput());
    }
}
```

[View in English](README.EN.md)
