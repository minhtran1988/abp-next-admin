using Volo.Abp.Data;
using Volo.Abp.MultiTenancy;

namespace LINGYUN.Abp.UI.Navigation.VueVbenAdmin5;

public class AbpUINavigationVueVbenAdmin5NavigationDefinitionProvider : NavigationDefinitionProvider
{
    public override void Define(INavigationDefinitionContext context)
    {
        context.Add(GetDashboard());
        context.Add(GetAccount());
        context.Add(GetManage());
        context.Add(GetAIManagement());
        context.Add(GetSaas());
        context.Add(GetPlatform());
        context.Add(GetBlobManagement());
        context.Add(GetTaskManagement());
        context.Add(GetWebhooksManagement());
        context.Add(GetTextTemplating());
        context.Add(GetWeChatManagement());
        context.Add(GetVbenDemos());
    }

    private static NavigationDefinition[] GetVbenDemos()
    {
        var project = new ApplicationMenu(
            name: "VbenProject",
            displayName: "project",
            url: "/vben-admin",
            component: "",
            description: "project",
            order: 9998,
            icon: "https://unpkg.com/@vbenjs/static-source@0.1.7/source/logo-v1.webp")
            .SetProperty("badgeType", "dot")
            .SetProperty("title", "demos.vben.title");
        project.AddItem(
            new ApplicationMenu(
                name: "VbenDocument",
                displayName: "Document",
                url: "/vben-admin/document",
                component: "",
                icon: "lucide:book-open-text",
                description: "Documentation")
            .SetProperty("link", "https://doc.vben.pro")
            .SetProperty("title", "demos.vben.document")
         );
        project.AddItem(
            new ApplicationMenu(
                name: "VbenGithub",
                displayName: "Github",
                url: "/vben-admin/github",
                component: "",
                icon: "mdi:github",
                description: "Github")
            .SetProperty("link", "https://github.com/vbenjs/vue-vben-admin")
            .SetProperty("title", "Github")
         );
        project.AddItem(
            new ApplicationMenu(
                name: "VbenNaive",
                displayName: "Naive UI version",
                url: "/vben-admin/naive",
                component: "",
                icon: "logos:naiveui",
                description: "Naive UI version")
            .SetProperty("badgeType", "dot")
            .SetProperty("link", "https://naive.vben.pro")
            .SetProperty("title", "demos.vben.naive-ui")
         );
        project.AddItem(
            new ApplicationMenu(
                name: "VbenElementPlus",
                displayName: "Element Plus version",
                url: "/vben-admin/ele",
                component: "",
                icon: "logos:element",
                description: "Element Plus version")
            .SetProperty("badgeType", "dot")
            .SetProperty("link", "https://ele.vben.pro")
            .SetProperty("title", "demos.vben.element-plus")
         );

        var about = new ApplicationMenu(
            name: "VbenAbout",
            displayName: "About",
            url: "/vben-admin/about",
            component: "/_core/about/index",
            description: "About",
            order: 9999,
            icon: "lucide:copyright")
            .SetProperty("title", "demos.vben.about");

        return new NavigationDefinition[2]
        {
            new NavigationDefinition(project),
            new NavigationDefinition(about),
        };
    }

    private static NavigationDefinition GetAccount()
    {
        var account = new ApplicationMenu(
            name: "Vben5Account",
            displayName: "Account Management",
            url: "/account",
            component: "",
            description: "Account Management",
            icon: "mdi:account-outline")
            .WithRoles(["Users"])
            .SetProperty("hideInMenu", "true")
            .SetProperty("title", "abp.account.title");

        account.AddItem(
            new ApplicationMenu(
                name: "Vben5AccountMySettings",
                displayName: "Personal Settings",
                url: "/account/my-settings",
                component: "/account/my-settings/index",
                icon: "tdesign:user-setting",
                description: "Personal Settings")
            .WithRoles(["Users"])
            .SetProperty("hideInMenu", "true")
            .SetProperty("title", "abp.account.settings.title")
         );

        return new NavigationDefinition(account);
    }

