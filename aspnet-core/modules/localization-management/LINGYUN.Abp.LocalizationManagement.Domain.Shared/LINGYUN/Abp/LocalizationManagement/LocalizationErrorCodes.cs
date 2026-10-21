namespace LINGYUN.Abp.LocalizationManagement;
public static class LocalizationErrorCodes
{
    public const string Namespace = "Localization";

    /// <summary>
    /// Language code 002
    /// </summary>
    public static class Language
    {
        public const string Prefix = Namespace + ":001";
        /// <summary>
        /// Language {CultureName} already exists
        /// </summary>
        public const string NameAlreadyExists = Prefix + "100";
        /// <summary>
        /// The language name named {CultureName} was not found or the built-in language does not allow the operation!
        /// </summary>
        public const string NameNotFoundOrStaticNotAllowed = Prefix + "400";
    }

    /// <summary>
    /// Resource code 002
    /// </summary>
    public static class Resource
    {
        public const string Prefix = Namespace + ":002";
        /// <summary>
        /// Resource {Name} already exists
        /// </summary>
        public const string NameAlreadyExists = Prefix + "100";
        /// <summary>
        /// The resource name named {Name} was not found or the built-in resource does not allow operation!
        /// </summary>
        public const string NameNotFoundOrStaticNotAllowed = Prefix + "400";
    }
}
