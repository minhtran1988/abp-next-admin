namespace LINGYUN.Platform.Routes;

public class RouteConsts
{
    public static int MaxNameLength
    {
        get;
        set;
    } = 64;

    public static int MaxFullNameLength
    {
        get;
        set;
    } = 128;

    public static int MaxDisplayNameLength
    {
        get;
        set;
    } = 128;

    public static int MaxDescriptionLength
    {
        get;
        set;
    } = 255;

    public static int MaxIconLength
    {
        get;
        set;
    } = 128;

    public static int MaxPathLength
    {
        get;
        set;
    } = 255;

    public static int MaxRedirectLength
    {
        get;
        set;
    } = 255;
    /// <summary>
    /// Level depth
    /// </summary>
    public const int MaxDepth = 16;
    /// <summary>
    /// Encoding length
    /// </summary>
    public const int CodeUnitLength = 5;
    /// <summary>
    /// Maximum number length
    /// </summary>
    public const int MaxCodeLength = MaxDepth * (CodeUnitLength + 1) - 1;
}
