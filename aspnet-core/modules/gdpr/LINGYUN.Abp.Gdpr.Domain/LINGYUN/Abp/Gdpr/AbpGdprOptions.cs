using System;
using System.Collections.Generic;

namespace LINGYUN.Abp.Gdpr;
/// <summary>
/// AbpGdpr options
/// See: https://abp.io/docs/latest/modules/gdpr#abpgdproptions
/// </summary>
public class AbpGdprOptions
{
    /// <summary>
    /// Used to indicate the allowed request interval.<br />
    /// If you want to increase or decrease the personal data request interval, you can configure this property.<br />
    /// By default, users can request their personal data once per day<br />
    /// Default: 1 day
    /// </summary>
    public TimeSpan RequestTimeInterval { get; set; }
    /// <summary>
    /// Since the GDPR module is designed to support distributed scenarios, collecting and preparing personal data should take some time.
    /// If you want to increase or decrease data preparation time based on the size of your application, you can configure this property.<br />
    /// Default: 60 minutes
    /// </summary>
    public TimeSpan MinutesForDataPreparation { get; set; }
    /// <summary>
    /// User data provider list
    /// </summary>
    public IList<IGdprUserDataProvider> GdprUserDataProviders { get; }
    /// <summary>
    /// User account provider list
    /// </summary>
    public IList<IGdprUserAccountProvider> GdprUserAccountProviders { get; }
    public AbpGdprOptions()
    {
        RequestTimeInterval = TimeSpan.FromDays(1);
        MinutesForDataPreparation = TimeSpan.FromMinutes(60);

        GdprUserDataProviders = new List<IGdprUserDataProvider>();
        GdprUserAccountProviders = new List<IGdprUserAccountProvider>();
    }
}