    private static NavigationDefinition GetDashboard()
    {
        var dashboard = new ApplicationMenu(
            name: "Vben5Dashboard",
            displayName: "Dashboard",
            url: "/dashboard",
            component: "",
            description: "Dashboard",
            icon: "lucide:layout-dashboard",
            order: -1)
            .WithRoles(["Users"])
            .SetProperty("title", "page.dashboard.title");

        dashboard.AddItem(
            new ApplicationMenu(
                name: "Vben5Analysis",
                displayName: "Analysis Page",
                url: "/analytics",
                component: "/dashboard/analytics/index",
                icon: "lucide:area-chart",
                description: "Analysis page")
            .SetProperty("affixTab", "true")
            .SetProperty("title", "page.dashboard.analytics")
         );

        dashboard.AddItem(
           new ApplicationMenu(
               name: "Vben5Workbench",
               displayName: "Workbench",
               url: "/workspace",
               component: "/dashboard/workspace/index",
               icon: "carbon:workspace",
               description: "Workbench")
           .WithRoles(["Users"])
           .SetProperty("title", "page.dashboard.workspace")
        );

        return new NavigationDefinition(dashboard);
    }

    private static NavigationDefinition GetManage()
    {
        var manage = new ApplicationMenu(
            name: "Vben5Manage",
            displayName: "Management",
            url: "/manage",
            component: "",
            description: "Management",
            icon: "arcticons:activity-manager")
            .SetProperty("title", "abp.manage.title");

        var openIddict = manage.AddItem(
                new ApplicationMenu(
                    name: "Vben5OpenIddict",
                    displayName: "Identity Authentication Server",
                    url: "/manage/openiddict",
                    component: "",
                    icon: "mdi:openid",
                    description: "Identity Authentication Server (OpenIddict)",
                    multiTenancySides: MultiTenancySides.Host)
            .SetProperty("title", "abp.openiddict.title"));
        openIddict.AddItem(
            new ApplicationMenu(
                name: "Vben5OpenIddictApplications",
                displayName: "Application Management",
                url: "/manage/openiddict/applications",
                component: "/openiddict/applications/index",
                icon: "carbon:application",
                description: "Application Management",
                multiTenancySides: MultiTenancySides.Host)
            .SetProperty("title", "abp.openiddict.applications"));
        openIddict.AddItem(
            new ApplicationMenu(
                name: "Vben5OpenIddictAuthorizations",
                displayName: "Authorization Management",
                url: "/manage/openiddict/authorizations",
                component: "/openiddict/authorizations/index",
                icon: "arcticons:ente-authenticator",
                description: "Authorization Management",
                multiTenancySides: MultiTenancySides.Host)
            .SetProperty("title", "abp.openiddict.authorizations"));
        openIddict.AddItem(
            new ApplicationMenu(
                name: "Vben5OpenIddictScopes",
                displayName: "Scope Management",
                url: "/manage/openiddict/scopes",
                component: "/openiddict/scopes/index",
                icon: "et:scope",
                description: "Scope Management",
                multiTenancySides: MultiTenancySides.Host)
            .SetProperty("title", "abp.openiddict.scopes"));
        openIddict.AddItem(
            new ApplicationMenu(
                name: "Vben5OpenIddictTokens",
                displayName: "Authorization Token",
                url: "/manage/openiddict/tokens",
                component: "/openiddict/tokens/index",
                icon: "oui:token-key",
                description: "Authorization Token",
                multiTenancySides: MultiTenancySides.Host)
            .SetProperty("title", "abp.openiddict.tokens"));

        var identity = manage.AddItem(
            new ApplicationMenu(
                name: "Vben5Identity",
                displayName: "Identity Authentication Management",
                url: "/manage/identity",
                component: "",
                icon: "teenyicons:id-outline",
                description: "Identity Authentication Management")
            .SetProperty("title", "abp.manage.identity.title"));
        identity.AddItem(
          new ApplicationMenu(
              name: "Vben5IdentityUsers",
              displayName: "User Management",
              url: "/manage/identity/users",
              component: "/identity/users/index",
              icon: "mdi:user-outline",
              description: "User Management")
          .SetProperty("title", "abp.manage.identity.user"));
        identity.AddItem(
          new ApplicationMenu(
              name: "Vben5IdentityRoles",
              displayName: "Role Management",
              url: "/manage/identity/roles",
              component: "/identity/roles/index",
              icon: "carbon:user-role",
              description: "Role Management")
          .SetProperty("title", "abp.manage.identity.role"));
        identity.AddItem(
          new ApplicationMenu(
              name: "Vben5IdentityClaimTypes",
              displayName: "Identity ID",
              url: "/manage/identity/claim-types",
              component: "/identity/claim-types/index",
              icon: "la:id-card-solid",
              description: "Identification",
              multiTenancySides: MultiTenancySides.Host)
          .SetProperty("title", "abp.manage.identity.claimTypes"));
        identity.AddItem(
          new ApplicationMenu(
              name: "Vben5IdentityOrganizationUnits",
              displayName: "Organization",
              url: "/manage/identity/organization-units",
              component: "/identity/organization-units/index",
              icon: "clarity:organization-line",
              description: "Organization")
          .SetProperty("title", "abp.manage.identity.organizationUnits"));
        identity.AddItem(
          new ApplicationMenu(
              name: "SecurityLogs",
              displayName: "Security Log",
              url: "/manage/identity/security-logs",
              component: "/identity/security-logs/index",
              icon: "carbon:security",
              description: "Security Log")
          .SetProperty("title", "abp.manage.identity.securityLogs")
          .SetProperty("requiredFeatures", "AbpAuditing.Logging.SecurityLog"));
        identity.AddItem(
          new ApplicationMenu(
              name: "Vben5IdentitySessions",
              displayName: "Session Management",
              url: "/manage/identity/sessions",
              component: "/identity/sessions/index",
              icon: "carbon:prompt-session",
              description: "Session Management")
          .SetProperty("title", "abp.manage.identity.sessions"));

        var permissionManagement = manage.AddItem(new ApplicationMenu(
              name: "Vben5Permissions",
              displayName: "Permission Management",
              url: "/manage/permissions",
              component: "",
              description: "Permission Management",
              icon: "arcticons:permissionsmanager",
              multiTenancySides: MultiTenancySides.Host)
            .SetProperty("title", "abp.manage.permissions.title"));
        permissionManagement.AddItem(new ApplicationMenu(
               name: "Vben5PermissionsGroupDefinitions",
               displayName: "Permission Group",
               url: "/manage/permissions/groups",
               component: "/permissions/groups/index",
               icon: "lucide:group",
               description: "Permission Group",
               multiTenancySides: MultiTenancySides.Host)
            .SetProperty("title", "abp.manage.permissions.groups"));
        permissionManagement.AddItem(new ApplicationMenu(
               name: "Vben5PermissionsDefinitions",
               displayName: "Permission Definition",
               url: "/manage/permissions/definitions",
               component: "/permissions/definitions/index",
               icon: "icon-park-outline:permissions",
               description: "Permission definition",
               multiTenancySides: MultiTenancySides.Host)
            .SetProperty("title", "abp.manage.permissions.definitions"));


        var featureManagement = manage.AddItem(new ApplicationMenu(
               name: "Vben5Features",
               displayName: "Function Management",
               url: "/manage/features",
               component: "",
               description: "Function Management",
               icon: "ant-design:gold-outlined",
               multiTenancySides: MultiTenancySides.Host)
            .SetProperty("title", "abp.manage.features.title"));
        featureManagement.AddItem(new ApplicationMenu(
               name: "Vben5FeaturesGroupDefinitions",
               displayName: "Function Grouping",
               url: "/manage/features/groups",
               component: "/features/groups/index",
               icon: "lucide:group",
               description: "Functional Grouping",
               multiTenancySides: MultiTenancySides.Host)
            .SetProperty("title", "abp.manage.features.groups"));
        featureManagement.AddItem(new ApplicationMenu(
               name: "Vben5FeaturesDefinitions",
               displayName: "Function Definition",
               url: "/manage/features/definitions",
               component: "/features/definitions/index",
               icon: "pajamas:feature-flag",
               description: "Function definition",
               multiTenancySides: MultiTenancySides.Host)
            .SetProperty("title", "abp.manage.features.definitions"));

        var settingManagement = manage.AddItem(new ApplicationMenu(
               name: "Vben5Settings",
               displayName: "Settings Management",
               url: "/manage/settings",
               component: "",
               description: "Settings Management",
               icon: "ic:outline-settings")
            .SetProperty("title", "abp.manage.settings.title")
            // This route needs to rely on the settings management feature
            .SetProperty("requiredFeatures", "SettingManagement.Enable"));
        settingManagement.AddItem(new ApplicationMenu(
               name: "Vben5SettingsSystem",
               displayName: "System Settings",
               url: "/manage/settings/system",
               component: "/settings/system/index",
               icon: "tabler:settings-cog",
               description: "System Settings")
            .SetProperty("title", "abp.manage.settings.system")
            // This route needs to rely on the settings management feature
            .SetProperty("requiredFeatures", "SettingManagement.Enable"));
        settingManagement.AddItem(new ApplicationMenu(
               name: "Vben5SettingsDefinitions",
               displayName: "Setting Definition",
               url: "/manage/settings/definitions",
               component: "/settings/definitions/index",
               icon: "codicon:settings",
               description: "Setting definition",
               multiTenancySides: MultiTenancySides.Host)
            .SetProperty("title", "abp.manage.settings.definitions"));

        var localization = manage.AddItem(new ApplicationMenu(
            name: "Vben5Localizations",
            displayName: "Localization Management",
            url: "/manage/localization",
            component: "",
            description: "Localization Management",
            icon: "ion:globe-outline",
            multiTenancySides: MultiTenancySides.Host)
            .SetProperty("title", "abp.manage.localization.title"));
        localization.AddItem(
          new ApplicationMenu(
              name: "Vben5LocalizationsLanguages",
              displayName: "Language Management",
              url: "/manage/localization/languages",
              component: "/localization/languages/index",
              icon: "cil:language",
              description: "Language Management",
              multiTenancySides: MultiTenancySides.Host)
            .SetProperty("title", "abp.manage.localization.languages")
            );
        localization.AddItem(
          new ApplicationMenu(
              name: "Vben5LocalizationsResources",
              displayName: "Resource Management",
              url: "/manage/localization/resources",
              component: "/localization/resources/index",
              icon: "grommet-icons:resources",
              description: "Resource Management",
              multiTenancySides: MultiTenancySides.Host)
            .SetProperty("title", "abp.manage.localization.resources")
            );
        localization.AddItem(
          new ApplicationMenu(
              name: "Vben5LocalizationsTexts",
              displayName: "Document Management",
              url: "/manage/localization/texts",
              component: "/localization/texts/index",
              icon: "mi:text",
              description: "Document Management",
              multiTenancySides: MultiTenancySides.Host)
            .SetProperty("title", "abp.manage.localization.texts")
            );

        var dataProtection = manage.AddItem(new ApplicationMenu(
              name: "Vben5DataProtection",
              displayName: "Data Protection",
              url: "/manage/data-protection",
              component: "",
              description: "Data Protection",
              icon: "icon-park-outline:protect",
              multiTenancySides: MultiTenancySides.Host)
            .SetProperty("title", "abp.manage.dataProtection.title"));
        dataProtection.AddItem(new ApplicationMenu(
               name: "Vben5DataProtectionEntityTypeInfos",
               displayName: "Entity Management",
               url: "/manage/data-protection/entity-type-infos",
               component: "/data-protection/entity-type-infos/index",
               icon: "iconamoon:type",
               description: "Entity Management",
               multiTenancySides: MultiTenancySides.Host)
            .SetProperty("title", "abp.manage.dataProtection.entityTypeInfos"));

        manage.AddItem(new ApplicationMenu(
               name: "Vben5AuditingAuditLogs",
               displayName: "Audit Log",
               url: "/manage/audit-logs",
               component: "/auditing/audit-logs/index",
               icon: "fluent-mdl2:compliance-audit",
               description: "Audit Log")
            .SetProperty("title", "abp.manage.auditLogs")
            // This route needs to rely on the audit log feature
            .SetProperty("requiredFeatures", "AbpAuditing.Logging.AuditLog"));

        manage.AddItem(
            new ApplicationMenu(
                name: "Vben5AuditingLoggings",
                displayName: "System Log",
                url: "/manage/sys-logs",
                component: "/auditing/loggings/index",
                icon: "icon-park-outline:log",
                description: "System Log",
                multiTenancySides: MultiTenancySides.Host)
            .SetProperty("title", "abp.manage.loggings"));

        var notificationManagement = manage.AddItem(new ApplicationMenu(
              name: "Vben5Notifications",
              displayName: "Notification Management",
              url: "/manage/notifications",
              component: "",
              description: "Notification Management",
              icon: "tabler:notification")
            .SetProperty("title", "abp.manage.notifications.title"));
        notificationManagement.AddItem(new ApplicationMenu(
               name: "Vben5NotificationsMyNotifilers",
               displayName: "My notifications",
               url: "/manage/notifications/my-notifilers",
               component: "/notifications/my-notifilers/index",
               icon: "ant-design:notification-outlined",
               description: "My notifications")
            .SetProperty("title", "abp.manage.notifications.myNotifilers"));
        notificationManagement.AddItem(new ApplicationMenu(
               name: "Vben5NotificationsGroupDefinitions",
               displayName: "Notification Group",
               url: "/manage/notifications/groups",
               component: "/notifications/groups/index",
               icon: "lucide:group",
               description: "Notification Group",
               multiTenancySides: MultiTenancySides.Host)
            .SetProperty("title", "abp.manage.notifications.groups"));
        notificationManagement.AddItem(new ApplicationMenu(
               name: "Vben5NotificationsDefinitions",
               displayName: "Notification Definition",
               url: "/manage/notifications/definitions",
               component: "/notifications/definitions/index",
               icon: "nimbus:notification",
               description: "Notification Definition",
               multiTenancySides: MultiTenancySides.Host)
            .SetProperty("title", "abp.manage.notifications.definitions"));
        notificationManagement.AddItem(new ApplicationMenu(
               name: "Vben5NotificationsSendRecords",
               displayName: "Send Record",
               url: "/manage/notifications/send-records",
               component: "/notifications/send-records/index",
               icon: "material-symbols:history",
               description: "Send record",
               multiTenancySides: MultiTenancySides.Host)
            .SetProperty("title", "abp.manage.notifications.sendRecords"));

        manage.AddItem(
            new ApplicationMenu(
                name: "Vben5ApiDocument",
                displayName: "Api Document",
                url: "/manage/openapi",
                component: "IFrame",
                icon: "material-icon-theme:swagger",
                description: "Api Documentation",
                multiTenancySides: MultiTenancySides.Host)
            .SetProperty("title", "abp.manage.openApi")
            // TODO: Note that you need to manually modify the iframe address of this menu after deployment is complete.
            .SetProperty("iframeSrc", "http://127.0.0.1:30000/swagger/index.html"));

        manage.AddItem(
            new ApplicationMenu(
                name: "Vben5Caches",
                displayName: "Cache Management",
                url: "/manage/cache",
                component: "/caching/caches/index",
                icon: "devicon:redis",
                description: "Cache Management")
            .SetProperty("title", "abp.manage.cache"));

        return new NavigationDefinition(manage);
    }

