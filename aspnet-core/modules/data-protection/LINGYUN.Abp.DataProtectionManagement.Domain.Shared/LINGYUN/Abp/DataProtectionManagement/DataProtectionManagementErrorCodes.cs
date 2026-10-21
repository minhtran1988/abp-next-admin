namespace LINGYUN.Abp.DataProtectionManagement;

public static class DataProtectionManagementErrorCodes
{
    public const string Namespace = "DataProtectionManagement";

    public static class EntityTypeInfo
    {
        public const string Prefix = Namespace + ":001";
        /// <summary>
        /// An entity type definition named {Name} already exists
        /// </summary>
        public const string DuplicateTypeInfo = Prefix + "100";
        /// <summary>
        /// The entity type already has a property definition named {Name}
        /// </summary>
        public const string DuplicateProperty = Prefix + "200";
        /// <summary>
        /// The entity property already has an enumeration definition named {Name}
        /// </summary>
        public const string DuplicateEnum = Prefix + "300";
    }

    public static class RoleEntityRule
    {
        public const string Prefix = Namespace + ":002";
        /// <summary>
        /// Role {RoleName} has been assigned the {Operation} access rule for entity {Name}!
        /// </summary>
        public const string DuplicateEntityRule = Prefix + "100";
    }

    public static class OrganizationUnitEntityRule
    {
        public const string Prefix = Namespace + ":003";
        /// <summary>
        /// Organization {OrgCode} has been assigned {Operation} access rules for entity {Name}!
        /// </summary>
        public const string DuplicateEntityRule = Prefix + "100";
    }
}
