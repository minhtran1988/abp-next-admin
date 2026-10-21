using Castle.DynamicProxy.Internal;
using LINGYUN.Abp.Dapr.Client.DynamicProxying;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Http.Client;
using Volo.Abp.Http.Client.ClientProxying;
using Volo.Abp.Http.Client.DynamicProxying;
using Volo.Abp.Http.Client.Proxying;
using Volo.Abp.Http.Modeling;
using Volo.Abp.Json;
using Volo.Abp.Localization;
using Volo.Abp.MultiTenancy;

namespace LINGYUN.Abp.BackgroundTasks.Jobs;

public class ServiceInvocationJob : IJobRunnable
{
    #region Definition Paramters

    public readonly static IReadOnlyList<JobDefinitionParamter> Paramters =
        new List<JobDefinitionParamter>
        {
            new JobDefinitionParamter(PropertyService, LocalizableStatic.Create("Http:Service"), required: true),
            new JobDefinitionParamter(PropertyMethod, LocalizableStatic.Create("Http:Method"), required: true),

            new JobDefinitionParamter(PropertyData, LocalizableStatic.Create("Http:Data")),
            new JobDefinitionParamter(PropertyCulture, LocalizableStatic.Create("Http:Culture")),
            new JobDefinitionParamter(PropertyTenant, LocalizableStatic.Create("Http:Tenant")),
            new JobDefinitionParamter(PropertyProvider, LocalizableStatic.Create("Http:Provider"), LocalizableStatic.Create("Http:ProviderDesc")),
            new JobDefinitionParamter(PropertyAppId, LocalizableStatic.Create("Http:AppId"), LocalizableStatic.Create("Http:AppIdDesc")),
        };

    #endregion

    // Required, interface type
    public const string PropertyService = "service";
    // Required, interface method name
    public const string PropertyMethod = "method";
    // Optional, request data, pass string parameters of type Dictionary<string, object>
    public const string PropertyData = "data";
    // Optional, specify culture when requesting
    public const string PropertyCulture = "culture";
    // Optional, specify the tenant when requesting
    public const string PropertyTenant = "tenant";
    // Optional, provider name, http, dapr, without this parameter the default is http
    public const string PropertyProvider = "provider";
    // Necessary parameters for Dapr call
    public const string PropertyAppId = "appId";

    // TODO: support grpc
    private readonly static string[] Providers = new string[] { "http", "dapr" };

    public async virtual Task ExecuteAsync(JobRunnableContext context)
    {
        // Get parameter list
        var type = context.GetString(PropertyService);
        var method = context.GetString(PropertyMethod);
        var serviceType = Type.GetType(type, true);

        Check.NotNull(serviceType, nameof(serviceType));

        var serviceMethod = serviceType.GetMethod(method, BindingFlags.Instance | BindingFlags.Public | BindingFlags.FlattenHierarchy);
        if (serviceMethod == null)
        {
            foreach (var ifce in serviceType.GetAllInterfaces())
            {
                serviceMethod = ifce.GetMethod(method, BindingFlags.Instance | BindingFlags.Public | BindingFlags.FlattenHierarchy);
                if (serviceMethod != null)
                {
                    break;
                }
            }
        }
        Check.NotNull(serviceMethod, nameof(serviceMethod));

        context.TryGetString(PropertyCulture, out var culture);
        context.TryGetString(PropertyProvider, out var provider);
        provider ??= "http";

        if (!Providers.Contains(provider))
        {
            throw new AbpJobExecutionException(
                GetType(),
                $"Invalid inter-service invocation provider: {provider}",
                null);
        }

        context.TryGetMultiTenantId(PropertyTenant, out var tenantId);
        var currentTenant = context.GetRequiredService<ICurrentTenant>();
        using (currentTenant.Change(tenantId))
        {
            using (CultureHelper.Use(culture ?? CultureInfo.CurrentCulture.Name))
            {
                switch (provider)
                {
                    case "http":
                    case "dapr":
                        // No difference in interface proxy
                        await ExecuteProxy(context, serviceType, serviceMethod);
                        break;
                }
            }
        }
    }

    #region HttpClient
    protected async virtual Task ExecuteProxy(
        JobRunnableContext context,
        Type serviceType,
        MethodInfo serviceMethod)
    {
        var clientProxy = context.GetRequiredService(serviceType);

        // Call parameters
        var methodParamters = serviceMethod.GetParameters();
        var invokeParameters = new List<object>();
        if (context.TryGetString(PropertyData, out var data))
        {
            var json = JsonNode.Parse(data);
            foreach(var param in methodParamters)
            {
                var input = json![param.Name!];
                if (input != null)
                {
                    invokeParameters.Add(input.Deserialize(param.ParameterType!)!);
                }
            }
        }

        if (serviceMethod.ReturnType.GenericTypeArguments.IsNullOrEmpty())
        {
            // call directly
            var taskProxy = (Task)serviceMethod.Invoke(clientProxy, invokeParameters.ToArray())!;
            await taskProxy;
        }
        else
        {
            // Call with return value
            var returnType = serviceMethod.ReturnType.GenericTypeArguments[0];
            var callResult = serviceMethod.Invoke(clientProxy, invokeParameters.ToArray())!;
            var result = (Task)callResult;
            context.SetResult(await GetResultAsync(result, returnType));
        }
    }

