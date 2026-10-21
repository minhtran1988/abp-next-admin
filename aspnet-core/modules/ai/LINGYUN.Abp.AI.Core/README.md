# LINGYUN.Abp.AI.Core

[Abp AI Module](https://abp.io/docs/latest/framework/infrastructure/artificial-intelligence) extension.

## Features


## Module reference

```csharp
[DependsOn(typeof(AbpAICoreModule))]
public class YouProjectModule : AbpModule
{
  // other
}
```

## Dependent modules

* [AbpAIModule](https://abp.io/docs/latest/framework/infrastructure/artificial-intelligence)  
* [AbpLocalizationModule](https://abp.io/docs/latest/framework/fundamentals/localization)  

## Basic usage

> Define system workspace
```csharp
[DependsOn(typeof(AbpAICoreModule))]
public class YouProjectModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        PreConfigure<AbpAIWorkspaceOptions>(options =>
        {
            options.Workspaces.Configure<YouWorkspace>(workspace =>
            {
                workspace.ConfigureChatClient(config =>
                {
                    config.Builder = new ChatClientBuilder(
                        sp => new OpenAIClient(
                            new ApiKeyCredential("YouApiKey"),
                            new OpenAIClientOptions
                            {
                                Endpoint = new Uri("https://api.openai.com/v1"),
                            }).GetChatClient("GPT-4").AsIChatClient());
                });

                workspace.ConfigureKernel(config =>
                {
                    config.Builder = Kernel.CreateBuilder()
                        .AddOpenAIChatClient(
                            modelId: "GPT-4",
                            openAIClient: new OpenAIClient(
                                new ApiKeyCredential("YouApiKey"),
                                new OpenAIClientOptions
                                {
                                    Endpoint = new Uri("https://api.openai.com/v1"),
                                }));
                });
            });
        });
    }
}
```

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

//Use IKernelFactory
Console.WriteLine("Use Microsoft.SemanticKernel:");
var kernelFactory = application.ServiceProvider.GetRequiredService<IKernelFactory>();

var kernel = await kernelFactory.CreateAsync<YouWorkspace>();
var kernelResponse = kernel.InvokePromptStreamingAsync("How to optimize C# code performance?");
await foreach (var item in kernelResponse)
{
    Console.Write(item);
}
Console.WriteLine();

//Use IKernelAccessor
var kernelWithAbp = application.ServiceProvider.GetRequiredService<IKernelAccessor<YouWorkspace>>();
var kernelWithAbpResponse = kernelWithAbp.InvokePromptStreamingAsync("How to optimize C# code performance?");
await foreach (var item in kernelWithAbpResponse)
{
    Console.Write(item);
}
Console.WriteLine();

await application.ShutdownAsync();

Console.WriteLine();
Console.WriteLine("AI Console completed!");

Console.ReadKey();
```

> Define dynamic workspace
```csharp
public class YouWorkspaceDefinitionProvider : WorkspaceDefinitionProvider
{
    public override void Define(IWorkspaceDefinitionContext context)
    {
        context.Add(
            new WorkspaceDefinition(
                "YouWorkspace",
                OpenAIChatClientProvider.ProviderName,
                "GPT-4",
                new FixedLocalizableString("YouWorkspace"))
            .WithApiKey("YouApiKey")
            .WithApiBaseUrl("https://api.openai.com/v1"));
    }
}

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

// Microsoft.SemanticKernel
Console.WriteLine("Use Microsoft.SemanticKernel:");
var kernelFactory = application.ServiceProvider.GetRequiredService<IKernelFactory>();

var kernel = await kernelFactory.CreateAsync("YouWorkspace");
var kernelResponse = kernel.InvokePromptStreamingAsync("How to optimize C# code performance?");
await foreach (var item in kernelResponse)
{
    Console.Write(item);
}
Console.WriteLine();

await application.ShutdownAsync();

Console.WriteLine();
Console.WriteLine("AI Console completed!");

Console.ReadKey();

```