# LINGYUN.Abp.Identity.WeChat

The WeChat user identity integration module provides integration functions between WeChat users and the ABP Identity system.

## Features

* WeChat user identity integration
* Support WeChat user information synchronization to Identity system
* Support WeChat UnionId mechanism
*Support automatic creation of users

## Module reference

```csharp
[DependsOn(typeof(AbpIdentityWeChatModule))]
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
      "CreateUserIfNotExist": true, // Whether to automatically create the user when it does not exist
      "UpdateUserIfExist": true // Whether to update information when the user exists
    }
  }
}
```

## More documentation

* [WeChat User Identity Integration Document](README.EN.md)
