using System.Threading.Tasks;

namespace LINGYUN.Abp.Sms.Aliyun;
/// <summary>
/// Alibaba Cloud sends SMS verification code interface
/// </summary>
public interface IAliyunSmsVerifyCodeSender
{
    /// <summary>
    /// Send SMS verification code
    /// </summary>
    /// <param name="message"></param>
    /// <returns></returns>
    Task SendAsync(SmsVerifyCodeMessage message);
}
