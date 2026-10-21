namespace LINGYUN.Abp.Saas.Features;
public static class SaasFeatureNames
{
    public const string GroupName = "AbpSaas";

    public static class Tenant
    {
        public const string Default = GroupName + ".Tenants";
        /// <summary>
        /// Resource recovery strategy
        /// </summary>
        public const string RecycleStrategy = Default + ".RecycleStrategy";
        /// <summary>
        /// Expired recycling time
        /// </summary>
        /// <remarks>
        /// When the resource is about to expire, the corresponding tenant administrator needs to be notified by email. If the resource is not processed for how long after the expiration, the tenant resource will be recycled.
        /// </remarks>
        public const string ExpiredRecoveryTime = Default + ".ExpiredRecoveryTime";
        /// <summary>
        /// Expiration warning days
        /// </summary>
        /// <remarks>
        /// When the resource expires in how many days, send a reminder to the administrator
        /// </remarks>
        public const string ExpirationReminderDays = Default + ".ExpirationReminderDays";
    }
}
