namespace LINGYUN.Platform.Menus;

public static class MenuConsts
{
    public static int MaxComponentLength
    {
        get;
        set;
    } = 255;

    /// <summary>
    /// Maximum depth
    /// </summary>
    /// <remarks>
    /// The default is 4, only four-level submenus are supported.
    /// </remarks>
    public const int MaxDepth = 4;

    public const int MaxCodeLength = MaxDepth * (PlatformConsts.CodeUnitLength + 1) - 1;
}
