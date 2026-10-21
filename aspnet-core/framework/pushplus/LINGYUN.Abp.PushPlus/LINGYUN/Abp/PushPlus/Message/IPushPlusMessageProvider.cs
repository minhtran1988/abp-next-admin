using System.Threading;
using System.Threading.Tasks;

namespace LINGYUN.Abp.PushPlus.Message;

public interface IPushPlusMessageProvider
{
    /// <summary>
    /// Query message sending results
    /// </summary>
    /// <param name="shortCode">Message short chain code</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<SendPushPlusMessageResult> GetSendResultAsync(
        string shortCode,
        CancellationToken cancellationToken = default);
    /// <summary>
    /// Message list
    /// </summary>
    /// <param name="current">Current page number</param>
    /// <param name="pageSize">The size of each page, the maximum value is 50</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<PushPlusPagedResponse<PushPlusMessage>> GetMessageListAsync(
        int current,
        int pageSize,
        CancellationToken cancellationToken = default); 
}
