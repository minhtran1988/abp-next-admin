# LINGYUN.Abp.Identity.Session

User session basic module, providing relevant common interfaces


## Features

* Provide basic interface for user session management
* Provide session caching and persistence synchronization mechanism
* Support session access time tracking
* Depends on AbpCachingModule module

## Configuration usage

```csharp
[DependsOn(typeof(AbpIdentitySessionModule))]
public class YouProjectModule : AbpModule
{
	public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<IdentitySessionCheckOptions>(options =>
        {
            //Set the session cache and persistence facility refresh interval to 10 minutes
            options.KeepAccessTimeSpan = TimeSpan.FromMinutes(10);
        });
    }
}
```

## Configuration items

### IdentitySessionCheckOptions

```json
{
  "Identity": {
    "Session": {
      "Check": {
        "KeepAccessTimeSpan": "00:01:00", //Keep access time (refresh cache session interval), default: 1 minute
        "SessionSyncTimeSpan": "00:10:00" // Session synchronization interval (from cache synchronization to persistence), default: 10 minutes
      }
    }
  }
}
```

## More information

* [ABP Identity Authentication Document](https://docs.abp.io/en/abp/latest/Identity)
