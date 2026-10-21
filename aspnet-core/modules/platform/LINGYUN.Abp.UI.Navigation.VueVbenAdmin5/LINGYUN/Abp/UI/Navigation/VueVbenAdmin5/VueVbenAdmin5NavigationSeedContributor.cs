using LINGYUN.Platform.Datas;
using LINGYUN.Platform.Layouts;
using LINGYUN.Platform.Menus;
using LINGYUN.Platform.Routes;
using LINGYUN.Platform.Utils;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.Guids;
using Volo.Abp.MultiTenancy;
using ValueType = LINGYUN.Platform.Datas.ValueType;

namespace LINGYUN.Abp.UI.Navigation.VueVbenAdmin5;

public class VueVbenAdmin5NavigationSeedContributor : NavigationSeedContributor
{
    private static int _lastCodeNumber = 0;
    protected ICurrentTenant CurrentTenant { get; }
    protected IGuidGenerator GuidGenerator { get; }
    protected IRouteDataSeeder RouteDataSeeder { get; }
    protected IDataDictionaryDataSeeder DataDictionaryDataSeeder { get; }
    protected IMenuRepository MenuRepository { get; }
    protected ILayoutRepository LayoutRepository { get; }
    protected AbpUINavigationVueVbenAdmin5Options Options { get; }

    public VueVbenAdmin5NavigationSeedContributor(
        ICurrentTenant currentTenant,
        IRouteDataSeeder routeDataSeeder,
        IMenuRepository menuRepository,
        ILayoutRepository layoutRepository,
        IGuidGenerator guidGenerator,
        IDataDictionaryDataSeeder dataDictionaryDataSeeder,
        IOptions<AbpUINavigationVueVbenAdmin5Options> options)
    {
        CurrentTenant = currentTenant;
        GuidGenerator = guidGenerator;
        RouteDataSeeder = routeDataSeeder;
        MenuRepository = menuRepository;
        LayoutRepository = layoutRepository;
        DataDictionaryDataSeeder = dataDictionaryDataSeeder;

        Options = options.Value;
    }

    public override async Task SeedAsync(NavigationSeedContext context)
    {
        var uiDataItem = await SeedUIFrameworkDataAsync(CurrentTenant.Id);

        var layoutData = await SeedLayoutDataAsync(CurrentTenant.Id);

        var layout = await SeedDefaultLayoutAsync(layoutData, uiDataItem);

        var latMenu = await MenuRepository.FindLastMenuAsync();

        if (int.TryParse(CodeNumberGenerator.GetLastCode(latMenu?.Code ?? "0"), out int _lastNumber))
        {
            Interlocked.Exchange(ref _lastCodeNumber, _lastNumber);
        }

        await SeedDefinitionMenusAsync(layout, layoutData, context.Menus, context.MultiTenancySides);
    }

    private async Task SeedDefinitionMenusAsync(
        Layout layout,
        Data data, 
        IReadOnlyCollection<ApplicationMenu> menus,
        MultiTenancySides multiTenancySides)
    {
        foreach (var menu in menus)
        {
            if (!menu.MultiTenancySides.HasFlag(multiTenancySides))
            {
                continue;
            }

            var menuMeta = new Dictionary<string, object?>(menu.ExtraProperties)
            {
                ["icon"] = menu.Icon ?? "",
                ["order"] = menu.Order
            };
            var roles = menu.GetRoles();

            var seedMenu = await SeedMenuAsync(
                layout:         layout,
                data:           data,
                name:           menu.Name,
                path:           menu.Url,
                code:           CodeNumberGenerator.CreateCode(GetNextCode()),
                component:      layout.Path!,
                displayName:    menu.DisplayName,
                redirect:       menu.Redirect,
                description:    menu.Description,
                parentId:       null,
                tenantId:       layout.TenantId,
                meta:           menuMeta,
                roles:          roles.Union(["admin"]).ToArray());

            await SeedDefinitionMenuItemsAsync(layout, data, seedMenu, menu.Items, multiTenancySides);
        }
    }

    private async Task SeedDefinitionMenuItemsAsync(
        Layout layout, 
        Data data, 
        Menu menu, 
        ApplicationMenuList items,
        MultiTenancySides multiTenancySides)
    {
        int index = 1;
        foreach (var item in items)
        {
            if (!item.MultiTenancySides.HasFlag(multiTenancySides))
            {
                continue;
            }

            var menuMeta = new Dictionary<string, object?>(item.ExtraProperties)
            {
                ["icon"] = item.Icon ?? "",
                ["order"] = item.Order
            };
            var roles = item.GetRoles();

            var seedMenu = await SeedMenuAsync(
                layout:         layout,
                data:           data,
                name:           item.Name,
                path:           item.Url,
                code:           CodeNumberGenerator.AppendCode(menu.Code, CodeNumberGenerator.CreateCode(index)),
                component:      item.Component.IsNullOrWhiteSpace() ? layout.Path! : item.Component,
                displayName:    item.DisplayName,
                redirect:       item.Redirect,
                description:    item.Description,
                parentId:       menu.Id,
                tenantId:       menu.TenantId,
                meta:           menuMeta,
                roles:          roles.Union(["admin"]).ToArray());

            await SeedDefinitionMenuItemsAsync(layout, data, seedMenu, item.Items, multiTenancySides);

            index++;
        }
    }

