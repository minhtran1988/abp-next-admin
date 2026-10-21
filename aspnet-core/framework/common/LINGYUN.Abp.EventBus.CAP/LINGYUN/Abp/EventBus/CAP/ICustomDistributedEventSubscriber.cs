using System;
using Volo.Abp.EventBus;

namespace LINGYUN.Abp.EventBus.CAP;

/// <summary>
/// Custom event subscriber
/// </summary>
public  interface ICustomDistributedEventSubscriber
{
    /// <summary>
    /// Subscribe to events
    /// </summary>
    /// <param name="eventType"></param>
    /// <param name="factory"></param>
    void Subscribe(Type eventType, IEventHandlerFactory factory);
    /// <summary>
    /// Unsubscribe
    /// </summary>
    /// <param name="eventType"></param>
    /// <param name="factory"></param>
    void UnSubscribe(Type eventType, IEventHandlerFactory factory);
}
