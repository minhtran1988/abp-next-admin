# LINGYUN.Abp.IdentityServer.Domain

IdentityServer domain module extends the domain layer functions of IdentityServer4.

## Features

* Event service extension
  * Custom event service implementation - `AbpEventService`
  * Configurable event handler - `IAbpIdentityServerEventServiceHandler`
  * Default event handler - `AbpIdentityServerEventServiceHandler`
    * Support failure event recording
    * Support information event recording
    * Support successful event recording
    * Support error event recording
  * Event handler registration mechanism
    * Configure event handlers via `AbpIdentityServerEventOptions`

## Module reference

```csharp
[DependsOn(
    typeof(AbpIdentityServerDomainModule)
)]
public class YourModule : AbpModule
{
    // ...
}
```

## Dependent modules

* `Volo.Abp.IdentityServer.AbpIdentityServerDomainModule` - ABP IdentityServer domain module

## Configuration usage

### Event handler configuration

```csharp
Configure<AbpIdentityServerEventOptions>(options =>
{
    //Add custom event handler
    options.EventServiceHandlers.Add<YourEventServiceHandler>();
});
```

### Event handler implementation

```csharp
public class YourEventServiceHandler : IAbpIdentityServerEventServiceHandler
{
    public virtual bool CanRaiseEventType(EventTypes evtType)
    {
        // Implement event type judgment logic
        return true;
    }

    public virtual Task RaiseAsync(Event evt)
    {
        // Implement event processing logic
        return Task.CompletedTask;
    }
}
```

Related documents:
* [IdentityServer4 Documentation](https://identityserver4.readthedocs.io/)
* [ABP IdentityServer Documentation](https://docs.abp.io/en/abp/latest/Modules/IdentityServer)

[View English document](README.EN.md)
