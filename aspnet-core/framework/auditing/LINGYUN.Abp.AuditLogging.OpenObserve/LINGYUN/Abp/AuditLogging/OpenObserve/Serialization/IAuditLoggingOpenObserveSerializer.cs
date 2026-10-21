namespace LINGYUN.Abp.AuditLogging.OpenObserve.Serialization;

/// <summary>
/// Audit log OpenObserve serializer
/// </summary>
/// <remarks>
/// Used for members that need to be saved as JSON string columns (such as attribute details of entity changes <c>propertychanges</c>),
/// The default implementation is delegated to ABP's <c>IJsonSerializer</c> for reuse
/// <c>AbpSystemTextJsonSerializerModifiersOptions</c> registered modifiers
/// </remarks>
public interface IAuditLoggingOpenObserveSerializer
{
    string Serialize<T>(T value);

    T? Deserialize<T>(string jsonString);
}
