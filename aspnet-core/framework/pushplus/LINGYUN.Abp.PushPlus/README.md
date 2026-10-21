# LINGYUN.Abp.PushPlus

Integrated PushPlus

Implement PushPlus related API documents and have PushPlus open capabilities

For details, see the PushPlus documentation: https://www.pushplus.plus/doc/guide/openApi.html#%E6%96%87%E6%A1%A3%E8%AF%B4%E6%98%8E

## Module reference

```csharp
[DependsOn(typeof(AbpPushPlusModule))]
public class YouProjectModule : AbpModule
{
  // other
}
```

## Features

* PushPlus PushPlus feature grouping
* PushPlus.Message.Enable globally enables PushPlus message channel
* PushPlus.Channel.WeChat WeChat public account message channel
* PushPlus.Channel.WeChat.Enable enables WeChat public account message channel
* PushPlus.Channel.WeChat.SendLimit WeChat public account message channel limit number
* PushPlus.Channel.WeChat.SendLimitInterval WeChat public account message channel limit period (days)
* PushPlus.Channel.WeWork Enterprise WeChat messaging channel
* PushPlus.Channel.WeWork.Enable enables enterprise WeChat messaging channel
* PushPlus.Channel.WeWork.SendLimit Enterprise WeChat message channel limit number
* PushPlus.Channel.WeWork.SendLimitInterval Enterprise WeChat message channel limit period (days)
* PushPlus.Channel.Webhook Webhook message channel
* PushPlus.Channel.Webhook.Enable enables Webhook message channel
* PushPlus.Channel.Webhook.SendLimit Webhook message channel limit number of times
* PushPlus.Channel.Webhook.SendLimitInterval Webhook message channel limit period (days)
* PushPlus.Channel.Email Email message channel
* PushPlus.Channel.Email.Enable enables the Email message channel
* PushPlus.Channel.Email.SendLimit Email message channel limit times
* PushPlus.Channel.Email.SendLimitInterval Email message channel limit period (days)
* PushPlus.Channel.Sms SMS message channel
* PushPlus.Channel.Sms.Enable enables SMS message channel
* PushPlus.Channel.Sms.SendLimit SMS message channel limit number
* PushPlus.Channel.Sms.SendLimitInterval SMS message channel limit period (days)

## Configuration items

```json
{
  "PushPlus": {
    "Security": {
      "Token": "Your PushPlus Token", //Token obtained by PushPlus platform
      "SecretKey": "Your PushPlus key" // The key obtained by the PushPlus platform
    }
  }
}
```

## Basic usage

1. Configure PushPlus credentials
   * Obtain Token and key on PushPlus platform
   * Set Token and key in configuration file

2. Send a message
   ```csharp
   public class YourService
   {
       private readonly IPushPlusMessageSender _messageSender;

       public YourService(IPushPlusMessageSender messageSender)
       {
           _messageSender = messageSender;
       }

       public async Task SendMessageAsync()
       {
           //Send WeChat message
           await _messageSender.SendWeChatAsync(
               title: "Message title",
               content: "Message content",
               topic: "optional topic",
               template: PushPlusMessageTemplate.Html
           );

           //Send corporate WeChat message
           await _messageSender.SendWeWorkAsync(
               title: "Message title",
               content: "Message content"
           );

           // send email
           await _messageSender.SendEmailAsync(
               title: "Email title",
               content: "email content"
           );

           //Send SMS
           await _messageSender.SendSmsAsync(
               title: "SMS title",
               content: "SMS content"
           );

           //Send Webhook message
           await _messageSender.SendWebhookAsync(
               title: "Message title",
               content: "Message content",
               webhook: "webhook address"
           );
       }
   }
   ```

## Message template

* Html - HTML format (default)
* Text - plain text format
*Json - JSON format
* Markdown - Markdown format

## More information

* [PushPlus official document](https://www.pushplus.plus/doc/guide/openApi.html)
* [ABP Framework](https://abp.io/)
