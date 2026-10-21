using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace LINGYUN.Abp.Notifications;

public interface IMySubscriptionAppService : IApplicationService
{
    /// <summary>
    /// Whether the message has been subscribed
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    Task<UserSubscriptionsResult> IsSubscribedAsync(SubscriptionsGetByNameDto input);
    /// <summary>
    /// Subscribe to messages
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    Task SubscribeAsync(SubscriptionsGetByNameDto input);
    /// <summary>
    /// Unsubscribe message
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    Task UnSubscribeAsync(SubscriptionsGetByNameDto input);
    /// <summary>
    /// Get subscription list
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    Task<PagedResultDto<UserSubscreNotificationDto>> GetListAsync(SubscriptionsGetByPagedDto input);
    /// <summary>
    /// Get all subscription lists
    /// </summary>
    /// <returns></returns>
    Task<ListResultDto<UserSubscreNotificationDto>> GetAllListAsync();
}
