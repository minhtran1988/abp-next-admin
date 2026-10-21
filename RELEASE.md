## Overview

# [2021-12-11]

* Upgrade abp framework 5.0.0 RC-2

1. Modify the SettingManagement module to only return all configurations of the specified provider;

2. Add Elasticsearch persistence layer extension to the workflow;
  
3. Fix the IM error caused by the inconsistency of SignalR serialization protocol;

4. Fix the building module dependency path reference error;

5. You can add token to the file URL to solve the identity authentication problem;

6. The tenant IsAvailable may be empty due to the aggregation gateway;

7. The aggregation gateway uses local configuration files.

# [2021-12-07]

* Upgrade abp framework 5.0.0 RC-1

1. Remove the **vueJs** module and import **vue-vben-admin** into [vue-vben-admin](./apps/vue);

2. Remove the Identity module api: ChangePassword;
  
3. Remove the distributed lock module. If any project references this module, please use **Volo.Abp.DistributedLocking**;

4. Remove the gateway management module and use local files as routing configuration;

5. Remove the dynamic gateway database script file;

6. Move the Profile related API from the Identity module to the Account module;

7. Add Fody to unified configuration ConfigureAwait;

8. Use Directory.Build.props to uniformly manage imported versions;

# [2021-03-29]
1. Add support for dynamic localization components, which are used to replace localized text at runtime. ILocalizationStore needs to be implemented;
  
2. Add a localized document management project and implement ILocalizationStore to persist local resources to the database;
  Notes for use: You need to first navigate to the [LocalizationManagement](./aspnet-core/services/localization/LINGYUN.Abp.LocalizationManagement.HttpApi.Host) directory
  Execute Ef migration command: **dotnet ef database update**

3. Add a dotnet-compose configuration file [docker-compose.configuration.yml](./docker-compose.configuration.yml);

4. vueJs adds localization management component view: views/localization-management;

5. The asynchronous waiting call after the vueJs user logs in is modified to call the abp module initialization event through the store;

6. vueJs fixes the problem of confusion between params and data usage passed by the HttpProxy module;

7. Synchronize the gateway routing initialization configuration of the localized management component.