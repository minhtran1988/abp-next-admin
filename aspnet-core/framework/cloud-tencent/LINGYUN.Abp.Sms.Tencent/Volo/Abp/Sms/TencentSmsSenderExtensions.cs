using LINGYUN.Abp.Sms.Tencent;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Volo.Abp.Sms;

public static class TencentSmsSenderExtensions
{
    /// <summary>
    /// Extended SMS interface
    /// </summary>
    /// <param name="smsSender"></param>
    /// <param name="templateCode">SMS template number</param>
    /// <param name="phoneNumber">Send mobile phone number</param>
    /// <param name="templateParams">SMS template parameters</param>
    /// <returns></returns>
    public static async Task SendAsync(
        this ISmsSender smsSender,
        string templateCode,
        string phoneNumber,
        IDictionary<string, object>? templateParams = null)
    {
        var smsMessage = new SmsMessage(phoneNumber, nameof(TencentCloudSmsSender));
        smsMessage.Properties.Add("TemplateCode", templateCode);
        if(templateParams != null)
        {
            smsMessage.Properties.Add("TemplateParam", templateParams);
        }
        await smsSender.SendAsync(smsMessage);
    }

    /// <summary>
    /// Extended SMS interface
    /// </summary>
    /// <param name="smsSender"></param>
    /// <param name="signName">SMS signature</param>
    /// <param name="templateCode">SMS template number</param>
    /// <param name="phoneNumber">Send mobile phone number</param>
    /// <param name="templateParams">SMS template parameters</param>
    /// <returns></returns>
    public static async Task SendAsync(
        this ISmsSender smsSender,
        string signName,
        string templateCode,
        string phoneNumber,
        IDictionary<string, object>? templateParams = null)
    {
        var smsMessage = new SmsMessage(phoneNumber, nameof(TencentCloudSmsSender));
        smsMessage.Properties.Add("SignName", signName);
        smsMessage.Properties.Add("TemplateCode", templateCode);
        if (templateParams != null)
        {
            smsMessage.Properties.Add("TemplateParam", templateParams);
        }
        await smsSender.SendAsync(smsMessage);
    }
}
