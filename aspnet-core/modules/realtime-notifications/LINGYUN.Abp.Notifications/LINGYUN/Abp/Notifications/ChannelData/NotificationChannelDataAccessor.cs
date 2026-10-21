using System;
using System.Collections;
using System.Globalization;
using System.Text.Json;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Newtonsoft.Json.Linq;
using Volo.Abp.DependencyInjection;

namespace LINGYUN.Abp.Notifications;

/// <summary>
/// Notification channel data (load) accessor
/// </summary>
/// <remarks>
/// The only implementation of channel data "weak type attribute bag ↔ strong type value" conversion,
/// By <see cref="INotificationChannelDataSerializer"/> and
/// <see cref="INotificationChannelDataResolver"/> is shared and guaranteed to be independent of the JSON implementation used.
/// </remarks>
public class NotificationChannelDataAccessor : INotificationChannelDataAccessor, ISingletonDependency
{
    protected ILogger<NotificationChannelDataAccessor> Logger { get; }
    protected INotificationChannelDataJsonSerializer JsonSerializer { get; }

    public NotificationChannelDataAccessor(
        INotificationChannelDataJsonSerializer jsonSerializer,
        ILogger<NotificationChannelDataAccessor>? logger = null)
    {
        JsonSerializer = jsonSerializer;
        Logger = logger ?? NullLogger<NotificationChannelDataAccessor>.Instance;
    }

    public virtual string? GetDataTypeKey(Type payloadType)
    {
        return payloadType.FullName;
    }

    public virtual bool IsPresent(NotificationData notificationData, string key)
    {
        return TryGetRawValue(notificationData, key, out _, out _);
    }

    public virtual bool TryGetRawValue(
        NotificationData notificationData,
        string key,
        out object? value,
        out string? matchedKey)
    {
        value = null;
        matchedKey = null;

        if (notificationData == null || key.IsNullOrWhiteSpace())
        {
            return false;
        }

        if (notificationData.ExtraProperties.TryGetValue(key, out var exactValue))
        {
            value = exactValue;
            matchedKey = key;

            return true;
        }

        // Case-insensitive matching when exact match misses, compatible with historical keys (such as TemplateId)
        foreach (var property in notificationData.ExtraProperties)
        {
            if (string.Equals(property.Key, key, StringComparison.OrdinalIgnoreCase))
            {
                value = property.Value;
                matchedKey = property.Key;

                return true;
            }
        }

        return false;
    }

    public virtual object? Get(NotificationData notificationData, string key, Type targetType)
    {
        return TryGetRawValue(notificationData, key, out var rawValue, out var matchedKey)
            ? ConvertValue(rawValue!, targetType, matchedKey ?? key)
            : null;
    }

    public virtual TValue? GetValue<TValue>(NotificationData notificationData, string key)
        where TValue : struct
    {
        var value = Get(notificationData, key, typeof(TValue));

        return value is TValue typedValue ? typedValue : null;
    }

    public virtual TValue GetValue<TValue>(NotificationData notificationData, string key, TValue defaultValue)
        where TValue : struct
    {
        return GetValue<TValue>(notificationData, key) ?? defaultValue;
    }

    public virtual string? GetOrNull(NotificationData notificationData, params string[] keys)
    {
        if (notificationData == null || keys == null)
        {
            return null;
        }

        foreach (var key in keys)
        {
            if (TryGetRawValue(notificationData, key, out var value, out _) && value != null)
            {
                return value.ToString();
            }
        }

        return null;
    }

    public virtual void Set(
        NotificationData notificationData,
        string key,
        Type valueType,
        object? value,
        NotificationChannelDataFormat format)
    {
        if (value == null)
        {
            notificationData.TrySetData(key, null);
            return;
        }

        var underlyingType = Nullable.GetUnderlyingType(valueType) ?? valueType;

        if (format == NotificationChannelDataFormat.Json || !NotificationChannelDataValueTypes.IsPrimitive(underlyingType))
        {
            notificationData.TrySetData(key, JsonSerializer.Serialize(value, underlyingType));
            return;
        }

        notificationData.TrySetData(key, value);
    }

    protected virtual object? ConvertValue(object rawValue, Type targetType, string key)
    {
        var underlyingType = Nullable.GetUnderlyingType(targetType) ?? targetType;

        if (underlyingType.IsInstanceOfType(rawValue))
        {
            return rawValue;
        }

        // The intermediate representation (JsonElement / JValue) left by different JSON implementations is first restored to the CLR original value
        var unwrapped = UnwrapJsonValue(rawValue);
        if (unwrapped == null)
        {
            return null;
        }

        if (underlyingType.IsInstanceOfType(unwrapped))
        {
            return unwrapped;
        }

        if (underlyingType == typeof(string))
        {
            return unwrapped.ToString();
        }

        if (underlyingType.IsEnum)
        {
            return ConvertEnumValue(unwrapped, underlyingType, key);
        }

        if (NotificationChannelDataValueTypes.IsPrimitive(underlyingType))
        {
            return ConvertPrimitiveValue(unwrapped, underlyingType, key);
        }

        return ConvertComplexValue(unwrapped, underlyingType, key);
    }

    /// <summary>
    /// Restore JSON intermediate representation
    /// </summary>
    /// <remarks>
    /// System.Text.Json produces <c>JsonElement</c>, Newtonsoft produces <c>JValue</c>;
    /// Both revert to CLR raw values, making the conversion logic independent of the JSON implementation.
    /// </remarks>
    protected virtual object? UnwrapJsonValue(object rawValue)
    {
        if (rawValue is JsonElement jsonElement)
        {
            return UnwrapJsonElement(jsonElement);
        }

        if (rawValue is JValue jValue)
        {
            return jValue.Value;
        }

        return rawValue;
    }

