using DotNetCore.CAP;
using DotNetCore.CAP.Internal;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EventBus;
using Volo.Abp.EventBus.Distributed;

namespace LINGYUN.Abp.EventBus.CAP;

/// <summary>
/// Consumer finder
/// </summary>
[Dependency(ServiceLifetime.Singleton, ReplaceServices = true)]
[ExposeServices(typeof(IConsumerServiceSelector), typeof(AbpCAPConsumerServiceSelector))]

public class AbpCAPConsumerServiceSelector : ConsumerServiceSelector
{
    /// <summary>
    /// CAP configuration
    /// </summary>
    protected CapOptions CapOptions { get; }
    /// <summary>
    /// Abp distributed event configuration
    /// </summary>
    protected AbpDistributedEventBusOptions AbpDistributedEventBusOptions { get; }
    /// <summary>
    /// Service provider
    /// </summary>
    protected IServiceProvider ServiceProvider { get; }

    /// <summary>
    /// Creates a new <see cref="T:DotNetCore.CAP.Internal.ConsumerServiceSelector" />.
    /// </summary>
    public AbpCAPConsumerServiceSelector(
        IServiceProvider serviceProvider,
        IOptions<CapOptions> capOptions,
        IOptions<AbpDistributedEventBusOptions> distributedEventBusOptions) : base(serviceProvider)
    {
        CapOptions = capOptions.Value;
        ServiceProvider = serviceProvider;
        AbpDistributedEventBusOptions = distributedEventBusOptions.Value;
    }
    /// <summary>
    /// Find consumer collection
    /// </summary>
    /// <param name="provider"></param>
    /// <returns></returns>
    protected override IEnumerable<ConsumerExecutorDescriptor> FindConsumersFromInterfaceTypes(IServiceProvider provider)
    {
        var executorDescriptorList = base.FindConsumersFromInterfaceTypes(provider).ToList();
        // handlers
        var handlers = AbpDistributedEventBusOptions.Handlers;
        var logger = provider.GetRequiredService<ILogger<AbpCAPConsumerServiceSelector>>();
        var consumerExecutorDescriptorComparer = new ConsumerExecutorDescriptorComparer(logger);

        foreach (var handler in handlers)
        {
            var interfaces = handler.GetInterfaces();
            foreach (var @interface in interfaces)
            {
                if (!typeof(IEventHandler).GetTypeInfo().IsAssignableFrom(@interface))
                {
                    continue;
                }
                var genericArgs = @interface.GetGenericArguments();
                if (genericArgs.Length == 1)
                {
                    var consumerExecutorDescriptors = GetHandlerDescription(genericArgs[0], handler);

                    foreach (var consumerExecutorDescriptor in consumerExecutorDescriptors)
                    {
                        if (executorDescriptorList.Any(x => consumerExecutorDescriptorComparer.Equals(x, consumerExecutorDescriptor)))
                        {
                            // If there are multiple consumers, subsequent consumers need to redefine the group in order not to be filtered out by the CAP framework
                            var groupAliaName = handler.IsGenericType
                                ? handler.GetGenericTypeDefinition().Name
                                : handler.Name;

                            // TODO: 2021-05-21 Directly using the full name of the type as GroupName will cause confusion for users. Add the group name first.
                            consumerExecutorDescriptor.Attribute.Group = $"{CapOptions.DefaultGroupName}.{groupAliaName}";
                            SetSubscribeAttribute(consumerExecutorDescriptor.Attribute);

                        }
                        executorDescriptorList.Add(consumerExecutorDescriptor);
                    }
                    // Subscribe(genericArgs[0], new IocEventHandlerFactory(ServiceScopeFactory, handler));
                }
            }
        }
        return executorDescriptorList;
    }
    /// <summary>
    /// Get the event handler collection
    /// </summary>
    /// <param name="eventType"></param>
    /// <param name="typeInfo"></param>
    /// <returns></returns>
    protected virtual IEnumerable<ConsumerExecutorDescriptor> GetHandlerDescription(Type eventType, Type typeInfo)
    {
        var serviceTypeInfo = typeof(IDistributedEventHandler<>)
            .MakeGenericType(eventType);
        var method = typeInfo
            .GetMethod(
                nameof(IDistributedEventHandler<object>.HandleEventAsync),
                new[] { eventType }
            );
        // TODO: If the event name is defined in the event parameter type, multiple subscriber classes cannot be created. Add optional configuration to let the user decide where the event name is defined.
        var eventName = EventNameAttribute.GetNameOrDefault(eventType);
        var topicAttr = method!.GetCustomAttributes<TopicAttribute>(true);
        var topicAttributes = topicAttr.ToList();

        topicAttributes.Add(new CapSubscribeAttribute(eventName));

        foreach (var attr in topicAttributes)
        {
            SetSubscribeAttribute(attr);

            var parameters = method!.GetParameters()
                .Select(parameter => new ParameterDescriptor
                {
                    Name = parameter.Name!,
                    ParameterType = parameter.ParameterType,
                    IsFromCap = parameter.GetCustomAttributes(typeof(FromCapAttribute)).Any()
                }).ToList();

            yield return InitDescriptor(attr, method, typeInfo.GetTypeInfo(), serviceTypeInfo.GetTypeInfo(), parameters);
        }
    }

    private static ConsumerExecutorDescriptor InitDescriptor(
        TopicAttribute attr,
        MethodInfo methodInfo,
        TypeInfo implType,
        TypeInfo serviceTypeInfo,
        IList<ParameterDescriptor> parameters)
    {
        var descriptor = new ConsumerExecutorDescriptor
        {
            Attribute = attr,
            MethodInfo = methodInfo,
            ImplTypeInfo = implType,
            ServiceTypeInfo = serviceTypeInfo,
            Parameters = parameters
        };

        return descriptor;
    }
}