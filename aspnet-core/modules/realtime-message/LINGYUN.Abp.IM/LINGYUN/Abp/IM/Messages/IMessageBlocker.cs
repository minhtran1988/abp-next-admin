using System.Threading.Tasks;

namespace LINGYUN.Abp.IM.Messages;

/// <summary>
/// Message interceptor
/// </summary>
public interface IMessageBlocker
{
    Task InterceptAsync(ChatMessage message);
}
