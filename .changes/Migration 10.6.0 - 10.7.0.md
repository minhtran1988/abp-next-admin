# 10.6.0 - 10.7.0

## Module changes

1. [Dynamic Definition] Added LINGYUN.Abp.Dynamic.Definitions dynamic definition module to carry definition extensions of functions/permissions/settings/notifications/Webhook/AI in a unified mechanism.
2. [Function Management] Rewrite ABP's default FeatureDefinitionManager to implement three strategies of merging, ignoring, and overwriting built-in definitions and dynamic definitions.
3. [Permission Management] Rewrite PermissionDefinitionManager to implement three strategies: merge, ignore, and overwrite.
4. [Setting Management] Rewrite SettingDefinitionManager to implement three strategies: merge, ignore, and overwrite.
5. [Notification Management] Rewrite NotificationDefinitionManager to implement three strategies: merge, ignore, and overwrite.
6. [Webhook Management] Rewrite WebhookDefinitionManager to implement three strategies: merge, ignore, and overwrite.
7. [AI Management] Rewrite WorkspaceDefinitionManager and AIToolDefinitionManager to implement three strategies: merge, ignore, and overwrite.
8. [Definition Management] Local events are triggered after function/permission/setting definition changes, making it easier for subscribers to refresh the cache.
9. [Definition Management] Optimize the definition management page and permission list styles.
10. [Account module] Enhance user security: verify the two-factor policy when creating a user, bind a two-factor authenticator when logging in, verify two-factor and email confirmation when registering, and verify email confirmation when logging in.
11. [Account module] Integrate picture verification code (LazyCaptcha) and Tencent Cloud verification code, covering login, registration and verification code verification interface.
12. [Account module] Reconstruct the login page and add interactions such as password display/hide, page content title, "Return to login" button style, etc.
13. [Account Module] User registration will be confirmed through email; the verification process of verification code and recovery code will be improved.
14. [Identity Module] Add user operation security log records and complete verification code-related security logs.
15. [Identity module] Reconstruct the session module to enhance the security of session verification and invalidate the user session after the user password is changed.
16. [Identity module] The admin role is forced to enable two-factor authentication and is prohibited from being set as the default role.
17. [Identity Module] Improve the default values of security related settings.
18. [Personal Center] Add binding information validator; synchronize HttpClientProxy of Identity interface.
19. [Audit Log] The audit log no longer records the tenant connection string.
20. [OpenIddict module] Add JsonWebKeySet support, and optimize OpenIddict management functions and identity token mapping.
21. [OpenIddict module] Enterprise WeChat authorization is split into two authorization processes: web page authorization and mini program (wecom_oauth / wecom_mp), and a new implementation of mini program temporary login credentials in exchange for sessions is added.
22. [OpenIddict module] QR code login is changed to use the tenant carried by the QR code itself, and the request parameter tenant_id is no longer accepted; and the security log actions for QR code login are refined.
23. [IdentityServer module] The enterprise WeChat authorization verifier is simultaneously split into two verifiers: web authorization and mini program.
24. [System Log] Added OpenObserve log query provider to support using OpenObserve instead of Elasticsearch to query system logs.
25. [Audit Log] Added new audit log storage based on OpenObserve: audit logs, security logs, entity changes, and operations are streamed into different data streams and streamed according to tenants.
26. [Audit Log] Added information contributor extension points for audit logs and security logs, allowing modules to supplement log information without replacing the storage implementation.
27. [Log module] Elasticsearch and OpenObserve log providers have built-in Serilog enhancer (ApplicationName / UniqueId).
28. [System Logs] Enable rolling policy for audit/security log index names.
29. [OpenTelemetry module] Add Redis tracking instrumentation and remove the deprecated Zipkin exporter configuration.
30. [IP positioning] Optimize the analysis and name localization of IP address geographical location.
31. [IP Positioning] Location remark rules are changed to configurable (removed by country/province) and bound by an independent configuration section.
32. [IP Location] Enable IP location resolution of audit logs for all services, gateways and Aspire hosts.
33. [IP Positioning] The MaxMind GeoIP2 offline library is changed to be provided by the user during deployment, and the module is no longer built-in for distribution; when the library file is missing, it falls back to the empty interpreter and records a warning.
34. [IP Positioning] The default location resolution provider of each host is switched from MaxMind GeoIP2 to IP2Region (built-in offline library).
35. [Notification module] Abstract template card messages support JSON round-trip: two new converters, Newtonsoft and System.Text.Json, are added to restore the specific card type according to card_type.
36. [Notification module] The notification data extension is split into multiple partial files according to message types, and switches such as confidential messages, ID translation, and duplicate message checking are added.
37. [Notification Module] Added a new mechanism to declare notification payloads by channel. The attribute package is divided into namespaces according to channel prefixes. New fields in channels do not need to change the notification table structure.
38. [Notification Module] Optimize the construction of channel load attribute metadata, remove global locks and change to delayed initialization.
39. [TuiJuhe Module] The notification publishing contract is unified into NotificationPublishContext (consistent with other channels).
40. [Blob Management] Fixed the path traversal vulnerability of the local file system storage provider, and added cross-platform name and path verification rules.
41. [Blob Management] File existence check is changed to filtering by container ID, and container table association query is removed.
42. [Deployment] Enable IP location resolution and comment removal rules for audit logs for each service in container orchestration; add container port and running configuration instructions.
43. [CLI] Added blob-migration command for migrating files from old version of file system storage to BlobManagement.
44. [CLI] Improve blob-migration: complete parameter prompts, alert and terminate the operation when the specified directory does not exist.
45. [Project structure] Microservice projects are renamed to LINGYUN.Abp.MicroService.*, and single application projects are renamed to LINGYUN.Abp.MicroService.AllInOne.
46. [Project structure] Remove the legacy IdentityServer and AIO single application host projects.
47. [Project structure] Add an AI microservice project and its data migration project, and add corresponding hosts in Aspire orchestration.
48. [Project structure] Remove the v1 VueVbenAdmin platform module (navigation/settings/theme) from the solution and retain the v5 version navigation.
49. [Deployment] Optimize Dockerfile, Docker Compose and image building scripts.
50. [Template] The microservice template is upgraded to 10.7.0, packaging, weaving and log configuration are synchronized, and appsettings.Production.json is added to the template Host.
51. [Build] Package output and whether to generate packages are changed to conditional control; new abppkg packaging content and client agent description files are automatically embedded; ConfigureAwait weaving only takes effect in Release, and redundant FodyWeavers configuration files of each project are removed.

