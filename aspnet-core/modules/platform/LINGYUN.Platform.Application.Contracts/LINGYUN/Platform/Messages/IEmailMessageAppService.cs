using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace LINGYUN.Platform.Messages;
public interface IEmailMessageAppService : IApplicationService
{
    /// <summary>
    /// Get email messages
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<EmailMessageDto> GetAsync(Guid id);
    /// <summary>
    /// Delete email message
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task DeleteAsync(Guid id);
    /// <summary>
    /// Send email message
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task SendAsync(Guid id);
    /// <summary>
    /// Get the email message list
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    Task<PagedResultDto<EmailMessageDto>> GetListAsync(EmailMessageGetListInput input);
}
