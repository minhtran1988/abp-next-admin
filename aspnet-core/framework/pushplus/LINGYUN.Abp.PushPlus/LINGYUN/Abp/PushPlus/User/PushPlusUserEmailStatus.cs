namespace LINGYUN.Abp.PushPlus.User;

public enum PushPlusUserEmailStatus
{
    /// <summary>
    /// Not verified
    /// </summary>
    None = 0,
    /// <summary>
    /// To be verified
    /// </summary>
    UnConfirmed = 1,
    /// <summary>
    /// Verified
    /// </summary>
    Confirmed = 2,
}
