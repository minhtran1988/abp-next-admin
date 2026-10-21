namespace LINGYUN.Platform;

public static class PlatformConsts
{
    /// <summary>
    /// If the number is insufficient, add characters. For example, the number is 3, the length is 5 digits, and the complement character is 0, then the number is 00003.
    /// </summary>
    public static char CodePrefix { get; set; } = '0';
    /// <summary>
    /// Number length
    /// </summary>
    public const int CodeUnitLength = 5;
}
