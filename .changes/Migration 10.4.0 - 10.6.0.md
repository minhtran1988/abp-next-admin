# 10.4.0 - 10.6.0

## Module changes

1. [Function Management Module] Function grouping/definition list is queried directly from the warehousing interface to avoid being affected by cache. [PR #1511](https://github.com/colinin/abp-next-admin/pull/1511)
2. [Function Management Module] Reconstruct FeatureDefinitionManager to merge built-in functions and dynamic function definitions. [PR #1559](https://github.com/colinin/abp-next-admin/pull/1559)
3. [Permission Management Module] Permission grouping/definition list is queried directly from the warehousing interface to avoid being affected by cache. [PR #1511](https://github.com/colinin/abp-next-admin/pull/1511)
4. [Permission Management Module] Reconstruct PermissionDefinitionManager to merge built-in permissions and dynamic permission definitions. [PR #1559](https://github.com/colinin/abp-next-admin/pull/1559)
5. [Permission Management Module] Fix the duplicate unique index of MySql resource permissions. [PR #1526](https://github.com/colinin/abp-next-admin/pull/1526)
6. [Notification Management Module] The notification group/definition list is queried directly from the warehousing interface to avoid being affected by cache. [PR #1511](https://github.com/colinin/abp-next-admin/pull/1511)
7. [Notification Management Module] Reconstruct NotificationDefinitionManager and add three strategies: merge, ignore, and overwrite. [PR #1559](https://github.com/colinin/abp-next-admin/pull/1559)
8. [Notification Management Module] Fix the invalid serialization of WeChat applet notification content. [PR #1530](https://github.com/colinin/abp-next-admin/pull/1530)
9. [Setting Management Module] The setting definition list is queried directly from the warehousing interface to avoid being affected by cache. [PR #1511](https://github.com/colinin/abp-next-admin/pull/1511)
10. [Setting Management Module] Reconstruct SettingDefinitionManager to merge built-in settings and dynamic setting definitions. [PR #1559](https://github.com/colinin/abp-next-admin/pull/1559)
11. [Webhook Management Module] Webhook grouping/definition list is queried directly from the warehousing interface to avoid being affected by cache. [PR #1511](https://github.com/colinin/abp-next-admin/pull/1511)
12. [Webhook Management Module] Reconstruct WebhookDefinitionManager and add three strategies: merge, ignore, and overwrite. [PR #1559](https://github.com/colinin/abp-next-admin/pull/1559)
13. [AI Management Module] Reconstruct WorkspaceDefinitionManager and add three strategies: merge, ignore, and overwrite. [PR #1516](https://github.com/colinin/abp-next-admin/pull/1516)
14. [AI Management Module] Reconstruct AIToolDefinitionManager and add three strategies: merge, ignore, and overwrite. [PR #1516](https://github.com/colinin/abp-next-admin/pull/1516)
15. [OpenIddict module] Rewrite the MySql warehousing Token batch cleaning and deletion interface and fix runtime errors. [PR #1512](https://github.com/colinin/abp-next-admin/pull/1512)
16. [Localization Management Module] The resource/language/localized text list is queried directly from the warehousing interface to avoid being affected by cache. [PR #1517](https://github.com/colinin/abp-next-admin/pull/1517)
17. [OpenIddict module] Add simulated user login implementation. [PR #1525](https://github.com/colinin/abp-next-admin/pull/1525)
18. [OpenIddict module] Add simulated tenant user login implementation. [PR #1525](https://github.com/colinin/abp-next-admin/pull/1525)
19. [Reverse proxy module] User configuration file rewrites reverse proxy server/address whitelist configuration.[PR #1532](https://github.com/colinin/abp-next-admin/pull/1532)
20. [Identity Authentication Module] After the user password is changed, the user session will become invalid (the current session will not be affected when changing the password yourself). [PR #1543](https://github.com/colinin/abp-next-admin/pull/1543)
21. [Multi-tenant UI module] Optimize the tenant selection pop-up component, and obtain the platform enterprise list as the selected tenant when the platform tenant relationship has been defined. [PR #1538](https://github.com/colinin/abp-next-admin/pull/1538)
22. [Elasticsearch module] Add expression tree translation ES query function. [PR #1552](https://github.com/colinin/abp-next-admin/pull/1552)
23. [System log module] Reconstruct system log list query to query through ES expression tree. [PR #1558](https://github.com/colinin/abp-next-admin/pull/1558)
24. [Audit log module] Reconstruct the audit log list query to query through ES expression tree. [PR #1558](https://github.com/colinin/abp-next-admin/pull/1558)

## Dependency changes

| Library name | Original version | Current version |
| -------------------------------------------- | --------- | ------ |
| AlibabaCloud.SDK.Dingtalk					   |    	   | 2.2.50 |
| BouncyCastle.Cryptography					   | 2.6.2     | 2.7.0  |
| IP2Region.Net								   | 3.0.0	   | 3.0.2  |
| Magicodes.IE.Excel    					   | 2.8.2	   | 2.9.0  |
| MailKit			    					   |    	   | 4.17.0 |
| Markdig			    					   | 0.44.0    | 1.3.2  |
| MimeCheck			    					   | 1.0.0     | 2.0.0  |
| MiniExcel			    					   | 1.43.0    | 1.45.0 |
| Microsoft.AspNetCore.*					   | 10.0.7	   | 10.0.9 |
| Microsoft.Bcl.AsyncInterfaces			       | 10.0.7	   | 10.0.9 |
| Microsoft.Data.Sqlite.Core                   | 10.0.7    | 10.0.9 |
| Microsoft.Data.SqlClient	                   | 6.1.1     | 7.0.2  |
| Microsoft.EntityFrameworkCore.*			   | 10.0.7	   | 10.0.9 |
| Microting.EntityFrameworkCore.MySql          | 10.0.7    | 10.0.9 |
| Microsoft.Extensions.*					   | 10.0.7    | 10.0.9 |
| Microsoft.IdentityModel.Tokens			   | 8.14.0    | 8.19.1 |
| MySqlConnector							   | 2.4.0     | 2.6.1  |
| OpenTelemetry.Exporter.Console               | 1.15.3    | 1.17.0 |
| OpenTelemetry.Exporter.OpenTelemetryProtocol | 1.15.3    | 1.17.3 |
| OpenTelemetry.Exporter.Zipkin                | 1.15.3    | 1.17.0 |
| OpenTelemetry.Extensions.Hosting             | 1.15.3    | 1.17.0 |
| OpenTelemetry.Instrumentation.AspNetCore     | 1.15.2    | 1.17.0 |
| OpenTelemetry.Instrumentation.EntityFrameworkCore     | 1.14.0-beta.2    | 1.17.0-beta.1 |
| OpenTelemetry.Instrumentation.Http	       | 1.15.1    | 1.17.0 |
| OpenTelemetry.Instrumentation.Quartz	       | 1.15.1-beta.1    | 1.17.0-beta.1 |
| OpenTelemetry.Instrumentation.Runtime	       | 1.15.1    | 1.17.0 |
| OpenTelemetry.Instrumentation.SqlClient      | 1.15.2    | 1.17.0 |
| Oracle.ManagedDataAccess.Core			       | 23.6.1    | 23.26.300 |
| QRCoder									   | 1.5.1     | 1.8.0  |
| RulesEngine								   | 5.0.5     | 6.0.0  |
| Scriban   								   | 7.0.0     | 7.2.5  |
| Serilog.Settings.Configuration               | 10.0.0    | 10.0.1 |
| Serilog.Sinks.InMemory		               |		   | 2.0.0  |
| Swashbuckle.AspNetCore                       | 10.0.1    | 10.2.3 |
| System.Security.Cryptography.Pkcs            |           | 10.0.9 |
| System.Threading.Channels                    | 10.0.7    | 10.0.9 |
| Volo.Abp.*                                   | 10.4.0    | 10.6.0 |
| Volo.Abp.AspNetCore.Mvc.UI.Theme.LeptonXLite | 5.4.0     | 5.6.0  |



## Database migration

| Project Name             | Database Provider | Scripts                                        | EF Core Version |
| ------------------------ | ----------------- | ---------------------------------------------- | --------------- |
| services/Single          | MySQL             | 20260626034342_Fix-MySql-Index-Key-Too-Lang-For-Resource-Permission-Grant		| 10.0.7          |
| services/BackendAdmin      | MySQL             | 20260626032421_Fix-MySql-Index-Key-Too-Lang-For-Resource-Permission-Grant		| 10.0.7          |


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
