using System;
using System.Collections.Generic;
using System.Net.Http;

namespace LY.MicroService.ApiGateway
{
    public class InternalApiGatewayOptions
    {
        public Aggregator Aggregator { get; set; }

        public InternalApiGatewayOptions()
        {
            Aggregator = new Aggregator();
        }
    }

    public class Aggregator
    {
        /// <summary>
        /// Set aggregate routing
        /// </summary>
        /// <remarks>
        /// Aggregation routing:
        /// /api/setting-management/settings
        /// </remarks>
        /// <example>
        /// Host-side routing:
        /// Get: /api/setting-management/settings/by-global
        /// Set: /api/setting-management/settings/change-global
        /// </example>
        /// <example>
        /// Tenant-side routing:
        /// Get: /api/setting-management/settings/by-current-tenant
        /// Set: /api/setting-management/settings/change-current-tenant
        /// </example>
        /// <example>
        /// Client routing:
        /// Get: /api/setting-management/settings/by-current-user
        /// Set: /api/setting-management/settings/change-current-user
        /// </example>
        public AggregatorUrl SettingUrl { get; set; }
        /// <summary>
        /// Apply configuration aggregate routing
        /// </summary>
        /// <remarks>
        /// Aggregation routing:
        /// /api/abp/application-configuration
        /// </remarks>
        public AggregatorUrl ConfigurationUrl { get; set; }
        /// <summary>
        /// Api interface definition configures aggregate routing
        /// </summary>
        /// <remarks>
        /// Aggregation routing:
        /// /api/abp/api-definition
        /// </remarks>
        public AggregatorUrl ApiDefinitionUrl { get; set; }
        public Aggregator()
        {
            SettingUrl = new AggregatorUrl();
            ConfigurationUrl = new AggregatorUrl();
            ApiDefinitionUrl = new AggregatorUrl();
        }
    }

    public class AggregatorUrl
    {
        public string ClientName { get; set; }

        public HttpHandlerOptions HttpHandler { get; set; }
        /// <summary>
        /// Query the aggregate route list
        /// </summary>
        public List<RequestUrl> GetUrls { get; set; }
        /// <summary>
        /// Change route
        /// </summary>
        public RequestUrl SetUrl { get; set; }
        /// <summary>
        /// Default timeout
        /// </summary>
        /// <remarks>
        /// default: 30s
        /// </remarks>
        public TimeSpan? DefaultTimeout { get; set; }

        public AggregatorUrl()
        {
            HttpHandler = new HttpHandlerOptions();
            GetUrls = new List<RequestUrl>();
            DefaultTimeout = TimeSpan.FromSeconds(30);
        }
    }

    public class RequestUrl
    {
        public HttpMethod Method { get; set; }
        public string Url { get; set; }
        public RequestUrl()
        {
            Method = HttpMethod.Get;
        }

        public RequestUrl(string url)
            : this(HttpMethod.Get, url)
        {
        }

        public RequestUrl(
            HttpMethod method,
            string url)
        {
            Method = method;
            Url = url;
        }
    }

    public class HttpHandlerOptions
    {
        public bool AllowAutoRedirect { get; set; }
        public bool UseCookieContainer { get; set; }
        public bool UseTracing { get; set; }
        public bool UseProxy { get; set; }
        public int MaxConnectionsPerServer { get; set; }
    }
}
