using System.Threading.Tasks;

namespace LINGYUN.Abp.IM.Messages;

/// <summary>
/// Message processor
/// </summary>
public interface IMessageProcessor
{
    /// <summary>
    /// Withdraw
    /// </summary>
    /// <param name="message"></param>
    /// <returns></returns>
    Task ReCallAsync(ChatMessage message);
    /// <summary>
    /// The message has been read
    /// </summary>
    /// <param name="message"></param>
    /// <returns></returns>
    Task ReadAsync(ChatMessage message);
}
