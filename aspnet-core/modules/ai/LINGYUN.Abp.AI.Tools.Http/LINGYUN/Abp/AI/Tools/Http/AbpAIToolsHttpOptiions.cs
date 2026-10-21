using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace LINGYUN.Abp.AI.Tools.Http;
public class AbpAIToolsHttpOptiions
{
    public List<Action<IHttpClientBuilder>> ClientBuildActions { get; }

    public List<Action<IServiceProvider, HttpClient>> ClientActions { get; }

    public List<Action<IServiceProvider, HttpClientHandler>> ClientHandlerActions { get; }
    /// <summary>
    /// Customize Http request response processing
    /// </summary>
    /// <remarks>
    /// Parameter 1: HttpAITool tool name
    /// Parameter 2: IServiceProvider
    /// Parameter 3: Request response
    /// </remarks>
    public IDictionary<string, Func<IServiceProvider, HttpResponseMessage, Task<object>>> HttpResponseActions { get; }
    public AbpAIToolsHttpOptiions()
    {
        ClientBuildActions = new List<Action<IHttpClientBuilder>>();
        ClientActions = new List<Action<IServiceProvider, HttpClient>>();
        ClientHandlerActions = new List<Action<IServiceProvider, HttpClientHandler>>();
        HttpResponseActions = new Dictionary<string, Func<IServiceProvider, HttpResponseMessage, Task<object>>>();
    }
}
