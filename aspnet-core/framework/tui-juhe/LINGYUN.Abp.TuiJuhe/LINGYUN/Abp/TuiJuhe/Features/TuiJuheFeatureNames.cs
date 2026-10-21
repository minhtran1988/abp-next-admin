namespace LINGYUN.Abp.TuiJuhe.Features;

public static class TuiJuheFeatureNames
{
    public const string GroupName = "TuiJuhe";

    /// <summary>
    /// Enable TuiJuhe
    /// </summary>
    public const string Enable = GroupName + ".Enable";

    public static class Message
    {
        public const string GroupName = TuiJuheFeatureNames.GroupName + ".Message";
        /// <summary>
        /// Enable message push
        /// </summary>
        public const string Enable = GroupName + ".Enable";
        /// <summary>
        /// Maximum number of sending times
        /// </summary>
        public const string SendLimit = GroupName + ".SendLimit";
        /// <summary>
        /// The upper limit of sending times
        /// </summary>
        public const string SendLimitInterval = GroupName + ".SendLimitInterval";
    }
}
