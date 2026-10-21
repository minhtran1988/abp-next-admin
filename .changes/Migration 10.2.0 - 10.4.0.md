# 10.2.0 - 10.4.0

## Module changes

1. [Identity Identification Module] Add inactive user notification/cleanup job, and implement three notification templates (account stay active notification, account deactivation notification, account deletion notification)
2. [Identity Module] Adding inactive user entity `IdentityUserInactive` requires database migration.
3. [Identity Module] Add user login Url setting definition: `Abp.Identity.Link.UserLoginUri`, used to associate user login pages in external systems
4. [Identity Authentication Module] Add user login Url definition: `Abp.Account.UserLogin`
5. [Identity Authentication Module] Added associated user login function, which can realize cross-tenant user association and switch associated user login
6. [Settings Management Module] Add missing setting items in the identity module: enable password reuse prevention, password reuse prevention number
7. [Setting Basic Module] Add common structure extensions such as setting definition groups, belonging settings, priorities, dependent permissions, dependent functions, options, etc.
8. [Setting Management Module] Reconstruct the setting management application service interface, and automatically form a Dto structure through preset setting definition groups, superior-subordinate relationships, etc.

## Dependency changes

| Library name | Original version | Current version |
| -------------------------------------------- | --------- | ------ |
| Microsoft.AspNetCore.*					   | 10.0.2	   | 10.0.7 |
| Microsoft.Bcl.AsyncInterfaces			       | 10.0.2	   | 10.0.7 |
| Microsoft.Data.Sqlite.Core                   | 10.0.0    | 10.0.7 |
| Microsoft.EntityFrameworkCore.*			   | 10.0.2	   | 10.0.7 |
| Microting.EntityFrameworkCore.MySql          | 10.0.3    | 10.0.7 |
| Microsoft.Extensions.*					   | 10.0.2    | 10.0.7 |
| OpenTelemetry.Extensions.Hosting             | 1.14.0    | 1.15.3 |
| OpenTelemetry.Exporter.Console               | 1.14.0    | 1.15.3 |
| OpenTelemetry.Exporter.OpenTelemetryProtocol | 1.14.0    | 1.15.3 |
| OpenTelemetry.Exporter.Zipkin                | 1.14.0    | 1.15.3 |
| OpenTelemetry.Instrumentation.AspNetCore     | 1.14.0    | 1.15.2 |
| OpenTelemetry.Instrumentation.Http	       | 1.14.0    | 1.15.1 |
| OpenTelemetry.Instrumentation.Quartz	       | 1.14.0-beta.2    | 1.15.1-beta.1 |
| OpenTelemetry.Instrumentation.Runtime	       | 1.14.0    | 1.15.1 |
| OpenTelemetry.Instrumentation.SqlClient      | 1.14.0-beta.1    | 1.15.2 |
| OpenIddict.Server.DataProtection             | 7.2.0     | 7.5.0  |
| OpenIddict.Validation.DataProtection         | 7.2.0     | 7.5.0  |
| System.Threading.Channels                    | 10.0.0    | 10.0.7 |
| Volo.Abp.*                                   | 10.2.0    | 10.4.0 |
| Volo.Abp.AspNetCore.Mvc.UI.Theme.LeptonXLite | 5.2.0     | 5.4.0  |



## Database migration

| Project Name             | Database Provider | Scripts                                        | EF Core Version |
| ------------------------ | ----------------- | ---------------------------------------------- | --------------- |
| aspire/AuthServer        | PostgreSQL        | 20260530060327_Add-Identity-User-Inactive		| 10.0.2          |
| services/Single          | MySQL             | 20260529063257_Add-Identity-User-Inactive		| 10.0.3          |
| services/Single          | PostgreSQL        | 20260530055941_Add-Identity-User-Inactive		| 10.0.2          |
| services/Single          | SqlServer         | 20260530060009_Add-Identity-User-Inactive      | 10.0.2          |
| services/AuthServer      | MySQL             | 20260530061141_Add-Identity-User-Inactive		| 10.0.3          |


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
