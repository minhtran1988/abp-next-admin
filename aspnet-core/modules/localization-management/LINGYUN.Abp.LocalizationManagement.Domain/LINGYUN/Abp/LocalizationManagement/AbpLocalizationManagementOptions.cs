namespace LINGYUN.Abp.LocalizationManagement;

public class AbpLocalizationManagementOptions
{
    /// <summary>
    /// Save localized text to database
    /// </summary>
    public bool SaveStaticLocalizationsToDatabase { get; set; } = true;
    /// <summary>
    /// Whether to dynamically localize text initialization nodes
    /// </summary>
    public bool IsDynamicLocalizationInitializerHost { get; set; } = false;

    public AbpLocalizationManagementOptions()
    {
    }
}
