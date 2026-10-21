# LINGYUN.Abp.UI.Navigation

Simplified Chinese | [English](./README.EN.md)

Menu navigation module, providing scalable and customizable menu items

## Features

* Support custom menu item definition and expansion
*Support multi-tenancy
* Support hierarchical structure of menu items
* Supports sorting of menu items
* Support the visibility and disabled state of menu items
* Support icons and redirection of menu items
* Support initialization of menu seed data

## Module reference

```csharp
[DependsOn(typeof(AbpUINavigationModule))]
public class YouProjectModule : AbpModule
{
  // other
}
```

## Basic usage

### 1. Define menu provider

Menu items are defined by implementing the `INavigationDefinitionProvider` interface:

```csharp
public class FakeNavigationDefinitionProvider : NavigationDefinitionProvider
{
    public override void Define(INavigationDefinitionContext context)
    {
        context.Add(GetNavigationDefinition());
    }

    private static NavigationDefinition GetNavigationDefinition()
    {
        var dashboard = new ApplicationMenu(
            name: "Vben Dashboard",
            displayName: "Dashboard",
            url: "/dashboard",
            component: "",
            description: "Dashboard",
            icon: "ion:grid-outline",
            redirect: "/dashboard/analysis");

         dashboard.AddItem(
            new ApplicationMenu(
            name: "Analysis",
            displayName: "Analysis Page",
            url: "/dashboard/analysis",
            component: "/dashboard/analysis/index",
            description: "Analysis page"));

         return new NavigationDefinition(dashboard);
    }
}
```

### 2. Initialize menu data

Initialize menu seed data by implementing the `INavigationSeedContributor` interface:

```csharp
public class YourNavigationDataSeeder : INavigationSeedContributor
{
    public async Task SeedAsync(NavigationSeedContext context)
    {
        //Initialize menu data here
    }
}
```

## Menu item attribute description

* Name - menu item name (unique identifier)
* DisplayName - display name
* Description - description
* Url - path
* Component - component path
* Redirect - redirect path
* Icon - icon
* Order - sorting (default 1000)
* IsDisabled - whether it is disabled
* IsVisible - whether it is visible
* Items - collection of submenu items
* ExtraProperties - extended properties
*MultiTenancySides - multi-tenant options

## Best Practices

1. It is recommended to use meaningful names for menu items to facilitate management and maintenance.
2. Use the order of menu items rationally to keep the interface clean and tidy
3. Use icons to enhance user experience
4. Appropriate use of redirection function to optimize navigation experience
5. Control the visibility and disabled status of menu items according to actual needs

## Notes

1. The name of the menu item must be unique
2. Submenu items will inherit the multi-tenant settings of the parent menu item.
3. The smaller the sorting value of the menu item, the higher it is.
4. Disabled menu items are still visible but cannot be clicked
5. The component path should be consistent with the front-end routing configuration
