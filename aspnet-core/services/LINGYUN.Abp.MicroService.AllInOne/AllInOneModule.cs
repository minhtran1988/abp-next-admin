namespace LINGYUN.Abp.MicroService.AllInOne;

[DependsOn(
    // CAP event bus module
    typeof(AbpCAPEventBusModule),
    // Serilog extension module application information
    typeof(AbpSerilogEnrichersApplicationModule),
    // Serilog extension module global unique ID
    typeof(AbpSerilogEnrichersUniqueIdModule),
    // Serilog module
    typeof(AbpAspNetCoreSerilogModule),
    // Identity authentication module extends UserToken verification
    typeof(AbpIdentityAspNetCoreModule),
    // Identity authentication module notification integration
    typeof(AbpIdentityNotificationsModule),
    // Identity authentication module organization integration
    typeof(AbpIdentityOrganizaztionUnitsModule),
    // Identity authentication module WeChat identity
    typeof(AbpIdentityWeChatModule),
    // Identity authentication module Enterprise WeChat identity
    typeof(AbpIdentityWeChatWorkModule),
    // Identity authentication module domain service
    typeof(AbpIdentityDomainModule),
    // Identity authentication module application service
    typeof(AbpIdentityApplicationModule),
    // Identity authentication module controller
    typeof(AbpIdentityHttpApiModule),
    // Identity authentication module Entity Framework
    typeof(AbpIdentityEntityFrameworkCoreModule),
    // Identity authentication module Mvc view
    typeof(AbpIdentityWebModule),

    // Account module application service
    typeof(AbpAccountApplicationModule),
    // Account module controller
    typeof(AbpAccountHttpApiModule),
    // Account module OpenIddict integration
    typeof(AbpAccountWebOpenIddictModule),
    // Account module OAuth integration
    typeof(AbpAccountWebOAuthModule),
    // Use LazyCaptcha verification code component
    typeof(AbpAccountWebLazyCaptchaModule),
    // To use the Tencent Cloud verification code component, Tencent Cloud related parameters need to be configured.
    typeof(AbpAccountWebTencentCaptchaModule),
    // To use the Alibaba Cloud verification code component, you need to configure Alibaba Cloud related parameters
    typeof(AbpAccountWebAliyunCaptchaModule),

    // Gdpr identity authentication provider module
    typeof(AbpGdprDomainIdentityModule),
    // Gdpr application service module
    typeof(AbpGdprApplicationModule),
    // Gdpr controller module
    typeof(AbpGdprHttpApiModule),
    // Gdpr Entity Framework module
    typeof(AbpGdprEntityFrameworkCoreModule),
    // Gdpr Mvc page
    typeof(AbpGdprWebModule),

    // MVC Theme
    typeof(AbpAspNetCoreMvcUiLeptonXLiteThemeModule),

    // Cache module Redis integration
    typeof(AbpCachingStackExchangeRedisModule),
    // Cache management module Redis integration
    typeof(AbpCachingManagementStackExchangeRedisModule),
    // Cache management module application service
    typeof(AbpCachingManagementApplicationModule),
    // Cache management module controller
    typeof(AbpCachingManagementHttpApiModule),

    // Multi-language management module domain service
    typeof(AbpLocalizationManagementDomainModule),
    // Multi-language management module application service
    typeof(AbpLocalizationManagementApplicationModule),
    // Multi-language management module controller
    typeof(AbpLocalizationManagementHttpApiModule),
    // Multi-language management module Entity Framework
    typeof(AbpLocalizationManagementEntityFrameworkCoreModule),


    // Message module domain service
    typeof(AbpMessageServiceDomainModule),
    // Message module application service
    typeof(AbpMessageServiceApplicationModule),
    // Message module controller
    typeof(AbpMessageServiceHttpApiModule),
    // Message module Entity Framework
    typeof(AbpMessageServiceEntityFrameworkCoreModule),
    // Notification module domain service
    typeof(AbpNotificationsDomainModule),
    // Notification module application service
    typeof(AbpNotificationsApplicationModule),
    // Notification module controller
    typeof(AbpNotificationsHttpApiModule),
    // Notification module Entity Framework
    typeof(AbpNotificationsEntityFrameworkCoreModule),

    // OpenIddict extension module application service
    typeof(AbpOpenIddictApplicationModule),
    // OpenIddict extension module controller
    typeof(AbpOpenIddictHttpApiModule),
    // OpenIddict extension module Entity Framework
    typeof(AbpOpenIddictEntityFrameworkCoreModule),
    // OpenIddict extension module SMS authentication
    typeof(AbpOpenIddictSmsModule),
    // OpenIddict extension module platform certification
    typeof(AbpOpenIddictPortalModule),
    // OpenIddict extension module WeChat authentication
    typeof(AbpOpenIddictWeChatModule),
    // OpenIddict extension module Enterprise WeChat authentication
    typeof(AbpOpenIddictWeChatWorkModule),
    // OpenIddict extension module associated account
    typeof(AbpOpenIddictLinkUserModule),
    // OpenIddict extension module scan QR code to log in
    typeof(AbpOpenIddictQrCodeModule),

    // Object storage module Alibaba Cloud
    typeof(AbpBlobManagementAliyunModule),
    // Object storage module file system
    typeof(AbpBlobManagementFileSystemModule),
    // Object storage module Minio
    typeof(AbpBlobManagementMinioModule),
    // Object storage module Tencent Cloud
    typeof(AbpBlobManagementTencentModule),
    // Object storage module MimeCheck extension
    typeof(AbpBlobManagementMimeCheckModule),
    // Object storage module application service
    typeof(AbpBlobManagementDomainModule),
    // Object storage module controller
    typeof(AbpBlobManagementApplicationModule),
    // Object storage module controller
    typeof(AbpBlobManagementHttpApiModule),
    // Object storage module settings management
    typeof(AbpBlobManagementSettingManagementModule),
    // Object storage module universal storage agent
    typeof(AbpBlobStoringBlobManagementModule),
    // Graphics processing module
    typeof(AbpImagingImageSharpModule),

    // Platform module domain service
    typeof(PlatformDomainModule),
    // Platform module application service
    typeof(PlatformApplicationModule),
    // Platform module controller
    typeof(PlatformHttpApiModule),
    // Platform module Entity Framework
    typeof(PlatformEntityFrameworkCoreModule),
    // Platform module VueVbenAdmin settings
    // typeof(PlatformSettingsVueVbenAdminModule),
    // Platform module VueVbenAdmin theme
    // typeof(PlatformThemeVueVbenAdminModule),
    // Platform module Vben2 routing
    // typeof(AbpUINavigationVueVbenAdminModule),
    // Platform module Vben5 routing
    typeof(AbpUINavigationVueVbenAdmin5Module),

    // Saas module domain service
    typeof(AbpSaasDomainModule),
    // Saas module application service
    typeof(AbpSaasApplicationModule),
    // Saas module controller
    typeof(AbpSaasHttpApiModule),
    // Saas module Entity Framework
    typeof(AbpSaasEntityFrameworkCoreModule),
    // Saas module database connection check
    typeof(AbpSaasDbCheckerModule),

    // Task management module domain service
    typeof(TaskManagementDomainModule),
    // Task management module application service
    typeof(TaskManagementApplicationModule),
    // Task management module controller
    typeof(TaskManagementHttpApiModule),
    // Task management module Entity Framework
    typeof(TaskManagementEntityFrameworkCoreModule),

    // Text template module domain service
    typeof(AbpTextTemplatingDomainModule),
    // Text template module application service
    typeof(AbpTextTemplatingApplicationModule),
    // Text template module controller
    typeof(AbpTextTemplatingHttpApiModule),
    // Text template module Entity Framework
    typeof(AbpTextTemplatingEntityFrameworkCoreModule),

    // Webhooks module domain service
    typeof(AbpWebhooksModule),
    // Webhooks module event bus
    typeof(AbpWebhooksEventBusModule),
    // Webhooks module identity authentication event
    typeof(AbpWebhooksIdentityModule),
    // Webhooks module Saas event
    typeof(AbpWebhooksSaasModule),
    // Webhooks module application service
    typeof(WebhooksManagementDomainModule),
    // Webhooks module controller
    typeof(WebhooksManagementApplicationModule),
    // Webhooks module controller
    typeof(WebhooksManagementHttpApiModule),
    // Webhooks module Entity Framework
    typeof(WebhooksManagementEntityFrameworkCoreModule),

    // Function management module application service
    typeof(LINGYUN.Abp.FeatureManagement.AbpFeatureManagementApplicationModule),
    // Function management module controller
    typeof(LINGYUN.Abp.FeatureManagement.HttpApi.AbpFeatureManagementHttpApiModule),
    // Function management module Entity Framework
    typeof(AbpFeatureManagementEntityFrameworkCoreModule),

    // Set management module domain service
    typeof(AbpSettingManagementDomainModule),
    // Set management module application service
    typeof(AbpSettingManagementApplicationModule),
    // Set management module controller
    typeof(AbpSettingManagementHttpApiModule),
    // Set up the management module Entity Framework
    typeof(AbpSettingManagementEntityFrameworkCoreModule),
    // Set the management module Mvc view
    typeof(AbpSettingManagementWebModule),

    // Permission management module application service
    typeof(LINGYUN.Abp.PermissionManagement.AbpPermissionManagementApplicationModule),
    // Permission management module controller
    typeof(AbpPermissionManagementHttpApiModule),
    // Permission management module identity authentication integration
    typeof(AbpPermissionManagementDomainIdentityModule),
    // Permission management module OpenIddict integration
    typeof(AbpPermissionManagementDomainOpenIddictModule),
    // typeof(AbpPermissionManagementDomainIdentityServerModule),
    // Permission management module Entity Framework
    typeof(AbpPermissionManagementEntityFrameworkCoreModule),
    // Permission management module organization integration
    typeof(AbpPermissionManagementDomainOrganizationUnitsModule), // Organization permission management
    // Permission management module Mvc view
    typeof(AbpPermissionManagementWebModule),

    // SMS module Alibaba Cloud integration
    typeof(AbpAliyunSmsModule),
    // Alibaba Cloud module settings management
    typeof(AbpAliyunSettingManagementModule),

    // Authentication module JWT authentication
    typeof(AbpAspNetCoreAuthenticationJwtBearerModule),
    // Authorization module organization integration
    typeof(AbpAuthorizationOrganizationUnitsModule),

    // Background task module
    typeof(AbpBackgroundTasksModule),
    // Background task module behavior processing
    typeof(AbpBackgroundTasksActivitiesModule),
    // Background task module distributed lock
    typeof(AbpBackgroundTasksDistributedLockingModule),
    // Background task module event bus
    typeof(AbpBackgroundTasksEventBusModule),
    // Background task module default job
    typeof(AbpBackgroundTasksJobsModule),
    // Background task module notification
    typeof(AbpBackgroundTasksNotificationsModule),
    // Background task module Quartz integration
    typeof(AbpBackgroundTasksQuartzModule),
    // Background task module identity authentication module job
    typeof(AbpIdentityJobsModule),
    // Background task module platform management module job
    typeof(PlatformJobsModule),

    // Data audit module application service
    typeof(AbpDataProtectionManagementApplicationModule),
    // Data audit module controller
    typeof(AbpDataProtectionManagementHttpApiModule),
    // Data audit module Entity Framework
    typeof(AbpDataProtectionManagementEntityFrameworkCoreModule),

    // Demo module application service
    typeof(AbpDemoApplicationModule),
    // Demo module controller
    typeof(AbpDemoHttpApiModule),
    // Demo module Entity Framework
    typeof(AbpDemoEntityFrameworkCoreModule),

    // Dapr module client
    typeof(AbpDaprClientModule),
    // Exception handling module
    typeof(AbpExceptionHandlingModule),
    // Exception handling module email notification
    typeof(AbpEmailingExceptionHandlingModule),

    // Function restriction module
    typeof(AbpFeaturesLimitValidationModule),
    // Client function restriction module Redis integration
    typeof(AbpFeaturesValidationRedisClientModule),
    // Function management module Mvc view
    typeof(AbpFeatureManagementWebModule),
    // Multi-language module language mapping
    typeof(AbpLocalizationCultureMapModule),

    // OpenApi module authorization
    typeof(AbpOpenApiAuthorizationModule),

    // Message module real-time framework
    typeof(AbpIMSignalRModule),

    // notification module
    typeof(AbpNotificationsModule),
    // Notification module default notification
    typeof(AbpNotificationsCommonModule),
    // Notification module real-time framework
    typeof(AbpNotificationsSignalRModule),
    // Notification module email notification
    typeof(AbpNotificationsEmailingModule),
    // Notification module template parsing
    typeof(AbpNotificationsTemplatingModule),
    // Notification module WeChat applet
    typeof(AbpNotificationsWeChatMiniProgramModule),
    // Notification module Enterprise WeChat
    typeof(AbpNotificationsWeChatWorkModule),
    // Notification module working day check interceptor
    typeof(AbpNotificationsCalendarModule),
    // Multi-tenant module version
    typeof(AbpMultiTenancyEditionsModule),

    // Tencent QQ module
    typeof(AbpTencentQQModule),
    // Tencent Cloud module settings management
    typeof(AbpTencentCloudSettingManagementModule),

    // WeChat module WeChat applet
    typeof(AbpWeChatMiniProgramModule),
    // WeChat module WeChat public account
    typeof(AbpWeChatOfficialModule),
    // WeChat module WeChat public account application service
    typeof(AbpWeChatOfficialApplicationModule),
    // WeChat module WeChat public account controller
    typeof(AbpWeChatOfficialHttpApiModule),
    // WeChat module Enterprise WeChat
    typeof(AbpWeChatWorkModule),
    // WeChat module Enterprise WeChat address book
    typeof(AbpWeChatWorkContactModule),
    // WeChat module Enterprise WeChat customer contact
    typeof(AbpWeChatWorkExternalContactModule),
    // WeChat module Enterprise WeChat office
    typeof(AbpWeChatWorkOAModule),
    // WeChat module Enterprise WeChat application service
    typeof(AbpWeChatWorkApplicationModule),
    // WeChat module Enterprise WeChat controller
    typeof(AbpWeChatWorkHttpApiModule),
    // WeChat module WeChat public account event processing
    typeof(AbpWeChatOfficialHandlersModule),
    // WeChat module Enterprise WeChat event processing
    typeof(AbpWeChatWorkHandlersModule),
    // WeChat module settings management
    typeof(AbpWeChatSettingManagementModule),

    // AI management module Http tool
    typeof(AbpAIToolsHttpModule),
    // AI management module Mcp tool
    typeof(AbpAIToolsMcpModule),
    // AI management module application service
    typeof(AbpAIManagementApplicationModule),
    // AI management module controller
    typeof(AbpAIManagementHttpApiModule),

    // Data migration module
    typeof(AbpDataDbMigratorModule),
    // IP analysis module IP2Region integration
    typeof(AbpIP2RegionModule),
    // Distributed Id generator module
    typeof(AbpIdGeneratorModule),
    // Custom navigation module
    typeof(AbpUINavigationModule),

    // Elsa workflow module
    typeof(AbpElsaModule),
    // Elsa workflow module workflow server
    typeof(AbpElsaServerModule),
    // Elsa workflow module activity
    typeof(AbpElsaActivitiesModule),
    // Elsa workflow module Entity Framework
    typeof(AbpElsaEntityFrameworkCoreModule),
    // Elsa workflow designer module
    typeof(AbpElsaDesignerModule),

    // Data export module MiniSoftware integration
    typeof(AbpExporterMiniSoftwareModule),

    // Virtual file browser Mvc view
    typeof(AbpVirtualFileExplorerWebModule),
    typeof(AbpLoggingSerilogElasticsearchModule),
    typeof(AbpHttpClientWrapperModule),
    typeof(AbpAspNetCoreMvcWrapperModule),
    typeof(AbpAspNetCoreSessionModule),
    typeof(AbpAspNetCoreMvcIdempotentWrapperModule),
    typeof(AbpAspNetCoreHttpOverridesModule),
    typeof(AbpHttpClientIdentityModelWebModule),
    typeof(AbpAspNetCoreMultiTenancyModule),
    typeof(AbpAspNetCoreMvcUiMultiTenancyModule),
    typeof(AbpDynamicDefinitionsModule),
    typeof(AbpAspNetCoreAuthenticationModule),
    typeof(AbpTelemetryOpenTelemetryModule),
    typeof(AbpSwashbuckleModule),
    typeof(AbpMailKitModule),
    typeof(AbpAutofacModule),

    // Single application system status interface
    typeof(AbpSystemInfoHttpApiModule),

    // MySql
     typeof(SingleMigrationsEntityFrameworkCoreMySqlModule),
    // SqlServer
    typeof(SingleMigrationsEntityFrameworkCoreSqlServerModule),
    // PostgreSql
    typeof(SingleMigrationsEntityFrameworkCorePostgreSqlModule),

    // Audit log module application service
    typeof(AbpAuditingApplicationModule),
    // Audit log module controller
    typeof(AbpAuditingHttpApiModule),
    // Audit log module IP address location
    typeof(AbpAuditLoggingIPLocationModule),
    // Audit log module Entity Framework implementation
    typeof(AbpAuditLoggingEntityFrameworkCoreModule),
    // Audit log module Elasticsearch implementation
    // typeof(AbpAuditLoggingElasticsearchModule),
    // Audit log module OpenObserve implementation
    // typeof(AbpAuditLoggingOpenObserveModule),
    // System log module Elasticsearch
    typeof(AbpLoggingSerilogElasticsearchModule),
    // System log module OpenObserve
    typeof(AbpLoggingSerilogOpenObserveModule)
    )]
