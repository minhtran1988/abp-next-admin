# LINGYUN.Abp.ExceptionHandling.Notifications

The real-time notification type based on **IExceptionSubscriber** at the bottom of the abp framework is used to send exception information to users through real-time notification.

## Features

* Support real-time notification of exceptions
*Support multi-tenancy
* Support notification templates
* Support system level notifications
* Integrated universal notification module

## Configuration usage

Before use, you need to configure **AbpExceptionHandlingOptions** to define the exceptions that need to be notified.

```csharp
    [DependsOn(
        typeof(AbpNotificationsExceptionHandlingModule)
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

## Notification content

Exception notifications contain the following information:
* `header`: Exception notification header information
* `footer`: Exception notification bottom information
* `loglevel`: log level
* `stackTrace`: Exception stack information

## Notification name

Modules use the following notification names:
* `NotificationsCommonNotificationNames.ExceptionHandling`: exception handling notification name

## Notification template

*Sender: System
* Notification level: Error
*Support multi-tenancy: Yes

## Dependent modules

* `AbpExceptionHandlingModule`: basic exception handling module
* `AbpNotificationsCommonModule`: Common notification module

## Related links

* [Basic exception handling module](../../../framework/common/LINGYUN.Abp.ExceptionHandling/README.md)
* [Exception email notification module](../../../framework/common/LINGYUN.Abp.ExceptionHandling.Emailing/README.md)

## More

See [Documentation](https://github.com/colinin/abp-next-admin) for more information and configuration examples.
