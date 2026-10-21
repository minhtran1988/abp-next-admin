using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace LINGYUN.Abp.WeChat.Work.Messages.Models;

/// <summary>
/// Template card message serialization
/// </summary>
/// <remarks>
/// <para>
/// <see cref="TemplateCard"/> is an abstract type, Newtonsoft.Json cannot instantiate it directly,
/// As a result, once the template card is serialized (for example, persisted as notification data, or entered the failure retry queue), it can no longer be restored:
/// <c>Could not create an instance of type ... Type is an interface or abstract class</c>。
/// </para>
/// <para>
/// Here press the <c>card_type</c> discriminator to restore the specific card type. The discriminator is passed to the base class in the constructor
/// (for example <c>base("template_card", taskId)</c>), obtained by reading the string constant in the constructor IL,
/// Therefore, adding a new card type does not require modifying this converter.
/// </para>
/// </remarks>
public class TemplateCardJsonConverter : JsonConverter
{
    private const byte LdstrOpCode = 0x72;

    private static readonly Dictionary<string, Type> CardTypes = new Dictionary<string, Type>(StringComparer.OrdinalIgnoreCase);

    private static readonly object SyncLock = new object();

    private static bool _initialized;

    /// <summary>
    /// Only take over the abstract card type
    /// </summary>
    /// <remarks>
    /// The specific card type is parsed recursively after the converter explicitly specifies the target type.
    /// If true is returned here for specific types, infinite recursion will occur.
    /// </remarks>
    public override bool CanConvert(Type objectType)
    {
        return objectType == typeof(TemplateCard);
    }

    public override bool CanWrite => false;

    public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
    {
        throw new NotSupportedException("The template card json converter does not support writing.");
    }

    public override object? ReadJson(
        JsonReader reader,
        Type objectType,
        object? existingValue,
        JsonSerializer serializer)
    {
        if (reader.TokenType == JsonToken.Null)
        {
            return null;
        }

        var jsonObject = JObject.Load(reader);

        var cardType = jsonObject["card_type"]?.ToString();
        var concreteType = ResolveCardType(cardType, objectType);

        if (concreteType == null)
        {
            throw new JsonSerializationException(
                $"Unable to resolve a template card type for card_type: '{cardType}'.");
        }

        using var subReader = jsonObject.CreateReader();

        return serializer.Deserialize(subReader, concreteType);
    }

    /// <summary>
    /// Parse the specific type of card
    /// </summary>
    private static Type? ResolveCardType(string? cardType, Type targetType)
    {
        EnsureInitialized();

        if (!cardType.IsNullOrWhiteSpace())
        {
            if (!CardTypes.TryGetValue(cardType!, out var mappedType))
            {
                throw new JsonSerializationException(
                    $"Unknown template card type: '{cardType}'.");
            }

            if (!targetType.IsAssignableFrom(mappedType))
            {
                throw new JsonSerializationException(
                    $"The template card data of card_type: '{cardType}' can not be deserialized as '{targetType.FullName}'.");
            }

            return mappedType;
        }

        // When the discriminator is missing, use it directly if the target is a specific type
        if (!targetType.IsAbstract)
        {
            return targetType;
        }

        // When the abstract target is missing and the discriminator is missing, fall back to the text template card based on historical behavior
        return CardTypes.TryGetValue(nameof(TextTemplateCard), out var fallbackType) ? fallbackType : null;
    }

    private static void EnsureInitialized()
    {
        if (_initialized)
        {
            return;
        }

        lock (SyncLock)
        {
            if (_initialized)
            {
                return;
            }

            foreach (var type in typeof(TemplateCardJsonConverter).Assembly.GetTypes())
            {
                if (type.IsAbstract || !typeof(TemplateCard).IsAssignableFrom(type))
                {
                    continue;
                }

                foreach (var discriminator in GetDiscriminators(type))
                {
                    CardTypes[discriminator] = type;
                }
            }

            _initialized = true;
        }
    }

    /// <summary>
    /// Get the discriminator of the card type
    /// </summary>
    /// <remarks>
    /// The discriminator is passed to the base class by the constructor, so the string constant in the constructor IL is read.
    /// At the same time, the type name is used as the key.
    /// </remarks>
    private static IEnumerable<string> GetDiscriminators(Type cardType)
    {
        var discriminators = new List<string>();

        foreach (var constructor in cardType.GetConstructors(BindingFlags.Public | BindingFlags.Instance))
        {
            try
            {
                var body = constructor.GetMethodBody();
                if (body == null)
                {
                    continue;
                }

                var discriminator = ReadFirstStringConstant(body.GetILAsByteArray(), constructor.Module);
                if (!discriminator.IsNullOrWhiteSpace() && !discriminators.Contains(discriminator!))
                {
                    discriminators.Add(discriminator!);
                }
            }
            catch
            {
                // Failure to read IL does not affect the bottom discriminator
            }
        }

        discriminators.Add(cardType.Name);

        return discriminators;
    }

    private static string? ReadFirstStringConstant(byte[]? il, Module module)
    {
        if (il == null)
        {
            return null;
        }

        for (var index = 0; index < il.Length; index++)
        {
            if (il[index] != LdstrOpCode || index + 4 >= il.Length)
            {
                continue;
            }

            var token = BitConverter.ToInt32(il, index + 1);

            try
            {
                return module.ResolveString(token);
            }
            catch
            {
                // Not a string metadata tag, continue scanning
            }
        }

        return null;
    }
}
