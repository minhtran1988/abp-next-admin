# LINGYUN.Abp.AI.Tools

AI tool module.

Reference: [AI tool call](https://learn.microsoft.com/zh-cn/dotnet/ai/conceptual/ai-tools)

## Features


## Module reference

```csharp
[DependsOn(typeof(AbpAIToolsModule))]
public class YouProjectModule : AbpModule
{
  // other
}
```

## Dependent modules

* [AbpAICoreModule](../LINGYUN.Abp.AI.Core)  

## Custom function

> Note: Custom functions follow a convention: synchronous tools need to define a function named `Invoke`; asynchronous tools need to define a function named `InvokeAsync`. If you want to use a DI container, you need to inject the tool into the container

```csharp

public class NowTimeTool : ITransientDependency
{
    private readonly IClock _clock;
    public NowTimeTool(IClock clock)
    {
        _clock = clock;
    }

    public object? Invoke()
    {
        return _clock.Now;
    }
}

public class NowTimeAIToolDefinitionProvider : AIToolDefinitionProvider
{
    context.Add(
            new AIToolDefinition(
                "GetNowTime",
                FunctionAIToolProvider.ProviderName)
            .WithFunction<NowTimeTool>());
}
```

## Extension

Implement the `IAIToolProvider` interface to add other tool calls
Implement the `IDynamicAIToolDefinitionStore` interface to add dynamic AI tool management

```csharp
public class McpAIToolProvider : IAIToolProvider, ITransientDependency
{
    public virtual Task<AITool[]> CreateToolsAsync(AIToolDefinition definition)
    {
        //Your specific implementation
    }
}

[DependsOn(typeof(AbpAIToolsModule))]
public class YouProjectModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpAIToolsOptions>(options =>
        {
            options.AIToolProviders.Add<McpAIToolProvider>();
        });
    }
}
```