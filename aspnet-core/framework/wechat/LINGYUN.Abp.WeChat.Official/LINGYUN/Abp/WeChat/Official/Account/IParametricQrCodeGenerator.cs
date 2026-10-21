using LINGYUN.Abp.WeChat.Official.Account.Models;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace LINGYUN.Abp.WeChat.Official.Account;
/// <summary>
/// Generate QR code interface with parameters
/// </summary>
/// <remarks>
/// For details, see: https://developers.weixin.qq.com/doc/offiaccount/Account_Management/Generating_a_Parametric_QR_Code.html
/// </remarks>
public interface IParametricQrCodeGenerator
{
    /// <summary>
    /// Create QR code ticket
    /// </summary>
    /// <param name="model"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<TicketModel> CreateTicketAsync(CreateTicketModel model, CancellationToken cancellationToken = default);
    /// <summary>
    /// Exchange tickets for QR codes
    /// </summary>
    /// <param name="ticket"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<Stream> ShowQrCodeAsync(string ticket, CancellationToken cancellationToken = default);
}
