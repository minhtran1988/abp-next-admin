namespace LINGYUN.Abp.OpenObserve.Models.Ingestion;

/// <summary>
/// OpenObserve data ingestion exception
/// </summary>
public class OpenObserveIngestionException : OpenObserveException
{
    /// <summary>
    /// Data stream name
    /// </summary>
    public string Stream { get; }

    public OpenObserveIngestionException(
        string stream,
        OpenObserveErrorResponse error,
        int statusCode = 0,
        string? responseBody = null)
        : base(error.Message, error, statusCode, responseBody)
    {
        Stream = stream;
    }
}
