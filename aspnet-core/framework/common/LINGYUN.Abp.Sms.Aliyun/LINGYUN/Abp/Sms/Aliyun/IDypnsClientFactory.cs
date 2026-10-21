using AlibabaCloud.SDK.Dypnsapi20170525;
using System.Threading.Tasks;

namespace LINGYUN.Abp.Sms.Aliyun;

public interface IDypnsClientFactory
{
    /// <summary>
    /// Build SMS authentication client
    /// </summary>
    /// <returns></returns>
    Task<Client> CreateAsync();
}
