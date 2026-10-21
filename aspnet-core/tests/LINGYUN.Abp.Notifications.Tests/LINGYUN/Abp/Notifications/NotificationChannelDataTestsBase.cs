using LINGYUN.Abp.Tests;
using System.Reflection;
using Volo.Abp.Json;
using Volo.Abp.Modularity;

namespace LINGYUN.Abp.Notifications;

/// <summary>
/// Channel data test base class
/// </summary>
/// <remarks>
/// The test projects of each channel inherit this class and specify their own test module (this module depends on the real channel module),
/// Thus sharing the same set of "new message object → <c>IJsonSerializer</c> serialization → channel side verification" writing method.
/// </remarks>
public abstract class NotificationChannelDataTestsBase<TStartupModule> : AbpTestsBase<TStartupModule>
    where TStartupModule : IAbpModule
{
    /// <summary>
    /// Channel side parser
    /// </summary>
    protected INotificationChannelDataResolver ChannelDataResolver
        => GetRequiredService<INotificationChannelDataResolver>();

    /// <summary>
    /// Channel data accessor (for production side writing)
    /// </summary>
    protected INotificationChannelDataAccessor ChannelDataAccessor
        => GetRequiredService<INotificationChannelDataAccessor>();

    /// <summary>
    /// Producer side serializer
    /// </summary>
    protected INotificationChannelDataSerializer ChannelDataSerializer
        => GetRequiredService<INotificationChannelDataSerializer>();

    /// <summary>
    /// ABP global JSON serializer
    /// </summary>
    protected IJsonSerializer JsonSerializer
        => GetRequiredService<IJsonSerializer>();

    /// <summary>
    /// Only serialize message objects to facilitate assertion of JSON contracts
    /// </summary>
    protected string Serialize<TPayload>(TPayload payload)
        where TPayload : class
    {
        return JsonSerializer.Serialize(payload);
    }

    /// <summary>
    /// Serialize the message object and write it into the payload key of the notification data
    /// </summary>
    /// <remarks>
    /// Take the production path: <see cref="INotificationChannelDataAccessor.Set"/> for complex types
    /// Call <see cref="IJsonSerializer"/> for serialization, which is exactly the same as the result written by the producer.
    /// </remarks>
    protected NotificationData WriteChannelData<TChannelData>(TChannelData channelData)
        where TChannelData : NotificationChannelData
    {
        return MergeChannelData(new NotificationData(), channelData);
    }

    /// <summary>
    /// Serialize the message object and merge it into the existing notification data.
    /// </summary>
    protected NotificationData MergeChannelData<TChannelData>(
        NotificationData notificationData,
        TChannelData channelData)
        where TChannelData : NotificationChannelData
    {
        foreach (var property in typeof(TChannelData).GetProperties(BindingFlags.Public | BindingFlags.Instance))
        {
            var attribute = property.GetCustomAttribute<NotificationChannelPropertyAttribute>();
            if (attribute == null)
            {
                continue;
            }

            var key = string.IsNullOrWhiteSpace(attribute.Name) ? property.Name : attribute.Name!;

            ChannelDataAccessor.Set(
                notificationData,
                key,
                property.PropertyType,
                property.GetValue(channelData),
                NotificationChannelDataFormat.Auto);
        }

        return notificationData;
    }
}
