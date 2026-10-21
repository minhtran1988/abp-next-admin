# LINGYUN.Abp.TextTemplating.Razor

## Module description

Text template Razor engine module, providing template rendering implementation based on Razor syntax.

### Basic module

* Volo.Abp.TextTemplating.Razor
* LINGYUN.Abp.TextTemplating.Domain

### Function definition

* Provides Razor template rendering engine
  * RazorTemplateRenderingEngine - Razor template rendering engine implementation
* Support the following functions
  * Use Razor syntax to write templates
  * Support model binding and strongly typed views
  * Support layout templates
  * Support partial view
  * Support HTML encoding and decoding
  * Support conditional statements and loops
  * Support C# expressions

### How to use

1. Add `AbpTextTemplatingRazorModule` dependency

```csharp
[DependsOn(typeof(AbpTextTemplatingRazorModule))]
public class YouProjectModule : AbpModule
{
}
```

2. Create and use Razor templates

```csharp
//Create template definition
var template = new TextTemplateDefinition(
    Guid.NewGuid(),
    "Welcome",
    "Welcome Email Template",
    renderEngine: "Razor");

// Template content example
@model WelcomeEmailModel

<!DOCTYPE html>
<html>
<body>
    <h1>Welcome @Model.UserName!</h1>
    <p>Thank you for joining us.</p>
    @if (Model.IsFirstTime)
    {
        <p>Here are some tips to get started...</p>
    }
</body>
</html>

// use template
public class YourService
{
    private readonly ITemplateRenderer _templateRenderer;

    public YourService(ITemplateRenderer templateRenderer)
    {
        _templateRenderer = templateRenderer;
    }

    public async Task<string> RenderWelcomeEmailAsync(string userName, bool isFirstTime)
    {
        var model = new WelcomeEmailModel
        {
            UserName = userName,
            IsFirstTime = isFirstTime
        };

        return await _templateRenderer.RenderAsync(
            "Welcome",
            model
        );
    }
}
```

[View in English](README.EN.md)