    protected virtual object? UnwrapJsonElement(JsonElement element)
    {
        switch (element.ValueKind)
        {
            case JsonValueKind.Null:
            case JsonValueKind.Undefined:
                return null;

            case JsonValueKind.String:
                return element.GetString();

            case JsonValueKind.True:
                return true;

            case JsonValueKind.False:
                return false;

            case JsonValueKind.Number:
                if (element.TryGetInt32(out var intValue))
                {
                    return intValue;
                }
                if (element.TryGetInt64(out var longValue))
                {
                    return longValue;
                }
                if (element.TryGetDecimal(out var decimalValue))
                {
                    return decimalValue;
                }
                return element.GetDouble();

            default:
                // Array/Object: Use raw JSON text to participate in subsequent deserialization
                return element.GetRawText();
        }
    }

    protected virtual object? ConvertEnumValue(object rawValue, Type underlyingType, string key)
    {
        if (rawValue is string enumName)
        {
            try
            {
                return Enum.Parse(underlyingType, enumName, ignoreCase: true);
            }
            catch (Exception ex)
            {
                Logger.LogWarning(ex,
                    "Failed to parse the channel data '{Key}' as enum '{Type}', the value will be ignored.",
                    key, underlyingType.FullName);
                return null;
            }
        }

        try
        {
            return Enum.ToObject(
                underlyingType,
                Convert.ChangeType(rawValue, Enum.GetUnderlyingType(underlyingType), CultureInfo.InvariantCulture)!);
        }
        catch (Exception ex)
        {
            Logger.LogWarning(ex,
                "Failed to convert the channel data '{Key}' to enum '{Type}', the value will be ignored.",
                key, underlyingType.FullName);
            return null;
        }
    }

    protected virtual object? ConvertPrimitiveValue(object rawValue, Type underlyingType, string key)
    {
        // After JSON round trip, the Boolean value may appear in the form of numbers or strings, while Convert only recognizes "True"/"False"
        if (underlyingType == typeof(bool) && TryConvertBoolean(rawValue, out var booleanValue))
        {
            return booleanValue;
        }

        try
        {
            return Convert.ChangeType(rawValue, underlyingType, CultureInfo.InvariantCulture);
        }
        catch (Exception ex) when (ex is FormatException || ex is InvalidCastException || ex is OverflowException)
        {
            // After JSON round trip, the string may be wrapped in quotation marks, such as "\"1\"", try again to remove quotation marks.
            if (rawValue is string text)
            {
                var trimmed = text.Trim().Trim('"');
                if (!ReferenceEquals(trimmed, text) && trimmed.Length > 0)
                {
                    try
                    {
                        return Convert.ChangeType(trimmed, underlyingType, CultureInfo.InvariantCulture);
                    }
                    catch (Exception inner) when (inner is FormatException || inner is InvalidCastException || inner is OverflowException)
                    {
                        // Ignore, record the log below
                    }
                }
            }

            Logger.LogWarning(ex,
                "Failed to convert the channel data '{Key}' of '{ValueType}' to '{TargetType}', the value will be ignored.",
                key, rawValue.GetType().FullName, underlyingType.FullName);
            return null;
        }
    }

    protected virtual object? ConvertComplexValue(object rawValue, Type underlyingType, string key)
    {
        if (rawValue is not string json)
        {
            Logger.LogWarning(
                "The channel data '{Key}' is stored as '{ValueType}', but '{TargetType}' was expected, the value will be ignored.",
                key, rawValue.GetType().FullName, underlyingType.FullName);
            return null;
        }

        try
        {
            return JsonSerializer.Deserialize(json, underlyingType);
        }
        catch (Exception ex)
        {
            // When the payload is illegal, only log is logged and null is returned,
            // The caller explicitly reports the error by "Declared but failed to parse"
            Logger.LogWarning(ex,
                "Failed to deserialize the channel data '{Key}' to '{TargetType}', the value will be ignored.",
                key, underlyingType.FullName);
            return null;
        }
    }

    /// <summary>
    /// Attempt to convert various representations to boolean values
    /// </summary>
    /// <remarks>
    /// Both numerical <c>0/1</c> and string <c>"0"/"1"</c> / <c>"true"/"false"</c> will be recognized,
    /// <c>Convert.ChangeType</c> only accepts <c>"True"/"False"</c>, so it needs to be handled separately.
    /// </remarks>
    protected virtual bool TryConvertBoolean(object rawValue, out bool result)
    {
        switch (rawValue)
        {
            case bool booleanValue:
                result = booleanValue;
                return true;

            case string text:
                var trimmed = text.Trim().Trim('"');
                if (bool.TryParse(trimmed, out var parsed))
                {
                    result = parsed;
                    return true;
                }
                if (double.TryParse(trimmed, NumberStyles.Any, CultureInfo.InvariantCulture, out var number))
                {
                    result = number != 0;
                    return true;
                }
                break;

            default:
                try
                {
                    result = Convert.ToDouble(rawValue, CultureInfo.InvariantCulture) != 0;
                    return true;
                }
                catch (Exception ex) when (ex is FormatException || ex is InvalidCastException || ex is OverflowException)
                {
                    break;
                }
        }

        result = false;
        return false;
    }
}
