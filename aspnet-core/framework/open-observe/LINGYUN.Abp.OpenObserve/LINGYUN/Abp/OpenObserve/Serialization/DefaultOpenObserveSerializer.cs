using System;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Json;

namespace LINGYUN.Abp.OpenObserve.Serialization;

/// <summary>
/// Default serializer
/// </summary>
public class DefaultOpenObserveSerializer : IOpenObserveSerializer, ITransientDependency
{
    protected IJsonSerializer JsonSerializer { get; }

    public DefaultOpenObserveSerializer(IJsonSerializer jsonSerializer)
    {
        JsonSerializer = jsonSerializer;
    }

    public virtual string Serialize<T>(T value)
    {
        return JsonSerializer.Serialize(value!);
    }

    public virtual T? Deserialize<T>(string jsonString)
    {
        return JsonSerializer.Deserialize<T>(jsonString);
    }

    public virtual bool TryDeserialize<T>(string jsonString, out T? value)
    {
        value = default;

        if (jsonString.IsNullOrWhiteSpace())
        {
            return false;
        }

        try
        {
            value = Deserialize<T>(jsonString);

            return value != null;
        }
        catch (Exception)
        {
            // Allow parsing failure scenarios (such as the error response body is not a standard structure)
            return false;
        }
    }
}
