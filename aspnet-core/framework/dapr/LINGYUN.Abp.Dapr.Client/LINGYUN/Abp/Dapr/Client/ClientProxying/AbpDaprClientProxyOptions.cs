using LINGYUN.Abp.Dapr.Client.DynamicProxying;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Http;

namespace LINGYUN.Abp.Dapr.Client.ClientProxying;

public class AbpDaprClientProxyOptions
{
    public Dictionary<Type, DynamicDaprClientProxyConfig> DaprClientProxies { get; set; }
    /// <summary>
    /// Add a configurable request message
    /// Parameter 1: appId
    /// Parameter two: HttpRequestMessage
    /// </summary>
    public List<Action<string, HttpRequestMessage>> ProxyRequestActions { get; }
    /// <summary>
    /// Process the response and return the response content
    /// </summary>
    public Func<HttpResponseMessage, IAbpLazyServiceProvider, Task<string?>>? ProxyResponseContent { get; private set; }
    /// <summary>
    /// Formatting error
    /// </summary>
    public Func<HttpResponseMessage, IAbpLazyServiceProvider, Task<RemoteServiceErrorInfo?>>? ProxyErrorFormat { get; private set; }
    public AbpDaprClientProxyOptions()
    {
        DaprClientProxies = new Dictionary<Type, DynamicDaprClientProxyConfig>();
        ProxyRequestActions = new List<Action<string, HttpRequestMessage>>();

        OnResponse(async (response, serviceProvider) =>
        {
            return await response.Content.ReadAsStringAsync();
        });
    }
    /// <summary>
    /// Process call response data between services
    /// </summary>
    /// <param name="func"></param>
    public void OnResponse(Func<HttpResponseMessage, IAbpLazyServiceProvider, Task<string?>> func)
    {
        ProxyResponseContent = func;
    }
    /// <summary>
    /// Handle error messages between services
    /// </summary>
    /// <param name="func"></param>
    public void OnError(Func<HttpResponseMessage, IAbpLazyServiceProvider, Task<RemoteServiceErrorInfo?>> func)
    {
        ProxyErrorFormat = func;
    }
}