## Dependency changes

| Library name | Original version | Current version |
| ------------------------------------------------------- | --------------- | ------------- |
| AlibabaCloud.SDK.Captcha20230305                        |                 | 1.1.4         |
| Duende.IdentityModel                                    |                 | 7.1.0         |
| IdentityModel | 7.0.0 | (removed) |
| Lazy.Captcha.Core                                       |                 | 2.2.5         |
| MaxMind.GeoIP2                                          |                 | 6.1.0         |
| Microsoft.AspNetCore.*                                  | 10.0.9          | 10.0.11       |
| Microsoft.Bcl.AsyncInterfaces                           | 10.0.9          | 10.0.11       |
| Microsoft.Data.Sqlite.Core                              | 10.0.9          | 10.0.11       |
| Microsoft.EntityFrameworkCore.*                         | 10.0.9          | 10.0.11       |
| Microsoft.Extensions.*                                  | 10.0.9          | 10.0.11       |
| Microsoft.IdentityModel.*                               | 8.19.1          | 8.19.2        |
| Microsoft.SourceLink.GitHub                             |                 | 8.0.0         |
| Microting.EntityFrameworkCore.MySql                     | 10.0.9          | 10.0.11       |
| MySql.EntityFrameworkCore                               | 10.0.1          | 10.0.9        |
| Npgsql                                                  | 10.0.0          | 10.0.3        |
| OpenIddict.Server.DataProtection                        | 7.5.0           | 7.7.0         |
| OpenIddict.Validation.DataProtection                    | 7.5.0           | 7.7.0         |
| OpenTelemetry.Exporter.Console                          | 1.17.0          | 1.19.0        |
| OpenTelemetry.Exporter.OpenTelemetryProtocol            | 1.17.0          | 1.19.0        |
| OpenTelemetry.Exporter.Zipkin                           | 1.17.0          | 1.19.0        |
| OpenTelemetry.Extensions.Hosting                        | 1.17.0          | 1.19.0        |
| OpenTelemetry.Instrumentation.AspNetCore                | 1.17.0          | 1.19.0        |
| OpenTelemetry.Instrumentation.EntityFrameworkCore       | 1.17.0-beta.1   | 1.19.0-beta.1 |
| OpenTelemetry.Instrumentation.Http                      | 1.17.0          | 1.19.0        |
| OpenTelemetry.Instrumentation.Quartz                    | 1.17.0-beta.1   | 1.19.0-beta.1 |
| OpenTelemetry.Instrumentation.Runtime                   | 1.17.0          | 1.19.0        |
| OpenTelemetry.Instrumentation.SqlClient                 | 1.17.0          | 1.19.0        |
| OpenTelemetry.Instrumentation.StackExchangeRedis        |                 | 1.19.0-beta.1 |
| System.IdentityModel.Tokens.Jwt                         | 8.19.1          | 8.19.2        |
| System.Security.Cryptography.Pkcs                       | 10.0.9          | 10.0.11       |
| System.Threading.Channels                               | 10.0.9          | 10.0.11       |
| Volo.Abp.*                                              | 10.6.0          | 10.7.0        |
| Volo.Abp.AspNetCore.Mvc.UI.Theme.LeptonXLite            | 5.6.0           | 5.7.0         |
| aliyun-net-sdk-core | 1.6.2 | (removed) |

