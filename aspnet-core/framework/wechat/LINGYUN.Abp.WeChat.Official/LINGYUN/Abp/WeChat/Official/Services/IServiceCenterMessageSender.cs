using LINGYUN.Abp.WeChat.Official.Services.Models;
using System.Threading;
using System.Threading.Tasks;

namespace LINGYUN.Abp.WeChat.Official.Services;
/// <summary>
/// Customer service center message interface
/// </summary>
public interface IServiceCenterMessageSender
{
    Task SendAsync(MessageModel message, CancellationToken cancellationToken = default);
}
