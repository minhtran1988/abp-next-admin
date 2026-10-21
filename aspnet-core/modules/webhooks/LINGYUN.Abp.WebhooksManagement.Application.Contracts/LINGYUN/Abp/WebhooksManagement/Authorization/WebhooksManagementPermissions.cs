namespace LINGYUN.Abp.WebhooksManagement.Authorization;

public static class WebhooksManagementPermissions
{
    public const string GroupName = "AbpWebhooks";

    /// <summary>
    /// Authorization allows publishing Webhooks events, it is recommended that the client authorize
    /// </summary>
    public const string Publish = GroupName + ".Publish";

    public const string ManageSettings = GroupName + ".ManageSettings";

    public static class WebhookGroupDefinition
    {
        public const string Default = GroupName + ".GroupDefinitions";
        public const string Create = Default + ".Create";
        public const string Update = Default + ".Update";
        public const string Delete = Default + ".Delete";
    }

    public static class WebhookDefinition
    {
        public const string Default = GroupName + ".Definitions";
        public const string Create = Default + ".Create";
        public const string Update = Default + ".Update";
        public const string Delete = Default + ".Delete";
    }

    public static class WebhookSubscription
    {
        public const string Default = GroupName + ".Subscriptions";
        public const string Create = Default + ".Create";
        public const string Update = Default + ".Update";
        public const string Delete = Default + ".Delete";
    }

    public static class WebhooksSendAttempts
    {
        public const string Default = GroupName + ".SendAttempts";
        public const string Delete = Default + ".Delete";
        public const string Resend = Default + ".Resend";
    }
}