    private async Task<Menu> SeedMenuAsync(
        Layout layout,
        Data data,
        string name,
        string path,
        string code,
        string component,
        string displayName,
        string? redirect = null,
        string? description = null,
        Guid? parentId = null,
        Guid? tenantId = null,
        Dictionary<string, object?>? meta = null,
        string[]? roles = null,
        Guid[]? users = null,
        bool isPublic = false
        )
    {
        var menuMeta = new Dictionary<string, object?>();
        foreach (var item in data.Items)
        {
            menuMeta[item.Name] = item.DefaultValue;
        }
        if (meta != null)
        {
            foreach (var item in meta)
            {
                menuMeta[item.Key] = item.Value;
            }
        }
        var menu = await RouteDataSeeder.SeedMenuAsync(
            layout,
            name,
            path,
            code,
            component,
            displayName,
            redirect,
            description,
            parentId,
            tenantId,
            isPublic,
            menuMeta);

        if (roles != null)
        {
            foreach (var role in roles.Distinct())
            {
                await RouteDataSeeder.SeedRoleMenuAsync(role, menu, tenantId);
            }
        }

        if (users != null)
        {
            foreach (var user in users.Distinct())
            {
                await RouteDataSeeder.SeedUserMenuAsync(user, menu, tenantId);
            }
        }

        return menu;
    }

    private async Task<DataItem> SeedUIFrameworkDataAsync(Guid? tenantId)
    {
        var data = new Data(
            GuidGenerator.Create(),
            "UI Framework",
            CodeNumberGenerator.CreateCode(30),
            "UI framework",
            "UI Framework",
            null,
            tenantId)
        {
            IsStatic = true,
        };

        data.AddItem(
            GuidGenerator,
            Options.UI,
            Options.UI,
            Options.UI,
            ValueType.String,
            Options.UI,
            isStatic: true);

        await DataDictionaryDataSeeder.SeedAsync(data);

        return data.FindItem(Options.UI)!;
    }

    private async Task<Layout> SeedDefaultLayoutAsync(Data data, DataItem uiDataItem)
    {
        var layout = await RouteDataSeeder.SeedLayoutAsync(
           Options.LayoutName,
           Options.LayoutPath, // The routing level has been processed. You only need to pass LAYOUT to automatically reference the layout.
           Options.LayoutName,
           data.Id,
           uiDataItem.Name,
           "",
           Options.LayoutName,
           data.TenantId
           );

        return layout;
    }

