using COSXML;
using COSXML.Auth;
using LINGYUN.Abp.Tencent;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Threading.Tasks;
using Volo.Abp.BlobStoring;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Settings;

namespace LINGYUN.Abp.BlobStoring.Tencent;

public class CosClientFactory : AbstractTencentCloudClientFactory<CosXml, TencentBlobProviderConfiguration>,
    ICosClientFactory,
    ITransientDependency
{
    private readonly static object _clientCacheSync = new();
    protected IBlobContainerConfigurationProvider ConfigurationProvider { get; }

    public CosClientFactory(
        IMemoryCache clientCache,
        ISettingProvider settingProvider,
        IBlobContainerConfigurationProvider configurationProvider)
        : base(clientCache, settingProvider)
    {
        ConfigurationProvider = configurationProvider;
    }

    public async virtual Task<CosXml> CreateAsync<TContainer>()
    {
        var configuration = ConfigurationProvider.Get<TContainer>();

        return await CreateAsync(configuration.GetTencentConfiguration());
    }

    public virtual Task<TencentBlobProviderConfiguration> GetConfigurationAsync<TContainer>()
    {
        var configuration = ConfigurationProvider.Get<TContainer>();

        return Task.FromResult(configuration.GetTencentConfiguration());
    }

    protected override CosXml CreateClient(TencentBlobProviderConfiguration configuration, TencentCloudClientCacheItem cloudCache)
    {
        // Recommended global single object, need to solve cache expiration events
        var cacheKey = TencentCloudClientCacheItem.CalculateCacheKey("client-instance");
        var cosXmlCache =  ClientCache.Get<CosXmlServer>(cacheKey);
        if (cosXmlCache == null)
        {
            lock(_clientCacheSync)
            {
                if (cosXmlCache == null)
                {
                    var configBuilder = new CosXmlConfig.Builder();
                    configBuilder
                        .IsHttps(true)
                        .SetAppid(configuration.AppId)
                        .SetRegion(configuration.Region)
                        .SetDebugLog(true);

                    var cred = new DefaultQCloudCredentialProvider(
                        cloudCache.SecretId,
                        cloudCache.SecretKey,
                        cloudCache.DurationSecond);

                    cosXmlCache = new CosXmlServer(configBuilder.Build(), cred);

                    ClientCache.Set(
                        cacheKey,
                        cosXmlCache,
                        // Expires 60 seconds before session duration
                        TimeSpan.FromSeconds(cloudCache.DurationSecond - 60));
                }
            }
        }

        return cosXmlCache;
    }
}
