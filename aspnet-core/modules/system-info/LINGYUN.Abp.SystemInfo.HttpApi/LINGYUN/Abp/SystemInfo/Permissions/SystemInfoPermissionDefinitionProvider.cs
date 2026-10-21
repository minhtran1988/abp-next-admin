using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace LINGYUN.Abp.SystemInfo.Permissions;

public class SystemInfoPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var systemInfoGroup = context.AddGroup(
            SystemInfoPermissions.GroupName,
            new FixedLocalizableString("System details"));

        systemInfoGroup.AddPermission(
            SystemInfoPermissions.Default,
            new FixedLocalizableString("Get system details"),
            Volo.Abp.MultiTenancy.MultiTenancySides.Host)
            .WithProviders(ClientPermissionValueProvider.ProviderName); // Use client authorization
    }
}
