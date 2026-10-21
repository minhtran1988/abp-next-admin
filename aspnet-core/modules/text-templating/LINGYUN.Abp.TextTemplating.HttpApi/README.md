# LINGYUN.Abp.TextTemplating.HttpApi

## Module description

Text template HTTP API module provides a RESTful API interface for text template management.

### Basic module

* LINGYUN.Abp.TextTemplating.Application.Contracts
* Volo.Abp.AspNetCore.Mvc

### Function definition

* API controller that provides text template management
  * TextTemplateDefinitionController - template definition management controller
  * TextTemplateContentController - Template content management controller

### API interface

* /api/text-templating/template-definitions
  * GET - Get a list of template definitions
  * POST - Create template definition
  * PUT - Update template definition
  * DELETE - delete template definition
  * GET /{name} - Get the specified template definition
* /api/text-templating/template-contents
  * GET - Get template content
  * PUT - update template content
  * DELETE - delete template content
  * POST /restore - restore template content

### Permission requirements

* AbpTextTemplating.TextTemplateDefinitions
  * Create - Create a template definition
  * Update - Update template definition
  * Delete - delete template definition
* AbpTextTemplating.TextTemplateContents
  * Update - update template content
  * Delete - delete template content

### How to use

1. Add `AbpTextTemplatingHttpApiModule` dependency

```csharp
[DependsOn(typeof(AbpTextTemplatingHttpApiModule))]
public class YouProjectModule : AbpModule
{
}
```

2. Use API interface

```csharp
public class YourService
{
    private readonly HttpClient _httpClient;

    public YourService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task ManageTemplateAsync()
    {
        // Get the template definition list
        var response = await _httpClient.GetAsync("/api/text-templating/template-definitions");
        var templates = await response.Content.ReadFromJsonAsync<ListResultDto<TextTemplateDefinitionDto>>();
    }
}
```

[View in English](README.EN.md)
