# LINGYUN.Abp.Identity.WeChat.Work

The enterprise WeChat user identity integration module provides integration functions between enterprise WeChat users and the ABP Identity system.

## Features

* Enterprise WeChat user identity integration
* Supports synchronization of enterprise WeChat user information to the Identity system
* Support enterprise WeChat organizational structure synchronization
*Support automatic creation of users

## Module reference

```csharp
[DependsOn(typeof(AbpIdentityWeChatWorkModule))]
public class YouProjectModule : AbpModule
{
  // other
}
```

## Configuration items

```json
{
  "Identity": {
    "WeChat": {
      "Work": {
        "CreateUserIfNotExist": true, // Whether to automatically create the user when it does not exist
        "UpdateUserIfExist": true, // Whether to update information when the user exists
        "SyncOrganizationUnit": true // Whether to synchronize the organizational structure
      }
    }
  }
}
```

## More documentation

* [Enterprise WeChat User Identity Integration Document](README.EN.md)