    private static NavigationDefinition GetAIManagement()
    {
        var aiManagement = new ApplicationMenu(
            name: "Vben5ArtificialIntelligence",
            displayName: "Artificial Intelligence",
            url: "/artificia-intelligence",
            component: "",
            description: "artificial intelligence",
            icon: "hugeicons:artificial-intelligence-04")
            .SetProperty("title", "abp.ai.title");
        aiManagement.AddItem(
          new ApplicationMenu(
              name: "Vben5AIToolDefinitions",
              displayName: "Tool Management",
              url: "/artificia-intelligence/tools",
              component: "/ai-management/tools/index",
              icon: "ant-design:tool-outlined",
              description: "Tool Management")
            .SetProperty("title", "abp.ai.tools"));
        aiManagement.AddItem(
          new ApplicationMenu(
              name: "Vben5AIWorkspaceDefinitions",
              displayName: "Workspace Management",
              url: "/artificia-intelligence/workspace",
              component: "/ai-management/workspaces/index",
              icon: "carbon:workspace",
              description: "Workspace Management")
            .SetProperty("title", "abp.ai.workspaces"));
        aiManagement.AddItem(
          new ApplicationMenu(
              name: "Vben5AIConversations",
              displayName: "Session Management",
              url: "/artificia-intelligence/conversations",
              component: "/ai-management/conversations/index",
              icon: "arcticons:conversations",
              description: "Session Management")
            .SetProperty("title", "abp.ai.conversations"));

        return new NavigationDefinition(aiManagement);
    }

