namespace LINGYUN.Abp.OpenObserve.Serialization;

/// <summary>
/// OpenObserve serializer
/// </summary>
/// <remarks>
/// Responsible for request body serialization and response deserialization, the client does not directly rely on specific serialization implementation.
/// For the default implementation, see <see cref="DefaultOpenObserveSerializer"/>,
/// Implement this interface when you need to customize it (such as replacing it with Newtonsoft or other strategies), and refer to the registration method of <see cref="DefaultOpenObserveSerializer"/> to override the default implementation.
/// </remarks>
public interface IOpenObserveSerializer
{
    /// <summary>
    /// Serialize request body
    /// </summary>
    string Serialize<T>(T value);

    /// <summary>
    /// Deserialize response
    /// </summary>
    /// <param name="jsonString">Response content</param>
    T? Deserialize<T>(string jsonString);

    /// <summary>
    /// Attempt to deserialize the response
    /// </summary>
    /// <remarks>
    /// Used for error responses and other scenarios where parsing failure is allowed. When parsing fails, false is returned without throwing an exception.
    /// </remarks>
    /// <param name="jsonString">Response content</param>
    /// <param name="value">Deserialization result</param>
    /// <returns>Returns true if parsed successfully</returns>
    bool TryDeserialize<T>(string jsonString, out T? value);
}
