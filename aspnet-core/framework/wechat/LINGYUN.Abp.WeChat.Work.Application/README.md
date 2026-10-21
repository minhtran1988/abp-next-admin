# LINGYUN.Abp.WeChat.Work.Application

The enterprise WeChat application service module provides application layer service implementation for enterprise WeChat application development.

## Features

* Address book management service
* Application management services
* Message push service
*Customer contact service
* Identity verification service
* Corporate payment services
* Electronic invoice service

## Module reference

```csharp
[DependsOn(typeof(AbpWeChatWorkApplicationModule))]
public class YouProjectModule : AbpModule
{
  // other
}
```

## Application Services

### Address book management

* `IWeChatWorkContactService`: Address book management service
  * Department management
  * Member management
  * Tag management
  *Internet enterprise management

### Application Management

* `IWeChatWorkAgentService`: application management service
  * Application creation
  * Application configuration
  * Apply visibility settings
  * Apply home page settings

### Message push

* `IWeChatWorkMessageService`: message push service
  * Text message
  * Picture message
  * Voice message
  * Video message
  * File message
  * Text card messages
  * Graphic messages
  * Template card message

## More documentation

* [Enterprise WeChat Application Service Module Document](README.EN.md)
