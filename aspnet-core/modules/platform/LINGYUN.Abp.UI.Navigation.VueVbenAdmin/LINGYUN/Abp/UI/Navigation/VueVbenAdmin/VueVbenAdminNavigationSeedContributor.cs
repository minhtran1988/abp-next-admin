using LINGYUN.Platform.Datas;
using LINGYUN.Platform.Layouts;
using LINGYUN.Platform.Menus;
using LINGYUN.Platform.Routes;
using LINGYUN.Platform.Utils;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.Guids;
using Volo.Abp.MultiTenancy;
using ValueType = LINGYUN.Platform.Datas.ValueType;

namespace LINGYUN.Abp.UI.Navigation.VueVbenAdmin;

public class VueVbenAdminNavigationSeedContributor : NavigationSeedContributor
{
    private static int _lastCodeNumber = 0;
    protected ICurrentTenant CurrentTenant { get; }
    protected IGuidGenerator GuidGenerator { get; }
    protected IRouteDataSeeder RouteDataSeeder { get; }
    protected IDataDictionaryDataSeeder DataDictionaryDataSeeder { get; }
    protected IMenuRepository MenuRepository { get; }
    protected ILayoutRepository LayoutRepository { get; }
    protected AbpUINavigationVueVbenAdminOptions Options { get; }

    public VueVbenAdminNavigationSeedContributor(
        ICurrentTenant currentTenant,
        IRouteDataSeeder routeDataSeeder,
        IMenuRepository menuRepository,
        ILayoutRepository layoutRepository,
        IGuidGenerator guidGenerator,
        IDataDictionaryDataSeeder dataDictionaryDataSeeder,
        IOptions<AbpUINavigationVueVbenAdminOptions> options)
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
                { "title", menu.DisplayName },
                { "icon", menu.Icon ?? "" },
                { "orderNo", menu.Order },
                { "hideTab", false },
                { "ignoreAuth", false },
            };

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
                roles:          new string[] { "admin" });

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
                { "title", item.DisplayName },
                { "icon", item.Icon ?? "" },
                { "orderNo", item.Order },
                { "hideTab", false },
                { "ignoreAuth", false },
            };

            var seedMenu = await SeedMenuAsync(
                layout: layout,
                data: data,
                name: item.Name,
                path: item.Url,
                code: CodeNumberGenerator.AppendCode(menu.Code, CodeNumberGenerator.CreateCode(index)),
                component: item.Component.IsNullOrWhiteSpace() ? layout.Path! : item.Component,
                displayName: item.DisplayName,
                redirect: item.Redirect,
                description: item.Description,
                parentId: menu.Id,
                tenantId: menu.TenantId,
                meta: menuMeta,
                roles: new string[] { "admin" });

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
            foreach (var role in roles)
            {
                await RouteDataSeeder.SeedRoleMenuAsync(role, menu, tenantId);
            }
        }

        if (users != null)
        {
            foreach (var user in users)
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
            CodeNumberGenerator.CreateCode(10),
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
            CodeNumberGenerator.CreateCode(10),
            "Vben Admin Layout Constraints",
            "Vben Admin Layout Meta Dictionary",
            null,
            tenantId)
        {
            IsStatic = true,
        };

        data.AddItem(
            GuidGenerator,
            "hideMenu",
            "Do not show in menu",
            "false",
            ValueType.Boolean,
            "The current route is not displayed in the menu",
            isStatic: true);
        data.AddItem(
            GuidGenerator,
            "icon",
            "icon",
            "",
            ValueType.String,
            "Icon is also a menu icon",
            isStatic: true);
        data.AddItem(
            GuidGenerator,
            "currentActiveMenu",
            "Currently active menu",
            "",
            ValueType.String,
            "The menu path activated on the left when configuring the details page",
            isStatic: true);
        data.AddItem(
            GuidGenerator,
            "ignoreKeepAlive",
            "KeepAlive cache",
            "false",
            ValueType.Boolean,
            "Whether to ignore KeepAlive cache",
            isStatic: true);
        data.AddItem(
            GuidGenerator,
            "frameSrc",
            "IFrame address",
            "",
            ValueType.String,
            "Address of embedded iframe",
            isStatic: true);
        data.AddItem(
            GuidGenerator,
            "transitionName",
            "Route switching animation",
            "",
            ValueType.String,
            "Specify the animation name of the route switching",
            isStatic: true);
        data.AddItem(
            GuidGenerator,
            "roles",
            "Accessible roles",
            "",
            ValueType.Array,
            "Accessible roles are only valid when the permission mode is Role",
            isStatic: true);
        data.AddItem(
            GuidGenerator,
            "title",
            "routing title",
            "",
            ValueType.String,
            "Routing title is generally required",
            false,
            isStatic: true);
        data.AddItem(
            GuidGenerator,
            "carryParam",
            "Show on tab page",
            "false",
            ValueType.Boolean,
            "If the route will carry parameters and need to be displayed on the tab page, it needs to be set to true",
            isStatic: true);
        data.AddItem(
            GuidGenerator,
            "hideBreadcrumb",
            "hide breadcrumbs",
            "false",
            ValueType.Boolean,
            "Hide the display of this route in breadcrumbs",
            isStatic: true);
        data.AddItem(
           GuidGenerator,
           "ignoreAuth",
           "Ignore permissions",
           "false",
           ValueType.Boolean,
           "Whether to ignore permissions, it is only effective when the permission mode is Role",
            isStatic: true);
        data.AddItem(
            GuidGenerator,
            "hideChildrenInMenu",
            "Hide all submenus",
            "false",
            ValueType.Boolean,
            "Hide all submenus",
            isStatic: true);
        data.AddItem(
            GuidGenerator,
            "hideTab",
            "Don't show in tabs",
            "false",
            ValueType.Boolean,
            "The current route is not displayed on the tab page",
            isStatic: true);
        data.AddItem(
            GuidGenerator,
            "affix",
            "Fixed tabs",
            "false",
            ValueType.Boolean,
            "Whether the tab is pinned",
            isStatic: true);
        data.AddItem(
            GuidGenerator,
            "requiredFeatures",
            "necessary functionality",
            "",
            ValueType.String,
            "Use English , to separate multiple functions",
            isStatic: true);
        data.AddItem(
            GuidGenerator,
            "dynamicLevel",
            "Number of tab pages that can be opened",
            "",
            ValueType.Numeic,
            "Number of tab pages that can be opened by dynamic routing",
            isStatic: true);
        data.AddItem(
            GuidGenerator,
            "hidePathForChildren",
            "Ignore this level path",
            "",
            ValueType.Boolean,
            "Whether to ignore this level path in the complete path of the sub-level menu. Valid in version 2.5.3 or above",
            isStatic: true);
        data.AddItem(
            GuidGenerator,
            "orderNo",
            "Menu sort",
            "",
            ValueType.Numeic,
            "Menu sorting, only valid for the first level",
            isStatic: true);
        data.AddItem(
            GuidGenerator,
            "realPath",
            "ActualPath",
            "",
            ValueType.String,
            "The actual Path of dynamic routing, that is, removing the dynamic part of routing;",
            isStatic: true);
        data.AddItem(
            GuidGenerator,
            "frameFormat",
            "Format IFrame",
            "false",
            ValueType.Boolean,
            "Extended formatted frame, {token}: pass the token request header in the opened iframe page");

        await DataDictionaryDataSeeder.SeedAsync(data);

        return data;
    }

    private int GetNextCode()
    {
        Interlocked.Increment(ref _lastCodeNumber);
        return _lastCodeNumber;
    }
}
