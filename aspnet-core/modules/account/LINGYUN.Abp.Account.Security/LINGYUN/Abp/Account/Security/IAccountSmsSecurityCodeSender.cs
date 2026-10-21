using System.Threading;
using System.Threading.Tasks;

namespace LINGYUN.Abp.Account.Security;
/// <summary>
/// SMS security code sending interface
/// </summary>
public interface IAccountSmsSecurityCodeSender
{
    /// <summary>
    /// Send SMS verification code
    /// </summary>
    /// <param name="phone">Mobile phone number</param>
    /// <param name="code">SMS verification code</param>
    /// <param name="templateCode">SMS verification template</param>
    /// <param name="cancellation"></param>
    /// <returns></returns>
    Task SendAsync(
        string phone,
        string code,
        string templateCode,
        CancellationToken cancellation = default);
}