    private static NavigationDefinition GetSaas()
    {
        var saas = new ApplicationMenu(
            name: "Vben5Saas",
            displayName: "Saas",
            url: "/saas",
            component: "",
            description: "Saas",
            icon: "ant-design:cloud-server-outlined",
            multiTenancySides: MultiTenancySides.Host)
            .SetProperty("title", "abp.saas.title");
        saas.AddItem(
          new ApplicationMenu(
              name: "Vben5SaasTenants",
              displayName: "Tenant Management",
              url: "/saas/tenants",
              component: "/saas/tenants/index",
              icon: "arcticons:tenantcloud-pro",
              description: "Tenant Management",
              multiTenancySides: MultiTenancySides.Host)
            .SetProperty("title", "abp.saas.tenants"));
        saas.AddItem(
          new ApplicationMenu(
              name: "Vben5SaasEditions",
              displayName: "Version Management",
              url: "/saas/editions",
              component: "/saas/editions/index",
              icon: "icon-park-outline:multi-rectangle",
              description: "Version Management",
              multiTenancySides: MultiTenancySides.Host)
            .SetProperty("title", "abp.saas.editions"));

        return new NavigationDefinition(saas);
    }

    private static NavigationDefinition GetPlatform()
    {
        var platform = new ApplicationMenu(
            name: "Vben5Platform",
            displayName: "Platform Management",
            url: "/platform",
            component: "",
            description: "Platform Management",
            icon: "ep:platform")
            .SetProperty("title", "abp.platform.title");
        platform.AddItem(
          new ApplicationMenu(
              name: "Vben5PlatformDataDictionaries",
              displayName: "data dictionary",
              url: "/platform/data-dictionaries",
              component: "/platform/data-dictionaries/index",
              icon: "material-symbols:dictionary-outline",
              description: "Data Dictionary")
            .SetProperty("title", "abp.platform.dataDictionaries"));
        platform.AddItem(
          new ApplicationMenu(
              name: "Vben5PlatformLayouts",
              displayName: "Layout Management",
              url: "/platform/layouts",
              component: "/platform/layouts/index",
              icon: "material-symbols-light:responsive-layout",
              description: "Layout Management")
            .SetProperty("title", "abp.platform.layouts"));
        platform.AddItem(
          new ApplicationMenu(
              name: "Vben5PlatformMenus",
              displayName: "Menu Management",
              url: "/platform/menus",
              component: "/platform/menus/index",
              icon: "material-symbols-light:menu",
              description: "Menu Management")
            .SetProperty("title", "abp.platform.menus"));

        var messages = platform.AddItem(
          new ApplicationMenu(
              name: "Vben5PlatformMessages",
              displayName: "Message Management",
              url: "/platform/messages",
              component: "",
              icon: "tabler:message-cog",
              description: "Message Management",
              multiTenancySides: MultiTenancySides.Host)
            .SetProperty("title", "abp.platform.messages.title"));
        messages.AddItem(
          new ApplicationMenu(
              name: "Vben5PlatformEmailMessages",
              displayName: "Email message",
              url: "/platform/messages/email",
              component: "/platform/messages/email/index",
              icon: "material-symbols:attach-email-outline",
              description: "Email message",
              multiTenancySides: MultiTenancySides.Host)
            .SetProperty("title", "abp.platform.messages.email"));
        messages.AddItem(
          new ApplicationMenu(
              name: "Vben5PlatformSmsMessages",
              displayName: "SMS message",
              url: "/platform/messages/sms",
              component: "/platform/messages/sms/index",
              icon: "material-symbols:sms-outline",
              description: "SMS message",
              multiTenancySides: MultiTenancySides.Host)
            .SetProperty("title", "abp.platform.messages.sms"));

        return new NavigationDefinition(platform);
    }

