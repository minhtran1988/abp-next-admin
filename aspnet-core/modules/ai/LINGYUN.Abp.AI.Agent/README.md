# LINGYUN.Abp.AI.Agent

[Abp AI Module](https://abp.io/docs/latest/framework/infrastructure/artificial-intelligence) extension.

## Features


## Module reference

```csharp
[DependsOn(typeof(AbpAIAgentModule))]
public class YouProjectModule : AbpModule
{
  // other
}
```

## Dependent modules

* [AbpAIModule](https://abp.io/docs/latest/framework/infrastructure/artificial-intelligence)  
* [AbpLocalizationModule](https://abp.io/docs/latest/framework/fundamentals/localization)  

## Basic usage

> Compatible with the syntax of `IKernelAccessor` and `IChatClientAccessor`.

```csharp
using LINGYUN.Abp.AI;
using LINGYUN.Abp.AI.Agent;
using Microsoft.Extensions.AI;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.SemanticKernel;
using Volo.Abp;

var application = await AbpApplicationFactory.CreateAsync<YouProjectModule>(options =>
{
    options.UseAutofac();
});

await application.InitializeAsync();

var chatClientAgentFactory = application.ServiceProvider.GetRequiredService<IChatClientAgentFactory>();

var agent = await chatClientAgentFactory.CreateAsync<YouWorkspace>();

var agentResponse = agent.RunStreamingAsync("Explain linear algebra");

await foreach (var item in agentResponse)
{
    Console.Write(item);
}
Console.WriteLine();

await application.ShutdownAsync();

Console.WriteLine();
Console.WriteLine("AI Console completed!");

Console.ReadKey();
```

> Support dynamic workspace syntax.

```csharp
using LINGYUN.Abp.AI;
using LINGYUN.Abp.AI.Agent;
using Microsoft.Extensions.AI;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.SemanticKernel;
using Volo.Abp;

var application = await AbpApplicationFactory.CreateAsync<YouProjectModule>(options =>
{
    options.UseAutofac();
});

await application.InitializeAsync();

var chatClientAgentFactory = application.ServiceProvider.GetRequiredService<IChatClientAgentFactory>();

var agent = await chatClientAgentFactory.CreateAsync("YouWorkspace");

var agentResponse = agent.RunStreamingAsync("Explain linear algebra");

await foreach (var item in agentResponse)
{
    Console.Write(item);
}
Console.WriteLine();

await application.ShutdownAsync();

Console.WriteLine();
Console.WriteLine("AI Console completed!");

Console.ReadKey();

```