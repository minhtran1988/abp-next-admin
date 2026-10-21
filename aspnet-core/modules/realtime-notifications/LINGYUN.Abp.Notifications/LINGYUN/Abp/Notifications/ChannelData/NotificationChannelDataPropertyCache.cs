using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading;

namespace LINGYUN.Abp.Notifications;

/// <summary>
/// Channel data attribute metadata cache
/// </summary>
/// <remarks>
/// <para>
/// Pure reflective metadata cache (no JSON, no logs, no configuration dependencies), so keep it static to avoid
/// Repeated injection between serializers, parsers, and existence providers.
/// </para>
/// </remarks>
public static class NotificationChannelDataPropertyCache
{
    /// <summary>
    /// Property metadata cache
    /// </summary>
    /// <remarks>
    /// The value is wrapped with <see cref="Lazy{T}"/>: The factory of <c>GetOrAdd</c> may be called multiple times under concurrency.
    /// But only one instance will be written to and subsequently read, the rest are discarded, so the build still only happens once.
    /// </remarks>
    private static readonly ConcurrentDictionary<Type, Lazy<NotificationChannelDataProperty[]>> PropertiesCache
        = new();

    /// <summary>
    /// Get the attribute metadata of the payload type
    /// </summary>
    public static NotificationChannelDataProperty[] GetProperties(Type channelDataType)
    {
        return PropertiesCache
            .GetOrAdd(
                channelDataType,
                type => new Lazy<NotificationChannelDataProperty[]>(
                    () => CreateProperties(type),
                    LazyThreadSafetyMode.ExecutionAndPublication))
            .Value;
    }

    /// <summary>
    /// Build attribute metadata
    /// </summary>
    private static NotificationChannelDataProperty[] CreateProperties(Type channelDataType)
    {
        return channelDataType
            .GetProperties(BindingFlags.Public | BindingFlags.Instance)
            .Select(TryCreateProperty)
            .Where(x => x != null)
            .Select(x => x!)
            .ToArray();
    }

    private static NotificationChannelDataProperty? TryCreateProperty(PropertyInfo property)
    {
        var attribute = property.GetCustomAttribute<NotificationChannelPropertyAttribute>(inherit: true);
        if (attribute == null || !property.CanRead)
        {
            return null;
        }

        var key = attribute.Name.IsNullOrWhiteSpace() ? property.Name : attribute.Name!;

        return new NotificationChannelDataProperty(
            key,
            property.PropertyType,
            NotificationChannelDataFormat.Auto,
            attribute.IgnoreWhenMatching,
            CreateGetter(property),
            property.CanWrite ? CreateSetter(property) : null!);
    }

    private static Func<object, object?> CreateGetter(PropertyInfo property)
    {
        var instance = Expression.Parameter(typeof(object), "instance");
        var typedInstance = Expression.Convert(instance, property.DeclaringType!);
        var propertyAccess = Expression.Property(typedInstance, property);

        return Expression
            .Lambda<Func<object, object?>>(Expression.Convert(propertyAccess, typeof(object)), instance)
            .Compile();
    }

    private static Action<object, object?> CreateSetter(PropertyInfo property)
    {
        var instance = Expression.Parameter(typeof(object), "instance");
        var value = Expression.Parameter(typeof(object), "value");
        var typedInstance = Expression.Convert(instance, property.DeclaringType!);

        var assign = Expression.Assign(
            Expression.Property(typedInstance, property),
            Expression.Convert(value, property.PropertyType));

        return Expression
            .Lambda<Action<object, object?>>(assign, instance, value)
            .Compile();
    }
}
