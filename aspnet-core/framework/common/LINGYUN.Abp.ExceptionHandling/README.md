# LINGYUN.Abp.ExceptionHandling

Based on the **IExceptionSubscriber** at the bottom of the abp framework, a secondary extension is implemented to customize the exception notification method.

## Features

* Provide unified exception handling and notification mechanism
* Support custom exception handler
* Support exception notification filtering
* Support integration with other notification modules (such as email, real-time notification, etc.)

## Configuration usage

Before use, you only need to configure **AbpExceptionHandlingOptions** to define the exceptions that need to be notified.

```csharp
    [DependsOn(
        typeof(AbpExceptionHandlingModule)
        )]
    public class YouProjectModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            // Customize the exceptions that need to be handled
            Configure<AbpExceptionHandlingOptions>(options =>
            {
                //Add the exception types that need to be handled
                options.Handlers.Add<AbpException>();
            });
        }
    }
```

## Configuration item description

* `Handlers`: List of exception handlers, used to define the types of exceptions that need to be handled
* `HasNotifierError`: Check whether the exception requires sending a notification

## Extension modules

* [LINGYUN.Abp.ExceptionHandling.Emailing](../LINGYUN.Abp.ExceptionHandling.Emailing/README.md): Exception email notification module
* [LINGYUN.Abp.ExceptionHandling.Notifications](../../../modules/realtime-notifications/LINGYUN.Abp.ExceptionHandling.Notifications/README.md): Exception real-time notification module

## More

See [Documentation](https://github.com/colinin/abp-next-admin) for more information and configuration examples.