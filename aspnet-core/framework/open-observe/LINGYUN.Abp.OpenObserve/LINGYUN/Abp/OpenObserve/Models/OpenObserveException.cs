using System;

namespace LINGYUN.Abp.OpenObserve.Models;

/// <summary>
/// OpenObserve business exception base class
/// </summary>
public class OpenObserveException : Exception
{
    /// <summary>
    /// Error code
    /// </summary>
    /// <remarks>
    /// For business errors, take the code in the OpenObserve response, otherwise take the HTTP status code
    /// </remarks>
    public int Code => Error?.Code ?? StatusCode;
    /// <summary>
    /// HTTP status code
    /// </summary>
    public int StatusCode { get; }
    /// <summary>
    /// OpenObserve error response
    /// </summary>
    public OpenObserveErrorResponse? Error { get; }
    /// <summary>
    /// Original response content
    /// </summary>
    public string? ResponseBody { get; }

    public OpenObserveException(
        string message,
        OpenObserveErrorResponse? error = null,
        int statusCode = 0,
        string? responseBody = null,
        Exception? innerException = null)
        : base(message, innerException)
    {
        Error = error;
        StatusCode = statusCode;
        ResponseBody = responseBody;
    }
}
