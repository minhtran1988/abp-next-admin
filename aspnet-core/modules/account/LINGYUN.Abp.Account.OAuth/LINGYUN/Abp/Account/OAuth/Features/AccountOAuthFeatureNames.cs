namespace LINGYUN.Abp.Account.OAuth.Features;

public static class AccountOAuthFeatureNames
{
    public const string GroupName = "Abp.Account.OAuth";
    public static class GitHub
    {
        public const string Prefix = GroupName + ".GitHub";
        /// <summary>
        /// Enable Github authentication login
        /// </summary>
        public const string Enable = Prefix + ".Enable";
    }
    public static class QQ
    {
        public const string Prefix = GroupName + ".QQ";
        /// <summary>
        /// Enable QQ authentication login
        /// </summary>
        public const string Enable = Prefix + ".Enable";
    }
    public static class WeChat
    {
        public const string Prefix = GroupName + ".WeChat";
        /// <summary>
        /// Enable WeChat authentication login
        /// </summary>
        public const string Enable = Prefix + ".Enable";
    }
    public static class WeCom
    {
        public const string Prefix = GroupName + ".WeCom";
        /// <summary>
        /// Enable enterprise WeChat authentication login
        /// </summary>
        public const string Enable = Prefix + ".Enable";
    }
    public static class Bilibili
    {
        public const string Prefix = GroupName + ".Bilibili";
        /// <summary>
        /// Enable Bilibili authentication login
        /// </summary>
        public const string Enable = Prefix + ".Enable";
    }
}