public partial class AllInOneModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        var configuration = context.Services.GetConfiguration();
        var hostingEnvironment = context.Services.GetHostingEnvironment();

        PreConfigureWrapper();
        PreConfigureFeature();
        PreConfigureIdentity();
        PreConfigureApp(configuration);
        PreConfigureCAP(configuration);
        PreConfigureQuartz(configuration);
        PreConfigureSignalR(configuration);
        PreConfigureAuthServer(configuration);
        PreConfigureElsa(context.Services, configuration);
        PreConfigureCertificate(configuration, hostingEnvironment);
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        var hostingEnvironment = context.Services.GetHostingEnvironment();
        var configuration = context.Services.GetConfiguration();

        ConfigureWeChat();
        ConfigureWrapper();
        ConfigureExporter();
        ConfigureAuditing();
        ConfigureIdempotent();
        ConfigureMvcUiTheme();
        ConfigureLocalization();
        ConfigureBackgroundTasks();
        ConfigureExceptionHandling();
        ConfigureVirtualFileSystem();

        ConfigureEntityDataProtected();
        ConfigureUrls(configuration);
        ConfigureAuditing(configuration);
        ConfigureIdentity(configuration);
        ConfigureDbContext(configuration);
        ConfigureAuthServer(configuration);
        ConfigureEndpoints(context.Services);
        ConfigureMultiTenancy(configuration);
        ConfigureJsonSerializer(configuration);
        ConfigureTextTemplating(configuration);
        ConfigureAIManagement(configuration);
        ConfigureFeatureManagement(configuration);
        ConfigureSettingManagement(configuration);
        ConfigureWebhooksManagement(configuration);
        ConfigurePermissionManagement(configuration);
        ConfigureNotificationManagement(configuration);
        ConfigureCors(context.Services, configuration);
        ConfigureSwagger(context.Services, configuration);
        ConfigureCaching(context.Services, configuration);
        ConfigureDistributedLock(context.Services, configuration);
        ConfigureKestrelServer(configuration, hostingEnvironment);
        ConfigureSecurity(context.Services, configuration, hostingEnvironment.IsDevelopment());

        ConfigureSingleModule(context.Services, hostingEnvironment.IsDevelopment());
    }

    public async override Task OnPostApplicationInitializationAsync(ApplicationInitializationContext context)
    {
        await context.ServiceProvider
            .GetRequiredService<IDataSeeder>()
            .SeedAsync();
    }
}
