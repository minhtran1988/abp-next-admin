using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Services;

namespace LINGYUN.Platform.Messages.Integration;

[IntegrationService]
public interface IEmailMessageIntegrationService : IApplicationService
{
    /// <summary>
    /// Create email message
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    Task<EmailMessageDto> CreateAsync(EmailMessageCreateDto input);
}
