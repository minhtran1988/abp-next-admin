# LINGYUN.Abp.WeChat.Work.Application.Contracts

The enterprise WeChat application service contract module provides application layer service interface definitions for enterprise WeChat application development.

## Features

* Address book management service interface
* Application management service interface
* Message push service interface
* Customer contact service interface
* Authentication service interface
* Enterprise payment service interface
* Electronic invoice service interface

## Module reference

```csharp
[DependsOn(typeof(AbpWeChatWorkApplicationContractsModule))]
public class YouProjectModule : AbpModule
{
  // other
}
```

## Service interface

### Address book management

* `IWeChatWorkContactService`
  * `CreateDepartmentAsync`: Create department
  * `UpdateDepartmentAsync`: update department
  * `DeleteDepartmentAsync`: delete department
  * `CreateUserAsync`: Create members
  * `UpdateUserAsync`: Update members
  * `DeleteUserAsync`: delete members
  * `CreateTagAsync`: Create a tag
  * `UpdateTagAsync`: Update tag
  * `DeleteTagAsync`: delete tag

### Application Management

* `IWeChatWorkAgentService`
  * `GetAgentAsync`: Get the application
  * `SetAgentAsync`: Set application
  * `GetAgentListAsync`: Get the application list
  * `SetWorkbenchTemplateAsync`: Set the workbench template

### Message push

* `IWeChatWorkMessageService`
  * `SendTextAsync`: Send text message
  * `SendImageAsync`: Send image message
  * `SendVoiceAsync`: Send voice message
  * `SendVideoAsync`: Send video message
  * `SendFileAsync`: Send file message
  * `SendTextCardAsync`: Send text card message
  * `SendNewsAsync`: Send graphic messages
  * `SendTemplateCardAsync`: Send template card message

## Permissions

* `WeChatWork.Contact`: Address book management
* `WeChatWork.Agent`: Application management
* `WeChatWork.Message`: Message management
* `WeChatWork.Customer`: customer management
* `WeChatWork.Payment`: Enterprise payment
* `WeChatWork.Invoice`: electronic invoice

## More documentation

* [Enterprise WeChat Application Service Contract Module Document](README.EN.md)
