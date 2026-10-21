# LINGYUN.Abp.ExceptionHandling.Emailing

Email notification type based on **IExceptionSubscriber** underlying the abp framework

## Features

*Support exception email notification
* Support custom email templates
*Support multi-language email content
* Support stack information sending
* Support exception type and receiving mailbox mapping

## Configuration usage

You need to configure **AbpExceptionHandlingOptions** before use to define the exceptions that need to be notified.
Then configure **AbpEmailExceptionHandlingOptions** to define the specific exception type notification method

```csharp
    [DependsOn(
        typeof(AbpEmailingExceptionHandlingModule)
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
            // Customize the exception types that need to send email notifications
            Configure<AbpEmailExceptionHandlingOptions>(options =>
            {
                // Whether to send stack information
                options.SendStackTrace = true;
                //Default received email if no exception recipient is specified
                options.DefaultReceiveEmail = "colin.in@foxmail.com";
                //Specify which email to send certain exceptions to
                options.HandReceivedException<AbpException>("colin.in@foxmail.com");
            });
        }
    }
```

## Configuration item description

* `SendStackTrace`: whether to include exception stack information in the email
* `DefaultTitle`: Default email title
* `DefaultContentHeader`: Default email content header
* `DefaultContentFooter`: The bottom of the default email content
* `DefaultReceiveEmail`: Default exception receiving email
* `Handlers`: mapping dictionary between exception types and receiving mailboxes

## Localized resources

The module contains localized resources for the following languages:
* en
* zh-Hans

## Related links

* [Basic exception handling module](../LINGYUN.Abp.ExceptionHandling/README.md)
* [Exception real-time notification module](../../../modules/realtime-notifications/LINGYUN.Abp.ExceptionHandling.Notifications/README.md)

## More

See [Documentation](https://github.com/colinin/abp-next-admin) for more information and configuration examples.