    private static NavigationDefinition GetBlobManagement()
    {
        var oss = new ApplicationMenu(
            name: "Vben5BlobManagement",
            displayName: "Object Storage",
            url: "/blob-management",
            component: "",
            description: "Object Storage",
            icon: "icon-park-outline:cloud-storage")
            .SetProperty("title", "abp.oss.title");
        oss.AddItem(
          new ApplicationMenu(
              name: "Vben5BlobContainers",
              displayName: "Container Management",
              url: "/blob-management/blob-containers",
              component: "/blob-management/containers/index",
              icon: "mdi:bucket-outline",
              description: "Container Management")
            .SetProperty("title", "abp.oss.containers"));
        oss.AddItem(
          new ApplicationMenu(
              name: "Vben5BlobFiles",
              displayName: "File Management",
              url: "/blob-management/blobs",
              component: "/blob-management/blobs/index",
              icon: "mdi-light:file",
              description: "File Management")
            .SetProperty("title", "abp.oss.objects"));

        return new NavigationDefinition(oss);
    }

    private static NavigationDefinition GetTaskManagement()
    {
        var task = new ApplicationMenu(
            name: "Vben5Tasks",
            displayName: "Task Management",
            url: "/task-management",
            component: "",
            description: "Task Management",
            icon: "eos-icons:background-tasks")
            .SetProperty("title", "abp.tasks.title");
        task.AddItem(
          new ApplicationMenu(
              name: "Vben5TasksJobInfos",
              displayName: "Task Queue",
              url: "/task-management/background-jobs",
              component: "/tasks/job-infos/index",
              icon: "eos-icons:job",
              description: "Task Queue")
            .SetProperty("title", "abp.tasks.jobInfo.title"));

        return new NavigationDefinition(task);
    }

