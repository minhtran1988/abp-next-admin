namespace LINGYUN.Abp.Demo;
public static class DemoErrorCodes
{
    public const string Namespace = "Demo";

    public static class Author
    {
        public const string Prefix = Namespace + ":00";
        /// <summary>
        /// Author {Name} already exists!
        /// </summary>
        public const string AuthorAlreadyExists = Prefix + "001";
    }
}
