using AlibabaCloud.SDK.Dysmsapi20170525;
using System.Threading.Tasks;

namespace LINGYUN.Abp.Sms.Aliyun;

public interface IDysmsClientFactory
{
    /// <summary>
    /// Build SMS client
    /// </summary>
    /// <returns></returns>
    Task<Client> CreateAsync();
}
