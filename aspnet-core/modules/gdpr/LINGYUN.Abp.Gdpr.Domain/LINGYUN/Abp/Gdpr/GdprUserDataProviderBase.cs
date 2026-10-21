using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using Volo.Abp.EventBus.Distributed;
using Volo.Abp.Gdpr;

namespace LINGYUN.Abp.Gdpr;

public abstract class GdprUserDataProviderBase: IGdprUserDataProvider
{
    public abstract string Name { get; }

    public abstract Task DeleteAsync(GdprDeleteUserDataContext context);
    public abstract Task PorepareAsync(GdprPrepareUserDataContext context);
    /// <summary>
    /// Publish personal data
    /// </summary>
    /// <param name="context"></param>
    /// <param name="gdprDataInfo">Personal data</param>
    /// <returns></returns>
    protected async virtual Task DispatchPrepareUserDataAsync(GdprPrepareUserDataContext context, GdprDataInfo gdprDataInfo)
    {
        var distributedEventBus = context.ServiceProvider.GetRequiredService<IDistributedEventBus>();

        var gdprUserData = new GdprUserDataPreparedEto
        {
            RequestId = context.RequestId,
            Provider = Name,
            Data = gdprDataInfo,
        };

        await distributedEventBus.PublishAsync(gdprUserData);
    }
}
