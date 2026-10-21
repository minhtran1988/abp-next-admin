namespace LINGYUN.Abp.Gdpr;

public static class GdprErrorCodes
{
    public const string Namespace = "Gdpr";
    /// <summary>
    /// You have submitted a personal data download request, please try again after {RequestTimeInterval}.
    /// </summary>
    public const string PersonalDataRequestAlreadyDays = Namespace + ":010001";
    /// <summary>
    /// Your personal data is being prepared, please try downloading again after {ReadyTime}.
    /// </summary>
    public const string DataNotPreparedYet = Namespace + ":010002";
}
