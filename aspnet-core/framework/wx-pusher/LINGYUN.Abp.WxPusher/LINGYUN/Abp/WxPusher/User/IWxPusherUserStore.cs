using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace LINGYUN.Abp.WxPusher.User;

public interface IWxPusherUserStore
{
    /// <summary>
    /// Get the list of topics subscribed by the user
    /// </summary>
    /// <param name="userIds">User ID list</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<List<int>> GetSubscribeTopicsAsync(
        IEnumerable<Guid> userIds,
        CancellationToken cancellationToken = default);
    /// <summary>
    /// Get the bound user uid list
    /// </summary>
    /// <param name="userIds"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<List<string>> GetBindUidsAsync(
        IEnumerable<Guid> userIds,
        CancellationToken cancellationToken = default);
}
