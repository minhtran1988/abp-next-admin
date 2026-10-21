using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace LINGYUN.Platform.Messages;
public interface ISmsMessageAppService : IApplicationService
{
    /// <summary>
    /// Get SMS messages
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<SmsMessageDto> GetAsync(Guid id);
    /// <summary>
    /// Delete SMS message
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task DeleteAsync(Guid id);
    /// <summary>
    /// Resend SMS message
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task SendAsync(Guid id);
    /// <summary>
    /// Get the SMS message list
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    Task<PagedResultDto<SmsMessageDto>> GetListAsync(SmsMessageGetListInput input);
}