    protected async virtual Task<ActionApiDescriptionModel> GetActionApiDescriptionModel(
        JobRunnableContext context,
        Type serviceType,
        MethodInfo serviceMethod)
    {
        var clientOptions = context.GetRequiredService<IOptions<AbpHttpClientOptions>>().Value;
        var remoteServiceConfigurationProvider = context.GetRequiredService<IRemoteServiceConfigurationProvider>();
        var proxyHttpClientFactory = context.GetRequiredService<IProxyHttpClientFactory>();
        var apiDescriptionFinder = context.GetRequiredService<IApiDescriptionFinder>();

        var clientConfig = clientOptions.HttpClientProxies.GetOrDefault(serviceType) ??
                           throw new AbpException($"Could not get DynamicHttpClientProxyConfig for {serviceType.FullName}.");
        var remoteServiceConfig = await remoteServiceConfigurationProvider.GetConfigurationOrDefaultAsync(clientConfig.RemoteServiceName);
        var client = proxyHttpClientFactory.Create(clientConfig.RemoteServiceName);

        return await apiDescriptionFinder.FindActionAsync(
            client,
            remoteServiceConfig.BaseUrl,
            serviceType,
            serviceMethod
        );
    }

    #endregion


    #region DaprClient

    protected readonly static string DaprCallRequestMethod = nameof(DynamicDaprProxyInterceptorClientProxy<object>.CallRequestAsync);
    protected readonly static MethodInfo DaprClientProxyMethod = typeof(DynamicDaprProxyInterceptorClientProxy<>)
        .GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
        .First(m => m.Name == DaprCallRequestMethod && !m.IsGenericMethodDefinition);
    protected readonly static MethodInfo DaprCallRequestAsyncMethod = typeof(DynamicDaprProxyInterceptorClientProxy<object>)
        .GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
        .First(m => m.Name == DaprCallRequestMethod && m.IsGenericMethodDefinition);

    protected async virtual Task ExecuteWithDaprProxy(
        JobRunnableContext context,
        Type serviceType,
        MethodInfo serviceMethod)
    {
        var appid = context.GetString(PropertyAppId);
        var clientOptions = context.GetRequiredService<IOptions<AbpHttpClientOptions>>().Value;
        var clientConfig = clientOptions.HttpClientProxies.GetOrDefault(serviceType) ??
               throw new AbpJobExecutionException(
                   GetType(),
                   $"Could not get DynamicHttpClientProxyConfig for {serviceType.FullName}.",
                   null);

        // Parameters required for reflection
        var clientProxyType = typeof(DynamicDaprProxyInterceptorClientProxy<>).MakeGenericType(serviceType);
        var clientProxy = context.GetRequiredService(clientProxyType);

        // Call the remote service discovery endpoint
        var actionApiDescription = await GetActionApiDescriptionModel(
            context, 
            clientConfig.RemoteServiceName, 
            appid, 
            serviceType, 
            serviceMethod);

        // Call parameters
        var invokeParameters = new Dictionary<string, object?>();
        if (context.TryGetString(PropertyData, out var data))
        {
            var jsonSerializer = context.GetRequiredService<IJsonSerializer>();
            invokeParameters = jsonSerializer.Deserialize<Dictionary<string, object?>>(data);
        }

        // Construct service proxy context
        var clientProxyRequestContext = new ClientProxyRequestContext(
           actionApiDescription,
           invokeParameters,
           serviceType);

        if (serviceMethod.ReturnType.GenericTypeArguments.IsNullOrEmpty())
        {
            // call directly
            var taskProxy = (Task)DaprClientProxyMethod
                .Invoke(clientProxy, new object[] { clientProxyRequestContext })!;
            await taskProxy;
        }
        else
        {
            // Call with return value
            var returnType = serviceMethod.ReturnType.GenericTypeArguments[0];
            var result = (Task)DaprCallRequestAsyncMethod
                .MakeGenericMethod(returnType)
                .Invoke(this, new object[] { context })!;

            context.SetResult(await GetResultAsync(result, returnType));
        }
    }

    protected async virtual Task<ActionApiDescriptionModel> GetActionApiDescriptionModel(
        JobRunnableContext context,
        string service,
        string appId,
        Type serviceType,
        MethodInfo serviceMethod)
    {
        var apiDescriptionFinder = context.GetRequiredService<IDaprApiDescriptionFinder>();

        return await apiDescriptionFinder.FindActionAsync(
            service,
            appId,
            serviceType,
            serviceMethod);
    }

    #endregion

    protected async virtual Task<object> GetResultAsync(Task task, Type resultType)
    {
        await task;
        var resultProperty = typeof(Task<>)
            .MakeGenericType(resultType)
            .GetProperty(nameof(Task<object>.Result), BindingFlags.Instance | BindingFlags.Public);
        Check.NotNull(resultProperty, nameof(resultProperty));
        return resultProperty.GetValue(task)!;
    }
}
