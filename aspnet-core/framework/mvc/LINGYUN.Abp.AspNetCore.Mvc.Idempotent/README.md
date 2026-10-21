# LINGYUN.Abp.AspNetCore.Mvc.Idempotent

MVC interface idempotence checking module

## Configuration usage

```csharp
[DependsOn(typeof(AbpAspNetCoreMvcIdempotentModule))]
public class YouProjectModule : AbpModule
{
	public override void ConfigureServices(ServiceConfigurationContext context)
	{
		Configure<AbpMvcIdempotentOptions>(options =>
		{
			// For example: perform idempotent verification on DELETE request method
			options.SupportedMethods.Add("DELETE");
        });
	}
}
```
## Configuration item description

## Others

