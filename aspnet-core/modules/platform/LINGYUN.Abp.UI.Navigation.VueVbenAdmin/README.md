# LINGYUN.Abp.UI.Navigation.VueVbenAdmin

Navigation menu initialization module for **abp-vue-vben-admin**. This module provides the menu data initialization function required to integrate with the VueVbenAdmin front-end framework.

## Features

* Menu data initialization
  * Default menu structure
  * Automatically register layout
  * Dynamic menu configuration

* VueVbenAdmin integration
  * Layout component configuration
  * UI theme adaptation
  * Menu item customization

* Extensibility support
  * Custom menu provider
  * Menu data rewriting
  * Layout configuration extension

## Quick start

1. Reference module
```csharp
[DependsOn(typeof(AbpUINavigationVueVbenAdminModule))]
public class YouProjectModule : AbpModule
{
    // other
}
```

2. Configuration options
```csharp
Configure<AbpUINavigationVueVbenAdminOptions>(options =>
{
    //UI name configuration
    options.UI = "Vue Vben Admin";
    // Layout name configuration
    options.LayoutName = "Vben Admin Layout";
    // Layout component configuration
    options.LayoutPath = "LAYOUT";
});
```

## Configuration item description

* `AbpUINavigationVueVbenAdminOptions.UI`
  * Description: UI name
  *Default: Vue Vben Admin
  * Note: Changes are not recommended and need to be consistent with the front end

* `AbpUINavigationVueVbenAdminOptions.LayoutName`
  * Description: Layout name
  *Default: Vben Admin Layout
  * Note: Changes are not recommended and need to be consistent with the front end

* `AbpUINavigationVueVbenAdminOptions.LayoutPath`
  * Description: Layout component path
  *Default value: LAYOUT
  * Note: Changes are not recommended and need to be consistent with the front end

## Instructions for use

1. Menu initialization
   * The module will automatically register default menu data
   * Custom menus can be added by implementing the `INavigationDataSeedContributor` interface

2. Layout configuration
   * The layout configuration needs to match the front-end routing configuration
   * When modifying the layout configuration, you need to modify the front-end related configuration simultaneously.

3. Extension development
   * You can customize the menu initialization logic by inheriting the `VueVbenAdminNavigationDataSeeder` class
   * Supports overriding default implementation through dependency injection

## More information

* [VueVbenAdmin official document](https://doc.vvbin.cn/)
* [ABP official document](https://docs.abp.io/)