    private static NavigationDefinition GetWebhooksManagement()
    {
        var webhooks = new ApplicationMenu(
            name: "Vben5Webhooks",
            displayName: "WebHooks",
            url: "/webhooks",
            component: "",
            description: "WebHooks",
            icon: "material-symbols:webhook",
            multiTenancySides: MultiTenancySides.Host)
            .SetProperty("title", "abp.webhooks.title");
        webhooks.AddItem(
          new ApplicationMenu(
              name: "Vben5WebhooksGroupDefinitions",
              displayName: "Webhook Group",
              url: "/webhooks/groups",
              component: "/webhooks/groups/index",
              icon: "lucide:group",
              description: "Webhook Group",
              multiTenancySides: MultiTenancySides.Host)
            .SetProperty("title", "abp.webhooks.groups"));
        webhooks.AddItem(
          new ApplicationMenu(
              name: "Vben5WebhooksDefinitions",
              displayName: "Webhook definition",
              url: "/webhooks/definitions",
              component: "/webhooks/definitions/index",
              icon: "material-symbols:webhook",
              description: "Webhook definition",
              multiTenancySides: MultiTenancySides.Host)
            .SetProperty("title", "abp.webhooks.definitions"));
        webhooks.AddItem(
          new ApplicationMenu(
              name: "Vben5WebhooksSubscriptions",
              displayName: "Manage Subscription",
              url: "/webhooks/subscriptions",
              component: "/webhooks/subscriptions/index",
              icon: "material-symbols:subscriptions",
              description: "Manage Subscriptions",
              multiTenancySides: MultiTenancySides.Host)
            .SetProperty("title", "abp.webhooks.subscriptions"));
        webhooks.AddItem(
          new ApplicationMenu(
              name: "Vben5WebhooksSendAttempts",
              displayName: "Management Records",
              url: "/webhooks/send-attempts",
              component: "/webhooks/send-attempts/index",
              icon: "material-symbols:history",
              description: "Management Records",
              multiTenancySides: MultiTenancySides.Host)
            .SetProperty("title", "abp.webhooks.sendAttempts"));

        return new NavigationDefinition(webhooks);
    }

