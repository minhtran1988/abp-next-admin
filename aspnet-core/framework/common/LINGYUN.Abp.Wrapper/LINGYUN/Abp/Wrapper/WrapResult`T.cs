using System;

namespace LINGYUN.Abp.Wrapper;

/// <summary>
/// Return value packaging structure
/// </summary>
/// <typeparam name="TResult"></typeparam>
[Serializable]
public class WrapResult<TResult>
{
    /// <summary>
    /// Error code
    /// </summary>
    public string Code { get; set; } = default!;
    /// <summary>
    /// Error message
    /// </summary>
    public string Message { get; set; } = default!;
    /// <summary>
    /// Supplementary message
    /// </summary>
    public string? Details { get; set; }
    /// <summary>
    /// return value
    /// </summary>
    public TResult? Result { get; set; }
    public WrapResult() { }
    public WrapResult(
        string code,
        string message,
        string? details = null)
    {
        Code = code;
        Message = message;
        Details = details;
    }

    public WrapResult(
        string code,
        TResult result,
        string message = "OK")
    {
        Code = code;
        Result = result;
        Message = message;
    }
}
