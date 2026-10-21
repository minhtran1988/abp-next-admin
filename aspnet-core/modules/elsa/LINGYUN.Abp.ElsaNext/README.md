# LINGYUN.Abp.ElsaNext

[elsa-core](https://github.com/elsa-workflows/elsa-core) abp integration of workflow (version 3.x)

## Features

* Define **AbpTenantResolver** to integrate with multi-tenants so that elsa supports abp tenant resolution
* Define **AbpTenantsProvider** to integrate with multi-tenants to enable elsa to obtain tenant information from abp
* Define **AbpElsaIdGenerator** to generate workflow identifiers through the **IGuidGenerator** interface
* Define **abp** related JavaScript extensions

## Configuration usage

```csharp

    [DependsOn(
        typeof(AbpElsaNextModule)
        )]
    public class YouProjectModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            PreConfigure<IModule>(elsa =>
            {
                // Customize elsa related configuration
            });
        }
    }
```