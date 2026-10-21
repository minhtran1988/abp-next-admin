using AlibabaCloud.SDK.Captcha20230305;
using System.Threading.Tasks;

namespace LINGYUN.Abp.Aliyun.Captcha;

public interface ICaptchaClientFactory
{
    /// <summary>
    /// Build verification code client
    /// </summary>
    /// <returns></returns>
    Task<Client> CreateAsync();
}
