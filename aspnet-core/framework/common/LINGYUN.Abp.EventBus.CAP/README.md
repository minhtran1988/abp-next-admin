# LINGYUN.Abp.EventBus.CAP

Distributed event bus CAP integration

#### NOTE

* The custom event handler interface ICustomDistributedEventSubscriber is not implemented. The current distributed events can only be discovered by CustomDistributedEventSubscriber during startup.

* Since CAP does not support multiple consumers of the same event parameter, create a new Group through the full class name of the consumer in the ConsumerServiceSelector interface

## Configuration usage

```csharp
[DependsOn(typeof(AbpCAPEventBusModule))]
public class YouProjectModule : AbpModule
{
  // other
}

## Change History

【2021-03-14】 Add multi-tenant support
