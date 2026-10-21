using System;

namespace LINGYUN.Platform;

/// <summary>
/// Platform type
/// </summary>
[Flags]
public enum PlatformType
{
    /// <summary>
    /// undefined
    /// </summary>
    None = 0,
    /// <summary>
    /// Windows CE
    /// </summary>
    WinCe = 2,
    /// <summary>
    /// Windows NT
    /// </summary>
    WinForm = 4,
    /// <summary>
    /// Windows desktop common
    /// </summary>
    Desktop = WinCe | WinForm,
    /// <summary>
    /// WebForm
    /// </summary>
    WebForm = 8,
    /// <summary>
    /// MVC
    /// </summary>
    WebMvc = 16,
    /// <summary>
    /// Other Mvvm architecture
    /// </summary>
    WebMvvm = 32,
    /// <summary>
    /// Web universal
    /// </summary>
    Web = WebForm | WebMvc | WebMvvm,
    /// <summary>
    /// Android
    /// </summary>
    Android = 64,
    /// <summary>
    /// IOS
    /// </summary>
    iOS = 128,
    /// <summary>
    /// Universal for mobile terminals
    /// </summary>
    Mobile = Android | iOS,
    /// <summary>
    /// Mini program
    /// </summary>
    MiniProgram = 256,
    /// <summary>
    /// Common to all platforms
    /// </summary>
    All = Desktop | Web | Mobile | MiniProgram
}
