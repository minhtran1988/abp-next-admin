# LINGYUN.Abp.TextTemplating.Domain.Shared

## Module description

The text template domain sharing module provides shared definitions of constants, enumerations, exceptions, etc. related to text templates.

### Basic module

* Volo.Abp.TextTemplating
* Volo.Abp.Validation

### Function definition

* Provide constant definitions related to text templates
  * TextTemplateDefinitionConsts - Template definition related constants
  * TextTemplateContentConsts - constants related to template content
* Provide error code definitions related to text templates
  * AbpTextTemplatingErrorCodes - error code constants
* Provide localized resource definitions
  * AbpTextTemplatingResource - localization resource

### Constant definition

* TextTemplateDefinitionConsts
  * MaxNameLength - Maximum length of template name (64)
  * MaxDisplayNameLength - Maximum length of display name (128)
  * MaxLayoutLength - Maximum length of layout name (256)
  * MaxDefaultCultureNameLength - Maximum length of default culture name (10)
  * MaxLocalizationResourceNameLength - Maximum length of localization resource name (128)
  * MaxRenderEngineLength - Maximum length of rendering engine name (64)

### Error code

* AbpTextTemplatingErrorCodes
  * TextTemplateDefinition:NameAlreadyExists - template name already exists
  * TextTemplateDefinition:NotFound - The template definition does not exist

### How to use

1. Add `AbpTextTemplatingDomainSharedModule` dependency

```csharp
[DependsOn(typeof(AbpTextTemplatingDomainSharedModule))]
public class YouProjectModule : AbpModule
{
}
```

2. Use constants and error codes

```csharp
public class YourService
{
    public void ValidateTemplateName(string name)
    {
        if (name.Length > TextTemplateDefinitionConsts.MaxNameLength)
        {
            throw new BusinessException(AbpTextTemplatingErrorCodes.TextTemplateDefinition.NameAlreadyExists);
        }
    }
}
```

[View in English](README.EN.md)
