# LINGYUN.Abp.WeChat.Work.Common

The enterprise WeChat general module provides basic function implementation for enterprise WeChat application development.

## Features

* Unified message processing framework
* Unified message encryption and decryption mechanism
* Unified event handling mechanism
* Unified API calling interface
* Unified error handling mechanism
* AccessToken management
* JsApi Ticket management

## Module reference

```csharp
[DependsOn(typeof(AbpWeChatWorkCommonModule))]
public class YouProjectModule : AbpModule
{
  // other
}
```

## Configuration items

```json
{
  "WeChat": {
    "Work": {
      "Common": {
        "MessageHandlerFactory": {
          "MessageResolvers": [], // Message resolver list
          "MessageHandlers": [] // Message handler list
        },
        "AccessToken": {
          "CacheExpiration": 7200, // AccessToken cache time (seconds)
          "CacheKey": "WeChat:Work:AccessToken:{0}" // AccessToken cache key template
        },
        "JsApiTicket": {
          "CacheExpiration": 7200, // JsApi Ticket cache time (seconds)
          "CacheKey": "WeChat:Work:JsApiTicket:{0}" // JsApi Ticket cache key template
        }
      }
    }
  }
}
```

## Message processing

* Support text message processing
* Support picture message processing
* Support voice message processing
* Support video message processing
* Support location message processing
* Support link message processing
* Support event message processing

## Event handling

* Support application menu events
* Support entering application events
*Support reporting of geographical location events
* Support asynchronous task completion events
* Support external contact events
* Support address book change event

## More documentation

* [Enterprise WeChat General Module Document](README.EN.md)
