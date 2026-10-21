# Enterprise WeChat message notification

## Features

* [Send application message](https://developer.work.weixin.qq.com/document/path/90236)
* [Send group chat message](https://developer.work.weixin.qq.com/document/path/90248)
* [Send Webhook message](https://developer.work.weixin.qq.com/document/path/99110)


## How to use

- Send Webhook messages
```csharp
public class Demo
{
	private readonly IWeChatWorkMessageSender _sender;
	
	public Demo(IWeChatWorkMessageSender sender)
	{
		_sender = sender;
	}

	public async Task Send()
	{
		await _sender.SendAsync(
            "693a91f6-7xxx-4bc4-97a0-0ec2sifa5aaa",
            new WeChatWorkWebhookTemplateCardMessage(
                new WebhookTextNoticeCardMessage(
                    WebhookTemplateCardAction.Link("https://developer.work.weixin.qq.com/document/path/99110"),
                    new WebhookTemplateCardMainTitle("Leave application approval notification", "Your leave application has been approved, please go and check it!"),
                    source: WebhookTemplateCardSource.Black("https://wwcdn.weixin.qq.com/node/wework/images/wecom-logo.a61830413b.svg", "Enterprise WeChat"),
                    horizontalContents: new List<WebhookTemplateCardHorizontalContent>
                    {
                        WebhookTemplateCardHorizontalContent.Default("Approval Number", "QJ20251000000136"),
                        WebhookTemplateCardHorizontalContent.Default("Leave Date", "2025/10/01-2025/10/10"),
                        WebhookTemplateCardHorizontalContent.Default("Passed time", "2025-10-01 15:30:00"),
                        WebhookTemplateCardHorizontalContent.Default("Approval Notes", "Do a good job in attendance and handover matters"),
                    },
                    jumps: new List<WebhookTemplateCardJump>
                    {
                        WebhookTemplateCardJump.Link("Go to OA to view", "https://developer.work.weixin.qq.com/document/path/99110")
                    })));
	}
}
```


## More documentation

* [Enterprise WeChat Document](https://developer.work.weixin.qq.com/document/path/90664)

