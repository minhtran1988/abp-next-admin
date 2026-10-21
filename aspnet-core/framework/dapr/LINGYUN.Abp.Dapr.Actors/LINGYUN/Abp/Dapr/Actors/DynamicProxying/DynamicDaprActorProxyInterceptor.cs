using Dapr.Actors;
using Dapr.Actors.Client;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.DependencyInjection;
using Volo.Abp.DynamicProxy;
using Volo.Abp.Http;
using Volo.Abp.Http.Client;
using Volo.Abp.Http.Client.Authentication;
using Volo.Abp.Http.Client.Proxying;
using Volo.Abp.Json.SystemTextJson;
using Volo.Abp.MultiTenancy;
using Volo.Abp.Threading;

namespace LINGYUN.Abp.Dapr.Actors.DynamicProxying;

public class DynamicDaprActorProxyInterceptor<TService> : AbpInterceptor, ITransientDependency
     where TService: IActor
{
    protected ICurrentTenant CurrentTenant { get; }
    protected AbpSystemTextJsonSerializerOptions JsonSerializerOptions { get; }
    protected AbpDaprActorProxyOptions DaprActorProxyOptions { get; }
    protected IProxyHttpClientFactory HttpClientFactory { get; }
    protected IRemoteServiceHttpClientAuthenticator ClientAuthenticator { get; }
    protected IRemoteServiceConfigurationProvider RemoteServiceConfigurationProvider { get; }
    public ILogger<DynamicDaprActorProxyInterceptor<TService>> Logger { get; set; }

    public DynamicDaprActorProxyInterceptor(
        IOptions<AbpDaprActorProxyOptions> daprActorProxyOptions,
        IOptions<AbpSystemTextJsonSerializerOptions> jsonSerializerOptions,
        IProxyHttpClientFactory httpClientFactory,
        IRemoteServiceHttpClientAuthenticator clientAuthenticator,
        IRemoteServiceConfigurationProvider remoteServiceConfigurationProvider,
        ICurrentTenant currentTenant)
    {
        CurrentTenant = currentTenant;
        HttpClientFactory = httpClientFactory;
        ClientAuthenticator = clientAuthenticator;
        DaprActorProxyOptions = daprActorProxyOptions.Value;
        JsonSerializerOptions = jsonSerializerOptions.Value;
        RemoteServiceConfigurationProvider = remoteServiceConfigurationProvider;

        Logger = NullLogger<DynamicDaprActorProxyInterceptor<TService>>.Instance;
    }

    public override async Task InterceptAsync(IAbpMethodInvocation invocation)
    {
        var isAsyncMethod = invocation.Method.IsAsync();
        if (!isAsyncMethod)
        {
            // see: https://docs.dapr.io/developing-applications/sdks/dotnet/dotnet-actors/dotnet-actors-howto/
            // Dapr Actor documentation: The return type of an Actor method must be Task or Task<object>
            throw new AbpException("The return type of Actor method must be Task or Task<object>");
        }
        if (invocation.Arguments.Length > 1)
        {
            // see: https://docs.dapr.io/developing-applications/sdks/dotnet/dotnet-actors/dotnet-actors-howto/
            // Dapr Actor documentation: Actor methods can have at most one parameter
            throw new AbpException("Actor method can have one argument at a maximum");
        }
        await MakeRequestAsync(invocation);
    }

    private async Task MakeRequestAsync(IAbpMethodInvocation invocation)
    {
        // Get Actor configuration
        var actorProxyConfig = DaprActorProxyOptions.ActorProxies.GetOrDefault(typeof(TService)) ?? throw new AbpException($"Could not get DynamicDaprActorProxyConfig for {typeof(TService).FullName}.");
        var remoteServiceConfig = await RemoteServiceConfigurationProvider.GetConfigurationOrDefaultAsync(actorProxyConfig.RemoteServiceName);

        // There are too many definitions of Actors, you can consider using the default BaseUrl as the remote address
        if (remoteServiceConfig.BaseUrl.IsNullOrWhiteSpace())
        {
            throw new AbpException($"Could not get BaseUrl for {actorProxyConfig.RemoteServiceName} Or Default.");
        }

        var actorProxyOptions = new ActorProxyOptions
        {
            HttpEndpoint = remoteServiceConfig.BaseUrl,
            RequestTimeout = TimeSpan.FromMilliseconds(remoteServiceConfig.GetRequestTimeOut()),
            DaprApiToken = remoteServiceConfig.GetApiToken(),
            JsonSerializerOptions = JsonSerializerOptions.JsonSerializerOptions,
        };

        // Custom request handler
        // Add request header to pass status
        // TODO: Actor can only handle one request at a time. Is it feasible to use state management to transfer state?
        var httpClientHandler = new DaprHttpClientHandler();

        AddHeaders(httpClientHandler);

        httpClientHandler.PreConfigure(async (requestMessage) =>
        {
            // Placeholder
            var httpClient = HttpClientFactory.Create(AbpDaprActorsModule.DaprHttpClient);

            await ClientAuthenticator.Authenticate(
                new RemoteServiceHttpClientAuthenticateContext(
                    httpClient,
                    requestMessage,
                    remoteServiceConfig,
                    actorProxyConfig.RemoteServiceName));
            // header
            if (requestMessage.Headers.Authorization == null &&
                httpClient.DefaultRequestHeaders.Authorization != null)
            {
                requestMessage.Headers.Authorization = httpClient.DefaultRequestHeaders.Authorization;
            }
        });

        // Agent factory
        var proxyFactory = new ActorProxyFactory(actorProxyOptions, (HttpMessageHandler)httpClientHandler);

        await MakeRequestAsync(invocation, proxyFactory);
    }

    private async Task MakeRequestAsync(
        IAbpMethodInvocation invocation,
        ActorProxyFactory proxyFactory
        )
    {
        var invokeType = typeof(TService);

        // The agreed RemoteServiceAttribute is the Actor name
        var remoteServiceAttr = invokeType.GetTypeInfo().GetCustomAttribute<RemoteServiceAttribute>();
        var actorType = remoteServiceAttr != null
            ? remoteServiceAttr.Name
            : invokeType.Name;

        var actorId = new ActorId(invokeType.FullName);

        try
        {
            // Create a strongly typed proxy
            var actorProxy = proxyFactory.CreateActorProxy<TService>(actorId, actorType);
            // Remote call
            var task = (Task)invocation.Method.Invoke(actorProxy, invocation.Arguments)!;
            await task;

            // There is a return value
            if (!invocation.Method.ReturnType.GenericTypeArguments.IsNullOrEmpty())
            {
                // handle return value
                invocation.ReturnValue = typeof(Task<>)
                    .MakeGenericType(invocation.Method.ReturnType.GenericTypeArguments[0])
                    .GetProperty(nameof(Task<object>.Result), BindingFlags.Public | BindingFlags.Instance)!
                    .GetValue(task)!;
            }
        }
        catch (ActorMethodInvocationException amie) // Other exceptions are ignored and handed over to the framework for processing
        {
            if (amie.InnerException != null && amie.InnerException is ActorInvokeException aie)
            {
                // Dapr wraps remote service exceptions
                throw new AbpDaprActorCallException(
                    new RemoteServiceErrorInfo
                    {
                        Message = aie.Message,
                        Code = aie.ActualExceptionType
                    }
                );
            }
            throw;
        }
    }

    private void AddHeaders(DaprHttpClientHandler handler)
    {
        // TenantId
        if (CurrentTenant.Id.HasValue)
        {
            // TODO: Use AbpAspNetCoreMultiTenancyOptions to get the key
            handler.AddHeader(TenantResolverConsts.DefaultTenantKey, CurrentTenant.Id.Value.ToString());
        }
        // Culture
        // TODO: Is that the way we want? Couldn't send the culture (not ui culture)
        var currentCulture = CultureInfo.CurrentUICulture.Name ?? CultureInfo.CurrentCulture.Name;
        if (!currentCulture.IsNullOrEmpty())
        {
            handler.AcceptLanguage(currentCulture);
        }
    }
}
