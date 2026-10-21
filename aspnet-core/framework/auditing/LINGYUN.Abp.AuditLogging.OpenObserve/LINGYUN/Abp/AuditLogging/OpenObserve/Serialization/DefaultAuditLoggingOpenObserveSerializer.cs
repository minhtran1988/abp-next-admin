using System;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Json;

namespace LINGYUN.Abp.AuditLogging.OpenObserve.Serialization;

/// <summary>
/// Default serializer
/// </summary>
/// <remarks>
/// Directly use ABP's <see cref="IJsonSerializer"/> to reuse the global
/// <c>AbpSystemTextJsonSerializerModifiersOptions</c> Modifier configuration
/// </remarks>
public class DefaultAuditLoggingOpenObserveSerializer : IAuditLoggingOpenObserveSerializer, ITransientDependency
{
    protected IJsonSerializer JsonSerializer { get; }

    public DefaultAuditLoggingOpenObserveSerializer(IJsonSerializer jsonSerializer)
    {
        JsonSerializer = jsonSerializer;
    }

    public virtual string Serialize<T>(T value)
    {
        return JsonSerializer.Serialize(value!);
    }

    public virtual T? Deserialize<T>(string jsonString)
    {
        if (jsonString.IsNullOrWhiteSpace())
        {
            return default;
        }

        try
        {
            return JsonSerializer.Deserialize<T>(jsonString);
        }
        catch (Exception)
        {
            // Return to default value when historical data or unexpected structure
            return default;
        }
    }
}
