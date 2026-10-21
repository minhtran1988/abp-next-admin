# LINGYUN.Abp.AspNetCore.Mvc.Client

Rewrite with reference to Volo.Abp.AspNetCore.Mvc.Client

Implement configurable user-configured cache time
Implement subscription configuration refresh event to clear user configuration cache
Implement user cache-based permissions, features, configuration, localization, and language interfaces
Reference LINGYUN.Abp.MultiTenancy.RemoteService to implement the multi-tenant interface
Completely separated from the specific database interface

#### NOTE


## Configuration usage


``` json

{
	"AbpMvcClient": {
		"Cache": {
			"UserCacheExpirationSeconds": 300,
			"AnonymousCacheExpirationSeconds": 300
		}
	}
}

```

```csharp
[DependsOn(typeof(AbpAspNetCoreMvcClientModule))]
public class YouProjectModule : AbpModule
{
  // other
}
```