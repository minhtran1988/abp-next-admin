using LINGYUN.Abp.WeChat.Work.JsSdk.Models;
using System.Threading;
using System.Threading.Tasks;

namespace LINGYUN.Abp.WeChat.Work.JsSdk;
/// <summary>
/// JS-SDK temporary ticket provider
/// See: https://developer.work.weixin.qq.com/document/path/90506
/// </summary>
public interface IJsApiTicketProvider
{
    /// <summary>
    /// Get enterprise jsapi_ticket
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<JsApiTicketInfo> GetTicketInfoAsync(CancellationToken cancellationToken = default);
    /// <summary>
    /// Get application jsapi_ticket
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<JsApiTicketInfo> GetAgentTicketInfoAsync(CancellationToken cancellationToken = default);
    /// <summary>
    /// Get JS-SDK signature
    /// </summary>
    /// <param name="ticketInfo">JS-SDK temporary ticket</param>
    /// <param name="url">Generate signed url</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    JsApiSignatureData GenerateSignature(JsApiTicketInfo ticketInfo, string url, CancellationToken cancellationToken = default);
}
