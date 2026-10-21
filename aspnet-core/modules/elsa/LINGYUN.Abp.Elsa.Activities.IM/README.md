# LINGYUN.Abp.Elsa.Activities.IM

Instant messaging activity integration module for Elsa workflow

## Function

* Provide **SendMessage** activity for sending instant messages
  * Support sending user messages and group messages
  * Integrate the `IMessageSender` interface of the ABP framework
  *Support multi-tenancy

## Configuration usage

```csharp
[DependsOn(
    typeof(AbpElsaActivitiesIMModule)
    )]
public class YouProjectModule : AbpModule
{
}
```

## appsettings.json

```json
{
    "Elsa": {
        "IM": true // Enable instant messaging activity
    }
}
```

## Activity parameters

* **Content**: message content
* **FormUser**: Sender user ID
* **FormUserName**: Sender user name
* **To**: Receive user ID (user message)
* **GroupId**: Receive group ID (group message)

## Output parameters

* **MessageId**: sent message ID

[English](./README.EN.md)
