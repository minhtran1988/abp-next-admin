namespace LINGYUN.Abp.Saas;

public static class AbpSaasErrorCodes
{
    public const string Namespace = "Saas";
    /// <summary>
    /// A version named {DisplayName} already exists!
    /// </summary>
    public const string DuplicateEditionDisplayName = Namespace + ":010001";
    /// <summary>
    /// Attempting to delete the version in use: {DisplayName}!
    /// </summary>
    public const string DeleteUsedEdition = Namespace + ":010002";
    /// <summary>
    /// A tenant named {Name} already exists!
    /// </summary>
    public const string DuplicateTenantName = Namespace + ":020001";
    /// <summary>
    /// Tenant: {Tenant} does not exist!
    /// </summary>
    public const string TenantIdOrNameNotFound = Namespace + ":020002";
    /// <summary>
    /// Unable to open the database connection pointed to by the default connection string
    /// </summary>
    public const string InvalidDefaultConnectionString = Namespace + ":020101";
    /// <summary>
    /// The database pointed to by the default connection string already exists
    /// </summary>
    public const string DefaultConnectionStringDatabaseExists = Namespace + ":020102";
    /// <summary>
    /// The connection string for {Name} is invalid
    /// </summary>
    public const string InvalidConnectionString = Namespace + ":020103";
    /// <summary>
    /// Database connection check of type {Name} is not supported
    /// </summary>
    public const string ConnectionStringProviderNotSupport = Namespace + ":020104";
}
