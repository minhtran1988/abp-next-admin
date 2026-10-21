using System;
using System.Text.Json;

namespace LINGYUN.Abp.Notifications.ChannelData.Tests;

/// <summary>
/// Test implemented in JSON
/// </summary>
/// <remarks>
/// Consistent with the responsibilities of <c>AbpNotificationChannelDataJsonSerializer</c> (delegated to the JSON library),
/// Use <see cref="JsonSerializer"/> directly here,
/// To avoid framework contract tests relying on ABP's full module loading.
/// <para>
/// Channel-specific polymorphic converters (template cards, etc.) are asserted in the respective channel's test projects.
/// </para>
/// </remarks>
public class TestNotificationChannelDataJsonSerializer : INotificationChannelDataJsonSerializer
{
    private static readonly JsonSerializerOptions Options = new();

    public string Serialize(object value, Type valueType)
    {
        return JsonSerializer.Serialize(value, valueType, Options);
    }

    public object? Deserialize(string json, Type targetType)
    {
        return JsonSerializer.Deserialize(json, targetType, Options);
    }
}
