# LINGYUN.Abp.Elsa.Notifications

Workflow notification integration, when the workflow is triggered, the corresponding event notification is released

## Available status

* Faulted: An error occurred during workflow execution
* Canceled: Workflow canceled
* Completed: Workflow execution is completed
*Suspended: Workflow suspended


## Configuration usage

```csharp

    [DependsOn(
        typeof(AbpElsaNotificationsModule)
        )]
    public class YouProjectModule : AbpModule
    {
    }
```

```csharp
    // define notification
    public class DemoNotificationDefinitionProvider : NotificationDefinitionProvider
    {
        public override void Define(INotificationDefinitionContext context)
        {
            var demoGroup = context.AddGroup("Group");

            // Due to the diversity of notifications, template messages need to be used to transfer data
            demoGroup.AddNotification("Faulted")
                .WithTemplate(template => { });
            demoGroup.AddNotification("Cancelled")
                .WithTemplate(template => { });
            demoGroup.AddNotification("Suspended")
                .WithTemplate(template => { });
            demoGroup.AddNotification("Completed")
                .WithTemplate(template => { });
        }
    }
```

```csharp

    //Define workflow
    public class DemoWorkflow : IWorkflow
    {
        public void Build(IWorkflowBuilder builder)
        {
            builder
                .WithFaultedNotification("Faulted")
                .WithCancelledNotification("Cancelled")
                .WithSuspendedNotification("Suspended")
                .WithCompletedNotification("Completed")
                .SetVariable("demo", context =>
                {
                    // Custom parameters can be passed and will be written into the published notification data as transient variables.
                    context.WithNotificationData("demo", "demo");
                    // Customize the tenant ID used to publish notifications
                    context.WithNotificationTenantId(Guid.NewGuid());
                })
                .WriteLine("Start a workflow.")
                .WriteLine("Workflow finished.");
        }
    }
```