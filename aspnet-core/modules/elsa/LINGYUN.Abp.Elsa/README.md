# LINGYUN.Abp.Elsa

[elsa-core](https://github.com/elsa-workflows/elsa-core) abp integration of workflow

## Features

* Provide default **AbpActivity** and multi-tenant integration
* Define **AbpTenantAccessor** to integrate with multi-tenants
* Define **AbpElsaIdGenerator** to generate workflow identifiers through the **IGuidGenerator** interface
* Define **abp** related JavaScript extensions

## Configuration usage

```csharp

    [DependsOn(
        typeof(AbpElsaModule)
        )]
    public class YouProjectModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            PreConfigure<ElsaOptionsBuilder>(elsa =>
            {
                // Customize elsa related configuration
            });
        }
    }
```