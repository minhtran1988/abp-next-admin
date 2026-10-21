using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using Volo.Abp;
using Volo.Abp.Data;
using Volo.Abp.MultiTenancy;

namespace LINGYUN.Abp.UI.Navigation;

public class ApplicationMenu : IHasMenuItems, IHasExtraProperties
{
    public const int DefaultOrder = 1000;
    /// <summary>
    /// Name
    /// </summary>
    [NotNull] 
    public string Name { get; }
    /// <summary>
    /// Display name
    /// </summary>
    [NotNull] 
    public string DisplayName { get; }
    /// <summary>
    /// Description
    /// </summary>
    [CanBeNull]
    public string? Description { get; }
    /// <summary>
    /// path
    /// </summary>
    [NotNull]
    public string Url { get; }
    /// <summary>
    /// Component
    /// </summary>
    [CanBeNull]
    public string? Component { get;  }
    /// <summary>
    /// Redirect
    /// </summary>
    [CanBeNull]
    public string? Redirect { get; }
    /// <summary>
    /// icon
    /// </summary>
    [CanBeNull] 
    public string? Icon { get; set; }
    /// <summary>
    /// Sort
    /// </summary>
    public int Order { get; set; }
    /// <summary>
    /// Whether to disable
    /// </summary>
    public bool IsDisabled { get; set; }
    /// <summary>
    /// Is it visible?
    /// </summary>
    public bool IsVisible { get; set; }

    [NotNull]
    public ApplicationMenuList Items { get; }

    public bool IsLeaf => Items.IsNullOrEmpty();

    [NotNull]
    public ExtraPropertyDictionary ExtraProperties { get; }

    public MultiTenancySides MultiTenancySides { get; }

    public ApplicationMenu(
        [NotNull] string name,
        [NotNull] string displayName,
        [NotNull] string url,
        [CanBeNull] string component,
        string? description = null,
        string? icon = null,
        string? redirect = null,
        int order = DefaultOrder,
        MultiTenancySides multiTenancySides = MultiTenancySides.Both)
    {
        Check.NotNullOrWhiteSpace(name, nameof(name));
        Check.NotNullOrWhiteSpace(displayName, nameof(displayName));

        Check.NotNullOrWhiteSpace(url, nameof(url));

        Name = name;
        DisplayName = displayName;
        Url = url;
        Component = component;
        Description = description;
        Icon = icon;
        Redirect = redirect ?? "";
        Order = order;
        MultiTenancySides = multiTenancySides;

        Items = new ApplicationMenuList();
        ExtraProperties = new ExtraPropertyDictionary();
        this.SetDefaultsForExtraProperties();
    }

    public ApplicationMenu AddItem([NotNull] ApplicationMenu menuItem)
    {
        Items.Add(menuItem);
        return menuItem;
    }

    public ApplicationMenu WithRoles(string[] roles)
    {
        this.SetProperty("roles", roles.JoinAsString(";"));
        return this;
    }

    public string[] GetRoles()
    {
        var roles = this.GetProperty<string>("roles") ?? "";

        return roles.Split(";");
    }

    public override string ToString()
    {
        return $"[ApplicationMenu] Name = {Name}";
    }
}
