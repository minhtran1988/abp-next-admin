using System;

namespace LINGYUN.Abp.Idempotent;

[AttributeUsage(AttributeTargets.Method)]
public class IdempotentAttribute : Attribute
{
    /// <summary>
    /// Timeout waiting time
    /// </summary>
    public int? Timeout { get; }
    /// <summary>
    /// List of parameter names used to create unique identifiers
    /// </summary>
    /// <remarks>
    /// Serialize unique md5 values by looking up the value definition in the parameter list
    /// </remarks>
    public string[]? KeyMap { get; }
    /// <summary>
    /// Resource redirection path template
    /// </summary>
    /// <remarks>
    /// Define a formatable resource path. When a request conflicts, if the parameter name matches successfully, the target path will be redirected.
    /// <br />
    /// Example: /api/app/demo/{id}
    /// <br />
    /// Example: /api/app/demo/method?id={id}
    /// </remarks>
    public string? RedirectUrl { get; }
    /// <summary>
    /// Custom idempotent key
    /// </summary>
    public string? IdempotentKey { get; set; }
    public IdempotentAttribute()
    {

    }

    public IdempotentAttribute(
        string? iodempotentKey = null,
        int? timeout = null, 
        string? redirectUrl = null,
        string[]? keyMap = null)
    {
        IdempotentKey = iodempotentKey;
        Timeout = timeout;
        KeyMap = keyMap;
        RedirectUrl = redirectUrl;
    }
}
