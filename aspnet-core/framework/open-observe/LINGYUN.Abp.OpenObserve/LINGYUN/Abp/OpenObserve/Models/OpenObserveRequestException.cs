using System.Net.Http;

namespace LINGYUN.Abp.OpenObserve.Models;

/// <summary>
/// OpenObserve request exception
/// </summary>
/// <remarks>
/// Thrown when the response content cannot be parsed as an OpenObserve standard error response
/// </remarks>
public class OpenObserveRequestException : OpenObserveException
{
    public HttpResponseMessage HttpResponseMessage { get; }

    public OpenObserveRequestException(
        HttpResponseMessage responseMessage,
        string? responseBody = null)
        : base(
            $"OpenObserve request failed with status code {(int)responseMessage.StatusCode} ({responseMessage.ReasonPhrase})",
            null,
            (int)responseMessage.StatusCode,
            responseBody)
    {
        HttpResponseMessage = responseMessage;
    }
}