## Database migration

| Project Name       | Database Provider | Scripts                              | EF Core Version |
| ------------------ | ----------------- | ------------------------------------ | --------------- |
| services/AIService | MySQL             | 20261007004411_Initial-AI-Service    | 10.0.11         |

> Note: This version adds a new AI microservice, its data migration project is `LINGYUN.Abp.MicroService.AIService.EntityFrameworkCore` / `.DbMigrator`, and the connection string name is `AIServiceDbMigrator`.
>
> There are no new migration scripts for the remaining databases. Only the migration project names change with the project renaming of this version: the original `LY.*` series are unified into `LINGYUN.Abp.MicroService.*`, `services/Single` is renamed to `LINGYUN.Abp.MicroService.AllInOne`, and the original `services/BackendAdmin` has been removed along with the old version of the host. When upgrading, please perform migration according to the new project name, and no structural changes are required for historical data.

## Database connection string

| Module Name                   | ConnectionString Name        | Example(MySQL)                                               |
| ----------------------------- | ---------------------------- | ------------------------------------------------------------ |
| Default                       | Default                      | Server=127.0.0.1;Database=abp;User Id=root;Password=123456;SslMode=None |
| abp/Identity                  | AbpIdentity                  | Server=127.0.0.1;Database=abp;User Id=root;Password=123456;SslMode=None |
| abp/IdentityServer            | AbpIdentityServer            | Server=127.0.0.1;Database=abp;User Id=root;Password=123456;SslMode=None |
| abp/OpenIddict                | AbpOpenIddict                | Server=127.0.0.1;Database=abp;User Id=root;Password=123456;SslMode=None |
| abp/TenantManagement          | AbpTenantManagement(AbpSaas) | Server=127.0.0.1;Database=abp;User Id=root;Password=123456;SslMode=None |
| abp/FeatureManagement         | AbpFeatureManagement         | Server=127.0.0.1;Database=abp;User Id=root;Password=123456;SslMode=None |
| abp/SettingManagement         | AbpSettingManagement         | Server=127.0.0.1;Database=abp;User Id=root;Password=123456;SslMode=None |
| abp/PermissionManagement      | AbpPermissionManagement      | Server=127.0.0.1;Database=abp;User Id=root;Password=123456;SslMode=None |
| labp/AIManagement             | AbpAIManagement              | Server=127.0.0.1;Database=abp;User Id=root;Password=123456;SslMode=None |
| labp/BlobManagement           | AbpBlobManagement            | Server=127.0.0.1;Database=abp;User Id=root;Password=123456;SslMode=None |
| labp/DataProtectionManagement | AbpDataProtectionManagement  | Server=127.0.0.1;Database=abp;User Id=root;Password=123456;SslMode=None |
| labp/Gdpr                     | AbpGdpr                      | Server=127.0.0.1;Database=abp;User Id=root;Password=123456;SslMode=None |
| labp/Saas                     | AbpSaas                      | Server=127.0.0.1;Database=abp;User Id=root;Password=123456;SslMode=None |
| labp/LocalizationManagement   | AbpLocalizationManagement    | Server=127.0.0.1;Database=abp;User Id=root;Password=123456;SslMode=None |
| labp/TextTemplating           | AbpTextTemplating            | Server=127.0.0.1;Database=abp;User Id=root;Password=123456;SslMode=None |
| labp/Platform                 | AppPlatform                  | Server=127.0.0.1;Database=abp;User Id=root;Password=123456;SslMode=None |
| labp/MessageService           | MessageService               | Server=127.0.0.1;Database=abp;User Id=root;Password=123456;SslMode=None |
| labp/NotificationService      | Notifications                | Server=127.0.0.1;Database=abp;User Id=root;Password=123456;SslMode=None |
| labp/TaskManagement           | TaskManagement               | Server=127.0.0.1;Database=abp;User Id=root;Password=123456;SslMode=None |
| labp/WebhooksManagement       | WebhooksManagement           | Server=127.0.0.1;Database=abp;User Id=root;Password=123456;SslMode=None |
| labp/Elsa                     | Workflow                     | Server=127.0.0.1;Database=abp;User Id=root;Password=123456;SslMode=None |

