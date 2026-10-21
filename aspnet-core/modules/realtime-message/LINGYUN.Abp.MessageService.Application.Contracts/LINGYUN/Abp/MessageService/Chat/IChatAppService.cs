using LINGYUN.Abp.IM.Messages;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace LINGYUN.Abp.MessageService.Chat;

public interface IChatAppService : IApplicationService
{
    /// <summary>
    /// Send message
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    Task<ChatMessageSendResultDto> SendMessageAsync(ChatMessage input);
    /// <summary>
    /// Get group messages
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    Task<PagedResultDto<ChatMessage>> GetMyGroupMessageAsync(GroupMessageGetByPagedDto input);
    /// <summary>
    /// Get my message
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    Task<PagedResultDto<ChatMessage>> GetMyChatMessageAsync(UserMessageGetByPagedDto input);
    /// <summary>
    /// Get my latest messages
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    Task<ListResultDto<LastChatMessage>> GetMyLastChatMessageAsync(GetUserLastMessageDto input);
    // TOTO: You should also get my unread messages and get my unread group messages.
}