    private static NavigationDefinition GetTextTemplating()
    {
        var textTemplating = new ApplicationMenu(
            name: "Vben5TextTemplating",
            displayName: "Template Management",
            url: "/text-templating",
            component: "",
            description: "Template Management",
            icon: "tdesign:template",
            multiTenancySides: MultiTenancySides.Host)
            .SetProperty("title", "abp.textTemplating.title");
        textTemplating.AddItem(
          new ApplicationMenu(
              name: "Vben5TextTemplatingDefinitions",
              displayName: "Template Definition",
              url: "/text-templating/definitions",
              component: "/text-templating/definitions/index",
              icon: "qlementine-icons:template-16",
              description: "Template definition",
              multiTenancySides: MultiTenancySides.Host)
            .SetProperty("title", "abp.textTemplating.definitions"));

        return new NavigationDefinition(textTemplating);
    }

    private static NavigationDefinition GetWeChatManagement()
    {
        var wechat = new ApplicationMenu(
            name: "Vben5Wechat",
            displayName: "WeChat integration",
            url: "/wechat",
            component: "",
            description: "WeChat integration",
            icon: "ant-design:wechat-outlined")
            .SetProperty("title", "abp.wechat.title");
        wechat.AddItem(
          new ApplicationMenu(
              name: "Vben5WechatSettings",
              displayName: "WeChat Settings",
              url: "/wechat/settings",
              component: "/wechat/settings/index",
              icon: "ant-design:setting-outlined",
              description: "WeChat Settings")
            .SetProperty("title", "abp.wechat.settings"));

        return new NavigationDefinition(wechat);
    }
}