> There is no new or renamed connection string key in this version, and the configuration structure is consistent with 10.6.0; the above table is organized by module, and the project name should correspond to the project naming of this version.

## Upgrade Notes

1. **Destructive changes to MaxMind GeoIP2**: The module is no longer built-in `GeoLite2-City.mmdb`, and the public constant `AbpMaxMindGeoIP2Module.DefaultDatabaseFile` has been removed. If you want to continue using it, please download the GeoLite2 offline library and configure `MaxMindGeoIP2:DatabaseFile` (or environment variable `MaxMindGeoIP2__DatabaseFile`); When not configured, location analysis returns empty results and records warnings. Each host has switched to IP2Region by default, and usually no additional processing is required.

2. **Enterprise WeChat authorization type change**: The authorization type is changed from `wecom` to `wecom_oauth`, and a new mini program authorization type `wecom_mp` is added. If the old client is still sending `grant_type=wecom`, the authentication will fail and needs to be upgraded simultaneously. At the same time, `WeChatWorkTokenExtensionGrant` / `WeChatWorkGrantValidator` has been changed to an abstract class.

3. **QR code login is no longer accepted `tenant_id`**: The tenant information is taken from the QR code itself, and the `tenant_id` in the request parameter will be ignored (no error will be reported). The caller that previously relied on this parameter to specify the tenant needs to remove this parameter.

4. **Fody weaving configuration changes**: `ConfigureAwait` weaving only takes effect in the Release configuration, and is inlined in `WeaverConfiguration` through the public property file; the original `FodyWeavers.xml` / `.xsd` of each project has been removed. If there is a custom Fody configuration, please declare it explicitly in the project instead.

5. **Packaging behavior change**: `GeneratePackageOnBuild` is changed to conditional control (no longer effective for all `LINGYUN*` assemblies), and `*.abppkg` content packaging and `HttpApi.Client` assembly are automatically embedded with `*generate-proxy.json`. If you rely on the old packaging product path, please confirm the `LocalNuget` output.
