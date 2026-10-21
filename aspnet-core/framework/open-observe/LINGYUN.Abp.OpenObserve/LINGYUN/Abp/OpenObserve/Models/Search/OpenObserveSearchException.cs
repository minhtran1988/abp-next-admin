namespace LINGYUN.Abp.OpenObserve.Models.Search;

/// <summary>
/// OpenObserve search exception
/// </summary>
public class OpenObserveSearchException : OpenObserveException
{
    public OpenObserveSearchException(
        OpenObserveErrorResponse error,
        int statusCode = 0,
        string? responseBody = null)
        : base(error.Message, error, statusCode, responseBody)
    {
    }
}
