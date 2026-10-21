using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.WeChat.Work.Messages.Models;

/// <summary>
/// Template card message System.Text.Json serialization
/// </summary>
/// <remarks>
/// <para>
/// <see cref="TemplateCard"/> is an abstract type, and the default serializer cannot instantiate it.
/// Causes the template card to be serialized once (such as persisting as notification data, or entering the failure retry queue)
/// It can no longer be restored.
/// </para>
/// <para>
/// Here press the <c>card_type</c> discriminator to restore the specific card type. The discriminator is passed to the base class in the constructor
/// (for example <c>base("template_card", taskId)</c>), obtained by reading the string constant in the constructor IL,
/// Therefore, adding a new card type does not require modifying this converter.
/// </para>
/// <para>
/// The JSON integration used by the project is <c>Volo.Abp.Json.SystemTextJson</c>,
/// Therefore, the channel module automatically registers to the global options at startup.
/// </para>
/// </remarks>
public class TemplateCardSystemTextJsonConverter : JsonConverter<TemplateCard>
{
    private const byte LdstrOpCode = 0x72;

    private static readonly Dictionary<string, Type> CardTypes = new(StringComparer.OrdinalIgnoreCase);

    private static readonly object SyncLock = new();

    private static bool _initialized;

    public override TemplateCard? Read(
        ref Utf8JsonReader reader,
        Type typeToConvert,
        JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.Null)
        {
            return null;
        }

        using var jsonDocument = JsonDocument.ParseValue(ref reader);
        var jsonElement = jsonDocument.RootElement;

        if (jsonElement.ValueKind != JsonValueKind.Object)
        {
            throw new JsonException(
                $"Unable to resolve a template card from a JSON token of kind '{jsonElement.ValueKind}'.");
        }

        var cardType = jsonElement.TryGetProperty("card_type", out var cardTypeElement)
            ? cardTypeElement.GetString()
            : null;

        var concreteType = ResolveCardType(cardType, typeToConvert);
        if (concreteType == null)
        {
            throw new JsonException($"Unable to resolve a template card type for card_type: '{cardType}'.");
        }

        // Specify a specific type for deserialization to avoid this converter (otherwise it will be recursive)
        return (TemplateCard?)JsonSerializer.Deserialize(jsonElement.GetRawText(), concreteType, options);
    }

    public override void Write(
        Utf8JsonWriter writer,
        TemplateCard value,
        JsonSerializerOptions options)
    {
        // Write according to runtime type to avoid outputting only base class members
        JsonSerializer.Serialize(writer, value, value.GetType(), options);
    }

    /// <summary>
    /// Press <c>card_type</c> to get the specific card type
    /// </summary>
    public static Type? GetCardType(string cardType)
    {
        EnsureInitialized();

        return CardTypes.TryGetValue(cardType, out var type) ? type : null;
    }

    private static Type? ResolveCardType(string? cardType, Type targetType)
    {
        EnsureInitialized();

        if (!cardType.IsNullOrWhiteSpace())
        {
            if (!CardTypes.TryGetValue(cardType!, out var mappedType))
            {
                throw new JsonException($"Unknown template card type: '{cardType}'.");
            }

            if (!targetType.IsAssignableFrom(mappedType))
            {
                throw new JsonException(
                    $"The template card data of card_type: '{cardType}' can not be deserialized as '{targetType.FullName}'.");
            }

            return mappedType;
        }

        // When the discriminator is missing: if the target is a specific type, use it directly
        if (!targetType.IsAbstract)
        {
            return targetType;
        }

        // Abstract target and missing discriminator: fall back to text template card
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

            foreach (var type in typeof(TemplateCardSystemTextJsonConverter).Assembly.GetTypes())
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
