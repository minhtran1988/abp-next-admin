namespace LINGYUN.Abp.AuditLogging.OpenObserve;

public static class AuditLoggingOpenObserveErrorCodes
{
    public const string Namespace = "AuditLoggingOpenObserve";

    /// <summary>
    /// The current log facility does not support deleting records!
    /// </summary>
    public const string DeleteNotSupported = Namespace + ":010001";
}
