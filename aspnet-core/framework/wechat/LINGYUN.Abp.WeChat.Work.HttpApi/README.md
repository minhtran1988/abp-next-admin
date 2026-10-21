# LINGYUN.Abp.WeChat.Work.HttpApi

The Enterprise WeChat HTTP API module provides HTTP API interface implementation for enterprise WeChat application development.

## Features

* Address book management API
* Application management API
* Message push API
*Customer contact API
* Authentication API
* Enterprise payment API
* Electronic invoice API
* Enterprise WeChat callback interface

## Module reference

```csharp
[DependsOn(typeof(AbpWeChatWorkHttpApiModule))]
public class YouProjectModule : AbpModule
{
  // other
}
```

## API interface

### Address book management

* POST /api/wechat/work/contact/department/create
* POST /api/wechat/work/contact/department/update
* DELETE /api/wechat/work/contact/department/{id}
* POST /api/wechat/work/contact/user/create
* POST /api/wechat/work/contact/user/update
* DELETE /api/wechat/work/contact/user/{id}

### Application Management

* GET /api/wechat/work/agent/{agentId}
* POST /api/wechat/work/agent/set
* GET /api/wechat/work/agent/list
* POST /api/wechat/work/agent/workbench/template

### Message push

* POST /api/wechat/work/message/text
* POST /api/wechat/work/message/image
* POST /api/wechat/work/message/voice
* POST /api/wechat/work/message/video
* POST /api/wechat/work/message/file
* POST /api/wechat/work/message/textcard
* POST /api/wechat/work/message/news
* POST /api/wechat/work/message/templatecard

### Callback interface

* POST /api/wechat/work/callback/{corpId}

## Permissions

* `WeChatWork.Contact`: Address book management
* `WeChatWork.Agent`: Application management
* `WeChatWork.Message`: Message management
* `WeChatWork.Customer`: customer management
* `WeChatWork.Payment`: Enterprise payment
* `WeChatWork.Invoice`: electronic invoice

## More documentation

* [Enterprise WeChat HTTP API module document](README.EN.md)
