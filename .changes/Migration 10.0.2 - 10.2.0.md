# 10.0.2 - 10.2.0

## Module changes

1. [Permission Management Module] abp officially added [ConfigurationFeatureValueProvider](https://github.com/abpframework/abp/blob/dev/framework/src/Volo.Abp.Features/Volo/Abp/Features/ConfigurationFeatureValueProvider.cs). This provider uses a function provider abbreviated as `C`, which is the same as the client function provider in this project. `ClientFeatureValueProvider` has the same name, so the abbreviation of `ClientFeatureValueProvider` will be changed to `CT`

   > *Manual execution of database statements is required to adapt to this change:*
   ```postgresql
   update "AbpFeatureValues" afv set "ProviderName" = 'CT' where afv."ProviderName" = 'C'
   ```

2. [Permission Management Module] Add [Resource Permissions](https://github.com/abpframework/abp/tree/dev/framework/src/Volo.Abp.Authorization/Volo/Abp/Authorization/Permissions/Resources) custom interface.

3. [Cache Management Module] Add the function of batch deletion of cache.

4. [Cache Management Module] Fixed the problem of empty data during fuzzy query.

5. [Cache Management Module] Add cache management page.

6. [Multi-language management module] Fix dynamic localization

7. [Permission Management Module] Add organization resource permission provider `OrganizationUnitResourcePermissionValueProvider`

8. [Object Storage Module] Reconstruct the domain layer, add a persistence layer, store object references and private file access permissions, and the implementation layer accesses Oss through object references.

9. [Single Application Service] Reconstruct the data seeds to resolve some of the seed data conflicts with the abp framework, causing the execution of new tenant events to fail.

10. [Elsa workload module] Since the Elsa2.x version depends on the Swashbuckle 6.x version, and abp depends on the 10.x version, the Elsa related interfaces are temporarily removed from the Swagger configuration.

11. [EFCoreMySql] Since `Pomelo.EntityFrameworkCore.MySql` has not been adapted to .NET 10 for too long, use `Microting.EntityFrameworkCore.MySql` to replace it. For details, see: https://github.com/PomeloFoundation/Pomelo.EntityFrameworkCore.MySql/issues/2007

12. [Single application service] Added the missing MySql database migration script. Since the MySql InnoDB engine has a length limit on the index, the abp resource permission index needs to limit the length. The resource name is temporarily removed and added to the index.

13. [Solution Change] Solution file changed from `sln` format to `slnx` format

14. [Data Export Module] Added `IExcelExporterProvider`, `IExcelImporterProvider`, `IWordExporterProvider` interfaces, the original `IExporterProvider`, `IImporterProvider` were marked as deprecated

15. [Data export module] Add `AbpExporterMiniSoftwareModule` module, integrate `MiniExcel`, `MiniWord` to implement `IExcelExporterProvider`, `IExcelImporterProvider`, `IWordExporterProvider` interface

16. [Solution Optimization] Merge Task, Webhook, Wechat, and Workflow solutions into All, and remove redundant solution files

## Dependency changes

| Library name | Original version | Current version |
| -------------------------------------------- | --------- | ------ |
| Volo.Abp.*                                   | 10.0.2    | 10.2.0 |
| Elsa2                                        | 2.16.0    | 2.16.1 |
| Elsa3                                        | 3.3.5     | 3.6.0  |
| Volo.Abp.AspNetCore.Mvc.UI.Theme.LeptonXLite | 5.0.2     | 5.2.0  |
| Microsoft.Bcl.AsyncInterfaces                | 10.0.0    | 10.0.2 |
| MySql.EntityFrameworkCore                    | 10.0.0-rc | 10.0.1 |
| Microting.EntityFrameworkCore.MySql          |           | 10.0.3 |
| Serilog.Extensions.Logging                   | 9.0.1     | 9.0.2  |
| DocumentFormat.OpenXml                       |           | 3.5.1  |
| MiniExcel                                    | 1.42.0    | 1.43.0 |
| MiniWord                                     |           | 0.9.2  |
| Scriban                                      | 6.3.0     | 7.0.0  |
| Swashbuckle.AspNetCore                       | 9.0.4     | 10.0.1 |



## Database migration

| Project Name             | Database Provider | Scripts                                        | EF Core Version |
| ------------------------ | ----------------- | ---------------------------------------------- | --------------- |
| aspire/AdminService      | PostgreSQL        | 20260403034826_Upgrade-Abp-Framework-To-10-2-0 | 10.0.2          |
| aspire/AuthServer        | PostgreSQL        | 20260402040021_Upgrade-Abp-Framework-To-10-2-0 | 10.0.2          |
| aspire/PlatformService   | PostgreSQL        | 20260411023117_Add-Blob-Management-Module      | 10.0.2          |
| services/Single          | MySQL             | 20260421022905_Upgrade-Abp-Framework-To-10-2-0 | 10.0.3          |
| services/Single          | PostgreSQL        | 20260403035350_Upgrade-Abp-Framework-To-10-2-0 | 10.0.2          |
| services/Single          | PostgreSQL        | 20260421023019_Add-Blob-Management-Module      | 10.0.2          |
| services/Single          | SqlServer         | 20260403035440_Upgrade-Abp-Framework-To-10-2-0 | 10.0.2          |
| services/Single          | SqlServer         | 20260421023044_Add-Blob-Management-Module      | 10.0.2          |
| services/AuthServer      | MySQL             | 20260420064613_Upgrade-Abp-Framework-To-10-0-2 | 10.0.3          |
| services/BackendAdmin    | MySQL             | 20260420064540_Upgrade-Abp-Framework-To-10-0-2 | 10.0.3          |
| services/Platform		   | MySQL             | 20260424033808_Add-Blob-Management-Module      | 10.0.3          |
| services/IdentityServer  | MySQL             | 20260420065028_Upgrade-Abp-Framework-To-10-0-2 | 10.0.3          |
| services/RealtimeMessage | MySQL             | 20260420065252_Upgrade-Abp-Framework-To-10-0-2 | 10.0.3          |
| services/TaskManagement  | MySQL             | 20260420073543_Upgrade-Abp-Framework-To-10-0-2 | 10.0.3          |


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
