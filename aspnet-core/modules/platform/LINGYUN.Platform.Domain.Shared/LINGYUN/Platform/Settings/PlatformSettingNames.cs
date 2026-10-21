namespace LINGYUN.Platform.Settings;

public class PlatformSettingNames
{
    public const string GroupName = "AppPlatform";

    public class AppVersion
    {
        public const string Default = GroupName + ".AppVersion";
        /// <summary>
        /// File limit length
        /// </summary>
        public const string VersionFileLimitLength = Default + ".VersionFileLimitLength";
        /// <summary>
        /// Allowed file extension types
        /// </summary>
        public const string AllowVersionFileExtensions = Default + ".AllowVersionFileExtensions";
    }
}
