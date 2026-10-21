namespace LINGYUN.Abp.WeChat.Work.JsSdk.Models;
public class JsApiTicketInfo
{
    /// <summary>
    /// jsapi_ticket required to generate signature, up to 512 bytes
    /// </summary>
    public string Ticket { get; set; } = default!;
    /// <summary>
    /// Validity time of the voucher (seconds)
    /// </summary>
    public int ExpiresIn { get; set; }
    public JsApiTicketInfo()
    {

    }

    public JsApiTicketInfo(string ticket, int expiresIn)
    {
        Ticket = ticket;
        ExpiresIn = expiresIn;
    }
}