    private async Task<Data> SeedLayoutDataAsync(Guid? tenantId)
    {
        var data = new Data(
            GuidGenerator.Create(),
            Options.LayoutName,
            CodeNumberGenerator.CreateCode(40),
            "Vben5 Admin Layout Constraints",
            "Vben5 Admin template layout constraints",
            null,
            tenantId)
        {
            IsStatic = true,
        };
        data.AddItem(
            GuidGenerator,
            "title",
            "Title",
            "",
            ValueType.String,
            "The title used to configure the page will be displayed in menus and tabs. It is generally used in conjunction with internationalization.",
            isStatic: true);
        data.AddItem(
            GuidGenerator,
            "icon",
            "icon",
            "",
            ValueType.String,
            "The icons used to configure the page will be displayed in menus and tabs. They are generally used in conjunction with the icon library. If it is an http link, the image will be automatically loaded.",
            isStatic: true);
        data.AddItem(
            GuidGenerator,
            "activeIcon",
            "activation icon",
            "",
            ValueType.String,
            "The activation icon used to configure the page will be displayed in the menu. It is generally used in conjunction with the icon library. If it is an http link, the image will be automatically loaded.",
            isStatic: true);
        data.AddItem(
            GuidGenerator,
            "keepAlive",
            "Whether to enable caching",
            "true",
            ValueType.Boolean,
            "Used to configure whether to enable caching on the page. When enabled, the page will be cached and will not be reloaded. It is only valid when the tab is enabled.",
            isStatic: true);
        data.AddItem(
            GuidGenerator,
            "hideInMenu",
            "Whether to hide in the menu",
            "false",
            ValueType.Boolean,
            "Used to configure whether the page is hidden in the menu. After hiding, the page will not be displayed in the menu.",
            isStatic: true);
        data.AddItem(
            GuidGenerator,
            "hideInTab",
            "Whether to hide in tabs",
            "false",
            ValueType.Boolean,
            "Used to configure whether the page is hidden in the tab page. After hiding, the page will not be displayed in the tab page.",
            isStatic: true);
        data.AddItem(
            GuidGenerator,
            "hideInBreadcrumb",
            "Whether to hide in breadcrumbs",
            "false",
            ValueType.Boolean,
            "Used to configure whether the page is hidden in breadcrumbs. After hiding, the page will not be displayed in breadcrumbs.",
            isStatic: true);
        data.AddItem(
            GuidGenerator,
            "hideChildrenInMenu",
            "Whether to hide the submenu",
            "false",
            ValueType.Boolean,
            "Used to configure whether the subpage of the page is hidden in the menu. After hiding, the subpage will not be displayed in the menu.",
            isStatic: true);
        data.AddItem(
            GuidGenerator,
            "authority",
            "Page Permissions",
            "",
            ValueType.Array,
            "Used to configure the permissions of the page. Only users with corresponding permissions can access the page. If not configured, no permissions are required.",
            isStatic: true);
        data.AddItem(
            GuidGenerator,
            "badge",
            "Page Logo",
            "",
            ValueType.String,
            "The logo used to configure the page will be displayed in the menu.",
            isStatic: true);
        data.AddItem(
           GuidGenerator,
           "badgeType",
           "logo type",
           "normal",
           ValueType.String,
           "Used to configure the logo type of the page, dot is the small red dot, and normal is the text.",
            isStatic: true);
        data.AddItem(
            GuidGenerator,
            "badgeVariants",
            "logo color",
            "success",
            ValueType.String,
            "Used to configure the logo color of the page,'default' | 'destructive' | 'primary' | 'success' | 'warning' | string",
            isStatic: true);
        data.AddItem(
            GuidGenerator,
            "activePath",
            "Currently active menu",
            "",
            ValueType.String,
            "Used to configure the currently activated menu. Sometimes the page is not displayed in the menu and is used when the parent menu needs to be activated.",
            isStatic: true);
        data.AddItem(
            GuidGenerator,
            "affixTab",
            "Whether the tab is pinned",
            "false",
            ValueType.Boolean,
            "Used to configure whether the page should pin the tab page. The page cannot be closed after it is pinned.",
            isStatic: true);
        data.AddItem(
            GuidGenerator,
            "affixTabOrder",
            "Fixed tab sorting,",
            "0",
            ValueType.Numeic,
            "Used to configure the sorting of fixed tabs on the page, using ascending order.",
            isStatic: true);
        data.AddItem(
            GuidGenerator,
            "iframeSrc",
            "embedded page address",
            "",
            ValueType.String,
            "Used to configure the iframe address of the embedded page. After setting, the corresponding page will be embedded in the current page.",
            isStatic: true);
        data.AddItem(
            GuidGenerator,
            "ignoreAccess",
            "Whether to ignore permissions",
            "false",
            ValueType.Boolean,
            "Used to configure whether the page ignores permissions and can be accessed directly.",
            isStatic: true);
        data.AddItem(
            GuidGenerator,
            "link",
            "External link jump path",
            "",
            ValueType.String,
            "Used to configure the external link jump path, it will open in a new window.",
            isStatic: true);
        data.AddItem(
            GuidGenerator,
            "maxNumOfOpenTab",
            "Maximum number of open tabs",
            "-1",
            ValueType.Numeic,
            "Used to configure the maximum number of open tabs. After setting, the earliest opened tab will be automatically closed when a new tab is opened (only effective when a tab with the same name is opened).",
            isStatic: true);
        data.AddItem(
            GuidGenerator,
            "menuVisibleWithForbidden",
            "Whether the menu is visible without permission",
            "false",
            ValueType.Boolean,
            "The configuration page can be seen in the menu, but access will be redirected to 403.");
        data.AddItem(
            GuidGenerator,
            "openInNewWindow",
            "Whether to open in a new page",
            "false",
            ValueType.Boolean,
            "When set to true, the page will be opened in a new window.");
        data.AddItem(
            GuidGenerator,
            "order",
            "Page Sort",
            "0",
            ValueType.Numeic,
            "Used to configure the sorting of pages and used to route to menu sorting. Note: Sorting is only valid for first-level menus. The sorting of second-level menus needs to be set in code order in the corresponding first-level menus.");
        data.AddItem(
            GuidGenerator,
            "noBasicLayout",
            "Whether not to use basic layout",
            "false",
            ValueType.Boolean,
            "Used to configure the current route not to use the basic layout, it only takes effect at the top level. By default, all routes will be wrapped in the basic layout (including navigation components such as top and side). If your page does not need these components, you can set noBasicLayout to true.");

        await DataDictionaryDataSeeder.SeedAsync(data);

        return data;
    }

    private int GetNextCode()
    {
        Interlocked.Increment(ref _lastCodeNumber);
        return _lastCodeNumber;
    }
}
